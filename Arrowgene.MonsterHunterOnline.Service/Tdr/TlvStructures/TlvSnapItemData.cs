using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for snapshot item data.
    /// C++ Reader: crygame.dll+sub_10186820 (UnkTlv0152)
    /// C++ Printer: crygame.dll+sub_10186E90
    /// </summary>
    public class TlvSnapItemData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxItems = 100;

        /// <summary>
        /// Has flag (byte).
        /// Field ID: 1
        /// </summary>
        public byte HasFlag { get; set; }

        /// <summary>
        /// Snap count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public int SnapCnt => ItemType?.Length ?? 0;

        /// <summary>
        /// Item type ints.
        /// Field ID: 3
        /// </summary>
        public int[] ItemType { get; set; }

        /// <summary>
        /// Bind type bytes.
        /// Field ID: 4
        /// </summary>
        public byte[] BindType { get; set; }

        /// <summary>
        /// Item count ints.
        /// Field ID: 5
        /// </summary>
        public int[] ItemCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemType?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvSnapItemData] ItemType exceeds the maximum of {MaxItems} elements.");
            if ((BindType?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvSnapItemData] BindType exceeds the maximum of {MaxItems} elements.");
            if ((ItemCount?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvSnapItemData] ItemCount exceeds the maximum of {MaxItems} elements.");

            WriteTlvByte(buffer, 1, HasFlag);
            WriteTlvInt32(buffer, 2, SnapCnt);
            WriteTlvInt32Arr(buffer, 3, ItemType);
            WriteTlvByteArr(buffer, 4, BindType);
            WriteTlvInt32Arr(buffer, 5, ItemCount);
        }
    }
}
