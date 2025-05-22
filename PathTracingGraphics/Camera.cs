using System;
using System.Threading.Tasks;
using Graphics;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class Camera : ICamera {
        public float Rotation { get; set; } = 0;
        public Vec3f LookingAt { get; set; } = new Vec3f(0, 0, 1).Normalize();
        public Vec3f Location { get; set; } = new Vec3f(0, 0, 0);

        public float FocalLength { get; set; } = 1F;

        private Rotation _horizontalRotation;
        private Rotation _verticalRotation;
        private Rotation _rotationRadians;

        public Ray[,] ShootRays(int width, int height) {
            Vec2f xz = new Vec2f(LookingAt.Z, LookingAt.X);

            _horizontalRotation = new Rotation(xz.GetAngle());
            _verticalRotation = new Rotation(new Vec2f(xz.GetMagnitude(), LookingAt.Y).GetAngle());
            _rotationRadians = new Rotation(-Rotation);

            Ray[,] rays = new Ray[width, height];

            float planeHeight = height / (float)width;

            float pixelWidth = 1F / width;
            float pixelHeight = planeHeight / height;

            Parallel.For(0, height, yPx => {
                for (int xPx = 0; xPx < width; xPx++) {
                    float x = 0.5F - pixelWidth * xPx + pixelWidth / 2;
                    float y = planeHeight / 2 - pixelHeight * yPx + pixelHeight / 2;
                    float z = FocalLength;

                    rays[xPx, yPx] = new Ray(Location, Rotate(new Vec3f(x, y, z)).Normalize());
                }
            });

            return rays;
        }

        private Vec3f Rotate(Vec3f point) {
            Vec2f rotated = new Vec2f(point.X, point.Y).Rotate(_rotationRadians);

            Vec2f zy = new Vec2f(point.Z, rotated.Y).Rotate(_verticalRotation);
            Vec2f xz = new Vec2f(zy.X, rotated.X).Rotate(_horizontalRotation);

            return new Vec3f(xz.Y, zy.Y, xz.X);
        }
    }
}
