using System;
using Vectors.Vec3;

namespace Graphics {
    public interface ILight {
        Vec3f Location { get; set; }
        Vec3f LightColor { get; set; }
        float Lumens { get; set; }

        Vec3f GetDirection(Vec3f point);
    }
}
