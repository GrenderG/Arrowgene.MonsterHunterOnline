using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvLeaderboardEntry.
    /// C++ Reader: crygame.dll+sub_1013AAE0 (UnkTlv0052)
    /// </summary>
    public class TlvSculptureLeaderboard : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Sculptures).
        /// Field ID: 1
        /// </summary>
        public int Count => Sculptures?.Count ?? 0;

        /// <summary>
        /// List of TlvLeaderboardEntry.
        /// Field ID: 2
        /// </summary>
        public List<TlvLeaderboardEntry> Sculptures { get; set; }

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
                    case 2: Sculptures = ReadTlvList<TlvLeaderboardEntry>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Sculptures);
        }
    }
}
