using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for count + int array (10 max).
    /// C++ Reader: crygame.dll+sub_1021C4D0 (UnkTlv0244)
    /// C++ Printer: crygame.dll+sub_1021C920
    /// </summary>
    public class TlvCountCards : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxElements = 10;

        /// <summary>
        /// Element count (derived from Data array).
        /// Field ID: 1
        /// </summary>
        public int Count => Cards?.Length ?? 0;

        /// <summary>
        /// Card values (int array).
        /// Field ID: 2
        /// </summary>
        public int[] Cards { get; set; }

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
                    case 2: Cards = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Cards?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvCountCards] Cards exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvIntArray(writer, 2, Cards, Count);
        }
    }
}
