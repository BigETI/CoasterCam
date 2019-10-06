using System;
using UnityEngine;

/// <summary>
/// Coaster Cam controlles namespace
/// </summary>
namespace CoasterCam.Controllers
{
    /// <summary>
    /// Bulb progress controller script class
    /// </summary>
    [ExecuteInEditMode]
    public class BulbProgressControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Progress
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float progress;

        /// <summary>
        /// Use smooth progress
        /// </summary>
        [SerializeField]
        bool useSmoothProgress = true;

        /// <summary>
        /// Background color
        /// </summary>
        [SerializeField]
        private Color backgroundColor = Color.white;

        /// <summary>
        /// Foregorund color
        /// </summary>
        [SerializeField]
        private Color foregroundColor = Color.white;

        /// <summary>
        /// Background glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float backgroundGlowIntensity = 1.0f;

        /// <summary>
        /// Foregorund glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float foregroundGlowIntensity = 1.0f;

        /// <summary>
        /// Bulb controllers
        /// </summary>
        [SerializeField]
        private BulbControllerScript[] bulbControllers;

        /// <summary>
        /// Progress
        /// </summary>
        public float Progress
        {
            get => progress;
            set
            {
                progress = Mathf.Clamp(value, 0.0f, 1.0f);
            }
        }

        /// <summary>
        /// USe smooth progress
        /// </summary>
        public bool UseSmoothProgress
        {
            get => useSmoothProgress;
            set => useSmoothProgress = value;
        }

        /// <summary>
        /// Background color
        /// </summary>
        public Color BackgroundColor
        {
            get => backgroundColor;
            set => backgroundColor = value;
        }

        /// <summary>
        /// Foreground color
        /// </summary>
        public Color ForegroundColor
        {
            get => foregroundColor;
            set => foregroundColor = value;
        }

        /// <summary>
        /// Background glow intensity
        /// </summary>
        public float BackgroundGlowIntensity
        {
            get => backgroundGlowIntensity;
            set => backgroundGlowIntensity = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Foreground glow intensity
        /// </summary>
        public float ForegroundGlowIntensity
        {
            get => foregroundGlowIntensity;
            set => foregroundGlowIntensity = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Bulb controllers
        /// </summary>
        private BulbControllerScript[] BulbControllers
        {
            get
            {
                if (bulbControllers == null)
                {
                    bulbControllers = Array.Empty<BulbControllerScript>();
                }
                return bulbControllers;
            }
        }

        /// <summary>
        /// Bulb count
        /// </summary>
        public int BulbCount => BulbControllers.Length;

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            for (int i = 0; i < BulbControllers.Length; i++)
            {
                BulbControllerScript bulb_controller = BulbControllers[i];
                if (bulb_controller != null)
                {
                    float t = (progress * bulbControllers.Length) - i;
                    if (useSmoothProgress)
                    {
                        t = Mathf.Clamp(t, 0.0f, 1.0f);
                        bulb_controller.BulbColor = Color.Lerp(backgroundColor, foregroundColor, t);
                        bulb_controller.GlowIntensity = Mathf.Lerp(backgroundGlowIntensity, foregroundGlowIntensity, t);
                    }
                    else
                    {
                        bool state = (t >= 0.5f);
                        bulb_controller.BulbColor = (state ? foregroundColor : backgroundColor);
                        bulb_controller.GlowIntensity = (state ? foregroundGlowIntensity : backgroundGlowIntensity);
                    }
                }
            }
        }
    }
}
