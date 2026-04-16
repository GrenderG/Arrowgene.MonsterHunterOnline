using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for item type.
    /// C++ Reader: crygame.dll+sub_1013B2E0 (UnkTlv0053)
    /// C++ Printer: crygame.dll+sub_1013B4D0
    /// </summary>
    public class TlvItemType : Structure, ITlvStructure
    {
        /// <summary>
        /// Item type.
        /// Field ID: 1
        /// </summary>
        public int ItemType { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, ItemType);
        }
    }
}
