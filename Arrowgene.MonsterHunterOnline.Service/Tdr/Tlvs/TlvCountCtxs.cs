using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for count + int array (200 max).
    /// C++ Reader: crygame.dll+sub_10174E00 (UnkTlv0127)
    /// C++ Printer: crygame.dll+sub_10175340
    /// </summary>
    public class TlvCountCtxs : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxElements = 200;

        /// <summary>
        /// Element count (derived from Data array).
        /// Field ID: 1
        /// </summary>
        public int Count => Ctxs?.Length ?? 0;

        /// <summary>
        /// Context values (int array).
        /// Field ID: 2
        /// </summary>
        public int[] Ctxs { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // count, derived from array
                    case 2: Ctxs = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Ctxs?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCountCtxs] Ctxs exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvIntArray(writer, 2, Ctxs, Count);
        }
    }
}
