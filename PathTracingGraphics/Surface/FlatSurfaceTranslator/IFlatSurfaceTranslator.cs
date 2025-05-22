using System;
using Vectors.Vec3;
using Vectors.Vec2;

namespace PathTracingGraphics {
    public interface IFlatSurfaceTranslator {
        public abstract Vec2f Get2dPoint(Vec3f point);
    }
}
