using System;
using Vectors.Vec3;

namespace Graphics {
    public interface ISceneObject {
        Vec3f Location { get; set; }

        float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface);
    }
}
