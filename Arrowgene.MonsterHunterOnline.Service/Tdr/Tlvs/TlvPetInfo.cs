using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for pet info.
    /// C++ Reader: crygame.dll+sub_1019F9B0 (UnkTlv0177)
    /// C++ Printer: crygame.dll+sub_1019FE20
    /// </summary>
    public class TlvPetInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Pet ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quality.
        /// Field ID: 2
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// Character type.
        /// Field ID: 3
        /// </summary>
        public int Character { get; set; }

        /// <summary>
        /// Attack target.
        /// Field ID: 4
        /// </summary>
        public int AtkTarget { get; set; }

        /// <summary>
        /// Attack mode.
        /// Field ID: 5
        /// </summary>
        public int AtkMode { get; set; }

        /// <summary>
        /// Pet name.
        /// Field ID: 6
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Skin ID.
        /// Field ID: 7
        /// </summary>
        public int Skin { get; set; }

        /// <summary>
        /// Support skill.
        /// Field ID: 8
        /// </summary>
        public int SupportSkill { get; set; }

        /// <summary>
        /// Random type.
        /// Field ID: 9
        /// </summary>
        public int RandType { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadInt(); break;
                    case 2: Quality = reader.ReadInt(); break;
                    case 3: Character = reader.ReadInt(); break;
                    case 4: AtkTarget = reader.ReadInt(); break;
                    case 5: AtkMode = reader.ReadInt(); break;
                    case 6:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            Name = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    case 7: Skin = reader.ReadInt(); break;
                    case 8: SupportSkill = reader.ReadInt(); break;
                    case 9: RandType = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvPetInfo] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt(writer, 1, Id);
            WriteTlvInt(writer, 2, Quality);
            WriteTlvInt(writer, 3, Character);
            WriteTlvInt(writer, 4, AtkTarget);
            WriteTlvInt(writer, 5, AtkMode);
            WriteTlvString(writer, 6, Name);
            WriteTlvInt(writer, 7, Skin);
            WriteTlvInt(writer, 8, SupportSkill);
            WriteTlvInt(writer, 9, RandType);
        }
    }
}
