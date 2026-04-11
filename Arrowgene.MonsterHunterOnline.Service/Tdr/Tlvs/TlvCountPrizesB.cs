using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for prize count + int array (3 max).
    /// C++ Reader: crygame.dll+sub_1012ACA0 (UnkTlv0037)
    /// C++ Printer: crygame.dll+sub_1012B0C0
    /// </summary>
    public class TlvCountPrizesB : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxElements = 3;

        /// <summary>
        /// Prize count (derived from Data array).
        /// Field ID: 1
        /// </summary>
        public int PrizeCount => Data?.Length ?? 0;

        /// <summary>
        /// Prize data array.
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
                throw new InvalidDataException($"[TlvCountPrizesB] Data exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt(writer, 1, PrizeCount);
            WriteTlvIntArray(writer, 2, Data, PrizeCount);
        }
    }
}
