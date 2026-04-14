using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for skill slot data.
    /// C++ Reader: crygame.dll+sub_101A2180 (UnkTlv0182)
    /// C++ Printer: crygame.dll+sub_101A28A0
    /// </summary>
    public class TlvSkillSlotData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxSlots = 10;

        /// <summary>
        /// Skill count (derived from list).
        /// Field ID: 1
        /// </summary>
        public short Count => (short)(Skill?.Count ?? 0);

        /// <summary>
        /// Skills (type + id pairs).
        /// Field ID: 2
        /// </summary>
        public List<TlvIdSlot> Skill { get; set; }

        /// <summary>
        /// Slot count (derived from list).
        /// Field ID: 3
        /// </summary>
        public short SlotCount => (short)(SlotLock?.Count ?? 0);

        /// <summary>
        /// Slot lock data.
        /// Field ID: 4
        /// </summary>
        public List<TlvTypeLockInfo> SlotLock { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Skill?.Count ?? 0) > MaxSlots)
                throw new InvalidDataException($"[TlvSkillSlotData] Skill exceeds the maximum of {MaxSlots} elements.");
            if ((SlotLock?.Count ?? 0) > MaxSlots)
                throw new InvalidDataException($"[TlvSkillSlotData] SlotLock exceeds the maximum of {MaxSlots} elements.");

            WriteTlvInt16(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Skill.Count, Skill);
            WriteTlvInt16(buffer, 3, SlotCount);
            WriteTlvSubStructureList(buffer, 4, SlotLock.Count, SlotLock);
        }
    }
}
