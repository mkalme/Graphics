using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vectors.Vec3;
using Graphics;

namespace PathTracingGraphics {
    public class IntersectedObject : SceneObject, IIntersectedPointObject {
        public IList<IIntersectedPointObject> Objects { get; set; }

        public IntersectedObject() {
            Objects = new List<IIntersectedPointObject>();

            Surface = new SimpleSurface(new SurfaceProperties(new Vec3f(0.6F, 0.43F, 0.3F), 0.4F));
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            object ignore = null;
            Vec3f origin = ray.Origin;
            float tR = 0;

            while (true) {
                bool intersected = false;

                for (int i = 0; i < Objects.Count; i++) {
                    IIntersectedPointObject obj = Objects[i];
                    if (ignore != null && obj == ignore) continue;

                    float t = obj.Intersect(new Ray(origin, ray.Direction), out Func<Vec3f, SurfaceInfo> thisSurface);
                    if (t >= 0) {
                        Vec3f point = origin + ray.Direction * t;
                        tR += t;

                        for (int j = 0; j < Objects.Count; j++) {
                            if (Objects[j] != obj && Objects[j].PointIsInsideObject(point)) {
                                surface = p => {
                                    return new SurfaceInfo(thisSurface(p).SurfaceNormal, Surface.DefaultProperties);
                                };
                                return tR;
                            }
                        }

                        ignore = obj;
                        origin = point;
                        intersected = true;
                    }
                }

                if (!intersected) break;
            }

            surface = null;
            return -1;
        }
        public bool PointIsInsideObject(Vec3f point) {
            for (int i = 0; i < Objects.Count; i++) {
                if (!Objects[i].PointIsInsideObject(point)) return false;
            }

            return true;
        }
    }

    public interface IIntersectedPointObject : ISceneObject {
        bool PointIsInsideObject(Vec3f point);
    }

    public class IntersectedSphere : IIntersectedPointObject {
        public Vec3f Location { get => Sphere.Location; set => Sphere.Location = value; }
        public Sphere Sphere { get; set; }

        public IntersectedSphere(Sphere sphere) {
            Sphere = sphere;
        }

        public float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            return Sphere.Intersect(ray, out surface);
        }

        public bool PointIsInsideObject(Vec3f point) {
            return (point - Location).GetMagnitude() <= Sphere.Radius;
        }
    }

    public class IntersectedBox : IIntersectedPointObject {
        public Vec3f Location { get => Box.Location; set => Box.Location = value; }
        public Box Box { get; set; }

        public IntersectedBox(Box box) {
            Box = box;
        }

        public float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            return Box.Intersect(ray, out surface);
        }

        public bool PointIsInsideObject(Vec3f p) {
            return p.X >= Box.BoxArea.BottomCorner.X && p.X <= Box.BoxArea.UpperCorner.X &&
                p.Y >= Box.BoxArea.BottomCorner.Y && p.Y <= Box.BoxArea.UpperCorner.Y &&
                p.Z >= Box.BoxArea.BottomCorner.Z && p.Z <= Box.BoxArea.UpperCorner.Z;
        }
    }

    public class IntersectedMesh : IIntersectedPointObject {
        public IList<IIntersectedPointObject> Objects { get; set; }
        public Vec3f Location { get; set; }

        public float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = -1;
            surface = null;

            for (int i = 0; i < Objects.Count; i++) {
                ISceneObject thisObject = Objects[i];

                float value = thisObject.Intersect(ray, out Func<Vec3f, SurfaceInfo> thisSurface);
                if (value >= 0 && (t < 0 || value < t)) {
                    t = value;
                    surface = thisSurface;
                }
            }

            return t;
        }

        public bool PointIsInsideObject(Vec3f point) {
            for (int i = 0; i < Objects.Count; i++) {
                if (Objects[i].PointIsInsideObject(point)) return true;
            }

            return false;
        }
    }
}
