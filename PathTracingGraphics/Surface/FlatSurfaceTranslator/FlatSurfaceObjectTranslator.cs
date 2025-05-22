using System;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class FlatSurfaceObjectTranslator : FlatSurfaceTranslator {
        public IFlatSurfaceObject SurfaceObject { get; set; }
        private Vec3f _surfaceNormal;

        public FlatSurfaceObjectTranslator(IFlatSurfaceObject flatSurfaceObject) {
            SurfaceObject = flatSurfaceObject;

            SetRotations();
        }

        private void SetRotations() {
            _surfaceNormal = SurfaceObject.SurfaceNormal;

            double normalAngle = new Vec2f(-_surfaceNormal.Z, -_surfaceNormal.X).GetAngle();
            double normalYAngle = (float)Math.Atan2(-_surfaceNormal.Y, new Vec2f(_surfaceNormal.Z, _surfaceNormal.X).GetMagnitude());

            HorizontalRotation = new Rotation((float)(Math.PI / 2) - normalAngle);
            VerticalRotation = new Rotation(-normalYAngle);
        }

        public override Vec2f Get2dPoint(Vec3f point) {
            if (_surfaceNormal != SurfaceObject.SurfaceNormal) SetRotations();
            return base.Get2dPoint(point);
        }
    }
}
