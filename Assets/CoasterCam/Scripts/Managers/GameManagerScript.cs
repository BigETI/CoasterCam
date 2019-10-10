using CoasterCam.Controllers;
using CoasterCam.Data;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Coaster Cam managers namespace
/// </summary>
namespace CoasterCam.Managers
{
    /// <summary>
    /// Game manager script class
    /// </summary>
    public class GameManagerScript : MonoBehaviour
    {
        /// <summary>
        /// Lifes
        /// </summary>
        [SerializeField]
        private uint lifes = 3U;

        /// <summary>
        /// Passenger colors
        /// </summary>
        [SerializeField]
        private PassengerColorData[] passengerColors;

        /// <summary>
        /// Init delay
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float initDelayMinimum = 2.0f;

        /// <summary>
        /// Init delay
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float initDelayRange = 1.0f;

        /// <summary>
        /// Delay minimum
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float delayMinimum = 1.0f;

        /// <summary>
        /// Minimum range
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float delayRange = 0.25f;

        /// <summary>
        /// Score per second
        /// </summary>
        [SerializeField]
        [Range(0.0f, float.MaxValue)]
        private float scorePerSecond = 500.0f;

        /// <summary>
        /// Points distribution
        /// </summary>
        [SerializeField]
        private PointsDistributionData pointsDistribution;

        /// <summary>
        /// Camera panel controller
        /// </summary>
        [SerializeField]
        private CoasterCamPanelControllerScript coasterCamPanelController = default;

        /// <summary>
        /// Wagon spawner controller
        /// </summary>
        [SerializeField]
        private WagonSpawnerControllerScript wagonSpawnerController = default;

        /// <summary>
        /// On waiting for input
        /// </summary>
        [SerializeField]
        private UnityEvent onWaitingForInput = default;

        /// <summary>
        /// On countdown started
        /// </summary>
        [SerializeField]
        private UnityEvent onCountdownStarted = default;

        /// <summary>
        /// On game started
        /// </summary>
        [SerializeField]
        private UnityEvent onGameStarted = default;

        /// <summary>
        /// On game paused
        /// </summary>
        [SerializeField]
        private UnityEvent onGamePaused = default;

        /// <summary>
        /// On game resumed
        /// </summary>
        [SerializeField]
        private UnityEvent onGameResumed = default;

        /// <summary>
        /// On game finished
        /// </summary>
        [SerializeField]
        private UnityEvent onGameFinished = default;

        /// <summary>
        /// Game state
        /// </summary>
        private EGameState gameState = EGameState.WaitingForInput;

        /// <summary>
        /// Current rail ID
        /// </summary>
        private int currentRailID = -1;

        /// <summary>
        /// Current wagon controller
        /// </summary>
        private WagonControllerScript currentWagonController = default;

        /// <summary>
        /// Score
        /// </summary>
        private uint score;

        /// <summary>
        /// Instance
        /// </summary>
        public static GameManagerScript Instance { get; private set; }


        /// <summary>
        /// Passenger colors
        /// </summary>
        private PassengerColorData[] PassengerColors
        {
            get
            {
                if (passengerColors == null)
                {
                    passengerColors = Array.Empty<PassengerColorData>();
                }
                return passengerColors;
            }
        }

