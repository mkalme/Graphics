using System;
using System.Drawing;
using Graphics;
using Vectors.Vec2;
using Vectors.Vec3;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class SphereTextureUvSurface : SphereUvSurface {
        public LockedBitmap Image { get; set; } = new LockedBitmap(new Bitmap("D:\\earth.jpg"));

        public float Offset { get; set; } = 0.5F;

        public SphereTextureUvSurface(Sphere sphere) : base(sphere) {}

        public override SurfaceProperties GetSurfaceFromUvPoint(Vec2f uv) {
            float u = (uv.X + Offset) % 1;

            Vec3f output = Image.GetPixel((int)(u * Image.Width), (int)(uv.Y * Image.Height)).ToVector();
            float blue = output.Z / (output.X + output.Y + output.Z);

            return new SurfaceProperties(output, blue > 0.4F ? 0.5F * 0.8F : 0, blue > 0.4F ? 1 : 0);
        }
    }
}
