using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for entrust group stat list.
    /// C++ Reader: crygame.dll+sub_1014A1C0 (UnkTlv0073)
    /// C++ Printer: crygame.dll+sub_10149FE0
    /// </summary>
    public class TlvEntrustGroupStatList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxGroups = 15;

        /// <summary>
        /// Group stat count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => EntrustGroupStatInfo?.Count ?? 0;

        /// <summary>
        /// Group stat entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvGroupEntrustStatData> EntrustGroupStatInfo { get; set; }

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
                    case 2: EntrustGroupStatInfo = ReadTlvList<TlvGroupEntrustStatData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((EntrustGroupStatInfo?.Count ?? 0) > MaxGroups)
                throw new InvalidDataException($"[TlvEntrustGroupStatList] EntrustGroupStatInfo exceeds the maximum of {MaxGroups} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, EntrustGroupStatInfo);
        }
    }
}
