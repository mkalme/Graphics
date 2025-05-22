using System;
using System.Drawing;
using Graphics;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class PanoramicSky : ImageSky {
        public float Offset { get; set; } = 0;

        public PanoramicSky() : base(new LockedBitmap(new Bitmap("D:\\PanoramicImage.jpg"))) {}

        public override Vec3f GetColor(Vec3f direction) {
            float u = 0.5F + (float)Math.Atan2(direction.X, direction.Z) / (float)(2 * Math.PI);
            float v = 0.5F - (float)Math.Asin(direction.Y) / (float)Math.PI;

            Vec2f location = new Vec2f(u + Offset % 1, v);

            return Image.GetPixel(
                (int)(location.X * Image.Width),
                (int)(location.Y * Image.Height)).ToVector();
        }
    }
}
