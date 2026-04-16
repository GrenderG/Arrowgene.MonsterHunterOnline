using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for position item with quality.
    /// C++ Reader: crygame.dll+sub_101900E0 (UnkTlv0164)
    /// C++ Printer: crygame.dll+sub_101903D0
    /// </summary>
    public class TlvPositionItemQuality : Structure, ITlvStructure
    {
        /// <summary>
        /// Position.
        /// Field ID: 1
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Item ID.
        /// Field ID: 2
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Item quantity.
        /// Field ID: 3
        /// </summary>
        public int ItemNum { get; set; }

        /// <summary>
        /// Item quality.
        /// Field ID: 4
        /// </summary>
        public int Quality { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Position);
            WriteTlvInt32(buffer, 2, ItemId);
            WriteTlvInt32(buffer, 3, ItemNum);
            WriteTlvInt32(buffer, 4, Quality);
        }
    }
}
