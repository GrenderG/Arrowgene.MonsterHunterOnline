using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvTypeTrace.
    /// C++ Reader: crygame.dll+sub_1017D230 (UnkTlv0140)
    /// C++ Printer: crygame.dll+sub_1017DA00
    /// </summary>
    public class TlvTypeTraceList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxTraces = 50;

        /// <summary>
        /// Count (derived from Data).
        /// Field ID: 1
        /// </summary>
        public int Count => Data?.Count ?? 0;

        /// <summary>
        /// Trace sets.
        /// Field ID: 2
        /// </summary>
        public List<TlvTypeTrace> Data { get; set; }

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
                    case 2: Data = ReadTlvList<TlvTypeTrace>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Data?.Count ?? 0) > MaxTraces)
                throw new InvalidDataException($"[TlvTypeTraceList] Data exceeds the maximum of {MaxTraces} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Data);
        }
    }
}
