using System;

namespace PathTracingGraphics {
    static class MathExtensions {
        public static float Clamp(this float value, float min, float max) {
            return value < min ? min : value > max ? max : value;
        }

        public static float Mod(this float x, float m) {
            return (x % m + m) % m;
        }
        public static int Mod(this int x, int m) {
            return (x % m + m) % m;
        }
    }
}
