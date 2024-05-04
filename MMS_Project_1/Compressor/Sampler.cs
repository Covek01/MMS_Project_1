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

namespace MMS_Project_1.Compressor
{
    public class Sampler : ISampler
    {
        public static readonly int bitsPerByte = 8;
        public byte[] Downsample(Bitmap image)
        {
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData imgData = image.LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int stride = imgData.Stride;
            int width = image.Width;
            int height = image.Height;

            IntPtr imgDataBegin = imgData.Scan0;

            ByteArray output = new ByteArray(2 * sizeof(int) * bitsPerByte + height * width * bytesPerPixel * bitsPerByte);

            int Y = 2, U = 1, V = 0;

            //writing dimensions to output
            output.WriteInt32(height);
            output.WriteInt32(width);


            unsafe
            {
                byte* dataBegin = (byte*)imgDataBegin;
                int offset, offsetRight, offsetLower, offsetLowerRight;
                int i, j;
                byte averageValue;
                for (i = 0; i < height - 1; i += 2)
                {
                    for (j = 0; j < width - 1; j += 2)
                    {

                        //V channel
                        offset = stride * i + j * bytesPerPixel; //upperleft
                        offsetLower = offset + stride;
                        averageValue = (byte)(((int)dataBegin[offset] + (int)dataBegin[offsetLower]) / 2);    //average value

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
                        averageValue = (byte)(((int)dataBegin[offset] + (int)dataBegin[offsetLower]) / 2);    //average value

                        output.WriteByte(averageValue);

                    }
                }

                for (i = 0; i < height - 1; i++)
                {
                    for (j = 0; j < width - 1; j++)
                    { 
                        //U channel
                        offset = stride * i + j * bytesPerPixel + Y; //upperleft
                        output.WriteByte(dataBegin[offset]);

                    }
                }   //Y channel
            } //calculates average values


            image.UnlockBits(imgData);


            //BitArray retBitArray = new BitArray(output.Data);

            return output.Data;
        }

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

            int stride = imgData.Stride;
            int bytesPerPixel = 3;
            int V = 0, U = 1, Y = 2;
            unsafe
            {
                byte* dataBegin = (byte*)imgData.Scan0;
                int offset, offsetRight, offsetLower, offsetLowerRight;
                int i, j;
                int counterY = 0, counterU = 0, counterV = 0;
                for (i = 0; i < height - 1; i += 2)
                {
                    for (j = 0; j < width - 1; j += 2)
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

                        //V channel
                        offset = stride * i + j * bytesPerPixel + V; //upperleft
                        offsetLower = offset + stride;
                        offsetRight = offset + bytesPerPixel;
                        offsetLowerRight = offsetLower + bytesPerPixel;

                        dataBegin[offset] = bytesV[counterV];
                        dataBegin[offsetRight] = bytesV[counterV];
                        dataBegin[offsetLower] = bytesV[counterV];
                        dataBegin[offsetLowerRight] = bytesV[counterV];


                        counterV++;
                        counterU++;
                    }
                }

                for (i = 0; i < height - 1; i++)
                {
                    for (j = 0; j < width - 1; j++)
                    {

                        //Y channel
                        offset = stride * i + j * bytesPerPixel + Y; //upperleft
                        dataBegin[offset] = bytesY[counterY++];
                    }
                }
            }

            img.UnlockBits(imgData);

            return img;
        }
    }
}
