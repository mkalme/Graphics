using System;
using Vectors.Vec3;
using Vectors.Vec2;
using Graphics;

namespace PathTracingGraphics {
    public abstract class SphereUvSurface : ISurface {
        public Sphere Sphere { get; set; }

        public SurfaceProperties DefaultProperties { get; set; } = new SurfaceProperties(0, 0, 1);

        public SphereUvSurface(Sphere sphere) {
            Sphere = sphere;
        }

        public SurfaceProperties GetSurface(Vec3f point) {
            Vec3f n = (point - Sphere.Location) / Sphere.Radius;

            float u = 0.5F + (float)Math.Atan2(n.X, n.Z) / (float)(2 * Math.PI);
            float v = 0.5F - (float)Math.Asin(n.Y) / (float)Math.PI;

            return GetSurfaceFromUvPoint(new Vec2f(u, v));
        }
        public abstract SurfaceProperties GetSurfaceFromUvPoint(Vec2f uv);
    }
}
