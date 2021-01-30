using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBImageConverter
{
    public static class ByteUtils
    {
        public static void SetBitInByte(int bitIndex, bool set, ref byte _byte)
        {
            if(set)
            {
                _byte = (byte)(_byte | (1 << bitIndex));
            }
            else
            {
                _byte = (byte)(_byte & ~(1 << bitIndex));
            }
        }
    }
}
