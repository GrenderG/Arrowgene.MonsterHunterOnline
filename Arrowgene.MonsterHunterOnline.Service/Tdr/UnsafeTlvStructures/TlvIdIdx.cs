using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for ID with index byte.
    /// C++ Reader: crygame.dll+sub_101EE9E0 (UnkTlv0200)
    /// C++ Printer: crygame.dll+sub_101EEAF0
    /// </summary>
    public class TlvIdIdx : Structure, ITlvStructure
    {
        /// <summary>
        /// ID value.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Index byte value.
        /// Field ID: 2
        /// </summary>
        public byte Idx { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvByte(buffer, 2, Idx);
        }
    }
}
