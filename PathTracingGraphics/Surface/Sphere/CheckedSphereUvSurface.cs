using System;
using System.Drawing;
using Graphics;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class CheckedSphereUvSurface : SphereUvSurface {
        public Vec3f PrimaryColor { get; set; }
        public Vec3f SecondaryColor { get; set; }

        public float Interval { get; set; }

        public CheckedSphereUvSurface(Sphere sphere, float interval, Color primaryColor, Color secondaryColor) : base(sphere){
            Interval = interval;
            PrimaryColor = primaryColor.ToVector();
            SecondaryColor = secondaryColor.ToVector();
        }

        public override SurfaceProperties GetSurfaceFromUvPoint(Vec2f uv) {
            float u = (uv.X + 0.5F) % 1 * 2;
            float interval = Interval / 90;

            Vec3f color = u % interval * 2 < interval ^ uv.Y % interval * 2 < interval ? PrimaryColor : SecondaryColor;

            return new SurfaceProperties(color, DefaultProperties.ReflectionIndex, DefaultProperties.Ks);
        }
    }
}
