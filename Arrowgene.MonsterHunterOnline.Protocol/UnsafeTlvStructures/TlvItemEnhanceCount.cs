using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for item enhance count.
    /// C++ Reader: crygame.dll+sub_10185370 (UnkTlv0150)
    /// C++ Printer: crygame.dll+sub_10185450
    /// </summary>
    public class TlvItemEnhanceCount : Structure, ITlvStructure
    {
        /// <summary>
        /// Item ID.
        /// Field ID: 1
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Enhance count.
        /// Field ID: 2
        /// </summary>
        public uint EnhanceCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, ItemId);
            WriteTlvInt32(buffer, 2, (int)EnhanceCount);
        }
    }
}
