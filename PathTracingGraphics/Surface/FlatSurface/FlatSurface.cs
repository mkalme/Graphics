using System;
using Vectors.Vec2;
using Vectors.Vec3;
using Graphics;

namespace PathTracingGraphics {
    public abstract class FlatSurface : ISurface {
        public virtual IFlatSurfaceTranslator FlatSurfaceTranslator { get; set; }
        public virtual SurfaceProperties DefaultProperties { get; set; }

        public FlatSurface(IFlatSurfaceTranslator flatSurfaceTranslator) {
            FlatSurfaceTranslator = flatSurfaceTranslator;
        }

        public virtual SurfaceProperties GetSurface(Vec3f point) {
            return GetSurfaceInfoFrom2dPoint(FlatSurfaceTranslator.Get2dPoint(point));
        }
        protected abstract SurfaceProperties GetSurfaceInfoFrom2dPoint(Vec2f point);
    }
}
