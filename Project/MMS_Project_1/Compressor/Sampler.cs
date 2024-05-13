using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS_Project_1.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;
using MMS_Project_1.Converters;
using MMS_Project_1.Compressor.CompressorUtils;
using System.Runtime.InteropServices.Marshalling;

namespace MMS_Project_1.Compressor
{
    public class Sampler : ISampler
    {
        public static readonly int bitsPerByte = 8;
        public byte[] Downsample(Bitmap image)
        {

            RgbYuvConverter.ConvertRgbToYuv(image);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int width = image.Width;
            int height = image.Height;
            int croppedHeight = (height % 2 == 0) ? height : height - 1;
            int croppedWidth = (width % 2 == 0) ? width : width - 1;

            Rectangle rect = new Rectangle(0, 0, image.Width, croppedHeight);
            BitmapData imgData = image.LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);
            int stride = imgData.Stride;

            IntPtr imgDataBegin = imgData.Scan0;

            
            ByteArray output = new ByteArray(2 * sizeof(int) + croppedHeight * croppedWidth * sizeof(byte) + 2 * croppedHeight * croppedWidth / 4 * sizeof(byte));

            int Y = 2, U = 1, V = 0;

            //writing dimensions to output
            output.WriteInt32(croppedHeight);
            output.WriteInt32(croppedWidth);


            unsafe
            {
                byte* dataBegin = (byte*)imgDataBegin;
                int offset, offsetRight, offsetLower, offsetLowerRight;
                int i, j;
                byte averageValue;
                int add1, add2;
                for (i = 0; i < height - 1; i += 2)
                {
                    for (j = 0; j < width - 1; j += 2)
                    {

                        //V channel
                        offset = stride * i + j * bytesPerPixel; //upperleft
                        offsetLower = offset + stride;
                        add1 = dataBegin[offset];
                        add2 = dataBegin[offsetLower];

                        //averageValue = (byte)(((int)dataBegin[offset] + (int)dataBegin[offsetLower]) / 2);    //average value
                        averageValue = (byte)((add1 + add2) / 2);
                        output.WriteByte(averageValue);
                    }
                }

                for (i = 0; i < height - 1; i += 2)
                {
                    for (j = 0; j < width - 1; j += 2)
                    {

                        //U channel
                        offset = stride * i + j * bytesPerPixel + U; //upperleft
                        offsetLower = offset + stride;

                        add1 = dataBegin[offset];
                        add2 = dataBegin[offsetLower];

                        //averageValue = (byte)(((int)dataBegin[offset] + (int)dataBegin[offsetLower]) / 2);    //average value
                        averageValue = (byte)((add1 + add2) / 2);

                        output.WriteByte(averageValue);

                    }
                }

                for (i = 0; i < croppedHeight; i++)
                {
                    for (j = 0; j < croppedWidth; j++)
                    { 
                        offset = stride * i + j * bytesPerPixel + Y; //upperleft
                        output.WriteByte(dataBegin[offset]);

                    }
                }   //Y channel
            } //calculates average values


            image.UnlockBits(imgData);


            //BitArray retBitArray = new BitArray(output.Data);

            return output.Data;
        }

  //      public byte[] Downsample2(Bitmap image)
     /*   {
            RgbYuvConverter.ConvertRgbToYuv(image);
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            //BitmapData imgData = image.LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
           // int stride = imgData.Stride;
            int width = image.Width;
            int height = image.Height;

            ByteArray output = new ByteArray(2 * sizeof(int) + height * width * sizeof(byte) + 2 * height * width * sizeof(byte) / 4);

            int Y = 2, U = 1, V = 0;

            //writing dimensions to output
            output.WriteInt32(height);
            output.WriteInt32(width);

            int i, j;
            byte averageValue;
            int add1, add2;

            for (i = 0; i < height - 1; i += 2)
            {
                for (j = 0; j < width - 1; j += 2)
                {

                    //V channel
                    add1 = image.GetPixel(j, i).B;
                    add2 = image.GetPixel(j, i + 1).B;

                    //averageValue = (byte)(((int)dataBegin[offset] + (int)dataBegin[offsetLower]) / 2);    //average value
                    averageValue = (byte)((add1 + add2) / 2);
                    output.WriteByte(averageValue);
                }
            }

            for (i = 0; i < height - 1; i += 2)
            {
                for (j = 0; j < width - 1; j += 2)
                {

                    //U channel
                    add1 = image.GetPixel(j, i).G;
                    add2 = image.GetPixel(j, i + 1).G;

                    //averageValue = (byte)(((int)dataBegin[offset] + (int)dataBegin[offsetLower]) / 2);    //average value
                    averageValue = (byte)((add1 + add2) / 2);
                    output.WriteByte(averageValue);
                }
            }

            byte colorY;
            for (i = 0; i < height - 1; i += 2)
            {
                for (j = 0; j < width - 1; j += 2)
                {
                    //Y channel
                    colorY = image.GetPixel(j, i).R;
                    output.WriteByte(colorY);
                }
            }

            return output.Data;
        }*/

