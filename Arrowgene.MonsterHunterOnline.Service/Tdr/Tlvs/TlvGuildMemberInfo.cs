using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild member info.
    /// C++ Reader: crygame.dll+sub_10123180 (UnkTlv0026)
    /// C++ Printer: crygame.dll+sub_10123940
    /// </summary>
    public class TlvGuildMemberInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxHunterStarLen = 128;
        public const int MaxNoteLen = 256;

        /// <summary>
        /// Member ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Role info.
        /// Field ID: 2
        /// </summary>
        public TlvUserInfo Role { get; set; } = new();

        /// <summary>
        /// Level.
        /// Field ID: 3
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Hunter star string.
        /// Field ID: 4
        /// </summary>
        public string HunterStar { get; set; } = string.Empty;

        /// <summary>
        /// Note string.
        /// Field ID: 5
        /// </summary>
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Time.
        /// Field ID: 6
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// HR level.
        /// Field ID: 7
        /// </summary>
        public int HRLevel { get; set; }

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
                    case 2: Role = ReadTlvObject<TlvUserInfo>(reader); break;
                    case 3: Level = reader.ReadInt(); break;
                    case 4:
                        int starLen = reader.ReadInt();
                        if (starLen > 0 && starLen < MaxHunterStarLen)
                            HunterStar = Encoding.UTF8.GetString(reader.ReadBytes(starLen));
                        break;
                    case 5:
                        int noteLen = reader.ReadInt();
                        if (noteLen > 0 && noteLen < MaxNoteLen)
                            Note = Encoding.UTF8.GetString(reader.ReadBytes(noteLen));
                        break;
                    case 6: Time = reader.ReadInt(); break;
                    case 7: HRLevel = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvObject(writer, 2, Role);
            WriteTlvInt(writer, 3, Level);

            byte[] starBytes = Encoding.UTF8.GetBytes(HunterStar ?? string.Empty);
            if (starBytes.Length > 0)
            {
                writer.WriteVarUInt((4u << 4) | 5);
                writer.WriteInt(starBytes.Length);
                writer.WriteBytes(starBytes);
            }

            byte[] noteBytes = Encoding.UTF8.GetBytes(Note ?? string.Empty);
            if (noteBytes.Length > 0)
            {
                writer.WriteVarUInt((5u << 4) | 5);
                writer.WriteInt(noteBytes.Length);
                writer.WriteBytes(noteBytes);
            }

            WriteTlvInt(writer, 6, Time);
            WriteTlvInt(writer, 7, HRLevel);
        }
    }
}
