using System;
using Vectors.Vec3;

namespace Graphics {
    public readonly struct SurfaceInfo {
        public Vec3f SurfaceNormal { get; }
        public SurfaceProperties SurfaceProperties { get; }

        public SurfaceInfo(Vec3f surfaceNormal, SurfaceProperties surfaceProperties) {
            SurfaceNormal = surfaceNormal;
            SurfaceProperties = surfaceProperties;
        }
    }
}
