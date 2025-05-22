using System;
using Vectors.Vec3;

namespace Graphics {
    public readonly struct SurfaceProperties {
        public Vec3f Color { get; }
        public float ReflectionIndex { get; }
        public float Ks { get; }

        public SurfaceProperties(Vec3f color, float reflectionIndex, float ks = 1) {
            Color = color;
            ReflectionIndex = reflectionIndex;
            Ks = ks;
        }
    }
}
