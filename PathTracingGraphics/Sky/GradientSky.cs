using System;
using System.Drawing;
using Graphics;
using Vectors.Vec3;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class GradientSky : ImageSky {
        public GradientSky() : base(new LockedBitmap(new Bitmap("D:\\SkyColor.png"))) {}

        public override Vec3f GetColor(Vec3f direction) {
            return Image.GetPixel(0, (int)((Image.Height - 1) * (1 - direction.Y).Clamp(0, 1))).ToVector();
        }
    }
}
