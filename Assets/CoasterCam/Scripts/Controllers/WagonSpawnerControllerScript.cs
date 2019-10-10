using CoasterCam.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Coaster Cam namespace
/// </summary>
namespace CoasterCam.Controllers
{
    /// <summary>
    /// Wagon spawner controller script class
    /// </summary>
    public class WagonSpawnerControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Default animation prefix
        /// </summary>
        private static readonly string defaultAnimationPrefix = "Ride";

        /// <summary>
        /// Animation prefix
        /// </summary>
        [SerializeField]
        private string animationPrefix = defaultAnimationPrefix;

        /// <summary>
        /// Wagon asset
        /// </summary>
        [SerializeField]
        private GameObject wagonAsset = default;

        /// <summary>
        /// On initialize wagon spawner
        /// </summary>
        [SerializeField]
        private UnityEvent onInitWagonSpawner = default;

        /// <summary>
        /// On spawn wagon
        /// </summary>
        [SerializeField]
        private UnityEvent onSpawnWagon = default;

        /// <summary>
        /// On finish wagon spawner
        /// </summary>
        [SerializeField]
        private UnityEvent onFinishWagonSpawner = default;

        /// <summary>
        /// Wagon spawn queue
        /// </summary>
        private Queue<WagonSpawnData> wagonSpawnQueue = new Queue<WagonSpawnData>();

        /// <summary>
        /// Rail ID
        /// </summary>
        private uint railID = 0U;

        /// <summary>
        /// Wagon count
        /// </summary>
        private uint wagonSeriesCount = 1U;

        /// <summary>
        /// Series delay
        /// </summary>
        private float wagonSeriesDelay = 0.0f;

        /// <summary>
        /// Elapsed time
        /// </summary>
        private float elapsedTime;

        /// <summary>
        /// Rail ID
        /// </summary>
        public uint RailID => railID;

        /// <summary>
        /// Animation prefix
        /// </summary>
        public string AnimationPrefix
        {
            get
            {
                if (animationPrefix == null)
                {
                    animationPrefix = defaultAnimationPrefix;
                }
                return animationPrefix;
            }
        }

        /// <summary>
        /// On initialize wagon spawner
        /// </summary>
        public UnityEvent OnInitWagonSpawner => onInitWagonSpawner;

        /// <summary>
        /// On spawn wagon
        /// </summary>
        public UnityEvent OnSpawnWagon => onSpawnWagon;

        /// <summary>
        /// On finish wagon spawner
        /// </summary>
        public UnityEvent OnFinishWagonSpawner => onFinishWagonSpawner;

        /// <summary>
        /// Current wagon controller
        /// </summary>
        public WagonControllerScript CurrentWagonController { get; private set; }

        /// <summary>
        /// Spawn wagons
        /// </summary>
        /// <param name="wagonSpawnData">Wagon spawn data</param>
        /// <param name="railID">Rail ID</param>
        /// <param name="wagonSeriesCount">Wagon series count</param>
        /// <param name="wagonSeriesDelay">Wagon series delay</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool SpawnWagons(IReadOnlyCollection<WagonSpawnData> wagonSpawnData, uint railID, uint wagonSeriesCount, float wagonSeriesDelay)
        {
            bool ret = false;
            if ((wagonSpawnQueue.Count <= 0) && (wagonSpawnData != null))
            {
                ret = true;
                foreach (WagonSpawnData wagon in wagonSpawnData)
                {
                    if (wagon == null)
                    {
                        wagonSpawnQueue.Clear();
                        ret = false;
                        break;
                    }
                    else
                    {
                        wagonSpawnQueue.Enqueue(wagon);
                    }
                }
                if (ret)
                {
                    this.railID = railID;
                    this.wagonSeriesCount = wagonSeriesCount;
                    this.wagonSeriesDelay = wagonSeriesDelay;
                    onInitWagonSpawner?.Invoke();
                }
            }
            return ret;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if ((wagonSpawnQueue.Count > 0) && (wagonAsset != null) && (CurrentWagonController == null))
            {
                WagonSpawnData wagon_spawn_data = wagonSpawnQueue.Peek();
                if (wagon_spawn_data == null)
                {
                    wagonSpawnQueue.Dequeue();
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= wagon_spawn_data.Delay)
                    {
                        elapsedTime = 0.0f;
                        GameObject go = Instantiate(wagonAsset, transform);
                        if (go != null)
                        {
                            WagonControllerScript wagon_controller = go.GetComponent<WagonControllerScript>();
                            Animator animator = go.GetComponent<Animator>();
                            if ((wagon_controller == null) || (animator == null))
                            {
                                Destroy(go);
                            }
                            else
                            {
                                CurrentWagonController = wagon_controller;
                                wagon_controller.WantsPhoto = wagon_spawn_data.WantsPhoto;
                                wagon_controller.OnFinishWagonRide?.AddListener(WagonFinishedRideEvent);
                                Debug.Log("Playing animation " + AnimationPrefix + (railID + 1));
                                animator.Play(AnimationPrefix + (railID + 1), 0);
                                onSpawnWagon?.Invoke();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Wagon finished ride
        /// </summary>
        private void WagonFinishedRideEvent()
        {
            if (CurrentWagonController != null)
            {
                CurrentWagonController = null;
                if (wagonSpawnQueue.Count > 0)
                {
                    wagonSpawnQueue.Dequeue();
                }
                elapsedTime = 0.0f;
                if (wagonSpawnQueue.Count <= 0)
                {
                    onFinishWagonSpawner?.Invoke();
                }
            }
        }
    }
}
