using EndGate.Server;

namespace EndGate.Server.BoundingObject
{
    /// <summary>
    /// Defines a rectangle that can be used to detect intersections.
    /// </summary>
    public class BoundingRectangle : Bounds2d
    {
        /// <summary>
        /// Creates a new instance of BoundingRectangle.
        /// </summary>
        /// <param name="position">Initial Position of the BoundingRectangle.</param>
        /// <param name="size">Initial Size of the BoundingRectangle.</param>
        public BoundingRectangle(Vector2d position, Size2d size) : base(position)
        {
            Size = size;
        }

        /// <summary>
        /// Gets or sets the Size of the rectangle.
        /// </summary>
        public Size2d Size { get; set; }

        /// <summary>
        /// Scales the width and height of the BoundingRectangle.
        /// </summary>
        /// <param name="x">Value to multiply the width by.</param>
        /// <param name="y">Value to multiply the height by.</param>
        public override void Scale(double x, double y)
        {
            this.Size.Width *= x;
            this.Size.Height *= y;
        }

        /// <summary>
        /// Returns a list of vertices that are the locations of each corner of the BoundingRectangle. Format: [TopLeft, TopRight, BotLeft, BotRight].
        /// </summary>
        /// <returns>The rectangles corners: [TopLeft, TopRight, BotLeft, BotRight].</returns>
        public Vector2d[] Corners()
        {
            return new Vector2d[] { TopLeft, TopRight, BotLeft, BotRight };
        }

        /// <summary>
        /// Gets the top left corner of the BoundingRectangle.
        /// </summary>
        public Vector2d TopLeft
        {
            get
            {
                var v = new Vector2d(Position.X - Size.HalfWidth, Position.Y - Size.HalfHeight);
                if (Rotation == 0)
                {
                    return v;
                }

                return v.RotateAround(Position, Rotation);
            }
        }

        /// <summary>
        /// Gets the top right corner of the BoundingRectangle.
        /// </summary>
        public Vector2d TopRight
        {
            get
            {
                var v = new Vector2d(Position.X + Size.HalfWidth, Position.Y - Size.HalfHeight);
                if (Rotation == 0)
                {
                    return v;
                }

                return v.RotateAround(Position, Rotation);
            }
        }

        /// <summary>
        /// Gets the bottom left corner of the BoundingRectangle.
        /// </summary>
        public Vector2d BotLeft
        {
            get
            {
                var v = new Vector2d(Position.X - Size.HalfWidth, Position.Y + Size.HalfHeight);
                if (Rotation == 0)
                {
                    return v;
                }

                return v.RotateAround(Position, Rotation);
            }
        }

        /// <summary>
        /// Gets the bottom right corner of the BoundingRectangle.
        /// </summary>
        public Vector2d BotRight
        {
            get
            {
                var v = new Vector2d(Position.X + Size.HalfWidth, Position.Y + Size.HalfHeight);
                if (Rotation == 0)
                {
                    return v;
                }

                return v.RotateAround(Position, Rotation);
            }
        }

        /// <summary>
        /// Determines if the current bounded object intersects another bounded object.
        /// </summary>
        /// <param name="obj">Bounding object to check collision with.</param>
        /// <returns>Whether the bounds intersects the object.</returns>
        public override bool Intersects(Bounds2d obj)
        {
            return obj.Intersects(this);
        }

        /// <summary>
        /// Determines if the current BoundingRectangle is intersecting the provided BoundingCircle.
        /// </summary>
        /// <param name="obj">BoundingCircle to check intersection with.</param>
        /// <returns>Whether the BoundingRectangle intersects the BoundingCircle.</returns>
        public override bool Intersects(BoundingCircle obj)
        {
            return obj.Intersects((BoundingRectangle)this);
        }

        /// <summary>
        /// Determines if the current BoundingRectangle is intersecting the provided BoundingRectangle.
        /// </summary>
        /// <param name="obj">BoundingRectangle to check intersection with.</param>
        /// <returns>Whether the BoundingRectangle intersects the BoundingRectangle.</returns>
        public override bool Intersects(BoundingRectangle obj)
        {
            if (Rotation == 0 && obj.Rotation == 0)
            {
                Vector2d myTopLeft = TopLeft,
                         myBotRight = BotRight,
                         theirTopLeft = obj.TopLeft,
                         theirBotRight = obj.BotRight;

                return theirTopLeft.X <= myBotRight.X && theirBotRight.X >= myTopLeft.X && theirTopLeft.Y <= myBotRight.Y && theirBotRight.Y >= myTopLeft.Y;

            }
            else if (obj.Position.Distance(Position).Magnitude() <= obj.Size.Radius + Size.Radius) // Check if we're somewhat close to the object that we might be colliding with
            {
                var axisList = new Vector2d[] { TopRight - TopLeft, TopRight - BotRight, obj.TopLeft - obj.BotLeft, obj.TopLeft - obj.TopRight };
                var myCorners = Corners();
                var theirCorners = obj.Corners();

                foreach (var axi in axisList)
                {
                    var myProjections = Vector2dHelpers.GetMinMaxProjections(axi, myCorners);
                    var theirProjections = Vector2dHelpers.GetMinMaxProjections(axi, theirCorners);

                    // No collision
                    if (theirProjections.Max < myProjections.Min || myProjections.Max < theirProjections.Min)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if the current BoundingRectangle contains the provided Vector2d.
        /// </summary>
        /// <param name="point">A point to check containment on.</param>
        /// <returns>Whether the BoundingRectangle contains the point.</returns>
        public override bool Contains(Vector2d point)
        {
            double savedRotation = Rotation;

            if (Rotation != 0)
            {
                Rotation = 0;
                point = point.RotateAround(Position, -savedRotation);
            }

            Vector2d myTopLeft = TopLeft,
                     myBotRight = BotRight;

            Rotation = savedRotation;

            return point.X <= myBotRight.X && point.X >= myTopLeft.X && point.Y <= myBotRight.Y && point.Y >= myTopLeft.Y;
        }

        /// <summary>
        /// Determines if the current bounded object completely contains another bounded object.
        /// </summary>
        /// <param name="obj">A bounded object to check containment on.</param>
        /// <returns>Whether the bounds contains the object.</returns>
        public override bool Contains(Bounds2d obj)
        {
            return this.Contains(obj);
        }

        /// <summary>
        /// Determines if the current BoundingRectangle contains the provided BoundingCircle.
        /// </summary>
        /// <param name="circle">A circle to check containment on.</param>
        /// <returns>Whether the BoundingRectangle contains the circle.</returns>
        public override bool Contains(BoundingCircle circle)
        {
            return this.Contains(new Vector2d(circle.Position.X - circle.Radius, circle.Position.Y)) &&
                this.Contains(new Vector2d(circle.Position.X, circle.Position.Y - circle.Radius)) &&
                this.Contains(new Vector2d(circle.Position.X + circle.Radius, circle.Position.Y)) &&
                this.Contains(new Vector2d(circle.Position.X, circle.Position.Y + circle.Radius));
        }

        /// <summary>
        /// Determines if the current BoundingRectangle contains the provided BoundingRectangle.
        /// </summary>
        /// <param name="rectangle">A rectangle to check containment on.</param>
        /// <returns>Whether the BoundingRectangle contains the rectangle.</returns>
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
