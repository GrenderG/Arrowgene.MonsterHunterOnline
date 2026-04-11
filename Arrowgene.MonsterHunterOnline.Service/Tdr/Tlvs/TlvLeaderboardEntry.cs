using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for leaderboard entry with score.
    /// C++ Reader: crygame.dll+sub_1013A390 (UnkTlv0051)
    /// C++ Printer: crygame.dll+sub_1013A5D0
    /// </summary>
    public class TlvLeaderboardEntry : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Maximum name length.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Player score.
        /// Field ID: 1
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Database ID.
        /// Field ID: 2
        /// </summary>
        public ulong DbId { get; set; }

        /// <summary>
        /// Player name.
        /// Field ID: 3
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// User identification number.
        /// Field ID: 4
        /// </summary>
        public ulong Uin { get; set; }

        /// <summary>
        /// Time value.
        /// Field ID: 5
        /// </summary>
        public int Time { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Score = reader.ReadInt(); break;
                    case 2: DbId = (ulong)reader.ReadLong(); break;
                    case 3:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            Name = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    case 4: Uin = (ulong)reader.ReadLong(); break;
                    case 5: Time = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvLeaderboardEntry] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            // --- SERIALIZATION ---
            WriteTlvInt(writer, 1, Score);
            WriteTlvULong(writer, 2, DbId);
            WriteTlvString(writer, 3, Name);
            WriteTlvULong(writer, 4, Uin);
            WriteTlvInt(writer, 5, Time);
        }
    }
}
