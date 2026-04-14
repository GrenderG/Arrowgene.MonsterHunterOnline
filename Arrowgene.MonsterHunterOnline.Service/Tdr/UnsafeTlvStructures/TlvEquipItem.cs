using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Equipped Item with Bead Slots).
    /// C++ Writer: crygame.dll+sub_1010FFF0
    /// C++ Printer: crygame.dll+sub_101106B0
    /// </summary>
    public class TlvEquipItem : Structure, ITlvStructure
    {
        // --- Hardcoded Exact Boundary ---
        // Equipment in MH ALWAYS expects exactly 3 jewel/bead slots in the buffer!
        public const int ExactSkillBeads = 3;

        public long ItemId { get; set; }
        public int ItemType { get; set; }
        public int TargetPos { get; set; }
        public byte PosColumn { get; set; }
        public short PosGrid { get; set; }

        public List<TlvSlotItem> SkillBeadsInfo { get; set; } = new List<TlvSlotItem>();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- STRICT BOUNDARY CHECK ---
// TODO boundary:             if (SkillBeadsInfo.Count != ExactSkillBeads)
// TODO boundary:                 throw new InvalidDataException($"[TlvEquipItem] SkillBeadsInfo count is {SkillBeadsInfo.Count}. It MUST be exactly {ExactSkillBeads} (pad with empty items if necessary).");

            // --- SERIALIZATION ---
            WriteTlvInt64(buffer, 1, ItemId);
            WriteTlvInt32(buffer, 2, ItemType);
            WriteTlvInt32(buffer, 3, TargetPos);
            WriteTlvByte(buffer, 4, PosColumn);
            WriteTlvInt16(buffer, 5, PosGrid);
            WriteTlvSubStructureList(buffer, 6, SkillBeadsInfo.Count, SkillBeadsInfo);
        }
    }
}
