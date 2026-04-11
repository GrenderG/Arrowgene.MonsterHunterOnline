using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for complete bits and illustrate card info.
    /// C++ Reader: crygame.dll+sub_1018D110 (UnkTlv0159)
    /// C++ Printer: crygame.dll+sub_1018D540
    /// </summary>
    public class TlvCompleteBitCards : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxCompleteBits = 320;
        public const int MaxCards = 1024;

        /// <summary>
        /// Complete bit count (derived from array).
        /// Field ID: 1
        /// </summary>
        public int CompleteBitCount => CompleteBit?.Length ?? 0;

        /// <summary>
        /// Complete bit data (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] CompleteBit { get; set; }

        /// <summary>
        /// Illustrate card count (derived from array).
        /// Field ID: 3
        /// </summary>
        public int IllustrateCardCount => IllustrateCardInfo?.Length ?? 0;

        /// <summary>
        /// Illustrate card info (int array).
        /// Field ID: 4
        /// </summary>
        public int[] IllustrateCardInfo { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // completeBitCount, derived
                    case 2:
                        int len2 = reader.ReadInt();
                        if (len2 > 0 && len2 <= MaxCompleteBits)
                            CompleteBit = reader.ReadBytes(len2);
                        break;
                    case 3: reader.ReadInt(); break; // illustrateCardCount, derived
                    case 4: IllustrateCardInfo = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((CompleteBit?.Length ?? 0) > MaxCompleteBits)
                throw new InvalidDataException($"[TlvCompleteBitCards] CompleteBit exceeds the maximum of {MaxCompleteBits} elements.");
            if ((IllustrateCardInfo?.Length ?? 0) > MaxCards)
                throw new InvalidDataException($"[TlvCompleteBitCards] IllustrateCardInfo exceeds the maximum of {MaxCards} elements.");

            WriteTlvInt(writer, 1, CompleteBitCount);
            WriteTlvByteArray(writer, 2, CompleteBit, CompleteBitCount);
            WriteTlvInt(writer, 3, IllustrateCardCount);
            WriteTlvIntArray(writer, 4, IllustrateCardInfo, IllustrateCardCount);
        }
    }
}
