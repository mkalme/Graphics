using System;

namespace Vectors.Extensions {
    public readonly struct Rotation {
        public double Cosine { get; }
        public double Sine { get; }

        public Rotation(double radians) {
            Cosine = Math.Cos(radians);
            Sine = Math.Sin(radians);
        }
    }
}
