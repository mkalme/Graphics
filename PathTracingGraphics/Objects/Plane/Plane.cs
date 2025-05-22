using System;
using System.Drawing;
using Graphics;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class Plane : SceneObject, IFlatSurfaceObject {
        public override ISurface Surface { get; set; }
        public virtual Vec3f SurfaceNormal { get; set; }

        Vec3f IFlatSurfaceObject.SurfaceNormal => SurfaceNormal;

        public Plane(Vec3f surfaceNormal) {
            SurfaceNormal = surfaceNormal;

            IFlatSurfaceTranslator translator = new FlatSurfaceObjectTranslator(this);
            translator = new RotatedFlatSurfaceTranslator(translator, (float)Math.PI / 4);
            translator = new CustomFlatSurfaceTranslator(translator, point => point * 2);

            Surface = new CheckedFlatSurface(translator, 0.25F, Color.FromArgb(153, 163, 184), Color.FromArgb(108, 115, 127));
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float d = SurfaceNormal.Dot(ray.Direction);

            if (d != 0) {
                surface = GetSurfaceInfo;
                return (Location - ray.Origin).Dot(SurfaceNormal) / d;
            }

            surface = null;
            return -1;
        }
        public SurfaceInfo GetSurfaceInfo(Vec3f point) {
            return new SurfaceInfo(SurfaceNormal, Surface.GetSurface(point));
        }
    }
}
