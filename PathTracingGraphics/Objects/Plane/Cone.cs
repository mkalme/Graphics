using System;
using Vectors.Vec3;
using Graphics;

namespace PathTracingGraphics {
    public class Cone : SceneObject {
        public float AngleHalf { get; set; }
        public float Height { get; set; }
        public Vec3f Axis { get; set; }

        public Cone() {
            Surface = new SimpleSurface(new SurfaceProperties(new Vec3f(1, 0, 0), 0.4F));
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            Vec3f co = ray.Origin - Location;

            float a = ray.Direction.Dot(Axis) * ray.Direction.Dot(Axis) - AngleHalf * AngleHalf;
            float b = 2 * (ray.Direction.Dot(Axis) * co.Dot(Axis) - ray.Direction.Dot(co) * AngleHalf * AngleHalf);
            float c = co.Dot(Axis) * co.Dot(Axis) - co.Dot(co) * AngleHalf * AngleHalf;

            float det = b * b - 4 * a * c;
            if (det < 0) {
                surface = null;
                return -1;
            }

            det = (float)Math.Sqrt(det);
            float t1 = (-b - det) / (2 * a);
            float t2 = (-b + det) / (2 * a);

            float t = t1;
            if (t < 0 || t2 > 0 && t2 < t) t = t2;
            if (t < 0) {
                surface = null;
                return -1;
            }

            Vec3f cp = ray.Origin + t * ray.Direction - Location;
            float h = cp.Dot(Axis);
            if (h < 0 || h > Height) {
                surface = null;
                return -1;
            }

            surface = x => {
                Vec3f n = (cp * Axis.Dot(cp) / cp.Dot(cp) - Axis).Normalize();
                return new SurfaceInfo(n, Surface.DefaultProperties);
            };
            return t;
        }
    }
}
