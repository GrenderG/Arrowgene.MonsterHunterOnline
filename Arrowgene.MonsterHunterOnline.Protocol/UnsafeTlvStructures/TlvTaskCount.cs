using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for task count.
    /// C++ Reader: crygame.dll+sub_10125700 (UnkTlv0030)
    /// C++ Printer: crygame.dll+sub_101257E0
    /// </summary>
    public class TlvTaskCount : Structure, ITlvStructure
    {
        /// <summary>
        /// Task identifier (short).
        /// Field ID: 1
        /// </summary>
        public short Task { get; set; }

        /// <summary>
        /// Count value.
        /// Field ID: 2
        /// </summary>
        public byte Count { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, Task);
            WriteTlvByte(buffer, 2, Count);
        }
    }
}
