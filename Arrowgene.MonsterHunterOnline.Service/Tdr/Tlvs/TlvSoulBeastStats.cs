using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for soul beast stats.
    /// C++ Reader: crygame.dll+sub_10206FE0 (UnkTlv0219)
    /// C++ Printer: crygame.dll+sub_10207430
    /// </summary>
    public class TlvSoulBeastStats : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Soul beast GID.
        /// Field ID: 1
        /// </summary>
        public ulong SoulBeastGid { get; set; }

        /// <summary>
        /// Character level.
        /// Field ID: 2
        /// </summary>
        public int CharLevel { get; set; }

        /// <summary>
        /// Character experience.
        /// Field ID: 4
        /// </summary>
        public int CharExp { get; set; }

        /// <summary>
        /// Character gluttony.
        /// Field ID: 5
        /// </summary>
        public int CharGlut { get; set; }

        /// <summary>
        /// Evolve stage.
        /// Field ID: 6
        /// </summary>
        public int EvolveStage { get; set; }

        /// <summary>
        /// Image.
        /// Field ID: 7
        /// </summary>
        public int Image { get; set; }

        /// <summary>
        /// Follow flag.
        /// Field ID: 8
        /// </summary>
        public int Follow { get; set; }

        /// <summary>
        /// Feed time.
        /// Field ID: 9
        /// </summary>
        public int FeedTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: SoulBeastGid = (ulong)reader.ReadLong(); break;
                    case 2: CharLevel = reader.ReadInt(); break;
                    case 4: CharExp = reader.ReadInt(); break;
                    case 5: CharGlut = reader.ReadInt(); break;
                    case 6: EvolveStage = reader.ReadInt(); break;
                    case 7: Image = reader.ReadInt(); break;
                    case 8: Follow = reader.ReadInt(); break;
                    case 9: FeedTime = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvLong(writer, 1, (long)SoulBeastGid);
            WriteTlvInt(writer, 2, CharLevel);
            WriteTlvInt(writer, 4, CharExp);
            WriteTlvInt(writer, 5, CharGlut);
            WriteTlvInt(writer, 6, EvolveStage);
            WriteTlvInt(writer, 7, Image);
            WriteTlvInt(writer, 8, Follow);
            WriteTlvInt(writer, 9, FeedTime);
        }
    }
}
