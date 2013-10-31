
namespace EndGate.Server.MovementControllers
{
    /// <summary>
    /// An abstract class that synchronizes positions of <see cref="IMoveable"/> objects.
    /// </summary>
    public abstract class MovementController : IMoveable, IUpdateable
    {
        /// <summary>
        /// Gets or sets the position of the MovementController.
        /// </summary>
        public virtual Vector2d Position { get; set; }
        /// <summary>
        /// Gets or sets the rotation of the MovementController.
        /// </summary>
        public virtual double Rotation { get; set; }
        /// <summary>
        /// Gets or sets the velocity of the MovementController
        /// </summary>
        public virtual Vector2d Velocity { get; set; }

        protected IMoveable[] Moveables;
        protected bool Frozen;

        /// <summary>
        /// Instantiates a MovementController with an array of <see cref="IMoveable"/> objects that are synchronized to the MovementControllers position and rotation.
        /// </summary>
        /// <param name="moveables">A list of moveables to synchronize the rotation and position of.</param>
        public MovementController(IMoveable[] moveables)
        {
            Position = moveables.Length > 0 ? moveables[0].Position : Vector2d.Zero;
            Velocity = Vector2d.Zero;
            Rotation = 0;
            Frozen = false;
            Moveables = moveables;
        }

        /// <summary>
        /// Prevents the synchronization of positions and rotations.
        /// </summary>
        public void Freeze()
        {
            Frozen = true;
        }

        /// <summary>
        /// If Frozen, allows the synchronization of positions and rotations.
        /// </summary>
        public void Thaw()
        {
            Frozen = false;
        }

        /// <summary>
        /// Determines whether the MovementController is in motion.
        /// </summary>
        /// <returns>Whether the movement controller is in motion.</returns>
        public bool IsMoving()
        {
            return !Frozen && !Velocity.IsZero();
        }

        /// <summary>
        /// Synchronizes the position of the MovementController with its list of moveables.
        /// </summary>
        /// <param name="gameTime">The games <see cref="GameTime"/>.</param>
        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.Moveables.Length; i++)
            {
                Moveables[i].Position = Position;
                Moveables[i].Rotation = Rotation;
            }
        }
    }
}
