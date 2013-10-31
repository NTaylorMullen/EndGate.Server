
namespace EndGate.Server
{
    /// <summary>
    /// Represents an object that can be moved by a <see cref="MovementController"/>.
    /// </summary>
    public interface IMoveable
    {
        /// <summary>
        /// The position of the moveable.
        /// </summary>
        Vector2d Position { get; set; }
        /// <summary>
        /// The rotation of the moveable.
        /// </summary>
        double Rotation { get; set; }
    }
}
