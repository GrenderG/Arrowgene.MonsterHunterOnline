using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for skill slot data.
    /// C++ Reader: crygame.dll+sub_101A2180 (UnkTlv0182)
    /// C++ Printer: crygame.dll+sub_101A28A0
    /// </summary>
    public class TlvSkillSlotData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadShort(); break; // count, derived
                    case 2: Skill = ReadTlvList<TlvIdSlot>(reader); break;
                    case 3: reader.ReadShort(); break; // slotCount, derived
                    case 4: SlotLock = ReadTlvList<TlvTypeLockInfo>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Skill?.Count ?? 0) > MaxSlots)
                throw new InvalidDataException($"[TlvSkillSlotData] Skill exceeds the maximum of {MaxSlots} elements.");
            if ((SlotLock?.Count ?? 0) > MaxSlots)
                throw new InvalidDataException($"[TlvSkillSlotData] SlotLock exceeds the maximum of {MaxSlots} elements.");

            WriteTlvShort(writer, 1, Count);
            WriteTlvList(writer, 2, Skill);
            WriteTlvShort(writer, 3, SlotCount);
            WriteTlvList(writer, 4, SlotLock);
        }
    }
}
