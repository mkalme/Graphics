using System;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public interface IFlatSurfaceObject {
        Vec3f SurfaceNormal { get; }
    }
}