        public Bitmap Upsample(byte[] input)
        {
            int readCounter = 0;

            int height = BitConverter.ToInt32(input, readCounter);
            readCounter += sizeof(int);
            int width = BitConverter.ToInt32(input, readCounter);
            readCounter += sizeof(int);

            Bitmap img = new Bitmap(width, height);

            int dim = width * height;
            int dim4 = dim / 4;
            byte[] bytesV = new byte[dim4];
            byte[] bytesU = new byte[dim4];
            byte[] bytesY = new byte[dim];


            Buffer.BlockCopy(input, readCounter, bytesV, 0, dim4 * sizeof(byte));
            readCounter += dim4 * sizeof(byte);
            Buffer.BlockCopy(input, readCounter, bytesU, 0, dim4 * sizeof(byte));
            readCounter += dim4 * sizeof(byte);

            Buffer.BlockCopy(input, readCounter, bytesY, 0, dim * sizeof(byte));

            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            BitmapData imgData = img.LockBits(rect, ImageLockMode.ReadWrite, img.PixelFormat);

            byte[] matrix = new byte[height * width * 4];


            int stride = imgData.Stride;
            int bytesPerPixel = 4;
            int V = 0, U = 1, Y = 2, A = 3;
            unsafe
            {
                byte* dataBegin = (byte*)imgData.Scan0;
                int offset, offsetRight, offsetLower, offsetLowerRight;
                int i, j;
                int counterY = 0, counterU = 0, counterV = 0;
                for (i = 0; i < height; i += 2)
                {
                    for (j = 0; j < width; j += 2)
                    {

                        //U channel
                        offset = stride * i + j * bytesPerPixel + U; //upperleft
                        offsetLower = offset + stride;
                        offsetRight = offset + bytesPerPixel;
                        offsetLowerRight = offsetLower + bytesPerPixel;

                        dataBegin[offset] = bytesU[counterU];
                        dataBegin[offsetRight] = bytesU[counterU];
                        dataBegin[offsetLower] = bytesU[counterU];
                        dataBegin[offsetLowerRight] = bytesU[counterU];

                        matrix[offset] = dataBegin[offset];
                        matrix[offsetRight] = dataBegin[offsetRight];
                        matrix[offsetLower] = dataBegin[offsetLower];
                        matrix[offsetLowerRight] = dataBegin[offsetLowerRight];



                        //V channel
                        offset = stride * i + j * bytesPerPixel + V; //upperleft
                        offsetLower = offset + stride;
                        offsetRight = offset + bytesPerPixel;
                        offsetLowerRight = offsetLower + bytesPerPixel;

                        dataBegin[offset] = bytesV[counterV];
                        dataBegin[offsetRight] = bytesV[counterV];
                        dataBegin[offsetLower] = bytesV[counterV];
                        dataBegin[offsetLowerRight] = bytesV[counterV];

                        matrix[offset] = dataBegin[offset];
                        matrix[offsetRight] = dataBegin[offsetRight];
                        matrix[offsetLowerRight] = dataBegin[offsetLowerRight];
                        matrix[offsetLower] = dataBegin[offsetLower];


                        counterV++;
                        counterU++;
                    }
                }

                for (i = 0; i < height; i++)
                {
                    for (j = 0; j < width; j++)
                    {

                        //Y channel
                        offset = stride * i + j * bytesPerPixel + Y; //upperleft
                        dataBegin[offset] = bytesY[counterY++];

                        matrix[offset] = dataBegin[offset];


                        //A channel
                        offset = stride * i + j * bytesPerPixel + A; //upperleft
                        dataBegin[offset] = 255;
                    }
                }
            }

            img.UnlockBits(imgData);

            RgbYuvConverter.ConvertYuvToRgb(img);


            return img;
        }

      //  public Bitmap Upsample2(byte[] input)
  /*      {
            int readCounter = 0;

            int height = BitConverter.ToInt32(input, readCounter);
            readCounter += sizeof(int);
            int width = BitConverter.ToInt32(input, readCounter);
            readCounter += sizeof(int);

            Bitmap img = new Bitmap(width, height);

            int dim = width * height;
            int dim4 = dim / 4;
            byte[] bytesV = new byte[dim4];
            byte[] bytesU = new byte[dim4];
            byte[] bytesY = new byte[dim];


            Buffer.BlockCopy(input, readCounter, bytesV, 0, dim4 * sizeof(byte));
            readCounter += dim4 * sizeof(byte);
            Buffer.BlockCopy(input, readCounter, bytesU, 0, dim4 * sizeof(byte));
            readCounter += dim4 * sizeof(byte);

            Buffer.BlockCopy(input, readCounter, bytesY, 0, dim * sizeof(byte));

            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            //BitmapData imgData = img.LockBits(rect, ImageLockMode.ReadWrite, img.PixelFormat);

            int i, j;
            int counterY = 0, counterU = 0, counterV = 0;
            Color col;

            for (i = 0; i < height - 1; i += 2)
            {
                for (j = 0; j < width - 1; j += 2)
                {

                    //U and V channel
                    col = img.GetPixel(j, i);
                    col = Color.FromArgb(col.A, col.R, bytesU[counterU], bytesV[counterV]);
                    img.SetPixel(j, i, col);

                    col = img.GetPixel(j + 1, i);
                    col = Color.FromArgb(col.A, col.R, bytesU[counterU], bytesV[counterV]);
                    img.SetPixel(j + 1, i, col);

                    col = img.GetPixel(j, i + 1);
                    col = Color.FromArgb(col.A, col.R, bytesU[counterU], bytesV[counterV]);
                    img.SetPixel(j, i + 1, col);

                    col = img.GetPixel(j + 1, i + 1);
                    col = Color.FromArgb(col.A, col.R, bytesU[counterU], bytesV[counterV]);
                    img.SetPixel(j + 1, i + 1, col);

                    counterV++;
                    counterU++;
                }
            }

            for (i = 0; i < height - 1; i++)
            {
                for (j = 0; j < width - 1; j++)
                { 
                    //Y channel
                    col = img.GetPixel(j, i);
                    col = Color.FromArgb(255, bytesY[counterY++], col.G, col.B);
                    img.SetPixel(j, i, col);
                }
            }

            return img;

        }*/

    }
}
