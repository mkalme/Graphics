using System;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class Disk : PlaneShape {
        public float Radius { get; set; }

        public Disk(Vec3f surfaceNormal) : base(surfaceNormal) {

        }

        public override bool Intersects(float distance) {
            return distance <= Radius;
        }
    }
}
