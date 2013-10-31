
namespace EndGate.Server.MovementControllers
{
    /// <summary>
    /// A utility class that represent the valid directions of a <see cref="LinearMovementController"/>.
    /// </summary>
    public class LinearDirections
    {
        /// <summary>
        /// Initiates a new LinearDirections object with all directions set to false.
        /// </summary>
        public LinearDirections()
        {
            Left = false;
            Right = false;
            Up = false;
            Down = false;
        }

        /// <summary>
        /// Gets or sets the Left value.
        /// </summary>
        public bool Left { get; set; }
        /// <summary>
        /// Gets or sets the Right value.
        /// </summary>
        public bool Right { get; set; }
        /// <summary>
        /// Gets or sets the Up value.
        /// </summary>
        public bool Up { get; set; }
        /// <summary>
        /// Gets or sets the Down value.
        /// </summary>
        public bool Down { get; set; }

        /// <summary>
        /// Determines if the string direction is a valid direction (case sensitive).
        /// </summary>
        /// <param name="direction">The direction to validate.</param>
        /// <returns>Whether the direction is Left, Right, Up or Down.</returns>
        public static bool Valid(string direction)
        {
            return direction == "Left" || direction == "Right" || direction == "Up" || direction == "Down";
        }
    }
}
