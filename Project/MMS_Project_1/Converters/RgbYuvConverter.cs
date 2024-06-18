using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.Converters
{
    public class RgbYuvConverter
    {
        public static YUV RGBtoYUV(byte R, byte G, byte B)
        {
            byte Y = (byte)(0.299 * R + 0.587 * G + 0.114 * B);
                byte U = (byte)(128 - 0.168736 * R - 0.331264 * G + 0.5 * B);
                byte V = (byte)(128 + 0.5 * R - 0.418688 * G - 0.081312 * B);

            return new YUV(Y, U, V);
        }

        public static RGB YUVtoRGB(byte Y, byte U, byte V)
        {
            int R = (int)(Y + 1.402 * (V - 128));
            int G = (int)(Y - 0.344136 * (U - 128) - 0.714136 * (V - 128));
            int B = (int)(Y + 1.772 * (U - 128));

            R = Math.Max(0,Math.Min(255, R));
            G = Math.Max(0, Math.Min(255, G));
            B = Math.Max(0, Math.Min(255, B));

            return new RGB((byte)R, (byte)G, (byte)B);
        }

        public static void ConvertRgbToYuv(Bitmap bitmap)
        {

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // Get RGB pixel value
                    Color pixelColor = bitmap.GetPixel(x, y);
                    byte R = pixelColor.R;
                    byte G = pixelColor.G;
                    byte B = pixelColor.B;

                    YUV yuv = RGBtoYUV(R, G, B);
                    RGB r = YUVtoRGB(yuv.Y, yuv.U, yuv.V);

                    // Update pixel with YUV values
                    bitmap.SetPixel(x, y, Color.FromArgb(yuv.Y, yuv.U, yuv.V));
                }
            }
        }

        public static void ConvertYuvToRgb(Bitmap bitmap)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // Get RGB pixel value
                    Color pixelColor = bitmap.GetPixel(x, y);
                    byte Y = pixelColor.R;
                    byte U = pixelColor.G;
                    byte V = pixelColor.B;

                    RGB rgb = YUVtoRGB(Y, U, V);

                    // Update pixel with YUV values
                    bitmap.SetPixel(x, y, Color.FromArgb(rgb.R, rgb.G, rgb.B));
                }
            }
        }

    }

    public class RGB
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public RGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    public class YUV
    {
        public byte Y { get; set; }
        public byte U { get; set; }
        public byte V { get; set; }
        public YUV(byte y, byte u, byte v)
        {
            Y = y;
            U = u;
            V = v;
        }
    }

}
