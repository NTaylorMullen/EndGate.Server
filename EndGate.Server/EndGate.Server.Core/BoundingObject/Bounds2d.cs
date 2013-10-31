using EndGate.Server;

namespace EndGate.Server.BoundingObject
{
    /// <summary>
    /// Abstract bounds type that is used to detect intersections.
    /// </summary>
    public abstract class Bounds2d
    {
        /// <summary>
        /// Initiates a bounded object with an initial position of (0, 0).
        /// </summary>
        public Bounds2d() : this(Vector2d.Zero)
        {
        }

        /// <summary>
        /// Initiates a bounded object.
        /// </summary>
        /// <param name="position">The initial position of the bounds2d</param>
        public Bounds2d(Vector2d position)
        {
            Position = position;
            Rotation = 0;
        }

        /// <summary>
        /// Gets or sets the Position of the bounds.
        /// </summary>
        public Vector2d Position { get; set; }
        /// <summary>
        /// Gets or sets the Rotation of the bounds.
        /// </summary>
        public double Rotation { get; set; }

        /// <summary>
        /// Scales the size of the bounded object.
        /// </summary>
        /// <param name="x">Value to multiply the horizontal component by.</param>
        /// <param name="y">Value to multiply the vertical component by.</param>
        public abstract void Scale(double x, double y);

        /// <summary>
        /// Determines if the current bounded object contains the provided Vector2d.
        /// </summary>
        /// <param name="point">A point to check containment on.</param>
        /// <returns>Whether the bounds contains the point.</returns>
        public abstract bool Contains(Vector2d point);
        /// <summary>
        /// Determines if the current bounded object completely contains another bounded object.
        /// </summary>
        /// <param name="obj">A bounded object to check containment on.</param>
        /// <returns>Whether the bounds contains the object.</returns>
        public abstract bool Contains(Bounds2d obj);
        /// <summary>
        /// Determines if the current bounded object completely contains the provided BoundingCircle.
        /// </summary>
        /// <param name="circle">A circle to check containment on.</param>
        /// <returns>Whether the bounds contains the circle.</returns>
        public abstract bool Contains(BoundingCircle circle);
        /// <summary>
        /// Determines if the current bounded object completely contains the provided BoundingRectangle.
        /// </summary>
        /// <param name="rectangle">A rectangle to check containment on.</param>
        /// <returns>Whether the bounds contains the rectangle.</returns>
        public abstract bool Contains(BoundingRectangle rectangle);

        /// <summary>
        /// Determines if the current bounded object intersects another bounded object.
        /// </summary>
        /// <param name="obj">Bounding object to check collision with.</param>
        /// <returns>Whether the bounds intersects the object.</returns>
        public abstract bool Intersects(Bounds2d obj);
        /// <summary>
        /// Determines if the current bounded object is intersecting the provided BoundingCircle.
        /// </summary>
        /// <param name="circle">BoundingCircle to check intersection with.</param>
        /// <returns>Whether the bounds intersects the BoundingCircle.</returns>
        public abstract bool Intersects(BoundingCircle circle);
        /// <summary>
        /// Determines if the current bounded object is intersecting the provided BoundingRectangle.    
        /// </summary>
        /// <param name="rectangle">BoundingRectangle to check intersection with.</param>
        /// <returns>Whether the bounds intersects the BoundingRectangle.</returns>
        public abstract bool Intersects(BoundingRectangle rectangle);
    }
}