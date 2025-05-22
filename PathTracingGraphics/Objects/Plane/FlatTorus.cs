using System;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class FlatTorus : PlaneShape {
        public float Radius { get; set; }
        public float InnerRadius { get; set; }

        public FlatTorus(Vec3f surfaceNormal) : base(surfaceNormal) { 
        
        }

        public override bool Intersects(float distance) {
            return distance >= InnerRadius && distance <= Radius;
        }
    }
}
