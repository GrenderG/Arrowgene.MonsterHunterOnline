using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for auction sale record with item data.
    /// C++ Reader: crygame.dll+sub_102364B0 (UnkTlv0279)
    /// C++ Printer: crygame.dll+sub_10236D70
    /// </summary>
    public class TlvAuctionSaleRecord : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;
        public const int MaxItemDataLength = 200;

        /// <summary>
        /// Record ID.
        /// Field ID: 1
        /// </summary>
        public ulong RecordId { get; set; }

        /// <summary>
        /// Database ID.
        /// Field ID: 2
        /// </summary>
        public ulong DbId { get; set; }

        /// <summary>
        /// Role name.
        /// Field ID: 3
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// Sale time.
        /// Field ID: 4
        /// </summary>
        public byte SaleTime { get; set; }

        /// <summary>
        /// Expire time.
        /// Field ID: 5
        /// </summary>
        public uint ExpireTime { get; set; }

        /// <summary>
        /// Initial price.
        /// Field ID: 6
        /// </summary>
        public uint InitPrice { get; set; }

        /// <summary>
        /// Maximum price.
        /// Field ID: 7
        /// </summary>
        public uint MaxPrice { get; set; }

        /// <summary>
        /// Current price.
        /// Field ID: 8
        /// </summary>
        public uint CurPrice { get; set; }

        /// <summary>
        /// Bid database ID.
        /// Field ID: 9
        /// </summary>
        public ulong BidDbId { get; set; }

        /// <summary>
        /// Bid role name.
        /// Field ID: 10
        /// </summary>
        public string BidRoleName { get; set; } = string.Empty;

        /// <summary>
        /// Item data size (derived from ItemData).
        /// Field ID: 11
        /// </summary>
        public short ItemDataSize => (short)(ItemData?.Length ?? 0);

        /// <summary>
        /// Item data bytes.
        /// Field ID: 12
        /// </summary>
        public byte[] ItemData { get; set; }

        /// <summary>
        /// Bid level.
        /// Field ID: 13
        /// </summary>
        public uint BidLevel { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if (!string.IsNullOrEmpty(RoleName) && Encoding.UTF8.GetByteCount(RoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvAuctionSaleRecord] RoleName exceeds max of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(BidRoleName) && Encoding.UTF8.GetByteCount(BidRoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvAuctionSaleRecord] BidRoleName exceeds max of {MaxNameLength} bytes.");

            WriteTlvInt64(buffer, 1, (long)RecordId);
            WriteTlvInt64(buffer, 2, (long)DbId);
            WriteTlvString(buffer, 3, RoleName);
            WriteTlvByte(buffer, 4, SaleTime);
            WriteTlvInt32(buffer, 5, (int)ExpireTime);
            WriteTlvInt32(buffer, 6, (int)InitPrice);
            WriteTlvInt32(buffer, 7, (int)MaxPrice);
            WriteTlvInt32(buffer, 8, (int)CurPrice);
            WriteTlvInt64(buffer, 9, (long)BidDbId);
            WriteTlvString(buffer, 10, BidRoleName);
            WriteTlvInt16(buffer, 11, ItemDataSize);
            WriteTlvByteArr(buffer, 12, ItemData);
            WriteTlvInt32(buffer, 13, (int)BidLevel);
        }
    }
}
