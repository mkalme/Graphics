using System;
using Vectors.Vec3;

namespace Graphics {
    public interface ICamera {
        float Rotation { get; set; }
        Vec3f LookingAt { get; set; }
        Vec3f Location { get; set; }

        Ray[,] ShootRays(int width, int height);
    }
}
