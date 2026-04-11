using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for typed trace data with three args.
    /// C++ Reader: crygame.dll+sub_1017CB40 (UnkTlv0139)
    /// C++ Printer: crygame.dll+sub_1017D010
    /// </summary>
    public class TlvTypeTrace : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxTrace = 5;

        /// <summary>
        /// Type.
        /// Field ID: 1
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Count (derived from Trace).
        /// Field ID: 2
        /// </summary>
        public int Count => Trace?.Count ?? 0;

        /// <summary>
        /// Trace list.
        /// Field ID: 3
        /// </summary>
        public List<TlvThreeArgs> Trace { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Type = reader.ReadByte(); break;
                    case 2: reader.ReadInt(); break; // count, derived
                    case 3: Trace = ReadTlvList<TlvThreeArgs>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Trace?.Count ?? 0) > MaxTrace)
                throw new InvalidDataException($"[TlvTypeTrace] Trace exceeds the maximum of {MaxTrace} elements.");

            WriteTlvByte(writer, 1, Type);
            WriteTlvInt(writer, 2, Count);
            WriteTlvList(writer, 3, Trace);
        }
    }
}
