using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Container for Equipment Loadouts/Plans).
    /// C++ Reader: crygame.dll+sub_10111D50
    /// C++ Printer: crygame.dll+sub_10112060
    /// </summary>
    public class TlvEquipPlanList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxPlans = 20; // 0x14u

        public byte CurPlan { get; set; }
        public byte PlanCnt { get; set; }

        public List<TlvEquipPlan> EquipPlanList { get; set; } = new List<TlvEquipPlan>();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
// TODO boundary:             if (EquipPlanList.Count > MaxPlans)
// TODO boundary:                 throw new InvalidDataException($"[TlvEquipPlanList] EquipPlanList count ({EquipPlanList.Count}) exceeds maximum of {MaxPlans}.");

            // --- SERIALIZATION ---
            WriteTlvByte(buffer, 1, CurPlan);
            WriteTlvByte(buffer, 2, (byte)EquipPlanList.Count);
            WriteTlvSubStructureList(buffer, 3, EquipPlanList.Count, EquipPlanList);
        }
    }
}