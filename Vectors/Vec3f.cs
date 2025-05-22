using System;

namespace Vectors.Vec3 {
    public readonly struct Vec3f : IEquatable<Vec3f> {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Vec3f(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        //Operators
        public static Vec3f operator +(Vec3f a, Vec3f b) => Add(a, b);
        public static Vec3f operator -(Vec3f a, Vec3f b) => Substract(a, b);
        public static Vec3f operator -(Vec3f vector) => Multiply(vector, -1);
        public static Vec3f operator *(Vec3f a, float value) => Multiply(a, value);
        public static Vec3f operator *(float value, Vec3f a) => Multiply(a, value);
        public static Vec3f operator *(Vec3f a, Vec3f b) => Multiply(a, b);
        public static Vec3f operator /(Vec3f a, float value) => Divide(a, value);
        public static Vec3f operator /(Vec3f a, Vec3f b) => Divide(a, b);

        public static bool operator ==(Vec3f a, Vec3f b) => a.Equals(b);
        public static bool operator !=(Vec3f a, Vec3f b) => !a.Equals(b);

        //Methods
        public Vec3f Normalize() => Normalize(this);
        public float GetMagnitude() => GetMagnitude(this);

        public Vec3f Add(Vec3f vector) => Add(this, vector);
        public Vec3f Substract(Vec3f vector) => Substract(this, vector);
        public Vec3f Multiply(float value) => Multiply(this, value);
        public Vec3f Multiply(Vec3f vector) => Multiply(this, vector);
        public Vec3f Divide(float value) => Divide(this, value);
        public Vec3f Divide(Vec3f vector) => Divide(this, vector);

        public Vec3f Cross(Vec3f vector) => Cross(this, vector);
        public float Dot(Vec3f vector) => Dot(this, vector);

        //Functions
        public static Vec3f Normalize(Vec3f vector) {
            float magnitude = GetMagnitude(vector);

            return new Vec3f(vector.X / magnitude, vector.Y / magnitude, vector.Z / magnitude);
        }
        public static float GetMagnitude(Vec3f vector) {
            return (float)Math.Sqrt(Dot(vector, vector));
        }

        public static Vec3f Add(Vec3f a, Vec3f b) {
            return new Vec3f(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vec3f Substract(Vec3f a, Vec3f b) {
            return new Vec3f(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vec3f Multiply(Vec3f a, float value) {
            return new Vec3f(a.X * value, a.Y * value, a.Z * value);
        }
        public static Vec3f Multiply(Vec3f a, Vec3f b) {
            return new Vec3f(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
        public static Vec3f Divide(Vec3f a, float value) {
            return new Vec3f(a.X / value, a.Y / value, a.Z / value);
        }
        public static Vec3f Divide(Vec3f a, Vec3f b) {
            return new Vec3f(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static Vec3f Cross(Vec3f a, Vec3f b) {
            float x = a.Y * b.Z - a.Z * b.Y;
            float y = a.Z * b.X - a.X * b.Z;
            float z = a.X * b.Y - a.Y * b.X;

            return new Vec3f(x, y, z);
        }
        public static float Dot(Vec3f a, Vec3f b) {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public bool Equals(Vec3f other) {
            return X == other.X && Y == other.Y && Z == other.Z;
        }
        public override bool Equals(object obj) {
            return obj is Vec3f f && Equals(f);
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y, Z);
        }

        public override string ToString() {
            return $"Vec3f [X={X}, Y={Y}, Z={Z}]";
        }

        public static Vec3f Empty { get; } = new Vec3f();

        public static implicit operator Vec3f(float n) => new Vec3f(n, n, n);
    }
}
