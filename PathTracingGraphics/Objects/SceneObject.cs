using System;
using Graphics;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public abstract class SceneObject : ISceneObject {
        public virtual Vec3f Location { get; set; }
        public virtual ISurface Surface { get; set; }

        public abstract float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface);
    }
}
