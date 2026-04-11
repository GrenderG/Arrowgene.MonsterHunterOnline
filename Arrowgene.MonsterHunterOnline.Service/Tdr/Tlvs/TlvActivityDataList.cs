using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for activity data list.
    /// C++ Reader: crygame.dll+sub_10156350 (UnkTlv0087)
    /// C++ Printer: crygame.dll+sub_10156960
    /// </summary>
    public class TlvActivityDataList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxActivities = 56;

        /// <summary>
        /// Activity count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int Count => Data?.Count ?? 0;

        /// <summary>
        /// Activity data entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvActivityData> Data { get; set; }

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
                    case 2: Data = ReadTlvList<TlvActivityData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Data?.Count ?? 0) > MaxActivities)
                throw new InvalidDataException($"[TlvActivityDataList] Data exceeds the maximum of {MaxActivities} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Data);
        }
    }
}
