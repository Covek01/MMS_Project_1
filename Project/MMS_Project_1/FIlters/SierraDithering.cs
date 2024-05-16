using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.FIlters
{
    public class SierraDithering
    {
        public static byte LIMIT = 127;
        public static byte MAX_VALUE = 255; 
        public static async Task PerformFilter(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData imgData = img.LockBits(rect, ImageLockMode.ReadWrite, img.PixelFormat);
            int stride = imgData.Stride;
            int bytesPerPixel = Bitmap.GetPixelFormatSize(img.PixelFormat) / 8;
            int diff;

            unsafe
            {
                byte* beginR = (byte*)imgData.Scan0 + 2 * sizeof(byte);
                byte* beginG = (byte*)imgData.Scan0 + sizeof(byte);
                byte* beginB = (byte*)imgData.Scan0;

                int offset, offsetLower, offsetBelowLower;

                int i, j;
                float chunk;
                for (i = 0; i < height - 2; i++)
                {
                    int r = 1;
                    //R channel
                    for (j = 0; j < width - 4; j++)
                    {
                        offset = i * stride + j * bytesPerPixel + bytesPerPixel * 2;
                        offsetLower = offset + stride;
                        offsetBelowLower = offsetLower + stride;
                        
                        diff = InitializeXAndGetDiff(ref beginR[offset]);
                        //chunk = (float)diff / 32;

                        //1st line
                        offset += bytesPerPixel;
                        PushColor(ref beginR[offset], diff * 5, i, j);

                        offset += bytesPerPixel;
                        PushColor(ref beginR[offset], diff * 7, i, j);

                        //second line
                        PushColor(ref beginR[offsetLower], diff * 5, i, j);
                        PushColor(ref beginR[offsetLower - bytesPerPixel], diff * 4, i, j);
                        PushColor(ref beginR[offsetLower + bytesPerPixel], diff * 4, i, j);
                        PushColor(ref beginR[offsetLower - 2 * bytesPerPixel], diff * 2, i, j);
                        PushColor(ref beginR[offsetLower + 2 * bytesPerPixel], diff * 2, i, j);

                        //third line
                        PushColor(ref beginR[offsetBelowLower], diff * 3, i, j);
                        PushColor(ref beginR[offsetBelowLower - bytesPerPixel], diff * 2, i, j);
                        PushColor(ref beginR[offsetBelowLower + bytesPerPixel], diff * 2, i, j);
                    }

                    int a = 1;
                    //G channel
                    for (j = 0; j < width - 4; j++)
                    {
                        offset = i * stride + j * bytesPerPixel + bytesPerPixel * 2;
                        offsetLower = offset + stride;
                        offsetBelowLower = offsetLower + stride;

                        diff = InitializeXAndGetDiff(ref beginG[offset]);
                        //chunk = (float)diff / 32;

                        //1st line
                        offset += bytesPerPixel;
                        PushColor(ref beginG[offset], diff * 5, i, j);

                        offset += bytesPerPixel;
                        PushColor(ref beginG[offset], diff * 7, i, j);

                        //second line
                        PushColor(ref beginG[offsetLower], diff * 5, i, j);
                        PushColor(ref beginG[offsetLower - bytesPerPixel], diff * 4, i, j);
                        PushColor(ref beginG[offsetLower + bytesPerPixel], diff * 4, i, j);
                        PushColor(ref beginG[offsetLower - 2 * bytesPerPixel], diff * 2, i, j);
                        PushColor(ref beginG[offsetLower + 2 * bytesPerPixel], diff * 2, i, j);

                        //third line
                        PushColor(ref beginG[offsetBelowLower], diff * 3, i, j);
                        PushColor(ref beginG[offsetBelowLower - bytesPerPixel], diff * 2, i, j);
                        PushColor(ref beginG[offsetBelowLower + bytesPerPixel], diff * 2, i, j);
                    }

                    int b = 1;

                    //B channel
                    for (j = 0; j < width - 4; j++)
                    {
                        offset = i * stride + j * bytesPerPixel + bytesPerPixel * 2;
                        offsetLower = offset + stride;
                        offsetBelowLower = offsetLower + stride;

                        diff = InitializeXAndGetDiff(ref beginB[offset]);
                        //chunk = (float)diff / 32;

                        //1st line
                        offset += bytesPerPixel;
                        PushColor(ref beginB[offset], diff * 5, i, j);

                        offset += bytesPerPixel;
                        PushColor(ref beginB[offset], diff * 7, i, j);

                        //second line
                        PushColor(ref beginB[offsetLower], diff * 5, i, j);
                        PushColor(ref beginB[offsetLower - bytesPerPixel], diff * 4, i, j);
                        PushColor(ref beginB[offsetLower + bytesPerPixel], diff * 4, i, j);
                        PushColor(ref beginB[offsetLower - 2 * bytesPerPixel], diff * 2, i, j);
                        PushColor(ref beginB[offsetLower + 2 * bytesPerPixel], diff * 2, i, j);

                        //third line
                        PushColor(ref beginB[offsetBelowLower], diff * 3, i, j);
                        PushColor(ref beginB[offsetBelowLower - bytesPerPixel], diff * 2, i, j);
                        PushColor(ref beginB[offsetBelowLower + bytesPerPixel], diff * 2, i, j);
                    }
                }
               

            }
            img.UnlockBits(imgData);



        }

        public static int InitializeXAndGetDiff(ref byte value)
        {
            int diff;
            if (value > LIMIT)
            {
                diff = value - MAX_VALUE;
                value = MAX_VALUE;
            }
            else
            {
                diff = value;
                value = 0;
            }

            return diff;
        }

        public static void PushColor(ref byte value, int increase, int i, int j)
        {
            increase >>= 5;
            if (value + increase > MAX_VALUE)
            {
                value = MAX_VALUE;
            }
            else if (value + increase < 0)
            {
                value = 0;
            }
            else
            {
                value = (byte)(value + increase);
            }

        }
    }
}
