using System;
using UnityEngine;

/// <summary>
/// Coaster cam data class
/// </summary>
namespace CoasterCam.Data
{
    /// <summary>
    /// Points distribution data class
    /// </summary>
    [Serializable]
    public class PointsDistributionData
    {
        /// <summary>
        /// Highest value
        /// </summary>
        [SerializeField]
        private float highestValue = 1.0f;

        /// <summary>
        /// Highest time
        /// </summary>
        [SerializeField]
        private float highestTime = 0.5f;

        /// <summary>
        /// Devation
        /// </summary>
        [SerializeField]
        private float deviation = 0.25f;

        /// <summary>
        /// Highest value
        /// </summary>
        public float HighestValue => highestValue;

        /// <summary>
        /// Highest time
        /// </summary>
        public float HighestTime => highestTime;

        /// <summary>
        /// Devation
        /// </summary>
        public float Deviation => deviation;

        /// <summary>
        /// Evaluate
        /// </summary>
        /// <param name="time">Time</param>
        /// <returns>Result</returns>
        public float Evaluate(float time)
        {
            float time_minus_highest_time = (time - highestTime);
            float deviation_squared = deviation * deviation;
            return ((1.0f / Mathf.Sqrt(2.0f * Mathf.PI * deviation_squared)) * Mathf.Exp(-(time_minus_highest_time * time_minus_highest_time) / (2.0f * deviation_squared))) * (deviation * Mathf.Sqrt(2.0f * Mathf.PI)) * highestValue;
        }
    }
}
