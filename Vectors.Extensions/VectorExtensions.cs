using System;
using System.Drawing;
using Vectors.Vec3;
using Vectors.Vec2;

namespace Vectors.Extensions {
    public static class VectorExtensions {
        public static Color ToColor(this Vec3f vector) {
            Vec3f clamped = vector.Clamp(0, 1);

            return Color.FromArgb((int)(clamped.X * 255), (int)(clamped.Y * 255), (int)(clamped.Z * 255));
        }
        public static Vec3f ToVector(this Color color) {
            return new Vec3f(color.R / 255F, color.G / 255F, color.B / 255F);
        }

        public static Vec3f Mix(this Vec3f a, Vec3f b, float intensity) {
            return a + (b - a) * intensity;
        }
        public static Vec3f Clamp(this Vec3f vector, float min, float max) {
            return new Vec3f(vector.X < min ? min : vector.X > max ? max : vector.X,
                vector.Y < min ? min : vector.Y > max ? max : vector.Y,
                vector.Z < min ? min : vector.Z > max ? max : vector.Z);
        }

        public static Vec3f Reflect(this Vec3f direction, Vec3f n) {
            return direction - 2 * direction.Dot(n) * n;
        }

        public static Vec2f Rotate(this Vec2f vector, Rotation rotation) {
            return new Vec2f(
                (float)(rotation.Cosine * vector.X - rotation.Sine * vector.Y),
                (float)(rotation.Sine * vector.X + rotation.Cosine * vector.Y));
        }
    }
}
