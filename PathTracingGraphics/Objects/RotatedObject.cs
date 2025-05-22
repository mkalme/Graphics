using System;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;
using Graphics;

namespace PathTracingGraphics {
    public class RotatedObject : SceneObject {
        public override Vec3f Location { get => Object.Location; set => Object.Location = value; }
        public SceneObject Object { get; set; }

        public Vec2f Direction {
            get => _direction;
            set {
                _direction = value;

                RayRotation = new Rotation(-value.GetAngle());
                NormalRotation = new Rotation(value.GetAngle());
            }
        }
        private Vec2f _direction;

        protected Rotation RayRotation;
        protected Rotation NormalRotation;

        public bool UseUserDefinedCenter { get; private set; } = false;
        public Vec3f UserDefinedCenter { get; private set; }

        public RotatedObject(SceneObject sceneObject) {
            Object = sceneObject;

            Direction = new Vec2f(1, 0).Normalize();
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            Vec3f newOrigin = RotatePoint(ray.Origin);
            Vec3f newDirection = RotateDirection(ray.Direction);

            float t = Object.Intersect(new Ray(newOrigin, newDirection), out Func<Vec3f, SurfaceInfo> objectSurface);
            surface = x => GetSurfaceInfo(objectSurface, x);

            return t;
        }

        protected virtual SurfaceInfo GetSurfaceInfo(Func<Vec3f, SurfaceInfo> surface, Vec3f point) {
            SurfaceInfo info = surface(RotatePoint(point));
            Vec3f newNormal = RotateNormal(info.SurfaceNormal);

            return new SurfaceInfo(newNormal, info.SurfaceProperties);
        }

        protected Vec3f RotateDirection(Vec3f direction) {
            Vec2f rotated = new Vec2f(direction.Z, direction.X).Rotate(RayRotation);
            
            return new Vec3f(rotated.Y, direction.Y, rotated.X);
        }
        protected Vec3f RotatePoint(Vec3f origin) {
            Vec3f l = origin - (UseUserDefinedCenter ? UserDefinedCenter : Location);

            return (UseUserDefinedCenter ? UserDefinedCenter : Location) + RotateDirection(l);
        }
        protected Vec3f RotateNormal(Vec3f normal) {
            Vec2f rotated = new Vec2f(normal.Z, normal.X).Rotate(NormalRotation);

            return new Vec3f(rotated.Y, normal.Y, rotated.X);
        }

        public void SetRadians(float radians) {
            Direction = new Vec2f((float)Math.Cos(radians), (float)Math.Sin(radians));
        }
        public void SetDegrees(float degrees) {
            SetRadians((float)(degrees / (180 / Math.PI)));
        }

        public void SetCenter(Vec3f center) {
            UseUserDefinedCenter = true;
            UserDefinedCenter = center;
        }
        public void ResetCenter() {
            UseUserDefinedCenter = false;
        }
    }
}
