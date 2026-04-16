using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for shop buy limit data.
    /// C++ Reader: crygame.dll+sub_1020E700 (UnkTlv0224)
    /// C++ Printer: crygame.dll+sub_1020F2F0
    /// </summary>
    public class TlvShopBuyLimitData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxLimits = 256;

        /// <summary>
        /// Last reset time.
        /// Field ID: 1
        /// </summary>
        public long LastResetTm { get; set; }

        /// <summary>
        /// Limit data count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public byte LimitDataCnt => (byte)(ShopType?.Length ?? 0);

        /// <summary>
        /// Shop types (byte array).
        /// Field ID: 5
        /// </summary>
        public byte[] ShopType { get; set; }

        /// <summary>
        /// Shop IDs (int array).
        /// Field ID: 6
        /// </summary>
        public int[] ShopID { get; set; }

        /// <summary>
        /// Sale IDs (int array).
        /// Field ID: 7
        /// </summary>
        public int[] SaleID { get; set; }

        /// <summary>
        /// Buy counts (short array).
        /// Field ID: 8
        /// </summary>
        public short[] BuyCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((ShopType?.Length ?? 0) > MaxLimits)
                throw new InvalidDataException($"[TlvShopBuyLimitData] ShopType exceeds the maximum of {MaxLimits} elements.");
            if ((ShopID?.Length ?? 0) > MaxLimits)
                throw new InvalidDataException($"[TlvShopBuyLimitData] ShopID exceeds the maximum of {MaxLimits} elements.");
            if ((SaleID?.Length ?? 0) > MaxLimits)
                throw new InvalidDataException($"[TlvShopBuyLimitData] SaleID exceeds the maximum of {MaxLimits} elements.");
            if ((BuyCount?.Length ?? 0) > MaxLimits)
                throw new InvalidDataException($"[TlvShopBuyLimitData] BuyCount exceeds the maximum of {MaxLimits} elements.");

            WriteTlvInt64(buffer, 1, LastResetTm);
            WriteTlvByte(buffer, 2, LimitDataCnt);
            WriteTlvByteArr(buffer, 5, ShopType);
            WriteTlvInt32Arr(buffer, 6, ShopID);
            WriteTlvInt32Arr(buffer, 7, SaleID);
            WriteTlvInt16Arr(buffer, 8, BuyCount);
        }
    }
}
