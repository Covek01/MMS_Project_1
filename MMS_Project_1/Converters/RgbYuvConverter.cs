using System;
using System.Collections.Generic;
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
            byte U = (byte)(0.492 * (B - Y));
            byte V = (byte)(0.877 * (R - Y));

            Y = Math.Max((byte)0, Math.Min((byte)255, Y));
            U = Math.Max((byte)0, Math.Min((byte)255, U));
            V = Math.Max((byte)0, Math.Min((byte)255, V));

            return new YUV(Y, U, V);
        }

        public static RGB YUVtoRGB(byte Y, byte U, byte V)
        {
            byte R = (byte)(Y + (byte)(1.13983 * V));
            byte G = (byte)(Y - (byte)(0.39465 * U + 0.58060 * V));
            byte B = (byte)(Y + (byte)(2.03211 * U));

            R = Math.Max((byte)0, Math.Min((byte)255, R));
            G = Math.Max((byte)0, Math.Min((byte)255, G));
            B = Math.Max((byte)0, Math.Min((byte)255, B));

            return new RGB(R, G, B);
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
