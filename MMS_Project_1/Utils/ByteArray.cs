using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.Utils
{
    public class ByteArray
    {
        public int Counter { get; set; }
        public byte[] Data { get; set; }
        public ByteArray(int size)
        {
            this.Data = new byte[size];
            this.Counter = 0;
        }

        public void WriteInt32(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Buffer.BlockCopy(bytes, 0, Data, Counter, sizeof(int));
        }

        public void WriteBytes(byte[] bytesInput, int size)
        {
            Buffer.BlockCopy(bytesInput, 0, Data, Counter, size);
        }

        public void WriteByte(byte value)
        {
            Data[Counter++] = value;
        }
    }
}
