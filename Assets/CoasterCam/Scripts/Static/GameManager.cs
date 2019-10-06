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
        /// Shoot photo
        /// </summary>
        public static void ShootPhoto()
        {
            if (GameManagerScript.Instance != null)
            {
                GameManagerScript.Instance.ShootPhoto();
            }
        }
    }
}
