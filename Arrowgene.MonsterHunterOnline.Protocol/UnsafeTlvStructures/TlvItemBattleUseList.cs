using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Container for Items Used in Battle).
    /// C++ Reader: crygame.dll+sub_10112E50
    /// C++ Printer: crygame.dll+sub_101130B0
    /// </summary>
    public class TlvItemBattleUseList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxItemBattleUse = 256;

        public List<TlvItemBattleUseCount> ItemBattleUse { get; set; } = new List<TlvItemBattleUseCount>();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
// TODO boundary:             if (ItemBattleUse.Count > MaxItemBattleUse)
// TODO boundary:                 throw new InvalidDataException($"[TlvItemBattleUseList] ItemBattleUse count ({ItemBattleUse.Count}) exceeds maximum of {MaxItemBattleUse}.");

            // --- SERIALIZATION ---

            // Re-inject the Count directly as Field 1
            WriteTlvInt32(buffer, 1, ItemBattleUse.Count);

            // Write the length-delimited list as Field 2
            WriteTlvSubStructureList(buffer, 2, ItemBattleUse.Count, ItemBattleUse);
        }
    }
}