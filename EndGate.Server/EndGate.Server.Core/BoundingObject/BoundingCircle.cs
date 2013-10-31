using System;
using EndGate.Server;

namespace EndGate.Server.BoundingObject
{
    /// <summary>
    /// Defines a circle that can be used to detect intersections.
    /// </summary>
    public class BoundingCircle : Bounds2d
    {
        /// <summary>
        /// Creates a new instance of BoundingCircle.
        /// </summary>
        /// <param name="position">Initial Position of the BoundingCircle.</param>
        /// <param name="radius">Initial Radius of the BoundingCircle.</param>
        public BoundingCircle(Vector2d position, double radius) : base(position)
        {
            Radius = radius;
        }

        /// <summary>
        /// Gets or sets the Radius of the circle.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Scales the radius of the BoundingCircle by the "x" value.  The "y" parameter is ignored."
        /// </summary>
        /// <param name="x">Value to multiply the radius by</param>
        /// <param name="y">Ignored.</param>
        public override void Scale(double x, double y)
        {
            Scale(x);
        }

        /// <summary>
        /// Scales the radius of the BoundingCircle.
        /// </summary>
        /// <param name="scale">Value to multiply the radius by.</param>
        public void Scale(double scale)
        {
            this.Radius *= scale;
        }

        /// <summary>
        /// Calculates the area of the BoundingCircle.
        /// </summary>
        /// <returns>The area of the BoundingCircle.</returns>
        public double Area()
        {
            return Math.PI * Radius * Radius;
        }

        /// <summary>
        /// Calculates the circumference of the BoundingCircle.
        /// </summary>
        /// <returns>The Circumference of the BoundingCircle</returns>
        public double Circumference()
        {
            return 2 * Math.PI * Radius;
        }

        /// <summary>
        /// Determines if the current BoundingCircle intersects another bounded object.
        /// </summary>
        /// <param name="obj">BoundingCircle to check collision with.</param>
        /// <returns>Whether the BoundingCircle intersects the object.</returns>
        public override bool Intersects(Bounds2d obj)
        {
            return obj.Intersects(this);
        }

        /// <summary>
        /// Determines if the current BoundingCircle is intersecting the provided BoundingCircle.
        /// </summary>
        /// <param name="circle">BoundingCircle to check intersection with.</param>
        /// <returns>Whether the BoundingCircle intersects the circle.</returns>
        public override bool Intersects(BoundingCircle circle)
        {
            return Position.Distance(circle.Position).Length() < Radius + circle.Radius;
        }

        /// <summary>
        /// Determines if the current BoundingCircle is intersecting the provided BoundingRectangle.
        /// </summary>
        /// <param name="rectangle">BoundingRectangle to check intersection with.</param>
        /// <returns>Whether the BoundingCircle intersects the rectangle.</returns>
        public override bool Intersects(BoundingRectangle rectangle)
        {            
            Vector2d translated = rectangle.Rotation == 0
                                  ? Position
                                  : Position.RotateAround(rectangle.Position, -rectangle.Rotation);

            var circleDistance = translated.Distance(rectangle.Position);

            if (circleDistance.X > (rectangle.Size.HalfWidth + this.Radius)) { return false; }
            if (circleDistance.Y > (rectangle.Size.HalfHeight + this.Radius)) { return false; }

            if (circleDistance.X <= (rectangle.Size.HalfWidth)) { return true; }
            if (circleDistance.Y <= (rectangle.Size.HalfHeight)) { return true; }

            var cornerDistance_sq = Math.Pow(circleDistance.X - rectangle.Size.HalfWidth, 2) + Math.Pow(circleDistance.Y - rectangle.Size.HalfHeight, 2);

            return (cornerDistance_sq <= (this.Radius * this.Radius));
        }

        /// <summary>
        /// Determines if the current BoundingCircle contains the provided Vector2d.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <returns>Whether the BoundingCircle contains the provided point.</returns>
        public override bool Contains(Vector2d point)
        {
            return Position.Distance(point).Magnitude() < Radius;
        }

        /// <summary>
        /// Determines if the current BoundingCircle contains the provided bounded object.
        /// </summary>
        /// <param name="obj">A bounded object to check containment on.</param>
        /// <returns>Whether the BoundingCircle contains the object.</returns>
        public override bool Contains(Bounds2d obj)
        {
            return obj.Contains(this);
        }

        /// <summary>
        /// Determines if the current BoundingCircle contains the provided BoundingCircle.
        /// </summary>
        /// <param name="circle">A circle to check containment on.</param>
        /// <returns>Whether the BoundingCircle contains the circle.</returns>
        public override bool Contains(BoundingCircle circle)
        {
            return circle.Position.Distance(this.Position).Length() + circle.Radius <= this.Radius;
        }

        /// <summary>
        /// Determines if the current BoundingCircle contains the provided BoundingRectangle.
        /// </summary>
        /// <param name="rectangle">A rectangle to check containment on.</param>
        /// <returns>Whether the BoundingCircle contains the rectangle.</returns>
        public override bool Contains(BoundingRectangle rectangle)
        {
            var corners = rectangle.Corners();

            for (var i = 0; i < corners.Length; i++)
            {
                if (!this.Contains(corners[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
