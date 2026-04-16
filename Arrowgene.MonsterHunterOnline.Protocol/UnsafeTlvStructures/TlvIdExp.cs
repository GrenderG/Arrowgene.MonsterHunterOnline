using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for ID and experience value.
    /// C++ Reader: crygame.dll+sub_1019E340 (UnkTlv0174)
    /// C++ Printer: crygame.dll+sub_1019E450
    /// </summary>
    public class TlvIdExp : Structure, ITlvStructure
    {
        /// <summary>
        /// ID value.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Experience value.
        /// Field ID: 2
        /// </summary>
        public byte Exp { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvByte(buffer, 2, Exp);
        }
    }
}
