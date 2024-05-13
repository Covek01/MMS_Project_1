using MMS_Project_1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MMS_Project_1.Compressor.CompressorUtils
{
    public class HuffmanTreeUtility
    {
        public static HuffmanTreeNode BuildHuffmanTree(Dictionary<byte, int> counts)
        {
            PriorityQueue<HuffmanTreeNode, int> priorityQueue = new PriorityQueue<HuffmanTreeNode, int>();

            foreach (KeyValuePair<byte, int> kvp in counts)
            {
                priorityQueue.Enqueue(new HuffmanTreeNode(kvp.Key, kvp.Value), kvp.Value);
            } //generating priority queue

            HuffmanTreeNode fakeNode = new HuffmanTreeNode(250, 1);
            fakeNode.Terminal = false;

            while (priorityQueue.Count > 1)
            {
                HuffmanTreeNode left = priorityQueue.Dequeue();
                HuffmanTreeNode right = priorityQueue.Dequeue();
                HuffmanTreeNode top = new HuffmanTreeNode(250, left.Frequency + right.Frequency);
                top.Left = left;
                top.Right = right;
                top.Terminal = false;
                priorityQueue.Enqueue(top, top.Frequency);
            }

            HuffmanTreeNode root = priorityQueue.Dequeue();
            fakeNode.Right = root;

            return fakeNode;
        }

        public static Dictionary<byte, int> GenerateCounts(byte[] data)
        {
            Dictionary<byte, int> counts = new Dictionary<byte, int>();
            foreach (byte b in data)
            {
                counts[b] = counts.ContainsKey(b) ? counts[b] + 1 : 1;
            }

            return counts;
        }

        //public static async Task<byte[]> ReadDataWithTree(byte[] data, HuffmanTreeNode treeRoot)
        //{
        //    HuffmanTreeNode root = treeRoot;
        //    int length = data.Length * 8;
        //    BitArray bits = new BitArray(data);
        //    //BitArrayUtil.ReverseBitArray(bits);
        //    //List<byte> result = new List<byte>();
        //    int counter = 0;
        //    byte[] result = new byte[length];
        //    int acc = 0;
        //    bool bit;
        //    for (int i = 0; i < length; i++)
        //    {
        //        bit = bits[0];

        //        root = bit ? root.Right : root.Left;

        //        if (root.Terminal)
        //        {
        //            result[counter++] = root.Data;
        //            root = treeRoot;
        //            if (bits.Count > 1 && !bits[1])
        //            {
        //                break;
        //            }
        //        }
        //        bits.RightShift(1);
        //    }

        //    byte[] trimmedResult = new byte[counter];
        //    Array.Copy(result, trimmedResult, counter);
        //    return trimmedResult;
        //    // return result.ToArray();
        //}

        //public static byte[] ReadDataWithTree(byte[] data, HuffmanTreeNode treeRoot)
        //{
        //    HuffmanTreeNode root = treeRoot;
        //    int length = data.Length * 8;
        //    BitArray bits = new BitArray(data);
        //    BitArrayUtil.ReverseBitArray(bits);
        //    List<byte> result = new List<byte>();
        //    int counter = 0, iterator = 0;
        //    byte[] result = new byte[length];
        //    int acc = 0;
        //    bool bit;
        //    int i;


        //    for (i = 0; i < length; i++)
        //    {
        //        bit = bits[iterator++];

        //        root = bit ? root.Right : root.Left;

        //        if (root.Terminal)
        //        {
        //            result[counter++] = root.Data;
        //            root = treeRoot;
        //            while (iterator < length && bits[iterator] != true)
        //            {
        //                iterator++;
        //                i++;
        //            }
        //            if (bits.Count > iterator && !bits[iterator])
        //            {
        //                break;
        //            }
        //        }
        //        bits.RightShift(1);
        //    }

        //    byte[] trimmedResult = new byte[counter];
        //    Array.Copy(result, trimmedResult, counter);
        //    return trimmedResult;
        //    return result.ToArray();
        //}

        public static byte[] ReadDataWithTree(byte[] data, Dictionary<uint, byte> codes)
        {
            int length = data.Length * 8;
            BitArray bits = new BitArray(data);
            //BitArrayUtil.ReverseBitArray(bits);
            //List<byte> result = new List<byte>();
            int counter = 0, iterator = 0;
            byte[] result = new byte[length];
            uint acc = 0;
            bool bit;
            int i;
            byte value;

            for (i = 0; i < length; i++)
            {
                bit = bits[iterator++];

                acc = bit ? (acc << 1) + 1 : (acc << 1);

                if (codes.TryGetValue(acc, out value))
                {
                    result[counter++] = value;
                    acc = 0;
                    //while (iterator < length && bits[iterator] != true)
                    //{
                    //    iterator++;
                    //    i++;
                    //}
                    //if (bits.Count > iterator && !bits[iterator])
                    //{
                    //    break;
                    //}
                }
                //bits.RightShift(1);
            }

            byte[] trimmedResult = new byte[counter];
            Array.Copy(result, trimmedResult, counter);
            return trimmedResult;
            // return result.ToArray();
        }
    }
}
