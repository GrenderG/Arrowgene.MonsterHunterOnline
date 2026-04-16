using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Item Use Count / Consumable Tracking).
    /// C++ Reader: crygame.dll+sub_10112590
    /// C++ Printer: crygame.dll+sub_101126B0
    /// </summary>
    public class TlvItemBattleUseCount : Structure, ITlvStructure
    {
        public int ItemId { get; set; }

        // Parsed manually in C++ as 2 bytes, meaning it's a Short (Int16)
        public short UseCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, ItemId);
            WriteTlvInt16(buffer, 2, UseCount);
        }
    }
}