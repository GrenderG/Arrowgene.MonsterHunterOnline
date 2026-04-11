using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Equipped Item with Bead Slots).
    /// C++ Writer: crygame.dll+sub_1010FFF0
    /// C++ Printer: crygame.dll+sub_101106B0
    /// </summary>
    public class TlvEquipItem : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Exact Boundary ---
        // Equipment in MH ALWAYS expects exactly 3 jewel/bead slots in the buffer!
        public const int ExactSkillBeads = 3;

        public long ItemId { get; set; }
        public int ItemType { get; set; }
        public int TargetPos { get; set; }
        public byte PosColumn { get; set; }
        public short PosGrid { get; set; }

        public List<TlvSlotItem> SkillBeadsInfo { get; set; } = new List<TlvSlotItem>();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ItemId = reader.ReadLong(); break;
                    case 2: ItemType = reader.ReadInt(); break;
                    case 3: TargetPos = reader.ReadInt(); break;
                    case 4: PosColumn = reader.ReadByte(); break;
                    case 5: PosGrid = reader.ReadShort(); break;
                    case 6: SkillBeadsInfo = ReadTlvList<TlvSlotItem>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- STRICT BOUNDARY CHECK ---
            if (SkillBeadsInfo.Count != ExactSkillBeads)
                throw new InvalidDataException($"[TlvEquipItem] SkillBeadsInfo count is {SkillBeadsInfo.Count}. It MUST be exactly {ExactSkillBeads} (pad with empty items if necessary).");

            // --- SERIALIZATION ---
            WriteTlvLong(writer, 1, ItemId);
            WriteTlvInt(writer, 2, ItemType);
            WriteTlvInt(writer, 3, TargetPos);
            WriteTlvByte(writer, 4, PosColumn);
            WriteTlvShort(writer, 5, PosGrid);
            WriteTlvList(writer, 6, SkillBeadsInfo);
        }
    }
}
