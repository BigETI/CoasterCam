using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Coaster Cam controlers namespace
/// </summary>
namespace CoasterCam.Controllers
{
    /// <summary>
    /// Wagon controller script class
    /// </summary>
    public class WagonControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float time = 5.0f;

        /// <summary>
        /// On finish wagon ride
        /// </summary>
        [SerializeField]
        private UnityEvent onFinishWagonRide = default;

        /// <summary>
        /// On finish wagon ride
        /// </summary>
        public UnityEvent OnFinishWagonRide => onFinishWagonRide;

        /// <summary>
        /// Elapsed time
        /// </summary>
        public float ElapsedTime { get; private set; }

        /// <summary>
        /// Wants photo
        /// </summary>
        public bool WantsPhoto { get; set; }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            ElapsedTime += Time.deltaTime;
            if (ElapsedTime >= time)
            {
                onFinishWagonRide?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
