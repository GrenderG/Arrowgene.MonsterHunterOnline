using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for task + count (variant B).
    /// C++ Reader: crygame.dll+sub_1021FBB0 (UnkTlv0248)
    /// C++ Printer: crygame.dll+sub_1021FFA0
    /// </summary>
    public class TlvTaskCountB : Structure, ITlvStructure
    {
        /// <summary>
        /// Task ID.
        /// Field ID: 1
        /// </summary>
        public short Task { get; set; }

        /// <summary>
        /// Count.
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
