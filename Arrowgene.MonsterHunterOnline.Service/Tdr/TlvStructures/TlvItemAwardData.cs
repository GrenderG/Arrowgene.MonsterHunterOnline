using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for item award data.
    /// C++ Reader: crygame.dll+sub_10152970 (UnkTlv0091)
    /// C++ Printer: crygame.dll+sub_10153400
    /// </summary>
    public class TlvItemAwardData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxItems = 20;

        /// <summary>
        /// Last time.
        /// Field ID: 1
        /// </summary>
        public long LastTime { get; set; }

        /// <summary>
        /// Award count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public int AwardCnt => ItemType?.Length ?? 0;

        /// <summary>
        /// Item type count (same as AwardCnt).
        /// Field ID: 3
        /// </summary>
        public byte ItemTypeCnt => (byte)AwardCnt;

        /// <summary>
        /// Item types (int array).
        /// Field ID: 4
        /// </summary>
        public int[] ItemType { get; set; }

        /// <summary>
        /// Item counts (short array).
        /// Field ID: 5
        /// </summary>
        public short[] ItemCnt { get; set; }

        /// <summary>
        /// Item bind types (byte array).
        /// Field ID: 6
        /// </summary>
        public byte[] ItemBindType { get; set; }

        /// <summary>
        /// VIP flag.
        /// Field ID: 7
        /// </summary>
        public byte VipFlag { get; set; }

        /// <summary>
        /// Farm level.
        /// Field ID: 8
        /// </summary>
        public short FarmLevel { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemType?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvItemAwardData] ItemType exceeds the maximum of {MaxItems} elements.");
            if ((ItemCnt?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvItemAwardData] ItemCnt exceeds the maximum of {MaxItems} elements.");
            if ((ItemBindType?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvItemAwardData] ItemBindType exceeds the maximum of {MaxItems} elements.");

            WriteTlvInt64(buffer, 1, LastTime);
            WriteTlvInt32(buffer, 2, AwardCnt);
            WriteTlvByte(buffer, 3, ItemTypeCnt);
            WriteTlvInt32Arr(buffer, 4, ItemType);
            WriteTlvInt16Arr(buffer, 5, ItemCnt);
            WriteTlvByteArr(buffer, 6, ItemBindType);
            WriteTlvByte(buffer, 7, VipFlag);
            WriteTlvInt16(buffer, 8, FarmLevel);
        }
    }
}
