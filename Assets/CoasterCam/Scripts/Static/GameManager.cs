using CoasterCam.Managers;

/// <summary>
/// Coaster Cam namespace
/// </summary>
namespace CoasterCam
{
    /// <summary>
    /// Game manager class
    /// </summary>
    public static class GameManager
    {
        /// <summary>
        /// Interact
        /// </summary>
        public static void Interact()
        {
            if (GameManagerScript.Instance != null)
            {
                GameManagerScript.Instance.Interact();
            }
        }
    }
}
