/// <summary>
/// Coaster cam namespace
/// </summary>
namespace CoasterCam
{
    /// <summary>
    /// Game state enumerator
    /// </summary>
    public enum EGameState
    {
        /// <summary>
        /// Waiting for input
        /// </summary>
        WaitingForInput,

        /// <summary>
        /// Counting down
        /// </summary>
        CountingDown,

        /// <summary>
        /// Game running
        /// </summary>
        GameRunning,

        /// <summary>
        /// Game pausing
        /// </summary>
        GamePausing,

        /// <summary>
        /// Game finished
        /// </summary>
        GameFinished
    }
}
