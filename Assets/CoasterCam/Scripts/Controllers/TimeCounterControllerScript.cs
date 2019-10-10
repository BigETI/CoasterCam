using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Coaster Cam controllers namespace
/// </summary>
namespace CoasterCam.Controllers
{
    /// <summary>
    /// Time counter controller script class
    /// </summary>
    public class TimeCounterControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Count
        /// </summary>
        [SerializeField]
        private uint count = default;

        /// <summary>
        /// Count delay time
        /// </summary>
        [SerializeField]
        [Range(0.0f, float.MaxValue)]
        private float countDelayTime = 1.0f;

        /// <summary>
        /// Text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI text = default;

        /// <summary>
        /// On count
        /// </summary>
        [SerializeField]
        public UnityEvent onCount;

        /// <summary>
        /// On count finished
        /// </summary>
        [SerializeField]
        public UnityEvent onCountFinished;

        /// <summary>
        /// Elapsed time
        /// </summary>
        private float elapsedTime;

        /// <summary>
        /// Current count
        /// </summary>
        private uint currentCount;

        /// <summary>
        /// On count
        /// </summary>
        public UnityEvent OnCount => onCount;

        /// <summary>
        /// On count finished
        /// </summary>
        public UnityEvent OnCountFinished => onCountFinished;

        /// <summary>
        /// Update text
        /// </summary>
        private void UpdateText()
        {
            if (text != null)
            {
                text.text = (currentCount > 0U) ? currentCount.ToString() : "Go!";
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            currentCount = count;
            UpdateText();
            onCount?.Invoke();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (currentCount > 0U)
            {
                elapsedTime += Time.deltaTime;
                while ((currentCount > 0U) && (elapsedTime >= countDelayTime))
                {
                    elapsedTime -= countDelayTime;
                    --currentCount;
                    onCount?.Invoke();
                }
                if (currentCount == 0U)
                {
                    elapsedTime = 0.0f;
                    onCount?.Invoke();
                    onCountFinished?.Invoke();
                }
                UpdateText();
            }
        }
    }
}
