using System;
using Vectors.Vec3;
using Graphics;

namespace PathTracingGraphics {
    public class SubtractedObject : SceneObject, IIntersectedPointObject {
        public IIntersectedPointObject MainObject { get; set; }
        public IIntersectedPointObject Subtractor { get; set; }

        public SubtractedObject(IIntersectedPointObject mainObject, IIntersectedPointObject subtractor) {
            MainObject = mainObject;
            Subtractor = subtractor;

            Surface = new SimpleSurface(new SurfaceProperties(new Vec3f(0.4F, 0.7F, 0.6F), 0.5F));
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            Func<Vec3f, SurfaceInfo> thisSurface;
            float t = 0;

            if (Subtractor.PointIsInsideObject(ray.Origin)) {
                t = IntersectFromInsideSubtractor(ray, out thisSurface);
            } else if (MainObject.PointIsInsideObject(ray.Origin)) {
                t = IntersectFromInsideMain(ray, out thisSurface);
            } else {
                t = IntersectFromOutside(ray, out thisSurface);
            }

            if (t >= 0) {
                surface = p => {
                    return new SurfaceInfo(thisSurface(p).SurfaceNormal, Surface.DefaultProperties);
                };
                return t;
            }

            surface = null;
            return -1;
        }

        private float IntersectFromOutside(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = MainObject.Intersect(ray, out surface);
            if (t >= 0) {
                Vec3f point = ray.Origin + ray.Direction * t;

                if (Subtractor.PointIsInsideObject(point)) {
                    float rT = IntersectFromInsideSubtractor(new Ray(point, ray.Direction), out surface);
                    if (rT < 0) return -1;

                    return rT + t;
                } else {
                    return t;
                }
            }

            surface = null;
            return -1;
        }
        private float IntersectFromInsideSubtractor(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = Subtractor.Intersect(ray, out surface);
            if (t >= 0) {
                Vec3f outsidePoint = ray.Origin + ray.Direction * t;

                if (MainObject.PointIsInsideObject(outsidePoint)) {
                    return t;
                } else {
                    float rT = IntersectFromOutside(new Ray(outsidePoint, ray.Direction), out surface);
                    if (rT < 0) return -1;

                    return rT + t;
                }
            }

            surface = null;
            return -1;
        }
        private float IntersectFromInsideMain(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float mainT = MainObject.Intersect(ray, out Func<Vec3f, SurfaceInfo> mainSurface);
            float subT = Subtractor.Intersect(ray, out Func<Vec3f, SurfaceInfo> subSurface);

            if (mainT >= 0 && (subT < 0 || subT > mainT)) {
                surface = mainSurface;
                return mainT;
            }

            if (subT >= 0 && (mainT < 0 || mainT > subT)) {
                surface = subSurface;
                return subT;
            }

            surface = null;
            return -1;
        }

        public bool PointIsInsideObject(Vec3f point) {
            return MainObject.PointIsInsideObject(point) && !Subtractor.PointIsInsideObject(point);
        }
    }
}
