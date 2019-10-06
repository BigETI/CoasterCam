using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Coaster Cam controllers namespace
/// </summary>
namespace CoasterCam.Controllers
{
    /// <summary>
    /// Coaster cam panel controller script class
    /// </summary>
    [ExecuteInEditMode]
    public class CoasterCamPanelControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Selected screen index
        /// </summary>
        [SerializeField]
        private int selectedScreenIndex = -1;

        /// <summary>
        /// Score
        /// </summary>
        [SerializeField]
        private long score;

        /// <summary>
        /// Score format
        /// </summary>
        [SerializeField]
        private string scoreFormat = "{0} P";

        /// <summary>
        /// Wagon count
        /// </summary>
        [SerializeField]
        private uint wagonCount;

        /// <summary>
        /// Wagon count format
        /// </summary>
        [SerializeField]
        private string wagonCountFormat = "{0}";

        /// <summary>
        /// Lives
        /// </summary>
        [SerializeField]
        private uint lifes = default;

        /// <summary>
        /// Photo quality
        /// </summary>
        [SerializeField]
        private uint photoQuality = default;

        /// <summary>
        /// Bad photo quality bulb color
        /// </summary>
        [SerializeField]
        private Color badPhotoQualityBulbColor = Color.white;

        /// <summary>
        /// Bad photo quality background glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float badPhotoQualityBackgroundGlowIntensity = 0.0f;

        /// <summary>
        /// Bad photo quality foreground glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float badPhotoQualityForegroundGlowIntensity = 1.0f;

        /// <summary>
        /// Average photo quality bulb color
        /// </summary>
        [SerializeField]
        private Color averagePhotoQualityBulbColor = Color.white;

        /// <summary>
        /// Average photo quality background glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float averagePhotoQualityBackgroundGlowIntensity = 0.0f;

        /// <summary>
        /// Average photo quality foreground glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float averagePhotoQualityForegroundGlowIntensity = 1.0f;

        /// <summary>
        /// Bad photo quality bulb color
        /// </summary>
        [SerializeField]
        private Color goodPhotoQualityBulbColor = Color.white;

        /// <summary>
        /// Good photo quality background glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float goodPhotoQualityBackgroundGlowIntensity = 0.0f;

        /// <summary>
        /// Good photo quality foreground glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float goodPhotoQualityForegroundGlowIntensity = 1.0f;

        /// <summary>
        /// Average photo quality points
        /// </summary>
        [SerializeField]
        private uint averagePhotoQualityPointsDelta = 3U;

        /// <summary>
        /// Average photo quality points
        /// </summary>
        [SerializeField]
        private uint goodPhotoQualityPointsDelta = 1U;

        /// <summary>
        /// Score text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI scoreText = default;

        /// <summary>
        /// Wagon count text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI wagonCountText = default;

        /// <summary>
        /// Screen raw image
        /// </summary>
        [SerializeField]
        private RawImage screenRawImage = default;

        /// <summary>
        /// Life progress
        /// </summary>
        [SerializeField]
        private BulbProgressControllerScript lifeProgress = default;

        /// <summary>
        /// Photo quality progress
        /// </summary>
        [SerializeField]
        private BulbProgressControllerScript photoQualityProgress = default;

        /// <summary>
        /// Passenger panel controllers
        /// </summary>
        [SerializeField]
        private PassengerPanelControllerScript[] passengerPanelControllers;

        /// <summary>
        /// Screen render textures
        /// </summary>
        [SerializeField]
        private RenderTexture[] screenRenderTextures;

        /// <summary>
        /// Last score
        /// </summary>
        private long lastScore;

        /// <summary>
        /// Last score text
        /// </summary>
        private TextMeshProUGUI lastScoreText;

        /// <summary>
        /// Last wagon count
        /// </summary>
        private long lastWagonCount;

        /// <summary>
        /// Last wagon count text
        /// </summary>
        private TextMeshProUGUI lastWagonCountText;

        /// <summary>
        /// Render screen index
        /// </summary>
        public int SelectedScreenIndex
        {
            get => selectedScreenIndex;
            set
            {
                selectedScreenIndex = value;
                if ((selectedScreenIndex < 0) || (selectedScreenIndex >= ScreenRenderTextures.Length))
                {
                    selectedScreenIndex = -1;
                }
            }
        }

        /// <summary>
        /// Score
        /// </summary>
        public long Score
        {
            get => score;
            set => score = value;
        }

