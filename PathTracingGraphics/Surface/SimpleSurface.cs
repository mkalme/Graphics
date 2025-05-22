using System;
using Graphics;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class SimpleSurface : ISurface {
        public SurfaceProperties DefaultProperties { get; set; } = new SurfaceProperties(0, 0, 1);

        public SimpleSurface(SurfaceProperties defaultProperties) {
            DefaultProperties = defaultProperties;
        }

        public SurfaceProperties GetSurface(Vec3f point) {
            return DefaultProperties;
        }
    }
}
