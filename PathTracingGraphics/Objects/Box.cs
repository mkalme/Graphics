using System;
using System.Drawing;
using Graphics;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class Box : SceneObject {
        public BoxArea BoxArea { get; set; }

        public Box(Vec3f point1, Vec3f point2) {
            BoxArea = new BoxArea(point1, point2);

            Surface = new SimpleSurface(new SurfaceProperties(Color.DarkCyan.ToVector(), 0.5F));
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = -1;
            Vec3f normal = new Vec3f();

            for (int i = 0; i < BoxArea.Areas.Length; i++) {
                RectangleArea area = BoxArea.Areas[i];

                float value = area.Intersect(ray, out Vec3f intersectionPoint, out Vec2f relativePoint);
                if (value >= 0 && (t < 0 || value < t)) {
                    t = value;
                    normal = area.SurfaceNormal;
                }
            }

            surface = x => GetSurface(x, normal);
            return t;
        }
        private SurfaceInfo GetSurface(Vec3f point, Vec3f normal) {
            return new SurfaceInfo(normal, Surface.GetSurface(point));
        }
    }
}
