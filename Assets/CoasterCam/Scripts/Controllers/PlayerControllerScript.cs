using CoasterCam.ActionInputs;
using UnityEngine;

/// <summary>
/// Coaster Cam controllers namespace
/// </summary>
namespace CoasterCam.Controllers
{
    /// <summary>
    /// Player controller script class
    /// </summary>
    public class PlayerControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Main input actions
        /// </summary>
        [SerializeField]
        private MainInputActions mainInputActions = default;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            if (mainInputActions != null)
            {
                mainInputActions.ActionMap.Interact.performed += (context) =>
                {
                    GameManager.ShootPhoto();
                };
            }
        }
    }
}
