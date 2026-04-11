using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Container for Equipment Loadouts/Plans).
    /// C++ Reader: crygame.dll+sub_10111D50
    /// C++ Printer: crygame.dll+sub_10112060
    /// </summary>
    public class TlvEquipPlanList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxPlans = 20; // 0x14u

        public byte CurPlan { get; set; }
        public byte PlanCnt { get; set; }

        public List<TlvEquipPlan> EquipPlanList { get; set; } = new List<TlvEquipPlan>();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CurPlan = reader.ReadByte(); break;
                    case 2: reader.ReadByte(); break; // Discard PlanCnt
                    case 3: EquipPlanList = ReadTlvList<TlvEquipPlan>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECKS ---
            if (EquipPlanList.Count > MaxPlans)
                throw new InvalidDataException($"[TlvEquipPlanList] EquipPlanList count ({EquipPlanList.Count}) exceeds maximum of {MaxPlans}.");

            // --- SERIALIZATION ---
            WriteTlvByte(writer, 1, CurPlan);
            WriteTlvByte(writer, 2, (byte)EquipPlanList.Count);
            WriteTlvList(writer, 3, EquipPlanList);
        }
    }
}