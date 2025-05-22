using System;
using Vectors.Vec3;
using Vectors.Vec2;

namespace PathTracingGraphics {
    public class StretchedFlatSurfaceTranslator : IFlatSurfaceTranslator {
        public IFlatSurfaceTranslator FlatSurfaceTranslator { get; set; }

        public float XScale { get; set; } = 1;
        public float YScale { get; set; } = 1;

        public StretchedFlatSurfaceTranslator(IFlatSurfaceTranslator flatSurfaceTranslator) {
            FlatSurfaceTranslator = flatSurfaceTranslator;
        }

        public Vec2f Get2dPoint(Vec3f point) {
            Vec2f output = FlatSurfaceTranslator.Get2dPoint(point);

            return new Vec2f(output.X * XScale, output.Y * YScale);
        }
    }
}
