using MMS_Project_1.FIlters.Matrix;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.FIlters
{
    public class MeanRemoval
    {
        public static async Task<Bitmap> Convolution3x3(Bitmap bSrc)
        {
            Matrix3 mat = MeanRemovalMatrix();

            Bitmap bmp = new Bitmap(bSrc.Width - 1, bSrc.Height - 1);

            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            int stride = bmSrc.Stride;
            int height = bmSrc.Height;
            int width = bmSrc.Width;
            int strideData = bmData.Stride;
            int pixel;
            int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;


            unsafe
            {
                byte* pData = (byte*)bmData.Scan0;
                byte* pSrc = (byte*)bmSrc.Scan0;
                int i, j;
                int B = 0, G = 1, R = 2, A = 3;
                int offset, offsetLower, offsetUpper;

                for (i = 1; i < height - 1; i++)
                {
                    //R channel
                    for (j = 1; j < width - 1; j++)
                    {
                        offset = i * stride + j * bytesPerPixel + R;
                        offsetUpper = offset - stride;
                        offsetLower = offset + stride;

                        pixel = (pSrc[offsetUpper - bytesPerPixel] * mat.TopLeft + pSrc[offsetUpper] * mat.TopMid + pSrc[offsetUpper + bytesPerPixel] * mat.TopRight
                            + pSrc[offset - bytesPerPixel] * mat.MidLeft + pSrc[offset] * mat.Pixel + pSrc[offset + bytesPerPixel] * mat.MidRight
                            + pSrc[offsetLower - bytesPerPixel] * mat.BottomLeft + pSrc[offsetLower] * mat.BottomMid + pSrc[offsetLower + bytesPerPixel] * mat.BottomRight) / mat.Factor + mat.Offset;

                        NormalizePixel(ref pixel);

                        offset = (i - 1) * strideData + (j - 1) * bytesPerPixel + R;
                        pData[offset] = (byte)pixel;
                    }

                    //G channel
                    for (j = 1; j < width - 1; j++)
                    {
                        offset = i * stride + j * bytesPerPixel + G;
                        offsetUpper = offset - stride;
                        offsetLower = offset + stride;

                        pixel = (pSrc[offsetUpper - bytesPerPixel] * mat.TopLeft + pSrc[offsetUpper] * mat.TopMid + pSrc[offsetUpper + bytesPerPixel] * mat.TopRight
                            + pSrc[offset - bytesPerPixel] * mat.MidLeft + pSrc[offset] * mat.Pixel + pSrc[offset + bytesPerPixel] * mat.MidRight
                            + pSrc[offsetLower - bytesPerPixel] * mat.BottomLeft + pSrc[offsetLower] * mat.BottomMid + pSrc[offsetLower + bytesPerPixel] * mat.BottomRight) / mat.Factor + mat.Offset;

                        NormalizePixel(ref pixel);

                        offset = (i - 1) * strideData + (j - 1) * bytesPerPixel + G;
                        pData[offset] = (byte)pixel;
                    }

                    //B channel
                    for (j = 1; j < width - 1; j++)
                    {
                        offset = i * stride + j * bytesPerPixel + B;
                        offsetUpper = offset - stride;
                        offsetLower = offset + stride;

                        pixel = (pSrc[offsetUpper - bytesPerPixel] * mat.TopLeft + pSrc[offsetUpper] * mat.TopMid + pSrc[offsetUpper + bytesPerPixel] * mat.TopRight
                            + pSrc[offset - bytesPerPixel] * mat.MidLeft + pSrc[offset] * mat.Pixel + pSrc[offset + bytesPerPixel] * mat.MidRight
                            + pSrc[offsetLower - bytesPerPixel] * mat.BottomLeft + pSrc[offsetLower] * mat.BottomMid + pSrc[offsetLower + bytesPerPixel] * mat.BottomRight) / mat.Factor + mat.Offset;

                        NormalizePixel(ref pixel);

                        offset = (i - 1) * strideData + (j - 1) * bytesPerPixel + B;
                        pData[offset] = (byte)pixel;
                    }

                    //A channel
                    for (j = 1; j < width - 1; j++)
                    {
                        offset = (i - 1) * strideData + (j - 1) * bytesPerPixel + A;
                        pData[offset] = 255;
                    }
                }

            }

            bmp.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);


            return bmp;
        }

        public static void NormalizePixel(ref int pixel)
        {
            if (pixel > 255)
                pixel = 255;
            if (pixel < 0)
                pixel = 0;
        }

        public static Matrix3 MeanRemovalMatrix(int nWeight = 9 )
        {
            Matrix3 m = new Matrix3();
            m.SetAll(-1);
            m.Pixel = nWeight;
            m.Factor = nWeight - 8;

            return m;
        }
    }
}
