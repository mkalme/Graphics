using System;

namespace Vectors.Vec2 {
    public readonly struct Vec2f : IEquatable<Vec2f> {
        public float X { get; }
        public float Y { get; }

        public Vec2f(float x, float y) {
            X = x;
            Y = y;
        }

        //Operators
        public static Vec2f operator +(Vec2f a, Vec2f b) => Add(a, b);
        public static Vec2f operator -(Vec2f a, Vec2f b) => Substract(a, b);
        public static Vec2f operator -(Vec2f vector) => Multiply(vector, -1);
        public static Vec2f operator *(Vec2f a, float value) => Multiply(a, value);
        public static Vec2f operator *(float value, Vec2f a) => Multiply(a, value);
        public static Vec2f operator *(Vec2f a, Vec2f b) => Multiply(a, b);
        public static Vec2f operator /(Vec2f a, float value) => Divide(a, value);
        public static Vec2f operator /(Vec2f a, Vec2f b) => Divide(a, b);

        public static bool operator ==(Vec2f a, Vec2f b) => a.Equals(b);
        public static bool operator !=(Vec2f a, Vec2f b) => !a.Equals(b);

        //Methods
        public Vec2f Normalize() => Normalize(this);
        public float GetMagnitude() => GetMagnitude(this);

        public Vec2f Add(Vec2f vector) => Add(this, vector);
        public Vec2f Substract(Vec2f vector) => Substract(this, vector);
        public Vec2f Multiply(float value) => Multiply(this, value);
        public Vec2f Multiply(Vec2f vector) => Multiply(this, vector);
        public Vec2f Divide(float value) => Divide(this, value);
        public Vec2f Divide(Vec2f vector) => Divide(this, vector);

        public float Dot(Vec2f vector) => Dot(this, vector);

        public Vec2f Rotate(float radians) => Rotate(this, radians);
        public float GetAngle() => GetAngle(this);

        //Functions
        public static Vec2f Normalize(Vec2f vector) {
            float magnitude = GetMagnitude(vector);

            return new Vec2f(vector.X / magnitude, vector.Y / magnitude);
        }
        public static float GetMagnitude(Vec2f vector) {
            return (float)Math.Sqrt(Dot(vector, vector));
        }

        public static Vec2f Add(Vec2f a, Vec2f b) {
            return new Vec2f(a.X + b.X, a.Y + b.Y);
        }
        public static Vec2f Substract(Vec2f a, Vec2f b) {
            return new Vec2f(a.X - b.X, a.Y - b.Y);
        }
        public static Vec2f Multiply(Vec2f a, float value) {
            return new Vec2f(a.X * value, a.Y * value);
        }
        public static Vec2f Multiply(Vec2f a, Vec2f b) {
            return new Vec2f(a.X * b.X, a.Y * b.Y);
        }
        public static Vec2f Divide(Vec2f a, float value) {
            return new Vec2f(a.X / value, a.Y / value);
        }
        public static Vec2f Divide(Vec2f a, Vec2f b) {
            return new Vec2f(a.X / b.X, a.Y / b.Y);
        }

        public static float Dot(Vec2f a, Vec2f b) {
            return a.X * b.X + a.Y * b.Y;
        }
        
        public static Vec2f Rotate(Vec2f vector, float radians) {
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);

            return new Vec2f((float)(cos * vector.X - sin * vector.Y), (float)(sin * vector.X + cos * vector.Y));
        }
        public static float GetAngle(Vec2f vector) {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public bool Equals(Vec2f other) {
            return X == other.X && Y == other.Y;
        }
        public override bool Equals(object obj) {
            return obj is Vec2f f && Equals(f);
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y);
        }

        public override string ToString() {
            return $"Vec2f [X={X}, Y={Y}]";
        }

        public static Vec2f Empty { get; } = new Vec2f();

        public static implicit operator Vec2f(float n) => new Vec2f(n, n);
    }
}
