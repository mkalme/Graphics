using System;
using System.Collections.Generic;
using Vectors.Vec3;
using Graphics;

namespace PathTracingGraphics {
    public class MeshObject : SceneObject {
        public IList<ISceneObject> Objects { get; set; }

        public MeshObject() {
            Objects = new List<ISceneObject>();
        }

        public override float Intersect(Ray ray, out Func<Vec3f, SurfaceInfo> surface) {
            float t = -1;
            surface = null;

            for (int i = 0; i < Objects.Count; i++) {
                ISceneObject thisObject = Objects[i];

                float value = thisObject.Intersect(ray, out Func<Vec3f, SurfaceInfo> thisSurface);
                if (value >= 0 && (t < 0 || value < t)) {
                    t = value;
                    surface = thisSurface;
                }
            }

            return t;
        }
    }
}
