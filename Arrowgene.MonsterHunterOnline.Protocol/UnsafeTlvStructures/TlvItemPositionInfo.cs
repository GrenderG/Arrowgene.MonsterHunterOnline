using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for item position info.
    /// C++ Reader: crygame.dll+sub_10191590 (UnkTlv0166)
    /// C++ Printer: crygame.dll+sub_10191700
    /// </summary>
    public class TlvItemPositionInfo : Structure, ITlvStructure
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
        /// Item number.
        /// Field ID: 3
        /// </summary>
        public int ItemNum { get; set; }

        /// <summary>
        /// Quality.
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
