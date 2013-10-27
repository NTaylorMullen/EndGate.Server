
namespace EndGate.Server.MovementControllers
{
    public abstract class MovementController : IMoveable, IUpdateable
    {
        public virtual Vector2d Position { get; set; }
        public virtual double Rotation { get; set; }
        public virtual Vector2d Velocity { get; set; }

        protected IMoveable[] Moveables;
        protected bool Frozen;

        public MovementController(IMoveable[] moveables)
        {
            Position = moveables.Length > 0 ? moveables[0].Position : Vector2d.Zero;
            Velocity = Vector2d.Zero;
            Rotation = 0;
            Frozen = false;
            Moveables = moveables;
        }

        public void Freeze()
        {
            Frozen = true;
        }

        public void Thaw()
        {
            Frozen = false;
        }

        public bool IsMoving()
        {
            return !Frozen && !Velocity.IsZero();
        }

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
