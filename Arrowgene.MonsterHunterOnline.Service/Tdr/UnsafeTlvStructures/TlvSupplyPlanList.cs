using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Container for Supply / Item Pouch Loadouts).
    /// C++ Reader: crygame.dll+sub_10114B70
    /// C++ Printer: crygame.dll+sub_10114E80
    /// </summary>
    public class TlvSupplyPlanList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        // A player can only have up to 5 saved item pouch plans!
        public const int MaxPlans = 5;

        public byte CurPlan { get; set; }

        // The PlanCnt (Field 2) is dynamically derived from this list.
        public List<TlvSupplyPlan> SupplyPlanList { get; set; } = new List<TlvSupplyPlan>();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
// TODO boundary:             if (SupplyPlanList.Count > MaxPlans)
// TODO boundary:                 throw new InvalidDataException($"[TlvSupplyPlanList] SupplyPlanList count ({SupplyPlanList.Count}) exceeds maximum of {MaxPlans}.");

            // --- SERIALIZATION ---
            WriteTlvByte(buffer, 1, CurPlan);

            // Re-inject the dynamically calculated count as Field 2
            WriteTlvByte(buffer, 2, (byte)SupplyPlanList.Count);

            // Write the length-delimited list as Field 3
            WriteTlvSubStructureList(buffer, 3, SupplyPlanList.Count, SupplyPlanList);
        }
    }
}