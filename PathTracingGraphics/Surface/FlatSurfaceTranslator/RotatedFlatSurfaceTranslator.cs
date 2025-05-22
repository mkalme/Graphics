using System;
using Vectors.Vec2;
using Vectors.Vec3;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class RotatedFlatSurfaceTranslator : IFlatSurfaceTranslator {
        public IFlatSurfaceTranslator FlatSurfaceTranslator { get; set; }

        public float Radians {
            get => _radians;
            set {
                _radians = value;
                _rotation = new Rotation(value);
            }
        }
        
        private float _radians;
        private Rotation _rotation;

        public RotatedFlatSurfaceTranslator(IFlatSurfaceTranslator flatSurfaceTranslator, float radians) {
            FlatSurfaceTranslator = flatSurfaceTranslator;
            Radians = radians;
        }

        public Vec2f Get2dPoint(Vec3f point) {
            Vec2f output = FlatSurfaceTranslator.Get2dPoint(point);

            return Radians != 0 ? output.Rotate(_rotation) : output;
        }
    }
}
