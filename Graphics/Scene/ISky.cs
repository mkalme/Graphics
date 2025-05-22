using System;
using Vectors.Vec3;

namespace Graphics {
    public interface ISky {
        Vec3f GetColor(Vec3f direction);
    }
}
