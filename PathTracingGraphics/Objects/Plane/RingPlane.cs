using System;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class RingPlane : PlaneShape {
        public float Radius { get; set; }
        public float Interval { get; set; }

        public RingPlane(Vec3f surfaceNormal) : base(surfaceNormal) { 
        
        }

        public override bool Intersects(float distance) {
            if (distance > Radius) return false;

            float d = (Radius - distance) / Interval;

            return d % 2 < 1;
        }
    }
}
