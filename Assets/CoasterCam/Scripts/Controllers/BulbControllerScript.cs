using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Coaster Cam controllers namespace
/// </summary>
namespace CoasterCam.Controllers
{
    /// <summary>
    /// Bulb controller script class
    /// </summary>
    [ExecuteInEditMode]
    public class BulbControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Bulb color
        /// </summary>
        [SerializeField]
        private Color bulbColor = Color.white;

        /// <summary>
        /// Glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float glowIntensity = 1.0f;

        /// <summary>
        /// Bulb image
        /// </summary>
        [SerializeField]
        private Image bulbImage = default;

        /// <summary>
        /// Bulb glow image
        /// </summary>
        [SerializeField]
        private Image bulbGlowImage = default;

        /// <summary>
        /// Bulb color
        /// </summary>
        public Color BulbColor
        {
            get => bulbColor;
            set => bulbColor = value;
        }

        /// <summary>
        /// Glow intensity
        /// </summary>
        public float GlowIntensity
        {
            get => glowIntensity;
            set => glowIntensity = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            Color albedo_color = new Color(bulbColor.r, bulbColor.g, bulbColor.b, 1.0f);
            float grayscale = albedo_color.grayscale;
            float intensity = Mathf.Clamp(bulbColor.a * glowIntensity, 0.0f, 1.0f);
            if (bulbImage != null)
            {
                bulbImage.color = Color.Lerp(new Color(grayscale, grayscale, grayscale, bulbColor.a), bulbColor, intensity);
            }
            if (bulbGlowImage != null)
            {
                bulbGlowImage.color = Color.Lerp(new Color(grayscale, grayscale, grayscale, 0.0f), albedo_color, intensity);
            }
        }
    }
}
