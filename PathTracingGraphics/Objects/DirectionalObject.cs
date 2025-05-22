using System;
using Graphics;
using Vectors.Vec2;
using Vectors.Vec3;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class DirectionalObject : SceneObject {
        public override Vec3f Location { get => Object.Location; set => Object.Location = value; }

        public SceneObject Object { get; set; }

        public Vec3f Direction {
            get => _direction;
            set {
                _direction = value;

                Vec2f xz = new Vec2f(value.Z, value.X);

                _horizontalRotation = new Rotation(xz.GetAngle());
                _verticalRotation = new Rotation(new Vec2f(value.Y, xz.GetMagnitude()).GetAngle());

                _horizontalRayRotation = new Rotation(xz.GetAngle());
                Console.WriteLine(xz.GetAngle());
                _verticalRayRotation = _verticalRotation;
            }
        }
        private Vec3f _direction;

        private Rotation _horizontalRotation;
        private Rotation _verticalRotation;

        private Rotation _horizontalRayRotation;
        private Rotation _verticalRayRotation;

        public DirectionalObject(SceneObject sceneObject) {
            Object = sceneObject;

            Direction = new Vec3f(1, 0, 0);

            Vec3f n = RotateRay(new Vec3f(0, 1, 1));

            Console.WriteLine(n);
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            Vec3f newOrigin = Object.Location - Rotate(Object.Location - ray.Origin);
            Vec3f newDirection = Rotate(ray.Direction);

            float t = Object.Intersect(new Ray(newOrigin, newDirection), out Func<Vec3f, SurfaceInfo> objectSurface);
            surface = x => GetSurfaceInfo(objectSurface, x);

            return t;
        }

        private SurfaceInfo GetSurfaceInfo(Func<Vec3f, SurfaceInfo> surface, Vec3f point) {
            SurfaceInfo info = surface(Object.Location - Rotate(Object.Location - point));
            Vec3f newNormal = Rotate(info.SurfaceNormal);

            return new SurfaceInfo(newNormal, info.SurfaceProperties);
        }
        private Vec3f Rotate(Vec3f point) {
            Vec2f zy = new Vec2f(point.Z, point.Y).Rotate(_verticalRotation);
            Vec2f xz = new Vec2f(zy.X, point.X).Rotate(_horizontalRotation);

            return new Vec3f(xz.Y, zy.Y, xz.X);
        }

        private Vec3f RotateRay(Vec3f diretion) {
            Vec2f zy = new Vec2f(diretion.Z, diretion.Y).Rotate(_verticalRotation);
            Vec2f xz = new Vec2f(zy.X, diretion.X).Rotate(_horizontalRayRotation);

            return new Vec3f(xz.Y, zy.Y, xz.X);
        }
    }
}
