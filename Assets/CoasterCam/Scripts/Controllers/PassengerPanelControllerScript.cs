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

        // Update is called once per frame
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
