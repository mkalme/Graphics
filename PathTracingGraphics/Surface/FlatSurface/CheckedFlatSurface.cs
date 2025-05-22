using System;
using System.Drawing;
using Graphics;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class CheckedFlatSurface : FlatSurface {
        public float Interval { get; set; } = 0.25F;

        public Vec3f PrimaryColor { get; set; } = Color.DarkBlue.ToVector();
        public Vec3f SecondaryColor { get; set; } = Color.LightBlue.ToVector();

        public CheckedFlatSurface(IFlatSurfaceTranslator translator, float interval, Color primaryColor, Color secondaryColor) : base(translator) {
            Interval = interval;
            PrimaryColor = primaryColor.ToVector();
            SecondaryColor = secondaryColor.ToVector();
        }

        protected override SurfaceProperties GetSurfaceInfoFrom2dPoint(Vec2f point) {
            bool primary = point.X.Mod(Interval * 2) <= Interval ^ point.Y.Mod(Interval * 2) <= Interval;

            return new SurfaceProperties(primary ? PrimaryColor : SecondaryColor, DefaultProperties.ReflectionIndex, DefaultProperties.Ks);
        }
    }
}
