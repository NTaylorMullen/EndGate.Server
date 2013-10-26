using EndGate.Server.Assets;

namespace EndGate.Server.BoundingObject
{
    public abstract class Bounds2d
    {
        public Bounds2d()
        {
            Position = Vector2d.Zero;
            Rotation = 0;
        }

        public Vector2d Position { get; set; }
        public double Rotation { get; set; }

        public abstract bool Contains(Vector2d point);
        public abstract bool Contains(Bounds2d obj);
        public abstract bool Contains(BoundingCircle circle);
        public abstract bool Contains(BoundingRectangle rectangle);

        public abstract bool Intersects(Bounds2d obj);
        public abstract bool Intersects(BoundingCircle circle);
        public abstract bool Intersects(BoundingRectangle rectangle);
    }
}