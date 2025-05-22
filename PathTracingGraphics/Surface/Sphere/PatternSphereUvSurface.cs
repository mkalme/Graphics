using System;
using System.Drawing;
using System.Collections.Concurrent;
using Graphics;
using Vectors.Vec3;
using Vectors.Vec2;
using Vectors.Extensions;

namespace PathTracingGraphics {
    public class PatternSphereUvSurface : SphereUvSurface {
        public Vec3f PrimaryColor { get; set; }
        public Vec3f SecondaryColor { get; set; }

        public float Interval { get; set; }

        private ConcurrentDictionary<(int, int), Vec3f> _dictionary = new ConcurrentDictionary<(int, int), Vec3f>();
        private Random _random = new Random();

        public PatternSphereUvSurface(Sphere sphere, float interval, Color primaryColor, Color secondaryColor) : base(sphere) {
            Interval = interval;
            PrimaryColor = primaryColor.ToVector();
            SecondaryColor = secondaryColor.ToVector();
        }

        public override SurfaceProperties GetSurfaceFromUvPoint(Vec2f uv) {
            float u = (uv.X + 0.5F) % 1 * 2;
            float interval = Interval / 180;

            int x = (int)(u / interval);
            int y = (int)(uv.Y / interval);

            (int, int) index = (x, y);

            if (!_dictionary.TryGetValue(index, out Vec3f output)) {
                output = PrimaryColor;
                output = output.Mix(SecondaryColor, (float)_random.NextDouble() / 2F + 0.5F);

                _dictionary.TryAdd(index, output);
            }

            return new SurfaceProperties(output, DefaultProperties.ReflectionIndex, DefaultProperties.Ks);
        }
    }
}
