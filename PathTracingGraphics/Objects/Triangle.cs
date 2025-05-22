using System;
using System.Drawing;
using Graphics;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class Triangle : SceneObject, IFlatSurfaceObject {
        public override ISurface Surface { get; set; }

        public override Vec3f Location {
            get => base.Location;
            set {
                base.Location = value;
                _surfaceNormal = GetSurfaceNormal();
            }
        }
        public Vec3f SecondPoint {
            get => _secondPoint;
            set {
                _secondPoint = value;
                _surfaceNormal = GetSurfaceNormal();
            }
        }
        public Vec3f ThirdPoint {
            get => _thirdPoint;
            set {
                _thirdPoint = value;
                _surfaceNormal = GetSurfaceNormal();
            }
        }

        private Vec3f _secondPoint;
        private Vec3f _thirdPoint;

        private Vec3f _surfaceNormal;
        Vec3f IFlatSurfaceObject.SurfaceNormal => _surfaceNormal;

        public Triangle() {
            FlatSurfaceObjectTranslator translator = new FlatSurfaceObjectTranslator(this);

            Surface = new CheckedFlatSurface(translator, 0.25F, Color.FromArgb(153, 163, 184), Color.FromArgb(108, 115, 127));
        }

        private Vec3f GetSurfaceNormal() {
            return Vec3f.Cross(_secondPoint - Location, _thirdPoint - Location).Normalize();
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float epsilon = 0.0000001F;

            Vec3f edge1, edge2, h, s, q;
            float a, f, u, v;

            edge1 = _secondPoint - Location;
            edge2 = _thirdPoint - Location;

            h = ray.Direction.Cross(edge2);
            a = edge1.Dot(h);
            if (a > -epsilon && a < epsilon) {
                surface = null;
                return -1;
            }
            
            f = 1.0F / a;
            s = ray.Origin - Location;
            u = f * s.Dot(h);
            if (u < 0.0 || u > 1.0) {
                surface = null;
                return -1;
            }

            q = s.Cross(edge1);
            v = f * ray.Direction.Dot(q);
            if (v < 0.0 || u + v > 1.0) {
                surface = null;
                return -1;
            }

            float t = f * edge2.Dot(q);
            if (t > epsilon) {
                surface = GetSurfaceInfo;
                return t;
            }

            surface = null;
            return -1;
        }
        public SurfaceInfo GetSurfaceInfo(Vec3f point) {
            return new SurfaceInfo(_surfaceNormal, Surface.GetSurface(point));
        }
    }
}
