﻿using EndGate.Server.Assets;

namespace EndGate.Server.BoundingObject
{
    public class BoundingRectangle : Bounds2d
    {
        public BoundingRectangle(Size2d size)
        {
            Size = size;
        }

        public BoundingRectangle(double width, double height)
        {
            Size = new Size2d(width, height);
        }

        public Size2d Size { get; set; }

        public Vector2d[] Corners()
        {
            return new Vector2d[] { TopLeft, TopRight, BotLeft, BotRight };
        }

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

        public override bool Intersects(Bounds2d obj)
        {
            return obj.Intersects(this);
        }

        public override bool Intersects(BoundingCircle obj)
        {
            return obj.Intersects((BoundingRectangle)this);
        }

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

        public override bool Contains(Bounds2d obj)
        {
            return this.Contains(obj);
        }

        public override bool Contains(BoundingCircle circle)
        {
            return this.Contains(new Vector2d(circle.Position.X - circle.Radius, circle.Position.Y)) &&
                this.Contains(new Vector2d(circle.Position.X, circle.Position.Y - circle.Radius)) &&
                this.Contains(new Vector2d(circle.Position.X + circle.Radius, circle.Position.Y)) &&
                this.Contains(new Vector2d(circle.Position.X, circle.Position.Y + circle.Radius));
        }

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
