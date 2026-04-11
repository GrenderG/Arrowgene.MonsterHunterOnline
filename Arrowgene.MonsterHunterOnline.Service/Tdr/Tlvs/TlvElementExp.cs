using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for element experience.
    /// C++ Reader: crygame.dll+sub_101856F0 (UnkTlv0151)
    /// C++ Printer: crygame.dll+sub_10185920
    /// </summary>
    public class TlvElementExp : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxSlots = 8;

        /// <summary>
        /// Water experience.
        /// Field ID: 1
        /// </summary>
        public int WaterExp { get; set; }

        /// <summary>
        /// Fire experience.
        /// Field ID: 2
        /// </summary>
        public int FireExp { get; set; }

        /// <summary>
        /// Thunder experience.
        /// Field ID: 3
        /// </summary>
        public int ThunderExp { get; set; }

        /// <summary>
        /// Dragon experience.
        /// Field ID: 4
        /// </summary>
        public int DragonExp { get; set; }

        /// <summary>
        /// Ice experience.
        /// Field ID: 5
        /// </summary>
        public int IceExp { get; set; }

        /// <summary>
        /// Duration value.
        /// Field ID: 6
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Slots (element slot list, max 8).
        /// Field ID: 7
        /// </summary>
        public List<TlvElementSlot> Slots { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: WaterExp = reader.ReadInt(); break;
                    case 2: FireExp = reader.ReadInt(); break;
                    case 3: ThunderExp = reader.ReadInt(); break;
                    case 4: DragonExp = reader.ReadInt(); break;
                    case 5: IceExp = reader.ReadInt(); break;
                    case 6: Duration = reader.ReadInt(); break;
                    case 7: Slots = ReadTlvList<TlvElementSlot>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, WaterExp);
            WriteTlvInt(writer, 2, FireExp);
            WriteTlvInt(writer, 3, ThunderExp);
            WriteTlvInt(writer, 4, DragonExp);
            WriteTlvInt(writer, 5, IceExp);
            WriteTlvInt(writer, 6, Duration);
            WriteTlvList(writer, 7, Slots);
        }
    }
}
