using System;
using Vectors.Vec3;
using Graphics;

namespace PathTracingGraphics {
    public abstract class PlaneShape : Plane {
        public PlaneShape(Vec3f surfaceNormal) : base(surfaceNormal) { 
        
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = base.Intersect(ray, out surface);
            if (t < 0) return -1;

            return Intersects((ray.Origin + (ray.Direction * t) - Location).GetMagnitude()) ? t : -1;
        }
        public abstract bool Intersects(float distance);
    }
}
