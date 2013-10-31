using System;

namespace EndGate.Server
{
    /// <summary>
    /// Defines a two dimensional vector object which specifies an X and Y.
    /// </summary>
    public class Vector2d
    {
        /// <summary>
        /// Creates a new instance of Vector2d with the X and Y components initialized to 0.
        /// </summary>
        public Vector2d()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Creates a new instance of Vector2d.
        /// </summary>
        /// <param name="x">Initial value of the X component of the Vector2d.</param>
        /// <param name="y">Initial value of the Y component of the Vector2d.</param>
        public Vector2d(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets or sets the X component of the vector.
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Gets or sets the Y component of the vector.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Returns a Vector2d that's reflected over the normal.
        /// </summary>
        /// <param name="normal">The normal to reflect over.</param>
        /// <returns>Reflected vector.</returns>
        public Vector2d Reflect(Vector2d normal)
        {
            var normalUnit = normal.Unit();
            var num = Dot(normalUnit) * 2;

            return new Vector2d(X - num * normalUnit.X, Y - num * normalUnit.Y);
        }

        /// <summary>
        /// Returns a Vector2d that represents the current Vector2d projected onto the provided Vector2d.
        /// </summary>
        /// <param name="v">Source vector</param>
        /// <returns>The projection vector.</returns>
        public Vector2d ProjectOnto(Vector2d v)
        {
            return (this.Dot(v) / v.Dot(v)) * v;
        }

        /// <summary>
        /// Returns a Vector2d that represents the current Vector2d rotated around the provided point and angle.
        /// </summary>
        /// <param name="point">Point to rotate around.</param>
        /// <param name="angle">How far to rotate around the point.</param>
        /// <param name="precision">The precision of the resulting Vector2d's X and Y components.  Defaults to 2.</param>
        /// <returns>The rotated Vector2d</returns>
        public Vector2d RotateAround(Vector2d point, double angle, int precision = 2)
        {
            var ca = Math.Cos(angle);
            var sa = Math.Sin(angle);

            return new Vector2d(
                Math.Round(ca * (X - point.X) - sa * (Y - point.Y) + point.X, precision),
                Math.Round(sa * (X - point.X) + ca * (Y - point.Y) + point.Y, precision)
            );
        }

        /// <summary>
        /// Executes the action with the X and Y components of this Vector2d and sets the X and Y components to the corresponding return values.
        /// </summary>
        /// <param name="action">The function used to modify the X and Y components.</param>
        public void Apply(Func<double, double> action)
        {
            X = action(X);
            Y = action(Y);
        }

        /// <summary>
        /// Executes the action with the X and Y components of this Vector2d.
        /// </summary>
        /// <param name="action">The function to pass the X and Y components to.</param>
        public void Trigger(Action<double> action)
        {
            action(X);
            action(Y);
        }

        /// <summary>
        /// Determines whether this Vector2d has the same X and Y of the provided Vector2d.
        /// </summary>
        /// <param name="v1">The Vector2d to compare the current Vector2d to.</param>
        /// <returns>Whether or not this Vector2d is equivalent to the provided vector2d.</returns>
        public bool Equivalent(Vector2d v1)
        {
            return v1.X == X && v1.Y == Y;
        }

        /// <summary>
        /// Determines whether this Vector2d's X and Y components are zero.
        /// </summary>
        /// <returns>Whether the X and Y components are 0.</returns>
        public bool IsZero() {
            return this.X == 0 && this.Y == 0;
        }

        /// <summary>
        /// Clones the vector2
        /// </summary>
        /// <returns>A new Vector2s</returns>
        public Vector2d Clone()
        {
            return new Vector2d(X, Y);
        }

        /// <summary>
        /// Creates a Normalized copy of the current Vector2
        /// </summary>
        /// <returns>A new Vector2</returns>
        public Vector2d Normalized()
        {
            var magnitude = Magnitude();
            return new Vector2d(X / magnitude, Y / magnitude);
        }

        /// <summary>
        /// Calculates the magnitude of the Vector2 (X*X + Y*Y)^.5
        /// </summary>
        /// <returns>A new Vector2</returns>
        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        /// Equivalent to Magnitude()
        /// </summary>
        /// <returns>A new Vector2</returns>
        public double Length()
        {
            return Magnitude();
        }

        /// <summary>
        /// Calculates the Dot product of the current vector and the one provided
        /// </summary>
        /// <param name="v1">Vector to dot product with</param>
        /// <returns>Dot product</returns>
        public double Dot(Vector2d v1)
        {
            return v1.X * X + v1.Y * Y;
        }

        /// <summary>
        /// Returns a new vector2 which has Math.Abs(X) and Math.Abs(Y) values
        /// </summary>
        /// <returns>A new Vector2</returns>
        public Vector2d Abs()
        {
            return new Vector2d(Math.Abs(X), Math.Abs(Y));
        }

        /// <summary>
        /// Calculates the Sign of the vector (X/|X|, Y/|Y|)
        /// </summary>
        /// <returns>A new Vector2</returns>
        public Vector2d Sign()
        {
            return new Vector2d(X / Math.Abs(X), Y / Math.Abs(Y));
        }

        /// <summary>
        /// Returns the unit vector of the current vector.
        /// </summary>
        /// <returns>The unit vector.</returns>
        public Vector2d Unit()
        {
            return this / Magnitude();
        }

        /// <summary>
        /// Calculates the distance between the current vector and the provided one.
        /// </summary>
        /// <param name="v1">The vector to calculate the distance to.</param>
        /// <returns>The distance from this vector to the provided vector.</returns>
        public Vector2d Distance(Vector2d v1)
        {
            return new Vector2d(Math.Abs(v1.X - X), Math.Abs(v1.Y - Y));
        }

        /// <summary>
        /// Returns a Vector2d that is the result of adding the X and Y of this Vector2d to the provided number.
        /// </summary>
        /// <param name="val">The number to add.</param>
        /// <returns>The result of this Vector2d added to the provided value</returns>
        public Vector2d Add(Number val)
        {
            return this + val;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of adding the X and Y of this Vector2d to the X and Y of the provided Vector2d.
        /// </summary>
        /// <param name="v1">The Vector2d to add.</param>
        /// <returns>The result of this Vector2d added to the provided Vector2d.</returns>
        public Vector2d Add(Vector2d v1)
        {
            return this + v1;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of adding the X and Y of this Vector2d to the Width and Height of the provided Size2d.
        /// </summary>
        /// <param name="val">The Size2d to add.</param>
        /// <returns>The result of this Vector2d added to the provided Size2d.</returns>
        public Vector2d Add(Size2d val)
        {
            return new Vector2d(X + val.Width, Y + val.Height);
        }

        /// <summary>
        /// Returns a Vector2d that is the result of subtracting the X and Y of this Vector2d by the provided number.
        /// </summary>
        /// <param name="val">The number to subtract.</param>
        /// <returns>The result of the provided vector subtracted from this vector.</returns>
        public Vector2d Subtract(Number val)
        {
            return this - val;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of subtracting the X and Y of this Vector2d by the Width and Height of the provided Size2d.
        /// </summary>
        /// <param name="val">The number to subtract.</param>
        /// <returns>The result of the provided Size2d subtracted from this Vector2d.</returns>
        public Vector2d Subtract(Size2d val)
        {
            return new Vector2d(X - val.Width, Y - val.Height);
        }

        /// <summary>
        /// Returns a Vector2d that is the result of subtracting the X and Y of this Vector2d from the provided number.
        /// </summary>
        /// <param name="val">The number to subtract from.</param>
        /// <returns>The result of the provided vector subtracted from this vector.</returns>
        public Vector2d SubtractFrom(Number val)
        {
            return val - this;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of subtracting the X and Y of this Vector2d from the Width and Height of the provided Size2d.
        /// </summary>
        /// <param name="val">The Size2d to subtract from.</param>
        /// <returns>The result of the provided Size2d subtracted from this Vector2d.</returns>
        public Vector2d SubtractFrom(Size2d val)
        {
            return new Vector2d(val.Width - X, val.Height - Y);
        }

        /// <summary>
        /// Returns a Vector2d that is the result of subtracting the X and Y of this Vector2d by the X and Y of the provided Vector2d.
        /// </summary>
        /// <param name="val">The Vector2d to subtract.</param>
        /// <returns>The result of the provided vector subtracted by this vector.</returns>
        public Vector2d Subtract(Vector2d v1)
        {
            return this - v1;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of multiplying the X and Y of this Vector2d by the X and Y of the provided Vector2d
        /// </summary>
        /// <param name="v1">The Vector2d to multiply.</param>
        /// <returns>The result of this Vector2d multiplied by the provided Vector2d.</returns>
        public Vector2d Multiply(Vector2d v1)
        {
            return this * v1;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of multiplying the X and Y of this Vector2d by the Width and Height of the provided Size2d.
        /// </summary>
        /// <param name="val">The Size2d to multiply.</param>
        /// <returns>The result of this Vector2d multiplied by the provided Size2d.</returns>
        public Vector2d Multiply(Size2d val)
        {
            return new Vector2d(X * val.Width, Y * val.Height);
        }

        /// <summary>
        /// Returns a Vector2d that is the result of multiplying the X and Y of this Vector2d by the provided number.
        /// </summary>
        /// <param name="val">The number to multiply.</param>
        /// <returns>The result of this Vector2d multiplied by the provided Vector2d.</returns>
        public Vector2d Multiply(Number val)
        {
            return this * val;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of dividing the X and Y of this Vector2d by the provided number.
        /// </summary>
        /// <param name="val">The number to divide.</param>
        /// <returns>The result of this Vector2d divided by the provided number.</returns>
        public Vector2d Divide(Number val)
        {
            return this / val;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of dividing the X and Y of this Vector2d by the X and Y of the provided Vector2d.
        /// </summary>
        /// <param name="val">The Vector2d to divide.</param>
        /// <returns>The result of this Vector2d divided by the provided number.</returns>
        public Vector2d Divide(Vector2d val)
        {
            return this / val;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of dividing the X and Y of this Vector2d by the Width and Height of the provided Size2d.
        /// </summary>
        /// <param name="val">The Size2d to divide.</param>
        /// <returns>The result of the Vector2d divided by the provided Size2d.</returns>
        public Vector2d Divide(Size2d val)
        {
            return new Vector2d(X / val.Width, Y / val.Height);
        }

        /// <summary>
        /// Returns a Vector2d that is the result of dividing the X and Y of this Vector2d from the provided number.
        /// </summary>
        /// <param name="val">The number to divide from.</param>
        /// <returns>The result of the Vector2d divided from the provided number.</returns>
        public Vector2d DivideFrom(Number val)
        {
            return val / this;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of dividing the X and Y of this Vector2d from the X and Y of the provided Vector2d.
        /// </summary>
        /// <param name="val">The Vector2d to divide from.</param>
        /// <returns>The result of the Vector2d divided from the provided Vector2d.</returns>
        public Vector2d DivideFrom(Vector2d val)
        {
            return val / this;
        }

        /// <summary>
        /// Returns a Vector2d that is the result of dividing the X and Y of this Vector2d from the Width and Height of the provided Size2d.
        /// </summary>
        /// <param name="val">The Vector2d to divide from.</param>
        /// <returns>The result of the Vector2d divided from the provided Size2d.</returns>
        public Vector2d DivideFrom(Size2d val)
        {
            return new Vector2d(val.Width / X, val.Height / Y);
        }

        /// <summary>
        /// Returns a Vector2d that is the negated version of this Vector2d.
        /// </summary>
        /// <returns>A copy of the current Vector2d multiplied by -1.</returns>
        public Vector2d Negate()
        {
            return this * -1;
        }
        
        public static Vector2d operator +(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2d operator +(Vector2d v1, Number val)
        {
            return new Vector2d(v1.X + val, v1.Y + val);
        }

        public static Vector2d operator +(Number val, Vector2d v1)
        {
            return v1 + val;
        }

        public static Vector2d operator -(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2d operator -(Vector2d v1, Number val)
        {
            return new Vector2d(v1.X - val, v1.Y - val);
        }

        public static Vector2d operator -(Number val, Vector2d v1)
        {
            return new Vector2d(val - v1.X, val - v1.Y);
        }

        public static Vector2d operator *(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2d operator *(Vector2d v1, Number val)
        {
            return new Vector2d(v1.X * val, v1.Y * val);
        }

        public static Vector2d operator *(Number val, Vector2d v1)
        {
            return v1 * val;
        }

        public static Vector2d operator /(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static Vector2d operator /(Vector2d v1, Number val)
        {
            return new Vector2d(v1.X / val, v1.Y / val);
        }

        public static Vector2d operator /(Number val, Vector2d v1)
        {
            return new Vector2d(val / v1.X, val / v1.Y);
        }

        public static Vector2d operator -(Vector2d v1)
        {
            return v1 * -1;
        }

        public static Vector2d operator ++(Vector2d v1)
        {
            return v1 + 1;
        }

        public static Vector2d operator --(Vector2d v1)
        {
            return v1 - 1;
        }

        /// <summary>
        /// Returns a Vector2d with X and Y components initialized to 0.
        /// </summary>
        public static Vector2d Zero
        {
            get
            {
                return new Vector2d(0, 0);
            }
        }

        /// <summary>
        /// Returns a Vector2d with X and Y components initialized to 1.
        /// </summary>
        public static Vector2d One
        {
            get
            {
                return new Vector2d(1,1);
            }
        }

        /// <summary>
        /// Builds a string in the format of (X, Y).
        /// </summary>
        /// <returns>A string version of the Vector2d.</returns>
        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
