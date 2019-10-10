using UnityEngine;

/// <summary>
/// Coaster Cam controllers namespace
/// </summary>
namespace CoasterCam.Controllers
{
    /// <summary>
    /// Passenger panel controller script class
    /// </summary>
    [ExecuteInEditMode]
    public class PassengerPanelControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Wants photo
        /// </summary>
        [SerializeField]
        private bool wantsPhoto = default;

        /// <summary>
        /// Does not want photo bulb color
        /// </summary>
        [SerializeField]
        private Color doesNotWantPhotoBulbColor = Color.white;

        /// <summary>
        /// Wants photo bulb color
        /// </summary>
        [SerializeField]
        private Color wantsPhotoBulbColor = Color.white;

        /// <summary>
        /// Does not want photo glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float doesNotWantPhotoGlowIntensity = 1.0f;

        /// <summary>
        /// Wants photo glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float wantsPhotoGlowIntensity = 1.0f;

        /// <summary>
        /// Wants photo bulb controller
        /// </summary>
        [SerializeField]
        private BulbControllerScript wantsPhotoBulbController = default;

        /// <summary>
        /// Passenger bulb controller
        /// </summary>
        [SerializeField]
        private BulbControllerScript passengerBulbController = default;

        /// <summary>
        /// Wants photo
        /// </summary>
        public bool WantsPhoto
        {
            get => wantsPhoto;
            set => wantsPhoto = value;
        }

        /// <summary>
        /// Passenger bulb color
        /// </summary>
        public Color PassengerBulbColor
        {
            get => ((passengerBulbController == null) ? Color.black : passengerBulbController.BulbColor);
            set
            {
                if (passengerBulbController != null)
                {
                    passengerBulbController.BulbColor = value;
                }
            }
        }

        /// <summary>
        /// Passenger bulb glow intensity
        /// </summary>
        public float PassengerBulbGlowIntensity
        {
            get => ((passengerBulbController == null) ? 0.0f : passengerBulbController.GlowIntensity);
            set
            {
                if (passengerBulbController != null)
                {
                    passengerBulbController.GlowIntensity = value;
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (wantsPhotoBulbController != null)
            {
                wantsPhotoBulbController.BulbColor = (wantsPhoto ? wantsPhotoBulbColor : doesNotWantPhotoBulbColor);
                wantsPhotoBulbController.GlowIntensity = (wantsPhoto ? wantsPhotoGlowIntensity : doesNotWantPhotoGlowIntensity);
            }
        }
    }
}
