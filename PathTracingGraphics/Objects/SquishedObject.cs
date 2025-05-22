using System;
using Vectors.Vec3;
using Vectors.Vec2;
using Graphics;

namespace PathTracingGraphics {
    public class SquishedObject : SceneObject {
        public BoxArea PrimaryArea { get; set; }
        public BoxArea ExpandedArea { get; set; }

        public SceneObject Object { get; set; }

        public SquishedObject(SceneObject sceneObject) {
            Object = sceneObject;

            PrimaryArea = new BoxArea(new Vec3f(0, -1, 7), new Vec3f(-0.5F, 1, 9));
            ExpandedArea = new BoxArea(new Vec3f(0.5F, -1, 7), new Vec3f(-0.5F, 1, 9));
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = ExpandedArea.Intersect(ray, out Vec3f intersectionPoint);
            if (t >= 0) {
                float ot = Object.Intersect(ray, out surface);
                if (ot >= 0) {
                    float primaryWidth = PrimaryArea.UpperCorner.X - PrimaryArea.BottomCorner.X;
                    float expandedWidth = ExpandedArea.UpperCorner.X - ExpandedArea.BottomCorner.X;

                    Vec3f point = ray.Origin + ray.Direction * ot;
                    float relX = point.X - PrimaryArea.BottomCorner.X;
                    relX *= (expandedWidth / primaryWidth) - 1;

                    //float x = PrimaryArea.UpperCorner.X + (intersectionPoint.X - ExpandedArea.UpperCorner.X) * (primaryWidth / expandedWidth);
                    //float relX = intersectionPoint.X - x;

                    Vec3f origin = new Vec3f(ray.Origin.X - relX, ray.Origin.Y, ray.Origin.Z);
                    return Object.Intersect(new Ray(origin, ray.Direction), out surface);
                }
            }

            surface = null;
            return -1;
        }
    }

    public class BoxArea { 
        public Vec3f BottomCorner { get; }
        public Vec3f UpperCorner { get; }

        public RectangleArea[] Areas { get; }

        public BoxArea(Vec3f firstPoint, Vec3f secondPoint) {
            BottomCorner = new Vec3f(Math.Min(firstPoint.X, secondPoint.X), Math.Min(firstPoint.Y, secondPoint.Y), Math.Min(firstPoint.Z, secondPoint.Z));
            UpperCorner = new Vec3f(Math.Max(firstPoint.X, secondPoint.X), Math.Max(firstPoint.Y, secondPoint.Y), Math.Max(firstPoint.Z, secondPoint.Z));

            Areas = new RectangleArea[6];

            //zAxis
            Areas[0] = new RectangleArea(BottomCorner, new Vec3f(1, 0, 0), new Vec2f(UpperCorner.Z - BottomCorner.Z, UpperCorner.Y - BottomCorner.Y));
            Areas[1] = new RectangleArea(new Vec3f(UpperCorner.X, BottomCorner.Y, BottomCorner.Z), new Vec3f(-1, 0, 0), new Vec2f(UpperCorner.Z - BottomCorner.Z, UpperCorner.Y - BottomCorner.Y));

            //xAxis
            Areas[2] = new RectangleArea(BottomCorner, new Vec3f(0, 0, -1), new Vec2f(UpperCorner.X - BottomCorner.X, UpperCorner.Y - BottomCorner.Y));
            Areas[3] = new RectangleArea(new Vec3f(BottomCorner.X, BottomCorner.Y, UpperCorner.Z), new Vec3f(0, 0, 1), new Vec2f(UpperCorner.X - BottomCorner.X, UpperCorner.Y- BottomCorner.Y));

            //yAxis
            Areas[4] = new RectangleArea(BottomCorner, new Vec3f(0, -1, 0), new Vec2f(UpperCorner.X - BottomCorner.X, UpperCorner.Z - BottomCorner.Z));
            Areas[5] = new RectangleArea(new Vec3f(BottomCorner.X, UpperCorner.Y, BottomCorner.Z), new Vec3f(0, 1, 0), new Vec2f(UpperCorner.X - BottomCorner.X, UpperCorner.Z - BottomCorner.Z));
        }

        public float Intersect(Ray ray, out Vec3f intersectionPoint) {
            float t = -1;
            intersectionPoint = new Vec3f();

            for (int i = 0; i < Areas.Length; i++) {
                RectangleArea area = Areas[i];

                float value = area.Intersect(ray, out Vec3f intersection, out Vec2f relativePoint);
                if (value >= 0 && (t < 0 || value < t)) {
                    t = value;
                    intersectionPoint = intersection;
                }
            }

            return t;
        }
    }

    public class RectangleArea : IFlatSurfaceObject, ISceneObject {
        public Vec3f Location { get; set; }
        public Vec3f SurfaceNormal { get; }
        
        public Vec2f Area { get; }
        
        public bool Vertical { get; }
        public bool ZAxisParallel { get; }

        public RectangleArea(Vec3f location, Vec3f surfaceNormal, Vec2f area) {
            Location = location;
            SurfaceNormal = surfaceNormal;
            Area = area;

            Vertical = surfaceNormal.Y == 0;
            ZAxisParallel = surfaceNormal.X == 1 || surfaceNormal.X == -1;
        }

        public float Intersect(Ray ray, out Vec3f intersectionPoint, out Vec2f relativePoint) {
            float d = SurfaceNormal.Dot(ray.Direction);
            intersectionPoint = new Vec3f();
            relativePoint = new Vec2f();

            if (d != 0) {
                float t = (Location - ray.Origin).Dot(SurfaceNormal) / d;

                intersectionPoint = ray.Origin + ray.Direction * t;
                Vec3f l = intersectionPoint - Location;

                if (!Vertical) {
                    relativePoint = new Vec2f(l.X, l.Z);
                } else if (ZAxisParallel) {
                    relativePoint = new Vec2f(l.Z, l.Y);
                } else {
                    relativePoint = new Vec2f(l.X, l.Y);
                }

                return relativePoint.X >= 0 && relativePoint.Y >= 0 && relativePoint.X <= Area.X && relativePoint.Y <= Area.Y ? t : -1;
            }

            return -1;
        }

        public float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = Intersect(ray, out Vec3f intersectionPoint, out Vec2f relativePoint);

            if (t >= 0) {
                surface = point => new SurfaceInfo(SurfaceNormal, new SurfaceProperties(0.5F, 0));
                return t;
            }

            surface = null;
            return -1;
        }
    }
}
