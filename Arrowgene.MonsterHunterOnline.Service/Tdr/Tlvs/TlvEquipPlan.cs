using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Equipment Loadout / Plan).
    /// C++ Writer: crygame.dll+sub_10110C50
    /// C++ Printer: crygame.dll+sub_101113F0
    /// </summary>
    public class TlvEquipPlan : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxNameLength = 20; // 0x14u
        public const int MaxEquipCount = 10; // 0x0Au

        public byte PlanId { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<TlvEquipItem> EquipList { get; set; } = new List<TlvEquipItem>();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: PlanId = reader.ReadByte(); break;
                    case 2: Name = reader.ReadUTF(); break;
                    case 3: reader.ReadByte(); break; // Discard EquipCnt
                    case 4: EquipList = ReadTlvList<TlvEquipItem>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // You can still do your safety check before writing
            if (!string.IsNullOrEmpty(Name) && System.Text.Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvEquipPlan] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            if (EquipList.Count > MaxEquipCount)
                throw new InvalidDataException($"[TlvEquipPlan] EquipList count exceeds maximum of {MaxEquipCount}.");

            // --- SERIALIZATION ---
            WriteTlvByte(writer, 1, PlanId);
            WriteTlvString(writer, 2, Name);
            WriteTlvByte(writer, 3, (byte)EquipList.Count);
            WriteTlvList(writer, 4, EquipList);
        }
    }
}