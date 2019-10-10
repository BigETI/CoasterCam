using System;
using UnityEngine;

/// <summary>
/// Coaster Cam data namespace
/// </summary>
namespace CoasterCam.Data
{
    /// <summary>
    /// Wagon spawner data class
    /// </summary>
    [Serializable]
    public class WagonSpawnData
    {
        /// <summary>
        /// Wants photo
        /// </summary>
        [SerializeField]
        private bool wantsPhoto;

        /// <summary>
        /// Passenger color
        /// </summary>
        [SerializeField]
        private Color passengerColor;

        /// <summary>
        /// Delay
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float delay;

        /// <summary>
        /// Wants photo
        /// </summary>
        public bool WantsPhoto => wantsPhoto;

        /// <summary>
        /// Passenger color
        /// </summary>
        public Color PassengerColor => passengerColor;

        /// <summary>
        /// Delay
        /// </summary>
        public float Delay
        {
            get => Mathf.Max(delay, 0.0f);
            set => delay = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="wantsPhoto">Wants photo</param>
        /// <param name="passengerColor">Passenger color</param>
        /// <param name="delay">Delay</param>
        public WagonSpawnData(bool wantsPhoto, Color passengerColor, float delay)
        {
            this.wantsPhoto = wantsPhoto;
            this.passengerColor = passengerColor;
            Delay = delay;
        }
    }
}
