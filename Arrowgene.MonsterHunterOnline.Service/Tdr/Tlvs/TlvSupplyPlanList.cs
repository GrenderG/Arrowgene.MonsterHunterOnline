using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Container for Supply / Item Pouch Loadouts).
    /// C++ Reader: crygame.dll+sub_10114B70
    /// C++ Printer: crygame.dll+sub_10114E80
    /// </summary>
    public class TlvSupplyPlanList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        // A player can only have up to 5 saved item pouch plans!
        public const int MaxPlans = 5;

        public byte CurPlan { get; set; }

        // The PlanCnt (Field 2) is dynamically derived from this list.
        public List<TlvSupplyPlan> SupplyPlanList { get; set; } = new List<TlvSupplyPlan>();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1:
                        CurPlan = reader.ReadByte();
                        break;

                    case 2:
                        // Field 2 is [planCnt]. 
                        // Read and discard it, relying completely on the list length.
                        reader.ReadByte();
                        break;

                    case 3:
                        // Field 3 is [supplyPlanList] (WireType 5 - Length Delimited List)
                        SupplyPlanList = ReadTlvList<TlvSupplyPlan>(reader);
                        break;

                    default:
                        SkipTlvField(reader, wireType);
                        break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECKS ---
            if (SupplyPlanList.Count > MaxPlans)
                throw new InvalidDataException($"[TlvSupplyPlanList] SupplyPlanList count ({SupplyPlanList.Count}) exceeds maximum of {MaxPlans}.");

            // --- SERIALIZATION ---
            WriteTlvByte(writer, 1, CurPlan);

            // Re-inject the dynamically calculated count as Field 2
            WriteTlvByte(writer, 2, (byte)SupplyPlanList.Count);

            // Write the length-delimited list as Field 3
            WriteTlvList(writer, 3, SupplyPlanList);
        }
    }
}