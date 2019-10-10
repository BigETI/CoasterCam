using System;
using UnityEngine;

/// <summary>
/// Passenger color data
/// </summary>
namespace CoasterCam.Data
{
    /// <summary>
    /// Passenger color data
    /// </summary>
    [Serializable]
    public struct PassengerColorData
    {
        /// <summary>
        /// White
        /// </summary>
        public static readonly PassengerColorData white = new PassengerColorData(Color.white, 1.0f);

        /// <summary>
        /// Color
        /// </summary>
        [SerializeField]
        private Color color;

        /// <summary>
        /// Glow intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float glowIntensity;

        /// <summary>
        /// Color
        /// </summary>
        public Color Color => color;

        /// <summary>
        /// Glow intensity
        /// </summary>
        public float GlowIntensity => glowIntensity;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="color">Color</param>
        /// <param name="glowIntensity">Glow intensity</param>
        public PassengerColorData(Color color, float glowIntensity)
        {
            this.color = color;
            this.glowIntensity = glowIntensity;
        }
    }
}