        /// <summary>
        /// Score format
        /// </summary>
        private string ScoreFormat
        {
            get
            {
                if (scoreFormat == null)
                {
                    scoreFormat = "{0} P";
                }
                return scoreFormat;
            }
        }

        /// <summary>
        /// Wagons count
        /// </summary>
        public uint WagonsCount
        {
            get => wagonCount;
            set => wagonCount = value;
        }

        /// <summary>
        /// Wagon count format
        /// </summary>
        private string WagonCountFormat
        {
            get
            {
                if (wagonCountFormat == null)
                {
                    wagonCountFormat = "{0}";
                }
                return wagonCountFormat;
            }
        }

        /// <summary>
        /// Passengers
        /// </summary>
        public IReadOnlyList<PassengerPanelControllerScript> PassengerPanelControllers
        {
            get
            {
                if (passengerPanelControllers == null)
                {
                    passengerPanelControllers = Array.Empty<PassengerPanelControllerScript>();
                }
                return passengerPanelControllers;
            }
        }

        /// <summary>
        /// Screen render textures
        /// </summary>
        private RenderTexture[] ScreenRenderTextures
        {
            get
            {
                if (screenRenderTextures == null)
                {
                    screenRenderTextures = Array.Empty<RenderTexture>();
                }
                return screenRenderTextures;
            }
        }

        /// <summary>
        /// Selected screen render texture
        /// </summary>
        public RenderTexture SelectedScreenRenderTexture => (((selectedScreenIndex >= 0) && (selectedScreenIndex < ScreenRenderTextures.Length)) ? ScreenRenderTextures[selectedScreenIndex] : null);

        /// <summary>
        /// Update text
        /// </summary>
        /// <typeparam name="T">Numeric type</typeparam>
        /// <param name="text">Text</param>
        /// <param name="value">Value</param>
        /// <param name="lastText">Last text</param>
        /// <param name="lastValue">Last value</param>
        private static void UpdateText<T>(TextMeshProUGUI text, T value, string format, ref TextMeshProUGUI lastText, ref T lastValue) where T : IEquatable<T>
        {
            if (text != null)
            {
                if (lastText == text)
                {
                    if (!(value.Equals(lastValue)))
                    {
                        lastValue = value;
                        text.text = string.Format(format, value);
                    }
                }
                else
                {
                    lastText = text;
                    lastValue = value;
                    text.text = string.Format(format, value);
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            UpdateText(scoreText, score, ScoreFormat, ref lastScoreText, ref lastScore);
            UpdateText(wagonCountText, wagonCount, WagonCountFormat, ref lastWagonCountText, ref lastWagonCount);
            if (lifeProgress != null)
            {
                lifeProgress.Progress = lifes / (float)(lifeProgress.BulbCount);
            }
            if (photoQualityProgress != null)
            {
                if (photoQualityProgress.BulbCount > 0)
                {
                    photoQualityProgress.Progress = photoQuality / (float)(photoQualityProgress.BulbCount);
                    if (photoQuality >= (goodPhotoQualityPointsDelta + averagePhotoQualityPointsDelta))
                    {
                        photoQualityProgress.BackgroundColor = goodPhotoQualityBulbColor;
                        photoQualityProgress.ForegroundColor = goodPhotoQualityBulbColor;
                        photoQualityProgress.BackgroundGlowIntensity = goodPhotoQualityBackgroundGlowIntensity;
                        photoQualityProgress.ForegroundGlowIntensity = goodPhotoQualityForegroundGlowIntensity;
                    }
                    else if (photoQuality >= averagePhotoQualityPointsDelta)
                    {
                        photoQualityProgress.BackgroundColor = averagePhotoQualityBulbColor;
                        photoQualityProgress.ForegroundColor = averagePhotoQualityBulbColor;
                        photoQualityProgress.BackgroundGlowIntensity = averagePhotoQualityBackgroundGlowIntensity;
                        photoQualityProgress.ForegroundGlowIntensity = averagePhotoQualityForegroundGlowIntensity;
                    }
                    else
                    {
                        photoQualityProgress.BackgroundColor = badPhotoQualityBulbColor;
                        photoQualityProgress.ForegroundColor = badPhotoQualityBulbColor;
                        photoQualityProgress.BackgroundGlowIntensity = badPhotoQualityBackgroundGlowIntensity;
                        photoQualityProgress.ForegroundGlowIntensity = badPhotoQualityForegroundGlowIntensity;
                    }
                }
            }
            if (screenRawImage != null)
            {
                screenRawImage.texture = SelectedScreenRenderTexture;
            }
        }
    }
}
