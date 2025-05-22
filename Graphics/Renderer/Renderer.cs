using System;
using System.Drawing;
using System.Threading.Tasks;
using Vectors.Vec3;
using Vectors.Extensions;

namespace Graphics {
    public abstract class Renderer : IRenderer {
        public virtual IScene Scene { get; set; }

        public virtual void Render(LockedBitmap output) {
            Ray[,] rays = Scene.Camera.ShootRays(output.Width, output.Height);

            Parallel.For(0, output.Height, y => {
                for (int x = 0; x < output.Width; x++) {
                    Color pixel = CastPrimaryRay(rays[x, y]).ToColor();

                    output.SetPixel(pixel, x, y);
                }
            });
        }

        protected abstract Vec3f CastPrimaryRay(Ray ray);
    }
}
