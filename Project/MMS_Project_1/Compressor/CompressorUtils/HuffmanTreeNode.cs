using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.Compressor.CompressorUtils
{
    public class HuffmanTreeNode : IComparable<HuffmanTreeNode>
    {
        public byte Data { get; set; }
        public int Frequency { get; set; }
        public bool Terminal { get; set; }
        public HuffmanTreeNode Left {  get; set; }
        public HuffmanTreeNode Right { get; set; }
        public HuffmanTreeNode(byte data, int frequency, HuffmanTreeNode left, HuffmanTreeNode right, bool term)
        {
            Data = data;
            Frequency = frequency;
            Left = left;
            Right = right;
            Terminal = term;
        }

        public HuffmanTreeNode(byte data, int freq)
        {
            Data = data;
            Frequency = freq;
            Left = null;
            Right = null;
            Terminal = true;
        }

        public int CompareTo(HuffmanTreeNode? other)
        {
            return this.Frequency - other.Frequency;
        }
    }
}
