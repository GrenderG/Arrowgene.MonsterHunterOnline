using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for task + time (variant B).
    /// C++ Reader: crygame.dll+sub_102220B0 (UnkTlv0252)
    /// C++ Printer: crygame.dll+sub_10222420
    /// </summary>
    public class TlvTaskTimeB : Structure, ITlvStructure
    {
        /// <summary>
        /// Task ID.
        /// Field ID: 1
        /// </summary>
        public short Task { get; set; }

        /// <summary>
        /// Time.
        /// Field ID: 2
        /// </summary>
        public uint Time { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, Task);
            WriteTlvInt32(buffer, 2, (int)Time);
        }
    }
}
