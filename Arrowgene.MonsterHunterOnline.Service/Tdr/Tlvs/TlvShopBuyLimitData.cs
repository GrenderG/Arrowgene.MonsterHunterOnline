using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for shop buy limit data.
    /// C++ Reader: crygame.dll+sub_1020E700 (UnkTlv0224)
    /// C++ Printer: crygame.dll+sub_1020F2F0
    /// </summary>
    public class TlvShopBuyLimitData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LastResetTm = reader.ReadLong(); break;
                    case 2: reader.ReadByte(); break; // limitDataCnt, derived
                    case 5:
                        int len5 = reader.ReadInt();
                        if (len5 > 0 && len5 <= MaxLimits)
                            ShopType = reader.ReadBytes(len5);
                        break;
                    case 6: ShopID = ReadTlvIntArray(reader); break;
                    case 7: SaleID = ReadTlvIntArray(reader); break;
                    case 8: BuyCount = ReadTlvShortArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
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

            WriteTlvLong(writer, 1, LastResetTm);
            WriteTlvByte(writer, 2, LimitDataCnt);
            WriteTlvByteArray(writer, 5, ShopType, LimitDataCnt);
            WriteTlvIntArray(writer, 6, ShopID, LimitDataCnt);
            WriteTlvIntArray(writer, 7, SaleID, LimitDataCnt);
            WriteTlvShortArray(writer, 8, BuyCount);
        }
    }
}
