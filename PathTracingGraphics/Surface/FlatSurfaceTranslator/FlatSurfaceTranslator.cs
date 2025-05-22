using System;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class FlatSurfaceTranslator : IFlatSurfaceTranslator {
        public Rotation HorizontalRotation { get; set; }
        public Rotation VerticalRotation { get; set; }

        public virtual Vec2f Get2dPoint(Vec3f point) {
            Vec2f xz = new Vec2f(point.Z, point.X).Rotate(HorizontalRotation);
            Vec2f xy = new Vec2f(xz.Y, point.Y).Rotate(VerticalRotation);

            return new Vec2f(xz.X, xy.Y);
        }
    }
}
