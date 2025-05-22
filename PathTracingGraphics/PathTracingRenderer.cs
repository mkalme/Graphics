using System;
using System.Drawing;
using Vectors.Extensions;
using Vectors.Vec3;
using Graphics;
using System.Collections.Generic;

namespace PathTracingGraphics {
    public class PathTracingRenderer : Renderer {
        public ISky Sky { get; set; }

        public Vec3f AmbientLight { get; set; }
        public float MinLumens { get; set; } = 600;
        public int MaxDepth { get; set; } = 10;
        public int SpecularPow { get; set; } = 100;

        public MeshObject Mesh { get; private set; }
        public Vec3f FogColor { get; set; }

        public PathTracingRenderer() {
            Scene = new Scene();
            //Sky = new SimpleSky(0);

            Sky = new PanoramicSky();
            AmbientLight = ((ImageSky)Sky).AverageBrightness;
            FogColor = ColorTranslator.FromHtml("#818FA5").ToVector();

            Sky = new FogSky(Sky, FogColor);

            LoadScene7();

            Mesh = new MeshObject() { 
                Objects = Scene.Objects
            };
        }

        private void LoadScene1() {
            Scene.Lights.Add(new PointLight(new Vec3f(5, 1, 0), 9000) {
                LightColor = Color.Red.ToVector().Mix(Color.White.ToVector(), 0.5F)
            });
            Scene.Lights.Add(new PointLight(new Vec3f(0, 1, -5), 9000) {
                LightColor = Color.Green.ToVector().Mix(Color.White.ToVector(), 0.5F)
            });
            Scene.Lights.Add(new PointLight(new Vec3f(-5, 1, 0), 9000) {
                LightColor = Color.Blue.ToVector().Mix(Color.White.ToVector(), 0.5F)
            });

            Scene.Lights.Add(new PointLight(new Vec3f(-1, 2, 8.5F), 5000));
            Scene.Lights.Add(new PointLight(new Vec3f(0, -0.25F, 8F), 2000));

            Scene.Objects.Add(new Plane(new Vec3f(0, 1, 0).Normalize()) {
                Location = new Vec3f(0, -1, 0),
            });
            (Scene.Objects[0] as SceneObject).Surface.DefaultProperties = new SurfaceProperties(0, 0.4F);

            Scene.Objects.Add(new Sphere() {
                Location = new Vec3f(2.5F, 0, 9),
                Radius = 1,
            });
            Scene.Objects.Add(new Sphere() {
                Location = new Vec3f(0, 0, 5),
                Radius = 1,
            });
            Scene.Objects.Add(new Sphere() {
                Location = new Vec3f(-2.5F, 0, 9),
                Radius = 1,
            });

            //var colorizer0 = new PatternSphereUvSurface(Scene.Objects[1] as Sphere, 4, Color.Red, Color.DarkRed) {
            //    DefaultProperties = new SurfaceProperties(0, 0.5F)
            //};
            //(Scene.Objects[1] as Sphere).Surface = colorizer0;

            //var colorizer1 = new PatternSphereUvSurface(Scene.Objects[2] as Sphere, 4, Color.Green, Color.LightGreen) {
            //    DefaultProperties = new SurfaceProperties(0, 0.5F)
            //};
            //(Scene.Objects[2] as Sphere).Surface = colorizer1;

            //var colorizer2 = new PatternSphereUvSurface(Scene.Objects[3] as Sphere, 4, Color.Blue, Color.LightBlue) {
            //    DefaultProperties = new SurfaceProperties(0, 0.5F)
            //};
            //(Scene.Objects[3] as Sphere).Surface = colorizer2;
        }
        private void LoadScene2() {
            Scene.Lights.Add(new PointLight(new Vec3f(-1, 1.5F, 0), 10000));
            Scene.Lights.Add(new PointLight(new Vec3f(-1, 1.5F, 10), 10000));

            Scene.Objects.Add(new HorizontalRectangle() {
                Location = new Vec3f(-30, -0.5F, -30),
                XLength = 60,
                ZLength = 60,
            });
            Scene.Objects.Add(new HorizontalRectangle() {
                Location = new Vec3f(-30, 2.5F, -30),
                XLength = 60,
                ZLength = 60,
            });

            (Scene.Objects[0] as SceneObject).Surface.DefaultProperties = new SurfaceProperties(0, 0.4F);
            (Scene.Objects[1] as SceneObject).Surface.DefaultProperties = new SurfaceProperties(0, 0.7F);

            MeshObject meshObject = new MeshObject();

            meshObject.Objects.Add(new Sphere() {
                Location = new Vec3f(1.05F, 0, 4),
                Radius = 0.5F,
                Surface = new SimpleSurface(new SurfaceProperties(Color.Blue.ToVector(), 0.5F))
            });
            meshObject.Objects.Add(new Sphere() {
                Location = new Vec3f(0, 0, 4),
                Radius = 0.5F,
                Surface = new SimpleSurface(new SurfaceProperties(Color.Red.ToVector(), 0.5F))
            });
            meshObject.Objects.Add(new Sphere() {
                Location = new Vec3f(-1.05F, 0, 4),
                Radius = 0.5F,
                Surface = new SimpleSurface(new SurfaceProperties(Color.Green.ToVector(), 0.5F))
            });

            RotatedObject rotated = new RotatedObject(meshObject);

            rotated.SetCenter(new Vec3f(0, 0, 4));
            rotated.SetDegrees(45);

            Scene.Objects.Add(new OffsetObject(rotated, new Vec3f(0, 1, 0)));
        }
        private void LoadScene3() {
            Scene.Lights.Add(new PointLight(new Vec3f(-5, 10, -5), 100000));

            Scene.Objects.Add(new Plane(new Vec3f(0, 1, 0).Normalize()) {
                Location = new Vec3f(0, -1, 0),
            });
            (Scene.Objects[0] as SceneObject).Surface.DefaultProperties = new SurfaceProperties(0, 0.5F);

            //Scene.Objects.Add(new Tube() { 
            //    Location = new Vec3f(0, -1, 8),
            //    Shape = new Circle() { 
            //        Radius = 0.5F
            //    },
            //    Height = 200
            //});

            Scene.Objects.Add(new Sphere() {
                Location = new Vec3f(0, 0, -8),
                Radius = 1
            });

            //RotatedObject rotated = new RotatedObject(new Box() {
            //    Location = new Vec3f(1, -1, 8),
            //    Size = new Vec3f(2, 2, 2)
            //});

            //rotated.SetCenter(new Vec3f(0, 0, 9));
            //rotated.SetDegrees(45);

            //Scene.Objects.Add(new OffsetObject(rotated, new Vec3f(0, 0.5F, 0)));
        }
        private void LoadScene4() {
            Scene.Lights.Add(new PointLight(new Vec3f(-5, 10, -5), 100000));

            Scene.Objects.Add(new Plane(new Vec3f(0, 1, 0).Normalize()) {
                Location = new Vec3f(0, -1, 0),
            });
            (Scene.Objects[0] as SceneObject).Surface.DefaultProperties = new SurfaceProperties(0, 0.5F);

            for (int y = 0; y < 3; y++) {
                MeshObject mesh = new MeshObject();

                int xn = 9, zn = 9;
                for (int x = 0; x < xn; x++) {
                    for (int z = 0; z < zn; z++) {
                        Vec3f point = new Vec3f(-x * 3, -1, z * 3);
                    }
                }

                RotatedObject rotated = new RotatedObject(mesh);

                rotated.SetCenter(new Vec3f(-(xn / 2 * 2.5F + 1), 0, zn / 2 * 2.5F + 1));
                rotated.SetDegrees(-33.5F);

                Scene.Objects.Add(new OffsetObject(rotated, new Vec3f(0, 0.5F + y * 2.5F, 0)));
            }
        }
        private void LoadScene5() {
            Scene.Lights.Add(new PointLight(new Vec3f(-5, 10, -5), 100000));

            Scene.Objects.Add(new Plane(new Vec3f(0, 1, 0).Normalize()) {
                Location = new Vec3f(0, -1, 0),
            });
            (Scene.Objects[0] as SceneObject).Surface.DefaultProperties = new SurfaceProperties(0, 0.5F);

            //Scene.Objects.Add(new Tube() { 
            //    Location = new Vec3f(0, -1, 8),
            //    Shape = new Circle() { 
            //        Radius = 0.5F
            //    },
            //    Height = 200
            //});

            //Scene.Objects.Add(new OffsetObject(new SquishedObject(new Sphere() {
            //    Location = new Vec3f(0, 0, 8),
            //    Radius = 1
            //}), new Vec3f(0, 1, 0)));

            Rectangle rec1 = new Rectangle(new Vec3f(-1, 0, 0)) { 
                Location = new Vec3f(0, -1, 7),
                Width = 0.5F,
                Height = 2
            };

            Rectangle rec2 = new Rectangle(new Vec3f(-1, 0, 0)) {
                Location = new Vec3f(-0.5F, -1, 7.5F),
                Width = 1,
                Height = 2
            };

            Rectangle rec3 = new Rectangle(new Vec3f(-1, 0, 0)) {
                Location = new Vec3f(0, -1, 8.5F),
                Width = 0.5F,
                Height = 2
            };

            Rectangle rec4 = new Rectangle(new Vec3f(0, 0, -1)) {
                Location = new Vec3f(0, -1, 7.5F),
                Width = 0.5F,
                Height = 2
            };

            Rectangle rec5 = new Rectangle(new Vec3f(0, 0, -1)) {
                Location = new Vec3f(0, -1, 8.5F),
                Width = 0.5F,
                Height = 2
            };

            MeshObject mesh = new MeshObject() { 
                Objects = new List<ISceneObject>() { rec1, rec2, rec3, rec4, rec5 }
            };

            Scene.Objects.Add(new SquishedObject(mesh));

            //RotatedObject rotated = new RotatedObject(new BoxNew(new Vec3f(1, -1, 8), new Vec3f(-1, 1, 10)));

            //rotated.SetCenter(new Vec3f(0, 0, 9));
            //rotated.SetDegrees(45);

            //Scene.Objects.Add(new OffsetObject(rotated, new Vec3f(0, 0.5F, 0)));
        }
        private void LoadScene6() {
            Scene.Lights.Add(new PointLight(new Vec3f(-5, 10, -5), 100000));

            Scene.Objects.Add(new Plane(new Vec3f(0, 1, 0).Normalize()) {
                Location = new Vec3f(0, -1, 0),
            });
            (Scene.Objects[0] as SceneObject).Surface.DefaultProperties = new SurfaceProperties(0, 0.5F);

            IntersectedSphere sphere1 = new IntersectedSphere(new Sphere() { 
                Location = new Vec3f(0.5F, 0, 8),
                Radius = 1
            });
            IntersectedBox sphere2 = new IntersectedBox(new Box(new Vec3f(-1, -1, 7), new Vec3f(1, 1, 8)));

            IntersectedObject obj = new IntersectedObject() { 
                Objects = new List<IIntersectedPointObject>() { sphere1, sphere2 }
            };

            RotatedObject rotated = new RotatedObject(obj);
            rotated.SetCenter(new Vec3f(0, 0, 8));
            rotated.SetDegrees(150);

            Scene.Objects.Add(rotated);
        }
        private void LoadScene7() {
            Scene.Lights.Add(new PointLight(new Vec3f(-5, 10, -5), 100000));

            Scene.Objects.Add(new Plane(new Vec3f(0, 1, 0).Normalize()) {
                Location = new Vec3f(0, -1, 0),
            });
            (Scene.Objects[0] as SceneObject).Surface.DefaultProperties = new SurfaceProperties(0, 0.5F);

            Box box1 = new Box(new Vec3f(2, -1, 7), new Vec3f(-2, 0, 8));
            Box box2 = new Box(new Vec3f(2, -1, 10), new Vec3f(-2, 0, 11));

            Box box3 = new Box(new Vec3f(1, -1.1F, 6), new Vec3f(-1, 0.1F, 12));

            IntersectedMesh mesh = new IntersectedMesh() {
                Objects = new List<IIntersectedPointObject>() { new IntersectedBox(box1), new IntersectedBox(box2) }
            };

            SubtractedObject obj = new SubtractedObject(mesh, new IntersectedBox(box3));

            Box box4 = new Box(new Vec3f(2.5F, -0.5F, 6.5F), new Vec3f(-2.5F, 0.5F, 13));

            IntersectedObject obj2 = new IntersectedObject();
            obj2.Objects.Add(obj);
            obj2.Objects.Add(new IntersectedBox(box4));

            RotatedObject rotated = new RotatedObject(obj2);
            rotated.SetCenter(new Vec3f(0, 0, 9));
            rotated.SetDegrees(45);

            Scene.Objects.Add(rotated);
        }

