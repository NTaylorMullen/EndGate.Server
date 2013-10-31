
namespace EndGate.Server.MovementControllers
{
    /// <summary>
    /// A movement event that represent the direction a move occurred and whether or not it was "started".
    /// </summary>
    public interface IMoveEvent
    {
        /// <summary>
        /// The direction in which the move event occurred.
        /// </summary>
        string Direction { get; set; }
        /// <summary>
        /// Whether or not the move was started.
        /// </summary>
        bool StartMoving { get; set; }
    }
}