        /// <summary>
        /// Game state
        /// </summary>
        public EGameState GameState
        {
            get => gameState;
            private set
            {
                if (gameState != value)
                {
                    switch (gameState)
                    {
                        case EGameState.WaitingForInput:
                            if (value == EGameState.CountingDown)
                            {
                                gameState = EGameState.CountingDown;
                                onCountdownStarted?.Invoke();
                            }
                            break;
                        case EGameState.CountingDown:
                            if (value == EGameState.GameRunning)
                            {
                                gameState = EGameState.GameRunning;
                                SpawnWagons();
                                onGameStarted?.Invoke();
                            }
                            break;
                        case EGameState.GameRunning:
                            switch (value)
                            {
                                case EGameState.GamePausing:
                                    Time.timeScale = 0.0f;
                                    gameState = EGameState.GamePausing;
                                    onGamePaused?.Invoke();
                                    break;
                                case EGameState.GameFinished:
                                    gameState = EGameState.GameFinished;
                                    onGameFinished?.Invoke();
                                    break;
                            }
                            break;
                        case EGameState.GamePausing:
                            if (value == EGameState.GameRunning)
                            {
                                Time.timeScale = 1.0f;
                                gameState = EGameState.GameRunning;
                                onGameResumed?.Invoke();
                            }
                            break;
                        case EGameState.GameFinished:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Points distribution
        /// </summary>
        public PointsDistributionData PointsDistribution
        {
            get
            {
                if (pointsDistribution == null)
                {
                    pointsDistribution = new PointsDistributionData();
                }
                return pointsDistribution;
            }
        }

        /// <summary>
        /// Interact
        /// </summary>
        public void Interact()
        {
            switch (gameState)
            {
                case EGameState.WaitingForInput:
                    GameState = EGameState.CountingDown;
                    break;
                case EGameState.GameRunning:
                    ShootPhoto();
                    break;
            }
        }

        /// <summary>
        /// Start counting down
        /// </summary>
        public void StartCountingDown()
        {
            GameState = EGameState.CountingDown;
        }

        /// <summary>
        /// Start game
        /// </summary>
        public void StartGame()
        {
            GameState = EGameState.GameRunning;
        }

        /// <summary>
        /// Pause game
        /// </summary>
        public void PauseGame()
        {
            GameState = EGameState.GamePausing;
        }

        /// <summary>
        /// Resume game
        /// </summary>
        public void ResumeGame()
        {
            GameState = EGameState.GameRunning;
        }

        /// <summary>
        /// Spawn wagons
        /// </summary>
        public void SpawnWagons()
        {
            if ((coasterCamPanelController != null) && (wagonSpawnerController != null))
            {
                WagonSpawnData[] wagon_spawn_data = new WagonSpawnData[coasterCamPanelController.PassengerPanelControllers.Count];
                ++currentRailID;
                if (currentRailID >= coasterCamPanelController.ScreenRenderTextures.Count)
                {
                    currentRailID = 0;
                }
                coasterCamPanelController.SelectedScreenIndex = currentRailID;
                int random_passenger_color_index = ((PassengerColors.Length > 0) ? UnityEngine.Random.Range(0, PassengerColors.Length) : 0);
                for (int i = 0; i < wagon_spawn_data.Length; i++)
                {
                    bool wants_photo = (UnityEngine.Random.Range(0, 10) != 5);
                    PassengerColorData passenger_color = ((PassengerColors.Length > 0) ? PassengerColors[(random_passenger_color_index + i) % PassengerColors.Length] : PassengerColorData.white);
                    wagon_spawn_data[i] = new WagonSpawnData(wants_photo, passenger_color.Color, UnityEngine.Random.Range(delayMinimum, delayMinimum + delayRange));
                    PassengerPanelControllerScript passenger_panel_controller = coasterCamPanelController.PassengerPanelControllers[i];
                    if (passenger_panel_controller != null)
                    {
                        passenger_panel_controller.WantsPhoto = wants_photo;
                        passenger_panel_controller.PassengerBulbColor = passenger_color.Color;
                        passenger_panel_controller.PassengerBulbGlowIntensity = passenger_color.GlowIntensity;
                    }
                }
                wagonSpawnerController.gameObject.SetActive(true);
                wagonSpawnerController.SpawnWagons(wagon_spawn_data, (uint)currentRailID, (uint)(UnityEngine.Random.Range(1, 4)), (uint)(UnityEngine.Random.Range(initDelayMinimum, initDelayMinimum + initDelayRange)));
            }
        }

        /// <summary>
        /// Shoot photo
        /// </summary>
        public void ShootPhoto()
        {
            if ((currentWagonController != null) && (gameState == EGameState.GameRunning))
            {
                float result = PointsDistribution.Evaluate(currentWagonController.ElapsedTime);
                if (currentWagonController.WantsPhoto)
                {
                    score += (uint)(Mathf.RoundToInt(result));
                }
                else
                {
                    uint take_score = (uint)(Mathf.RoundToInt(result * 0.25f));
                    if (score >= take_score)
                    {
                        score -= take_score;
                    }
                    else
                    {
                        score = 0U;
                    }
                    if (lifes > 1U)
                    {
                        --lifes;
                    }
                    else
                    {
                        lifes = 0U;
                        GameState = EGameState.GameFinished;
                    }
                }
                currentWagonController = null;
            }
        }

        /// <summary>
        /// Finish wagon ride
        /// </summary>
        public void FinishWagonRide()
        {
            // TODO
        }

        /// <summary>
        /// Deactivate wagon spawner
        /// </summary>
        public void DeactivateWagonSpawner()
        {
            // TODO
        }

        /// <summary>
        /// Show game over screen
        /// </summary>
        public void ShowGameOverScreen()
        {
            // TODO
        }

        /// <summary>
        /// Submit dialog
        /// </summary>
        public void SubmitDialog()
        {
            // TODO
        }

        /// <summary>
        /// On enable
        /// </summary>
        private void OnEnable()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        /// <summary>
        /// On disable
        /// </summary>
        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            if (wagonSpawnerController != null)
            {
                wagonSpawnerController.OnSpawnWagon?.AddListener(SpawnWagonEvent);
                wagonSpawnerController.OnFinishWagonSpawner?.AddListener(SpawnWagons);
            }
            onWaitingForInput?.Invoke();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameState = ((gameState == EGameState.GameRunning) ? EGameState.GamePausing : ((gameState == EGameState.GamePausing) ? EGameState.GameRunning : gameState));
            }
            if (coasterCamPanelController != null)
            {
                long delta = score - coasterCamPanelController.Score;
                uint score_delta = (uint)(Mathf.RoundToInt(scorePerSecond * Time.deltaTime));
                if (delta > 0L)
                {
                    if (delta >= score_delta)
                    {
                        coasterCamPanelController.Score += score_delta;
                    }
                    else
                    {
                        coasterCamPanelController.Score = score;
                    }
                }
                else if (delta < 0L)
                {
                    delta = -delta;
                    if (delta >= score_delta)
                    {
                        coasterCamPanelController.Score -= score_delta;
                    }
                    else
                    {
                        coasterCamPanelController.Score = score;
                    }
                }
                coasterCamPanelController.Lifes = lifes;
                coasterCamPanelController.PhotoQuality = ((currentWagonController == null) ? 0U : (uint)Mathf.Max(Mathf.RoundToInt(PointsDistribution.Evaluate(currentWagonController.ElapsedTime) * coasterCamPanelController.MaxPhotoQuality / PointsDistribution.HighestValue), 0));
            }
        }

        /// <summary>
        /// Spawn wagon event
        /// </summary>
        private void SpawnWagonEvent()
        {
            if (wagonSpawnerController != null)
            {
                currentWagonController = wagonSpawnerController.CurrentWagonController;
            }
        }
    }
}
