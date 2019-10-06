using CoasterCam.Controllers;
using System;
using UnityEngine;

/// <summary>
/// Coaster Cam managers namespace
/// </summary>
namespace CoasterCam.Managers
{
    /// <summary>
    /// Game manager script class
    /// </summary>
    public class GameManagerScript : MonoBehaviour
    {
        /// <summary>
        /// Camera panel controller
        /// </summary>
        [SerializeField]
        private CoasterCamPanelControllerScript cameraPanelController = default;

        /// <summary>
        /// Instance
        /// </summary>
        public static GameManagerScript Instance { get; private set; }

        /// <summary>
        /// Shoot photo
        /// </summary>
        public void ShootPhoto()
        {
            // TODO
        }

        /// <summary>
        /// On enable
        /// </summary>
        private void OnEnable()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        /// <summary>
        /// On disable
        /// </summary>
        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            // TODO
            // Implement game logic
        }
    }
}
