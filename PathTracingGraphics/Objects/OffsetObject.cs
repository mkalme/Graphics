using System;
using Vectors.Vec3;
using Graphics;

namespace PathTracingGraphics {
    public class OffsetObject : SceneObject {
        public ISceneObject Object { get; set; }
        public Vec3f Offset { get; set; }

        public OffsetObject(ISceneObject sceneObject, Vec3f offset) {
            Object = sceneObject;
            Offset = offset;
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            Vec3f newOrigin = ray.Origin - Offset;

            float t = Object.Intersect(new Ray(newOrigin, ray.Direction), out Func<Vec3f, SurfaceInfo> objectSurface);
            surface = x => GetSurfaceInfo(objectSurface, x);

            return t;
        }
        private SurfaceInfo GetSurfaceInfo(Func<Vec3f, SurfaceInfo> surface, Vec3f point) {
            return surface(point - Offset);
        }
    }
}
