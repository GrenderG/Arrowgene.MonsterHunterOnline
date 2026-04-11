using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for sculpture history list (count + entries).
    /// C++ Reader: crygame.dll+sub_10139980 (UnkTlv0057 internal)
    /// </summary>
    public class TlvSculptureHistoryList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public const int MaxEntries = 3;

        /// <summary>Count (derived). Field ID: 1</summary>
        public int Count => Entries?.Count ?? 0;

        /// <summary>History entries. Field ID: 2</summary>
        public List<TlvSculptureScoreEntry> Entries { get; set; }

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
                    case 2: Entries = ReadTlvList<TlvSculptureScoreEntry>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Entries?.Count ?? 0) > MaxEntries)
                throw new InvalidDataException($"[TlvSculptureHistoryList] Entries exceeds {MaxEntries}.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Entries);
        }
    }
}
