
namespace EndGate.Server.MovementControllers
{
    /// <summary>
    /// Represents a <see cref="LinearMovementControllers"/>'s move event.
    /// </summary>
    public class MoveEvent : IMoveEvent
    {
        /// <summary>
        /// The direction the <see cref="LinearMovementController"/> is now moving.
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// Whether the <see cref="LinearMovementController"/> started or stopped moving in the <see cref="Direction"/>.
        /// </summary>
        public bool StartMoving { get; set; }

        /// <summary>
        /// Initiates a new MoveEvent with the provided direction and startMoving
        /// </summary>
        /// <param name="direction">The direction which was moved.</param>
        /// <param name="startMoving">Whether or not we started moving in the provided direction.</param>
        public MoveEvent(string direction, bool startMoving)
        {
            Direction = direction;
            StartMoving = startMoving;
        }
    }
}
