using System;
using Graphics;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class SimpleSky : ISky {
        public Vec3f SkyColor { get; set; }

        public SimpleSky(Vec3f skyColor) {
            SkyColor = skyColor;
        }

        public Vec3f GetColor(Vec3f direction) {
            return SkyColor;
        }
    }
}
