using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for seed slot info.
    /// C++ Reader: crygame.dll+sub_10200330 (UnkTlv0212)
    /// C++ Printer: crygame.dll+sub_10200470
    /// </summary>
    public class TlvSeedSlot : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Open flag.
        /// Field ID: 1
        /// </summary>
        public byte Open { get; set; }

        /// <summary>
        /// Seed ID.
        /// Field ID: 2
        /// </summary>
        public int SeedId { get; set; }

        /// <summary>
        /// Seed time.
        /// Field ID: 3
        /// </summary>
        public int SeedTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Open = reader.ReadByte(); break;
                    case 2: SeedId = reader.ReadInt(); break;
                    case 3: SeedTime = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, Open);
            WriteTlvInt(writer, 2, SeedId);
            WriteTlvInt(writer, 3, SeedTime);
        }
    }
}
