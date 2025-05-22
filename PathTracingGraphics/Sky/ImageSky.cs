using System;
using Graphics;
using Vectors.Vec3;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public abstract class ImageSky : ISky {
        public LockedBitmap Image { get; set; }

        public float AverageBrightness { get; private set; }
        public Vec3f AverageColor { get; private set; }

        public ImageSky(LockedBitmap image) {
            Image = image;

            float brightness = 0;
            Vec3f averageColor = 0;

            for (int y = 0; y < Image.Height; y++) {
                brightness += Image.GetPixel(0, y).GetBrightness();
                averageColor += Image.GetPixel(0, y).ToVector();
            }

            AverageBrightness = brightness / Image.Height;
            AverageColor = averageColor / Image.Height;
        }

        public abstract Vec3f GetColor(Vec3f direction);
    }
}
