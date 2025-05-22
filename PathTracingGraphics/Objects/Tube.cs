using System;
using System.Drawing;
using System.Collections.Generic;
using Graphics;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class Tube : SceneObject {
        public override Vec3f Location {
            get => base.Location;
            set {
                base.Location = value;
                if (Shape != null) {
                    Shape.Location = new Vec2f(value.X, value.Z);
                }
            }
        }

        public float Height { get; set; }

        public ISceneObject UpperTip { get; set; }
        public ISceneObject LowerTip { get; set; }

        public I2dObject Shape {
            get => _shape;
            set {
                _shape = value;
                _shape.Location = new Vec2f(Location.X, Location.Z);
            }
        }
        private I2dObject _shape;

        public Tube() {
            Surface = new SimpleSurface(new SurfaceProperties(Color.Orange.ToVector(), 0.5F));

            //UpperTip = new Sphere() {
            //    Location = new Vec3f(0, 1, 8),
            //    Radius = 0.5F,
            //    Surface = Surface
            //};

            LowerTip = new Sphere() {
                Location = new Vec3f(0, -1, 8),
                Radius = 0.5F,
                Surface = Surface
            };

            UpperTip = new Disk(new Vec3f(0, 199, 0)) {
                Location = new Vec3f(0, 1, 8),
                Radius = 0.5F,
                Surface = Surface
            };
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            Vec2f o = new Vec2f(ray.Origin.X, ray.Origin.Z);
            Vec2f p2 = new Vec2f(ray.Direction.X, ray.Direction.Z);

            float magnitude = p2.GetMagnitude();
            Vec2f direction = p2 / magnitude;

            float t = _shape.Intersect(o, direction, out Func<Vec2f, Vec2f> normal);

            if (t < 0) {
                surface = null;
                return -1;
            } else {
                Vec3f point = ray.Origin + ray.Direction * (t / magnitude);

                if (point.Y - Location.Y > Height) {
                    return UpperTip.Intersect(ray, out surface);
                } else if (point.Y - Location.Y <= 0) {
                    return LowerTip.Intersect(ray, out surface);
                }

                surface = x => GetSurfaceInfo(x, normal);
                return t / magnitude;
            }
        }
        public SurfaceInfo GetSurfaceInfo(Vec3f point, Func<Vec2f, Vec2f> normalFunction) {
            Vec2f normal = normalFunction(new Vec2f(point.X, point.Z));

            return new SurfaceInfo(new Vec3f(normal.X, 0, normal.Y).Normalize(), Surface.GetSurface(point));
        }
    }

    public interface I2dObject {
        Vec2f Location { get; set; }

        float Intersect(Vec2f origin, Vec2f direction, out Func<Vec2f, Vec2f> normal);
    }

    public class Circle : I2dObject {
        public Vec2f Location { get; set; }
        public float Radius { get; set; }

        public float Intersect(Vec2f origin, Vec2f direction, out Func<Vec2f, Vec2f> normal) {
            Vec2f f = origin - Location;

            float b = 2 * direction.Dot(f);
            float c = f.Dot(f) - Radius * Radius;
            float d = b * b - 4 * c;

            if (d > 0) {
                float sqrt = (float)Math.Sqrt(d);
                float x1 = (-b - sqrt) / 2;

                if (x1 >= 0) {
                    float x2 = (-b + sqrt) / 2;
                    if (x2 >= 0) {
                        normal = GetNormal;
                        return x1;
                    }
                } else {
                    float x2 = (-b + sqrt) / 2;
                    if (x2 >= 0) {
                        normal = GetNormal;
                        return x2;
                    }
                }
            }

            normal = null;
            return -1;
        }
        public Vec2f GetNormal(Vec2f point) {
            return (point - Location).Normalize();
        }
    }

    public class Object2dMesh : I2dObject { 
        public IList<I2dObject> Mesh { get; set; }
        public Vec2f Location { get; set; }

        public float Intersect(Vec2f origin, Vec2f direction, out Func<Vec2f, Vec2f> normal) {
            float t = -1;
            normal = null;

            for (int i = 0; i < Mesh.Count; i++) {
                float value = Mesh[i].Intersect(origin, direction, out Func<Vec2f, Vec2f> normalFunction);

                if (value >= 0 && (t < 0 || value < t)) {
                    t = value;
                    normal = normalFunction;
                }
            }

            return t;
        }
    }

    public class Line : I2dObject {
        public Vec2f Location {
            get => _location;
            set {
                _location = value;
                _normal = GetNormal();
            }
        }
        private Vec2f _location;

        public Vec2f SecondPoint {
            get => _secondPoint;
            set {
                _secondPoint = value;
                _normal = GetNormal();
            }
        }
        private Vec2f _secondPoint;

        private Vec2f _normal;

        public float Intersect(Vec2f origin, Vec2f direction, out Func<Vec2f, Vec2f> normal) {
            Vec2f v1 = origin - Location;
            Vec2f v2 = SecondPoint - Location;
            Vec2f v3 = new Vec2f(-direction.Y, direction.X);

            float dot = v2.Dot(v3);
            if (Math.Abs(dot) <= 0) {
                normal = null;
                return -1;
            }

            float t1 = (v2.X * v1.Y - v2.Y * v1.X) / dot;
            float t2 = v1.Dot(v3) / dot;

            if (t1 >= 0 && t2 >= 0 && t2 <= 1) {
                normal = x => _normal;
                return t1;
            }

            normal = null;
            return -1;
        }
        public Vec2f GetNormal() {
            return (SecondPoint - Location).Rotate((float)Math.PI / 2).Normalize();
        }
    }
}
