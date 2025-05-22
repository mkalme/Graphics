using System;
using Graphics;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class HorizontalRectangle : SceneObject, IFlatSurfaceObject {
        public override ISurface Surface {
            get => _plane.Surface;
            set => _plane.Surface = value;
        }
        public override Vec3f Location {
            get => _plane.Location;
            set => _plane.Location = value;
        }

        private readonly Plane _plane = new Plane(new Vec3f(0, 1, 0)) {
            Location = 0
        };

        public float XLength { get; set; }
        public float ZLength { get; set; }

        Vec3f IFlatSurfaceObject.SurfaceNormal => _plane.SurfaceNormal;

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = _plane.Intersect(ray, out surface);
            if (t < 0) return -1;

            Vec3f rel = ray.Origin + (ray.Direction * t) - _plane.Location;
            if (rel.X > XLength || rel.Z > ZLength || rel.X < 0 || rel.Z < 0) return -1;

            surface = GetSurfaceInfo;
            return t;
        }
        public SurfaceInfo GetSurfaceInfo(Vec3f point) {
            return new SurfaceInfo(_plane.SurfaceNormal, Surface.GetSurface(point));
        }
    }
}