        protected override Vec3f CastPrimaryRay(Ray ray) {
            return CastRay(ray, 1);
        }

        private Vec3f CastRay(Ray ray, int depth) {
            float t = Mesh.Intersect(ray, out Func<Vec3f, SurfaceInfo> surface);
            if (t < 0) return Sky.GetColor(ray.Direction);

            Vec3f intersectionPoint = ray.Origin + ray.Direction * (t * 0.9999F);
            SurfaceInfo surfaceInfo = surface(intersectionPoint);

            (Vec3f lightColor, Vec3f specularColor) = GetLightColor(ray.Direction, intersectionPoint, surfaceInfo);
            Vec3f output = surfaceInfo.SurfaceProperties.Color * lightColor;

            if (depth < MaxDepth && surfaceInfo.SurfaceProperties.ReflectionIndex > 0) {
                Ray reflectionRay = new Ray(intersectionPoint, ray.Direction.Reflect(surfaceInfo.SurfaceNormal));

                output = output.Mix(CastRay(reflectionRay, depth + 1), surfaceInfo.SurfaceProperties.ReflectionIndex);
            }

            if (t > 20) {
                float intensity = (float)Math.Sqrt(t - 20) * 6;

                output = output.Mix(FogColor, (intensity / 100).Clamp(0, 1));
            }

            return output + specularColor;
        }
        private (Vec3f, Vec3f) GetLightColor(Vec3f originalDirection, Vec3f intersectionPoint, SurfaceInfo surfaceInfo) {
            Vec3f lightColor = AmbientLight, specularColor = 0;

            for (int i = 0; i < Scene.Lights.Count; i++) {
                ILight light = Scene.Lights[i];

                Vec3f distanceVec = light.Location - intersectionPoint;
                float distance = distanceVec.GetMagnitude();

                Ray ray = new Ray(intersectionPoint, distanceVec / distance);
                if (Intersects(ray, distance)) continue;

                Vec3f lightVector = light.GetDirection(intersectionPoint);
                float angle = Math.Abs(surfaceInfo.SurfaceNormal.Dot(lightVector));

                float lumenIntensity = light.Lumens / (distance * distance) / MinLumens;
                float lightIntensity = lumenIntensity.Clamp(0, 1) * angle;

                float specular = GetSpecularHighlight(originalDirection, ray.Direction.Reflect(surfaceInfo.SurfaceNormal)) * lightIntensity * surfaceInfo.SurfaceProperties.Ks;

                lightColor += light.LightColor * lightIntensity;
                specularColor += light.LightColor * specular;
            }

            return (lightColor.Clamp(0, 1), specularColor);
        }
        private float GetSpecularHighlight(Vec3f v, Vec3f r) {
            float dot = v.Dot(r);
            if (dot < 0) return 0;

            return ((float)Math.Pow(dot, SpecularPow)).Clamp(0, 1);
        }

        private bool Intersects(Ray ray, float maxDistance = float.MaxValue) {
            for (int i = 0; i < Scene.Objects.Count; i++) {
                ISceneObject thisObject = Scene.Objects[i];

                float t = thisObject.Intersect(ray, out Func<Vec3f, SurfaceInfo> surface);
                if (t >= 0 && t <= maxDistance) return true;
            }

            return false;
        }
    }
}
