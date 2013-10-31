using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndGate.Server
{
    /// <summary>
    /// Defines a two dimensional size object which specifies a Width and Height.
    /// </summary>
    public class Size2d
    {
        /// <summary>
        /// Creates a new instance of Size2d.
        /// </summary>
        /// <param name="size">Initial value of the Width and Height components of Size2d.</param>
        public Size2d(double size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Creates a new instance of Size2d.
        /// </summary>
        /// <param name="width">Initial value of the Width component of Size2d.</param>
        /// <param name="height">Initial value of the Height component of Size2d.</param>
        public Size2d(double width, double height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets or sets the horizontal component of this Size structure.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the vertical component of this Size structure.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets the radius that encompasses the two dimensional size of this Size2d.
        /// </summary>
        public double Radius
        {
            get
            {
                return .5 * Math.Sqrt(Width * Width + Height * Height);
            }
        }

        /// <summary>
        /// Returns a Size2d that has identical Width's and Height's as the current Size2d.
        /// </summary>
        /// <returns>A duplicate of the current Size2d.</returns>
        public Size2d Clone()
        {
            return new Size2d(Width, Height);
        }

        /// <summary>
        /// Determines whether this Size2d has the same Width and Height of another Size2d.
        /// </summary>
        /// <param name="s1">The Size2d to compare the current Size2d to.</param>
        /// <returns>Whether the Width and Height of the current Size2d are equivalent to the provided Size2d's.</returns>
        public bool Equivalent(Size2d s1)
        {
            return Width == s1.Width && Height == s1.Height;
        }

        /// <summary>
        /// Executes the action with the Width and Height of this Size2d and sets the Width and Height to the corresponding return values.
        /// </summary>
        /// <param name="action">The function used to modify the Width and Height.</param>
        public void Apply(Func<double, double> action)
        {
            Width = action(Width);
            Height = action(Height);
        }

        /// <summary>
        /// Executes the action with the Width and Height of this Size2d.
        /// </summary>
        /// <param name="action">The function to pass the Width and Height components to.</param>
        public void Trigger(Action<double> action)
        {
            action(Width);
            action(Height);
        }

        /// <summary>
        /// Gets half of the Width component of this Size2d.
        /// </summary>
        public double HalfWidth
        {
            get
            {
                return Width / 2;
            }
        }

        /// <summary>
        /// Gets half of the Height component of this Size2d.
        /// </summary>
        public double HalfHeight
        {
            get
            {
                return Height / 2;
            }
        }

        /// <summary>
        /// Returns a Size2d with all its components set to zero.
        /// </summary>
        public static Size2d Zero
        {
            get
            {
                return new Size2d(0);
            }
        }

        /// <summary>
        /// Returns a Size2d with all its components set to one.
        /// </summary>
        public static Size2d One
        {
            get
            {
                return new Size2d(1);
            }
        }

        /// <summary>
        /// Returns a Size2d that is the result of adding the Width and Height of this Size2d to a number.
        /// </summary>
        /// <param name="val">The number to add.</param>
        /// <returns>The result of this Size2d added to the provided number.</returns>
        public Size2d Add(Number val)
        {
            return this + val;
        }

        /// <summary>
        /// Returns a Size2d that is the result of adding the Width and Height of this Size2d to the Width and Height of a Size2d.
        /// </summary>
        /// <param name="s1">The Size2d to add.</param>
        /// <returns>The result of this Size2d added to the provided Size2d.</returns>
        public Size2d Add(Size2d s1)
        {
            return this + s1;
        }

        /// <summary>
        /// Returns a Size2d that is the result of adding the Width and Height of this Size2d to the X and Y of a Vector2d.
        /// </summary>
        /// <param name="v1">The Vector2d to add.</param>
        /// <returns>The result of this Size2d added to the provided Vector2d.</returns>
        public Size2d Add(Vector2d v1)
        {
            return this + v1;
        }

        /// <summary>
        /// Returns a Size2d that is the result of multiplying the Width and Height of this Size2d by a number.
        /// </summary>
        /// <param name="val">The number to multiply.</param>
        /// <returns>The result of this Size2d multiplied by the provided number.</returns>
        public Size2d Multiply(Number val)
        {
            return this * val;
        }

        /// <summary>
        /// Returns a Size2d that is the result of multiplying the Width and Height of this Size2d by the Width and Height of a Size2d.
        /// </summary>
        /// <param name="s1">The Size2d to multiply.</param>
        /// <returns>The result of this Size2d multiplied by the provided Size2d.</returns>
        public Size2d Multiply(Size2d s1)
        {
            return this * s1;
        }

        /// <summary>
        /// Returns a Size2d that is the result of multiplying the Width and Height of this Size2d by the X and Y of a Vector2d.
        /// </summary>
        /// <param name="v1">The Vector2d to multiply.</param>
        /// <returns>The result of this Size2d multiplied by the provided Size2d.</returns>
        public Size2d Multiply(Vector2d v1)
        {
            return this * v1;
        }

        /// <summary>
        /// Returns a Size2d that is the result of subtracting the Width and Height of this Size2d by a number.
        /// </summary>
        /// <param name="val">The number to subtract.</param>
        /// <returns>The result of this Size2d subtracted by the provided number.</returns>
        public Size2d Subtract(Number val)
        {
            return this - val;
        }

        /// <summary>
        /// Returns a Size2d that is the result of subtracting the Width and Height of this Size2d by the Width and Height of a Size2d.
        /// </summary>
        /// <param name="s1">The Size2d to subtract.</param>
        /// <returns>The result of this Size2d subtracted by the provided number.</returns>
        public Size2d Subtract(Size2d s1)
        {
            return this - s1;
        }

        /// <summary>
        /// Returns a Size2d that is the result of subtracting the Width and Height of this Size2d by the X and Y of a Vector2d.
        /// </summary>
        /// <param name="v1">The Vector2d to subtract.</param>
        /// <returns>The result of this Size2d subtracted by the provided Vector2d.</returns>
        public Size2d Subtract(Vector2d v1)
        {
            return this - v1;
        }

        /// <summary>
        /// Returns a Size2d that is the result of subtracting the Width and Height of this Size2d from the Width and Height of a Size2d.
        /// </summary>
        /// <param name="s1">The Size2d to subtract from.</param>
        /// <returns>The result of this Size2d subtracted from the provided Size2d.</returns>
        public Size2d SubtractFrom(Size2d s1)
        {
            return s1 - this;
        }

        /// <summary>
        /// Returns a Size2d that is the result of subtracting the Width and Height of this Size2d from a number.
        /// </summary>
        /// <param name="val">The number to subtract from.</param>
        /// <returns>The result of this Size2d subtracted from the provided Size2d.</returns>
        public Size2d SubtractFrom(Number val)
        {
            return val - this;
        }

        /// <summary>
        /// Returns a Size2d that is the result of subtracting the Width and Height of this Size2d from the X and Y of a Vector2d.
        /// </summary>
        /// <param name="v1">The Vector2d to subtract from.</param>
        /// <returns>The result of this Size2d subtracted from the provided Size2d.</returns>
        public Size2d SubtractFrom(Vector2d v1)
        {
            return v1 - this;
        }

        /// <summary>
        /// Returns a Size2d that is the result of dividing the Width and Height of this Size2d by a number.
        /// </summary>
        /// <param name="val">The number to divide.</param>
        /// <returns>The result of this Size2d divided by the provided number.</returns>
        public Size2d Divide(Number val)
        {
            return this / val;
        }

        /// <summary>
        /// Returns a Size2d that is the result of dividing the Width and Height of this Size2d by the X and Y of a Vector2d.
        /// </summary>
        /// <param name="s1">The Size2d to divide.</param>
        /// <returns>The result of this Size2d divided by the provided Size2d.</returns>
        public Size2d Divide(Size2d s1)
        {
            return this / s1;
        }

        /// <summary>
        /// Returns a Size2d that is the result of dividing the Width and Height of this Size2d by the X and Y of a Vector2d.
        /// </summary>
        /// <param name="v1">The Vector2d to divide.</param>
        /// <returns>The result of this Size2d divided by the provided Vector2d.</returns>
        public Size2d Divide(Vector2d v1)
        {
            return this / v1;
        }

        /// <summary>
        /// Returns a Size2d that is the result of dividing the Width and Height of this Size2d from a number.
        /// </summary>
        /// <param name="val">The number to divide from.</param>
        /// <returns>The result of this Size2d divided from the provided number.</returns>
        public Size2d DivideFrom(Number val)
        {
            return val / this;
        }

        /// <summary>
        /// Returns a Size2d that is the result of dividing the Width and Height of this Size2d from the Width and Height of a Size2d.
        /// </summary>
        /// <param name="s1">The Size2d to divide</param>
        /// <returns>The result of this Size2d divided from the provided number.</returns>
        public Size2d DivideFrom(Size2d s1)
        {
            return s1 / this;
        }

        /// <summary>
        /// Returns a Size2d that is the result of dividing the Width and Height of this Size2d from the X and Y of a Vector2d.
        /// </summary>
        /// <param name="v1">The Vector2d to divide from.</param>
        /// <returns>The result of this Size2d divided from the provided Vector2d.</returns>
        public Size2d DivideFrom(Vector2d v1)
        {
            return v1 / this;
        }

        public static Size2d operator *(Size2d s1, Size2d s2)
        {
            return new Size2d(s1.Width * s2.Width, s1.Height * s2.Height);
        }

        public static Size2d operator *(Size2d s1, Number val)
        {
            return new Size2d(s1.Width * val, s1.Height * val);
        }

        public static Size2d operator *(Number val, Size2d s1)
        {
            return s1 * val;
        }

        public static Size2d operator *(Size2d s1, Vector2d v1)
        {
            return new Size2d(s1.Width * v1.X, s1.Height * v1.Y);
        }

        public static Size2d operator *(Vector2d v1, Size2d s1)
        {
            return s1 * v1;
        }

        public static Size2d operator +(Size2d s1, Size2d s2)
        {
            return new Size2d(s1.Width + s2.Width, s1.Height + s2.Height);
        }

        public static Size2d operator +(Size2d s1, Number val)
        {
            return new Size2d(s1.Width + val, s1.Height + val);
        }

        public static Size2d operator +(Number val, Size2d s1)
        {
            return s1 + val;
        }

        public static Size2d operator +(Size2d s1, Vector2d v1)
        {
            return new Size2d(s1.Width + v1.X, s1.Height + v1.Y);
        }

        public static Size2d operator +(Vector2d v1, Size2d s1)
        {
            return s1 + v1;
        }

        public static Size2d operator -(Size2d s1, Size2d s2)
        {
            return new Size2d(s1.Width - s2.Width, s1.Height - s2.Height);
        }

        public static Size2d operator -(Size2d s1, Number val)
        {
            return new Size2d(s1.Width - val, s1.Height - val);
        }

        public static Size2d operator -(Number val, Size2d s1)
        {
            return new Size2d(val - s1.Width, val - s1.Height);
        }

        public static Size2d operator -(Size2d s1, Vector2d v1)
        {
            return new Size2d(s1.Width - v1.X, s1.Height - v1.Y);
        }

        public static Size2d operator -(Vector2d v1, Size2d s1)
        {
            return new Size2d(v1.X - s1.Width, v1.Y - s1.Height);
        }

        public static Size2d operator /(Size2d s1, Size2d s2)
        {
            return new Size2d(s1.Width / s2.Width, s1.Height / s2.Height);
        }

        public static Size2d operator /(Size2d s1, Number val)
        {
            return new Size2d(s1.Width / val, s1.Height / val);
        }

        public static Size2d operator /(Number val, Size2d s1)
        {
            return new Size2d(val / s1.Width, val / s1.Height);
        }

        public static Size2d operator /(Size2d s1, Vector2d v1)
        {
            return new Size2d(s1.Width / v1.X, s1.Height / v1.Y);
        }

        public static Size2d operator /(Vector2d v1, Size2d s1)
        {
            return new Size2d(v1.X / s1.Width, v1.Y / s1.Height);
        }

        public static Size2d operator -(Size2d s1)
        {
            return s1 * -1;
        }

        /// <summary>
        /// Returns a Size2d that is the negated version of this Size2d.
        /// </summary>
        /// <returns>A copy of the current Size2d multiplied by -1.</returns>
        public Size2d Negate()
        {
            return -this;
        }

        /// <summary>
        /// Builds a string in the format of (Width, Height).
        /// </summary>
        /// <returns>A string version of the Size2d.</returns>
        public override string ToString()
        {
            return "("+Width + ", " + Height + ")";
        }
    }
}
