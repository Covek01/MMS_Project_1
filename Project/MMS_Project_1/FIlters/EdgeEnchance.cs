using MMS_Project_1.FIlters.Matrix;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.FIlters
{
    internal class EdgeEnchance
    {


        public static async Task<Bitmap> Convolution3x3(Bitmap bSrc, byte nThreshold)
        {
            Matrix3 mat = new Matrix3();

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
                int offset, offsetLower, offsetUpper, offsetResult;
                byte[,] matrix = new byte[3, 3];
                Matrix3 matrixPom = new Matrix3();
                int nPixelMax;

                for (i = 1; i < height - 1; i++)
                {
                    for (j = 1; j < width - 1; j++)
                    {

                        //R channel
                        offset = i * stride + j * bytesPerPixel + R;
                        offsetUpper = offset - stride;
                        offsetLower = offset + stride;

                        matrixPom.TopLeft = pSrc[offset - bytesPerPixel];
                        matrixPom.TopMid = pSrc[offset];
                        matrixPom.TopRight = pSrc[offset + bytesPerPixel];

                        matrixPom.MidLeft = pSrc[offsetUpper - bytesPerPixel];
                        matrixPom.Pixel = pSrc[offsetUpper];
                        matrixPom.MidRight = pSrc[offsetUpper + bytesPerPixel];

                        matrixPom.BottomLeft = pSrc[offsetLower - bytesPerPixel];
                        matrixPom.BottomMid = pSrc[offsetLower];
                        matrixPom.BottomRight = pSrc[offsetLower + bytesPerPixel];

                        //NormalizePixel(ref pixel);
                        nPixelMax = CalculateMaxAbsValue(matrixPom);

                        offsetResult = (i - 1) * strideData + (j - 1) * bytesPerPixel + R;
                        if (nPixelMax > nThreshold && nPixelMax > pSrc[offset])
                        {
                            pData[offsetResult] = (byte)Math.Max(pSrc[offset], nPixelMax);
                        }
                        else
                        {
                            pData[offsetResult] = pSrc[offset];
                        }






                        //G channel
                        offset = i * stride + j * bytesPerPixel + G;
                        offsetUpper = offset - stride;
                        offsetLower = offset + stride;

                        matrixPom.TopLeft = pSrc[offset - bytesPerPixel];
                        matrixPom.TopMid = pSrc[offset];
                        matrixPom.TopRight = pSrc[offset + bytesPerPixel];

                        matrixPom.MidLeft = pSrc[offsetUpper - bytesPerPixel];
                        matrixPom.Pixel = pSrc[offsetUpper];
                        matrixPom.MidRight = pSrc[offsetUpper + bytesPerPixel];

                        matrixPom.BottomLeft = pSrc[offsetLower - bytesPerPixel];
                        matrixPom.BottomMid = pSrc[offsetLower];
                        matrixPom.BottomRight = pSrc[offsetLower + bytesPerPixel];

                        //NormalizePixel(ref pixel);
                        nPixelMax = CalculateMaxAbsValue(matrixPom);

                        offsetResult = (i - 1) * strideData + (j - 1) * bytesPerPixel + G;
                        if (nPixelMax > nThreshold && nPixelMax > pSrc[offset])
                        {
                            pData[offsetResult] = (byte)Math.Max(pSrc[offset], nPixelMax);
                        }
                        else
                        {
                            pData[offsetResult] = pSrc[offset];
                        }






                        //B channel
                        offset = i * stride + j * bytesPerPixel + B;
                        offsetUpper = offset - stride;
                        offsetLower = offset + stride;

                        matrixPom.TopLeft = pSrc[offset - bytesPerPixel];
                        matrixPom.TopMid = pSrc[offset];
                        matrixPom.TopRight = pSrc[offset + bytesPerPixel];

                        matrixPom.MidLeft = pSrc[offsetUpper - bytesPerPixel];
                        matrixPom.Pixel = pSrc[offsetUpper];
                        matrixPom.MidRight = pSrc[offsetUpper + bytesPerPixel];

                        matrixPom.BottomLeft = pSrc[offsetLower - bytesPerPixel];
                        matrixPom.BottomMid = pSrc[offsetLower];
                        matrixPom.BottomRight = pSrc[offsetLower + bytesPerPixel];

                        //NormalizePixel(ref pixel);
                        nPixelMax = CalculateMaxAbsValue(matrixPom);

                        offsetResult = (i - 1) * strideData + (j - 1) * bytesPerPixel + B;
                        if (nPixelMax > nThreshold && nPixelMax > pSrc[offset])
                        {
                            pData[offsetResult] = (byte)Math.Max(pSrc[offset], nPixelMax);
                        }
                        else
                        {
                            pData[offsetResult] = pSrc[offset];
                        }


                        //A channel
                        offset = (i - 1) * strideData + (j - 1) * bytesPerPixel + A;
                        pData[offset] = 255;

                    }

                    
                }

            }

            bmp.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);


            return bmp;
        }

        public static Matrix3 GenerateMatrix(byte[,] m)
        {
            Matrix3 mat = new Matrix3();

            mat.TopLeft = m[0, 0];
            mat.TopMid = m[0, 1];
            mat.TopRight = m[0, 2];

            mat.MidLeft = m[1, 0];
            mat.Pixel = m[1, 1];
            mat.MidRight = m[1, 2];

            mat.BottomLeft = m[2, 0];
            mat.BottomMid = m[2, 1];
            mat.BottomRight = m[2, 2];

            return mat;
        }

        public static int CalculateMaxAbsValue(Matrix3 mat)
        {
            int value;

            int max = Math.Abs(mat.TopRight - mat.BottomLeft);
            value = Math.Abs(mat.BottomRight - mat.TopLeft);
            if (value > max)
                max = value;
            value = Math.Abs(mat.BottomMid - mat.TopMid);
            if (value > max)
                max = value;
            value = Math.Abs(mat.MidLeft - mat.MidRight);
            if (value > max)
                max = value;

            return max;
        }
    }
}
