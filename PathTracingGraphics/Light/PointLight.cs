using System;
using System.Drawing;
using Graphics;
using Vectors.Vec3;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class PointLight : ILight {
        public Vec3f Location { get; set; }
        public Vec3f LightColor { get; set; } = Color.White.ToVector();
        public float Lumens { get; set; }

        public PointLight(Vec3f location, float lumens) {
            Location = location;
            Lumens = lumens;
        }

        public Vec3f GetDirection(Vec3f point) {
            return (Location - point).Normalize();
        }
    }
}
