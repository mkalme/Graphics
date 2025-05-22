using System;
using Graphics;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class Sphere : SceneObject {
        public override ISurface Surface { get; set; }

        public Sphere() {
            Surface = new SphereTextureUvSurface(this);
        }

        public float Radius {
            get => _radius;
            set {
                _radius = value;
                _radiusSquared = value * value;
            }
        }

        private float _radius;
        private float _radiusSquared;

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            Vec3f v = ray.Origin - Location;

            float b = 2 * ray.Direction.Dot(v);
            float c = v.Dot(v) - _radiusSquared;
            float d = b * b - 4 * c;

            if (d > 0) {
                float sqrt = (float)Math.Sqrt(d);
                float x1 = (-b - sqrt) / 2;

                if (x1 >= 0) {
                    float x2 = (-b + sqrt) / 2;
                    if (x2 >= 0) {
                        surface = GetSurfaceInfo;
                        return x1;
                    } 
                } else {
                    float x2 = (-b + sqrt) / 2;
                    if (x2 >= 0) {
                        surface = GetSurfaceInfo;
                        return x2;
                    }
                }
            }

            surface = null;
            return -1;
        }
        public SurfaceInfo GetSurfaceInfo(Vec3f point) {
            return new SurfaceInfo((point - Location).Normalize(), Surface.GetSurface(point));
        }
    }
}
