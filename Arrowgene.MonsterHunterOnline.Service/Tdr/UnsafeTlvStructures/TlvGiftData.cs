using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for gift data with nested gift attributes.
    /// C++ Reader: crygame.dll+sub_1024CA40 (UnkTlv0299)
    /// C++ Printer: crygame.dll+sub_1024CF20
    /// </summary>
    public class TlvGiftData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxGifts = 100;

        /// <summary>
        /// Gift attributes (nested sub-structure).
        /// Field ID: 2
        /// </summary>
        public TlvOnlineConsecRefresh GiftAttr { get; set; } = new();

        /// <summary>
        /// Gift count (derived from arrays).
        /// Field ID: 3
        /// </summary>
        public byte GiftNum => (byte)(GiftId?.Length ?? 0);

        /// <summary>
        /// Gift IDs (int array).
        /// Field ID: 4
        /// </summary>
        public int[] GiftId { get; set; }

        /// <summary>
        /// Gift states (byte array).
        /// Field ID: 5
        /// </summary>
        public byte[] GiftState { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((GiftId?.Length ?? 0) > MaxGifts)
                throw new InvalidDataException($"[TlvGiftData] GiftId exceeds the maximum of {MaxGifts} elements.");
            if ((GiftState?.Length ?? 0) > MaxGifts)
                throw new InvalidDataException($"[TlvGiftData] GiftState exceeds the maximum of {MaxGifts} elements.");

            WriteTlvSubStructure(buffer, 2, GiftAttr);
            WriteTlvByte(buffer, 3, GiftNum);
            WriteTlvInt32Arr(buffer, 4, GiftId);
            WriteTlvByteArr(buffer, 5, GiftState);
        }
    }
}
