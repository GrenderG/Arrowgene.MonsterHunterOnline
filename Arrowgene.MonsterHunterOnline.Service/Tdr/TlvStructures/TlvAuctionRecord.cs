using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for auction record.
    /// C++ Reader: crygame.dll+sub_102385A0 (UnkTlv0281)
    /// C++ Printer: crygame.dll+sub_10238C30
    /// </summary>
    public class TlvAuctionRecord : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

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
        /// Expire time.
        /// Field ID: 4
        /// </summary>
        public uint ExpireTime { get; set; }

        /// <summary>
        /// Money.
        /// Field ID: 5
        /// </summary>
        public uint Money { get; set; }

        /// <summary>
        /// Credit.
        /// Field ID: 6
        /// </summary>
        public uint Credit { get; set; }

        /// <summary>
        /// Record time.
        /// Field ID: 7
        /// </summary>
        public uint RecordTime { get; set; }

        /// <summary>
        /// Bid database ID.
        /// Field ID: 8
        /// </summary>
        public ulong BidDbId { get; set; }

        /// <summary>
        /// Bid role name.
        /// Field ID: 9
        /// </summary>
        public string BidRoleName { get; set; } = string.Empty;

        /// <summary>
        /// Bid level.
        /// Field ID: 10
        /// </summary>
        public uint BidLevel { get; set; }

        /// <summary>
        /// UIN.
        /// Field ID: 11
        /// </summary>
        public uint Uin { get; set; }

        /// <summary>
        /// Bid UIN.
        /// Field ID: 12
        /// </summary>
        public uint BidUin { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(RoleName) && Encoding.UTF8.GetByteCount(RoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvAuctionRecord] RoleName exceeds or equals the maximum of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(BidRoleName) && Encoding.UTF8.GetByteCount(BidRoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvAuctionRecord] BidRoleName exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvInt64(buffer, 1, (long)RecordId);
            WriteTlvInt64(buffer, 2, (long)DbId);
            WriteTlvString(buffer, 3, RoleName);
            WriteTlvInt32(buffer, 4, (int)ExpireTime);
            WriteTlvInt32(buffer, 5, (int)Money);
            WriteTlvInt32(buffer, 6, (int)Credit);
            WriteTlvInt32(buffer, 7, (int)RecordTime);
            WriteTlvInt64(buffer, 8, (long)BidDbId);
            WriteTlvString(buffer, 9, BidRoleName);
            WriteTlvInt32(buffer, 10, (int)BidLevel);
            WriteTlvInt32(buffer, 11, (int)Uin);
            WriteTlvInt32(buffer, 12, (int)BidUin);
        }
    }
}
