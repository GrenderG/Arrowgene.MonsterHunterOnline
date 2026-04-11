using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for player list with guild info.
    /// C++ Reader: crygame.dll+sub_1011A600 (UnkTlv0016)
    /// C++ Printer: crygame.dll+sub_1011ABA0
    /// </summary>
    public class TlvPlayerListGuild : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxPlayers = 256;

        /// <summary>
        /// Player count (derived from PlayerIds array).
        /// Field ID: 1
        /// </summary>
        public int PlayerCount => PlayerIds?.Length ?? 0;

        /// <summary>
        /// Player IDs (long array).
        /// Field ID: 2
        /// </summary>
        public long[] PlayerIds { get; set; }

        /// <summary>
        /// Own guild ID.
        /// Field ID: 3
        /// </summary>
        public ulong OwnGuildId { get; set; }

        /// <summary>
        /// Minimum time.
        /// Field ID: 4
        /// </summary>
        public uint MinTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break;
                    case 2: PlayerIds = ReadTlvLongArray(reader); break;
                    case 3: OwnGuildId = (ulong)reader.ReadLong(); break;
                    case 4: MinTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((PlayerIds?.Length ?? 0) > MaxPlayers)
                throw new InvalidDataException($"[TlvPlayerListGuild] PlayerIds exceeds the maximum of {MaxPlayers} elements.");

            WriteTlvInt(writer, 1, PlayerCount);
            WriteTlvLongArray(writer, 2, PlayerIds);
            WriteTlvLong(writer, 3, (long)OwnGuildId);
            WriteTlvInt(writer, 4, (int)MinTime);
        }
    }
}
