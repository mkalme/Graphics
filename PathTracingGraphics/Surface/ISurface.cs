using System;
using Graphics;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public interface ISurface {
        SurfaceProperties DefaultProperties { get; set; }

        SurfaceProperties GetSurface(Vec3f point);
    }
}
