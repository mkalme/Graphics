using System;
using Graphics;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class Rectangle : Plane {
        public override Vec3f SurfaceNormal {
            get => base.SurfaceNormal;
            set {
                base.SurfaceNormal = value;
                double normalAngle = new Vec2f(-value.Z, -value.X).GetAngle();
                double normalYAngle = (float)Math.Atan2(-value.Y, new Vec2f(value.Z, value.X).GetMagnitude());

                _surfaceTranslater.HorizontalRotation = new Rotation((float)(Math.PI / 2) - normalAngle);
                _surfaceTranslater.VerticalRotation = new Rotation(-normalYAngle);
            }
        }

        public float Width { get; set; }
        public float Height { get; set; }

        public float Rotation {
            get => _rotation;
            set {
                _rotatedSurface.Radians = value;
                _rotation = value;
            }
        }
        private float _rotation;

        private FlatSurfaceTranslator _surfaceTranslater = new FlatSurfaceTranslator();
        private RotatedFlatSurfaceTranslator _rotatedSurface;

        public Rectangle(Vec3f surfaceNormal) : base(surfaceNormal) {
            _rotatedSurface = new RotatedFlatSurfaceTranslator(_surfaceTranslater, 0);
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = base.Intersect(ray, out surface);
            if (t < 0) return -1;

            Vec3f intersectionPoint = ray.Origin + (ray.Direction * t) - Location;
            Vec2f point = _rotatedSurface.Get2dPoint(intersectionPoint);

            if (point.X >= 0 && point.X <= Width && point.Y >= 0 && point.Y <= Height) {
                return t;
            }

            return -1;
        }
    }
}
