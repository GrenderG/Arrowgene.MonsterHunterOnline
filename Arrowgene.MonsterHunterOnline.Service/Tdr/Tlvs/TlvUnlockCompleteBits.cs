using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for unlock/complete bits and new finish card list.
    /// C++ Reader: crygame.dll+sub_10200A30 (UnkTlv0240)
    /// C++ Printer: crygame.dll+sub_10201520
    /// </summary>
    public class TlvUnlockCompleteBits : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxBits = 2500;
        public const int MaxNewCards = 10;

        /// <summary>
        /// Unlock bit count (derived from array).
        /// Field ID: 1
        /// </summary>
        public int UnlockBitCount => UnlockBit?.Length ?? 0;

        /// <summary>
        /// Unlock bit data (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] UnlockBit { get; set; }

        /// <summary>
        /// Complete bit count (derived from array).
        /// Field ID: 3
        /// </summary>
        public int CompleteBitCount => CompleteBit?.Length ?? 0;

        /// <summary>
        /// Complete bit data (byte array).
        /// Field ID: 4
        /// </summary>
        public byte[] CompleteBit { get; set; }

        /// <summary>
        /// New finish card count (derived from array).
        /// Field ID: 5
        /// </summary>
        public short NewFinishCardNum => (short)(NewFinishCardList?.Length ?? 0);

        /// <summary>
        /// New finish card IDs (int array).
        /// Field ID: 6
        /// </summary>
        public int[] NewFinishCardList { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // unlockBitCount, derived
                    case 2:
                        int len2 = reader.ReadInt();
                        if (len2 > 0 && len2 <= MaxBits)
                            UnlockBit = reader.ReadBytes(len2);
                        break;
                    case 3: reader.ReadInt(); break; // completeBitCount, derived
                    case 4:
                        int len4 = reader.ReadInt();
                        if (len4 > 0 && len4 <= MaxBits)
                            CompleteBit = reader.ReadBytes(len4);
                        break;
                    case 5: reader.ReadShort(); break; // newFinishCardNum, derived
                    case 6: NewFinishCardList = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((UnlockBit?.Length ?? 0) > MaxBits)
                throw new InvalidDataException($"[TlvUnlockCompleteBits] UnlockBit exceeds the maximum of {MaxBits} elements.");
            if ((CompleteBit?.Length ?? 0) > MaxBits)
                throw new InvalidDataException($"[TlvUnlockCompleteBits] CompleteBit exceeds the maximum of {MaxBits} elements.");
            if ((NewFinishCardList?.Length ?? 0) > MaxNewCards)
                throw new InvalidDataException($"[TlvUnlockCompleteBits] NewFinishCardList exceeds the maximum of {MaxNewCards} elements.");

            WriteTlvInt(writer, 1, UnlockBitCount);
            WriteTlvByteArray(writer, 2, UnlockBit, UnlockBitCount);
            WriteTlvInt(writer, 3, CompleteBitCount);
            WriteTlvByteArray(writer, 4, CompleteBit, CompleteBitCount);
            WriteTlvShort(writer, 5, NewFinishCardNum);
            WriteTlvIntArray(writer, 6, NewFinishCardList, NewFinishCardNum);
        }
    }
}
