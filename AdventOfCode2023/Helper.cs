using System;
using System.Numerics;

namespace AdventOfCode2023;
public static class Helper
{
    public readonly record struct Position2D(int X, int Y) : IComparable<Position2D>
    {
        public static implicit operator Position2D((int, int) p) => new(p.Item1, p.Item2);
        public static explicit operator (int, int)(Position2D p) => (p.X, p.Y);

        public int CompareTo(Position2D other)
        {
            if (Y < other.Y) return -1;
            if (Y > other.Y) return 1;
            if (X < other.X) return -1;
            if (X > other.X) return 1;
            return 0;
        }
        public static bool operator >(Position2D left, Position2D right) => left.CompareTo(right) > 0;
        public static bool operator <(Position2D left, Position2D right) => left.CompareTo(right) < 0;
        public static bool operator >=(Position2D left, Position2D right) => left.CompareTo(right) >= 0;
        public static bool operator <=(Position2D left, Position2D right) => left.CompareTo(right) <= 0;
    }

    public readonly record struct Position2DLong(long X, long Y) : IComparable<Position2DLong>
    {
        public static implicit operator Position2DLong((long, long) p) => new(p.Item1, p.Item2);
        public static explicit operator (long, long)(Position2DLong p) => (p.X, p.Y);

        public static implicit operator Position2DLong(Position2D p) => new(p.X, p.Y);
        public static explicit operator Position2D(Position2DLong p) => new((int)p.X, (int)p.Y);

        public int CompareTo(Position2DLong other)
        {
            if (Y < other.Y) return -1;
            if (Y > other.Y) return 1;
            if (X < other.X) return -1;
            if (X > other.X) return 1;
            return 0;
        }
        public static bool operator >(Position2DLong left, Position2DLong right) => left.CompareTo(right) > 0;
        public static bool operator <(Position2DLong left, Position2DLong right) => left.CompareTo(right) < 0;
        public static bool operator >=(Position2DLong left, Position2DLong right) => left.CompareTo(right) >= 0;
        public static bool operator <=(Position2DLong left, Position2DLong right) => left.CompareTo(right) <= 0;
    }

    public readonly record struct Position3D(int X, int Y, int Z) : IComparable<Position3D>
    {
        public static implicit operator Position3D((int, int, int) p) => new(p.Item1, p.Item2, p.Item3);
        public static explicit operator (int, int, int)(Position3D p) => (p.X, p.Y, p.Z);

        public static implicit operator Position3D(Position2D p) => new(p.X, p.Y, 0);
        public static explicit operator Position2D(Position3D p) => new(p.X, p.Y);

        public int CompareTo(Position3D other)
        {
            if (Z < other.Z) return -1;
            if (Z > other.Z) return 1;
            if (Y < other.Y) return -1;
            if (Y > other.Y) return 1;
            if (X < other.X) return -1;
            if (X > other.X) return 1;
            return 0;
        }
        public static bool operator >(Position3D left, Position3D right) => left.CompareTo(right) > 0;
        public static bool operator <(Position3D left, Position3D right) => left.CompareTo(right) < 0;
        public static bool operator >=(Position3D left, Position3D right) => left.CompareTo(right) >= 0;
        public static bool operator <=(Position3D left, Position3D right) => left.CompareTo(right) <= 0;
    }

    public readonly record struct Position3DLong(long X, long Y, long Z) : IComparable<Position3DLong>
    {
        public static implicit operator Position3DLong((long, long, long) p) => new(p.Item1, p.Item2, p.Item3);
        public static explicit operator (long, long, long)(Position3DLong p) => (p.X, p.Y, p.Z);

        public static implicit operator Position3DLong(Position2DLong p) => new(p.X, p.Y, 0);
        public static explicit operator Position2DLong(Position3DLong p) => new(p.X, p.Y);

        public static implicit operator Position3DLong(Position3D p) => new(p.X, p.Y, p.Z);
        public static explicit operator Position3D(Position3DLong p) => new((int)p.X, (int)p.Y, (int)p.Z);

        public int CompareTo(Position3DLong other)
        {
            if (Z < other.Z) return -1;
            if (Z > other.Z) return 1;
            if (Y < other.Y) return -1;
            if (Y > other.Y) return 1;
            if (X < other.X) return -1;
            if (X > other.X) return 1;
            return 0;
        }
        public static bool operator >(Position3DLong left, Position3DLong right) => left.CompareTo(right) > 0;
        public static bool operator <(Position3DLong left, Position3DLong right) => left.CompareTo(right) < 0;
        public static bool operator >=(Position3DLong left, Position3DLong right) => left.CompareTo(right) >= 0;
        public static bool operator <=(Position3DLong left, Position3DLong right) => left.CompareTo(right) <= 0;
    }

    public readonly record struct Vector2D(double X, double Y)
    {
        public static implicit operator Vector2D((double, double) v) => new(v.Item1, v.Item2);
        public static explicit operator (double, double)(Vector2D v) => (v.X, v.Y);

        public Vector2D Zero { get { return new(); } }
        public double Magnitude { get { return Math.Sqrt(X * X + Y * Y); } }
        public double Angle { get { return Math.Atan2(Y, X); } }

        public static Vector2D operator +(Vector2D v) => v;
        public static Vector2D operator -(Vector2D v) => new(-v.X, -v.Y);
        public static Vector2D operator +(Vector2D v1, Vector2D v2) => new(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2D operator -(Vector2D v1, Vector2D v2) => new(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2D operator *(Vector2D v, double s) => new(v.X * s, v.Y * s);
        public static Vector2D operator *(double s, Vector2D v) => new(v.X * s, v.Y * s);
        public static Vector2D operator /(Vector2D v, double s) => new(v.X / s, v.Y / s);
    }

    public static T Pow<T>(this T bas, int exp) where T : INumber<T>
    {
        if (exp == 0) return T.MultiplicativeIdentity;
        return Enumerable
              .Repeat(bas, exp)
              .Aggregate((a, b) => a * b);
    }

    public static IEnumerable<T> ShowElements<T>(this IEnumerable<T> collection)
    {
        foreach (var elem in collection)
        {
            Console.WriteLine($"{elem}");
        }
        Console.WriteLine();
        return collection;
    }

    public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T>
        => a / GreatestCommonDivisor(a, b) * b;

    public static T LeastCommonMultiple<T>(this IEnumerable<T> values) where T : INumber<T>
        => values.Aggregate(LeastCommonMultiple);

    public static int MDist(this Position2D pos1, Position2D pos2) =>
        Math.Abs(pos1.X - pos2.X) + Math.Abs(pos1.Y - pos2.Y);
    public static int MDist(this Position3D pos1, Position3D pos2) =>
        Math.Abs(pos1.X - pos2.X) + Math.Abs(pos1.Y - pos2.Y) + Math.Abs(pos1.Z - pos2.Z);
    public static long MDist(this Position2DLong pos1, Position2DLong pos2) =>
        Math.Abs(pos1.X - pos2.X) + Math.Abs(pos1.Y - pos2.Y);
    public static long MDist(this Position3DLong pos1, Position3DLong pos2) =>
        Math.Abs(pos1.X - pos2.X) + Math.Abs(pos1.Y - pos2.Y) + Math.Abs(pos1.Z - pos2.Z);
}
