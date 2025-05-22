using System;
using System.Drawing;
using Graphics;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class TexturedFlatSurface : FlatSurface {
        public LockedBitmap Image { get; set; } = new LockedBitmap(new Bitmap("D:\\Canyon_River_Tree_(165872763).jpg"));
        public float Scale { get; set; } = 4 / 2048F;

        public TexturedFlatSurface(IFlatSurfaceTranslator flatSurfaceTranslator) : base(flatSurfaceTranslator) {}

        protected override SurfaceProperties GetSurfaceInfoFrom2dPoint(Vec2f point) {
            int x = (int)(point.X / Scale);
            int y = (int)(point.Y / Scale);

            x = x.Mod(Image.Width - 1);
            y = y.Mod(Image.Height - 1);

            return new SurfaceProperties(Image.GetPixel(x, Image.Height - y - 1).ToVector(), DefaultProperties.ReflectionIndex, DefaultProperties.Ks);
        }
    }
}
