using System;
using Graphics;
using Vectors.Vec3;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class FogSky : ISky {
        public ISky Sky { get; set; }
        public Vec3f FogColor { get; set; }

        public FogSky(ISky sky, Vec3f fogColor) {
            Sky = sky;
            FogColor = fogColor;
        }

        public Vec3f GetColor(Vec3f direction) {
            return Sky.GetColor(direction).Mix(FogColor, 1 - (Math.Abs(direction.Y) * 15).Clamp(0, 1));
        }
    }
}
