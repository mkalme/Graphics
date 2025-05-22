using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Graphics {
    public class LockedBitmap {
        public Bitmap Source { get; set; }
        public BitmapData Data { get; set; }
        public byte[] RGBValues { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public LockedBitmap(Bitmap bitmap) {
            Lock(bitmap);
        }
        public LockedBitmap(Color color, int width, int height) {
            Bitmap bitmap = new Bitmap(width, height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.Clear(color);

            Lock(bitmap);
        }

        public void SetPixel(Color color, int x, int y) {
            int index = (y * Width + x) * 4;

            RGBValues[index + 3] = color.A;
            RGBValues[index + 2] = color.R;
            RGBValues[index + 1] = color.G;
            RGBValues[index] = color.B;
        }
        public Color GetPixel(int x, int y) {
            int index = (y * Width + x) * 4;

            return Color.FromArgb(RGBValues[index + 3], RGBValues[index + 2], RGBValues[index + 1], RGBValues[index]);
        }

        public void Lock(Bitmap bitmap) {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int bytes = Math.Abs(data.Stride) * data.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(data.Scan0, rgbValues, 0, bytes);

            Source = bitmap;
            Data = data;
            RGBValues = rgbValues;
            Width = bitmap.Width;
            Height = bitmap.Height;
        }
        public void Unlock() {
            if (Data == null) {
                using (MemoryStream stream = new MemoryStream(RGBValues)) {
                    Source = new Bitmap(stream);
                }
            } else {
                Marshal.Copy(RGBValues, 0, Data.Scan0, RGBValues.Length);
                Source.UnlockBits(Data);
            }
        }
    }
}