using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Magnitude
        { get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); } }

        public Vector3 Normalized
        { get { return Normalize(this); } }

        public Vector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector3(Vector2 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Returns the given vector normalized
        /// </summary>
        /// <param name="vector">The vector that will be normalized</param>
        /// <returns>The normalized vector</returns>
        public static Vector3 Normalize(Vector3 vector)
        {
            if (vector.Magnitude == 0)
                return new Vector3();
            return vector / vector.Magnitude;
        }

        /// <summary>
        /// Returns the dot product of the two vectors given
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        { return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z); }

        public static Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                lhs.Z * rhs.X - lhs.X * rhs.Z,
                lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        { return new Vector3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z); }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        { return new Vector3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z); }

        public static Vector3 operator *(Vector3 lhs, float rhs)
        { return new Vector3(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs); }

        public static Vector3 operator /(Vector3 lhs, float rhs)
        { return new Vector3(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs); }
    }
}
