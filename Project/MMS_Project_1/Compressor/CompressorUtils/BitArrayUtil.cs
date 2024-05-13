using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MMS_Project_1.Compressor.CompressorUtils
{
    public class BitArrayUtil
    {
        public static void CopyFromBitArrayToBitArray(BitArray src, int indexSrc, BitArray dst, int indexDst, int count)
        {
            if (dst.Count < indexDst + count)
            {
                return;
            }

            for (int i = 0; i < count;  i++)
            {
                dst[indexDst + i] = src[indexSrc + i];
            }
        }

        public static byte[] BitArrayToByteArray(BitArray bitArray)
        {
            byte[] byteArray = new byte[(bitArray.Length + 7) / 8];
            bitArray.CopyTo(byteArray, 0);
            return byteArray;
        }

        public static void ReverseBitArray(BitArray bitArray)
        {
            int left = 0;
            int right = bitArray.Length - 1;

            while (left < right)
            {
                bool temp = bitArray[left];
                bitArray[left] = bitArray[right];
                bitArray[right] = temp;

                left++;
                right--;
            }
        }
    }
}
