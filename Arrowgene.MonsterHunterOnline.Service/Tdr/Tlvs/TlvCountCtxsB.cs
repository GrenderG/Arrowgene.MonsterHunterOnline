using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for count + int array (30 max).
    /// C++ Reader: crygame.dll+sub_1012A460 (UnkTlv0036)
    /// C++ Printer: crygame.dll+sub_1012A8E0
    /// </summary>
    public class TlvCountCtxsB : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxElements = 30;

        /// <summary>
        /// Element count (derived from Data array).
        /// Field ID: 1
        /// </summary>
        public int Count => Data?.Length ?? 0;

        /// <summary>
        /// Integer data array.
        /// Field ID: 2
        /// </summary>
        public int[] Data { get; set; }

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
                    case 2: Data = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Data?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCountCtxsB] Data exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvIntArray(writer, 2, Data, Count);
        }
    }
}
