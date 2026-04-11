using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for pet avatar data.
    /// C++ Reader: crygame.dll+sub_10200640 (UnkTlv0213)
    /// C++ Printer: crygame.dll+sub_10200DF0
    /// </summary>
    public class TlvPetAvatarData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int AvatarInfoSize = 6;

        /// <summary>
        /// Pet ID.
        /// Field ID: 1
        /// </summary>
        public int PetID { get; set; }

        /// <summary>
        /// Skin ID.
        /// Field ID: 2
        /// </summary>
        public int SkinID { get; set; }

        /// <summary>
        /// Avatar info (int array, exactly 6).
        /// Field ID: 3
        /// </summary>
        public int[] AvatarInfo { get; set; } = new int[AvatarInfoSize];

        /// <summary>
        /// Sex.
        /// Field ID: 4
        /// </summary>
        public byte Sex { get; set; }

        /// <summary>
        /// Slot.
        /// Field ID: 5
        /// </summary>
        public int Slot { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: PetID = reader.ReadInt(); break;
                    case 2: SkinID = reader.ReadInt(); break;
                    case 3: AvatarInfo = ReadTlvIntArray(reader); break;
                    case 4: Sex = reader.ReadByte(); break;
                    case 5: Slot = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, PetID);
            WriteTlvInt(writer, 2, SkinID);
            WriteStrictIntArray(writer, 3, AvatarInfo, AvatarInfoSize);
            WriteTlvByte(writer, 4, Sex);
            WriteTlvInt(writer, 5, Slot);
        }
    }
}
