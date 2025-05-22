using System;
using Vectors.Vec2;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class CustomFlatSurfaceTranslator : IFlatSurfaceTranslator {
        public IFlatSurfaceTranslator FlatSurfaceTranslator { get; set; }
        public virtual Func<Vec2f, Vec2f> PointSurface { get; set; }

        public CustomFlatSurfaceTranslator(IFlatSurfaceTranslator flatSurfaceTranslator, Func<Vec2f, Vec2f> pointSurface) {
            FlatSurfaceTranslator = flatSurfaceTranslator;
            PointSurface = pointSurface;
        }

        public Vec2f Get2dPoint(Vec3f point) {
            return PointSurface(FlatSurfaceTranslator.Get2dPoint(point));
        }
    }
}
