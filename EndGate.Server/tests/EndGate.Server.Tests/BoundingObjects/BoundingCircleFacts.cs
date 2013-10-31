using System;
using EndGate.Server;
using EndGate.Server.BoundingObject;
using Xunit;

namespace EndGate.Server.Tests
{
    public class BoundingCircleFacts
    {
        [Fact]
        public void AreaWorks()
        {
            var obj = new BoundingCircle(Vector2d.Zero, 10);

            Assert.Equal(Math.Round(obj.Area()), 314);
        }

        [Fact]
        public void CircumferenceWorks()
        {
            var obj = new BoundingCircle(Vector2d.Zero, 10);

            Assert.Equal(Math.Round(obj.Circumference()), 63);
        }

        [Fact]
        public void IsCollidingWithOtherCircleWorks()
        {
            var circle1 = new BoundingCircle(Vector2d.Zero, 10)
            {
                Position = new Vector2d(10, 5)
            };

            var circle2 = new BoundingCircle(Vector2d.Zero, 6)
            {
                Position = new Vector2d(17, 5)
            };

            Assert.True(circle1.Intersects(circle2));

            circle2.Position.X = -5;

            Assert.True(circle1.Intersects(circle2));

            circle2.Position.X = -6;

            Assert.False(circle1.Intersects(circle2));
        }

        [Fact]
        public void IsCollidingWithRectangleWorks()
        {
            var rect = new BoundingRectangle(Vector2d.Zero, new Size2d(10, 6))
            {
                Position = new Vector2d(5, 3)
            };

            var circle = new BoundingCircle(Vector2d.Zero, 3)
            {
                Position = new Vector2d(14, 3)
            };
            
            Assert.False(circle.Intersects(rect));
            circle.Position.X--;
            Assert.True(circle.Intersects(rect));
            rect.Rotation = Math.PI * .5;
            rect.Position.X = 3;
            rect.Position.Y = 5;

            circle.Position.X = 8;
            circle.Position.Y = 12;

            Assert.True(circle.Intersects(rect));

            circle = new BoundingCircle(Vector2d.Zero, 50)
            {
                Position = new Vector2d(156, 165)
            };
            rect = new BoundingRectangle(Vector2d.Zero, new Size2d(200, 100))
            {
                Position = new Vector2d(300, 200)
            };

            Assert.True(circle.Intersects(rect));

            circle.Position.X = 300;
            circle.Position.Y = 350;

            Assert.False(circle.Intersects(rect));
        }

        [Fact]
        public void ContainsPointWorks()
        {
            var circle = new BoundingCircle(Vector2d.Zero, 10)
            {
                Position = new Vector2d(10, 5)
            };

            Assert.False(circle.Contains(Vector2d.Zero));
            Assert.True(circle.Contains(new Vector2d(3, 3)));
        }
    }
}
