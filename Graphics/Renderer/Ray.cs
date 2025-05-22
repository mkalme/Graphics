using System;
using Vectors.Vec3;

namespace Graphics {
    public readonly struct Ray {
        public Vec3f Origin { get; }
        public Vec3f Direction { get; }

        public Ray(Vec3f origin, Vec3f direction) {
            Origin = origin;
            Direction = direction;
        }

        public static Ray FromTwoPoints(Vec3f origin, Vec3f point2) {
            return new Ray(origin, (point2 - origin).Normalize());
        }
    }
}
