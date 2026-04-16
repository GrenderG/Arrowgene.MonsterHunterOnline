using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Equipment Loadout / Plan).
    /// C++ Writer: crygame.dll+sub_10110C50
    /// C++ Printer: crygame.dll+sub_101113F0
    /// </summary>
    public class TlvEquipPlan : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxNameLength = 20; // 0x14u
        public const int MaxEquipCount = 10; // 0x0Au

        public byte PlanId { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<TlvEquipItem> EquipList { get; set; } = new List<TlvEquipItem>();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // You can still do your safety check before writing
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvEquipPlan] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

// TODO boundary:             if (EquipList.Count > MaxEquipCount)
// TODO boundary:                 throw new InvalidDataException($"[TlvEquipPlan] EquipList count exceeds maximum of {MaxEquipCount}.");

            // --- SERIALIZATION ---
            WriteTlvByte(buffer, 1, PlanId);
            WriteTlvString(buffer, 2, Name);
            WriteTlvByte(buffer, 3, (byte)EquipList.Count);
            WriteTlvSubStructureList(buffer, 4, EquipList.Count, EquipList);
        }
    }
}