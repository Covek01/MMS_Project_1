using MMS_Project_1.Utils;
using MMS_Project_1.Compressor.CompressorUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.Collections;
using System.DirectoryServices.ActiveDirectory;

namespace MMS_Project_1.Compressor
{
    public class HuffmanCompressor : ICompressor
    {
        private Dictionary<byte, uint> _codes = new Dictionary<byte, uint>();
        public byte[] Compress(byte[] data)
        {
            Dictionary<byte, int> counts = HuffmanTreeUtility.GenerateCounts(data);
            HuffmanTreeNode root = HuffmanTreeUtility.BuildHuffmanTree(counts);

            _codes.Clear();
            StoreCodes(root);

            BitArray bits = GenerateBitsToWrite(data);

            byte[] resultBytes = BitArrayUtil.BitArrayToByteArray(bits);



            //get bytes from counts
            int countsCount = counts.Count;
            int counter = 0;
            byte[] header = new byte[sizeof(int) + countsCount * (sizeof(byte) + sizeof(int))];
            Buffer.BlockCopy(BitConverter.GetBytes(countsCount), 0, header, counter, sizeof(int));
            counter += sizeof(int);
            foreach (var c in _codes)
            {
                header[counter++] = c.Key;
                Buffer.BlockCopy(BitConverter.GetBytes(c.Value), 0, header, counter, sizeof(int));
                counter += sizeof(int);
            }


            return header.Concat(resultBytes).ToArray();
        }

        public byte[] Decompress(byte[] data)
        {
            Dictionary<byte, uint> counts = new Dictionary<byte, uint>();

            //parse dictionary
            int counter = 0;
            int codesCount;
            byte key;
            uint value;
            unsafe
            {
                fixed (byte* dataBegin = &data[0])
                {
                    Buffer.MemoryCopy(dataBegin, &codesCount, sizeof(int), sizeof(int));

                    counter += sizeof(int);

                    for (int i = 0; i < codesCount; i++)
                    {
                        key = data[counter++];
                        Buffer.MemoryCopy(dataBegin + counter, &value, sizeof(int), sizeof(int));
                        counter += sizeof(int);

                        counts.Add(key, value);
                    }
                }
            }

            byte[] afterHeader = new byte[data.Length - counter];
            Array.Copy(data, counter, afterHeader, 0, afterHeader.Length);

            Dictionary<uint, byte> invertedCodes = counts.ToDictionary(x => x.Value, x => x.Key);
            byte[] resultLetters = HuffmanTreeUtility.ReadDataWithTree(afterHeader, invertedCodes);


            return resultLetters;
        }

        private void StoreCodes(HuffmanTreeNode root, uint code = 0)
        {
            if (root == null)
            {
                return;
            }
            if (root.Terminal == true)
            {
                _codes[root.Data] = code;
            }


            StoreCodes(root.Left, (code << 1));
            StoreCodes(root.Right, ((code << 1) + 1));
        }

        public BitArray GenerateBitsToWrite(byte[] data)
        {
            BitArray letter;
            byte[] arr;
            int counter, pointerArray = 0;
            int lastIndex = sizeof(uint) * 8 - 1;
            BitArray dstBitArray = new BitArray(data.Length * 8 * sizeof(uint), false);
            foreach (byte b in data) 
            {
                arr = BitConverter.GetBytes(_codes[b]);
                letter = new BitArray(arr);

                counter = 0;
                while (letter[lastIndex] != true)
                {
                    letter.LeftShift(1);
                    counter++;
                }

                BitArrayUtil.ReverseBitArray(letter);
                int len = sizeof(uint) * 8 - counter;
                BitArrayUtil.CopyFromBitArrayToBitArray(letter, 0, dstBitArray, pointerArray, len);
                pointerArray += len;
            }
            byte[] btsB = BitArrayUtil.BitArrayToByteArray(dstBitArray);
            dstBitArray.Length = pointerArray;
            byte[] bts = BitArrayUtil.BitArrayToByteArray(dstBitArray);

            return dstBitArray;
        }
        

        
    }
}
