using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for element slot (item ID + enhance count).
    /// C++ Reader: crygame.dll+sub_101853A0 (inner of UnkTlv0151)
    /// C++ Printer: crygame.dll+sub_10185540
    /// </summary>
    public class TlvElementSlot : Structure, ITlvStructure
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
        public int EnchanseCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, ItemId);
            WriteTlvInt32(buffer, 2, EnchanseCount);
        }
    }
}
