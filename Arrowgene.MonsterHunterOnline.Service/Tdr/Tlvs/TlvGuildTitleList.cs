using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild title list.
    /// C++ Reader: crygame.dll+sub_1011F020 (UnkTlv0022)
    /// C++ Printer: crygame.dll+sub_1011F430
    /// </summary>
    public class TlvGuildTitleList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxTitles = 16;

        /// <summary>
        /// Title count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => Titles?.Count ?? 0;

        /// <summary>
        /// Title entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvGuildTitleData> Titles { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // count, derived
                    case 2: Titles = ReadTlvList<TlvGuildTitleData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Titles?.Count ?? 0) > MaxTitles)
                throw new InvalidDataException($"[TlvGuildTitleList] Titles exceeds the maximum of {MaxTitles} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Titles);
        }
    }
}
