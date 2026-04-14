using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Inventory / Equipment Item).
    /// C++ Writer: crygame.dll+sub_1010E010
    /// C++ Printer: crygame.dll+sub_1010E990
    /// </summary>
    public class TlvItem : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxExtAttrs = 32; // 0x20u

        public long ItemId { get; set; }
        public int ItemType { get; set; }
        public byte PosColumn { get; set; }
        public short PosGrid { get; set; }
        public short Count { get; set; }
        public byte Bind { get; set; }
        public byte AttrCount { get; set; }

        public byte[] ItemExtAttrIds { get; set; } = new byte[0];
        public int[] ItemExtAttrVals { get; set; } = new int[0];

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECKS ---
// TODO boundary:             if (AttrCount > MaxExtAttrs)
// TODO boundary:                 throw new InvalidDataException($"[TlvItem] AttrCount ({AttrCount}) exceeds maximum of {MaxExtAttrs}.");
// TODO boundary:             if (ItemExtAttrIds.Length > MaxExtAttrs)
// TODO boundary:                 throw new InvalidDataException($"[TlvItem] ItemExtAttrIds length exceeds maximum of {MaxExtAttrs}.");
// TODO boundary:             if (ItemExtAttrVals.Length > MaxExtAttrs)
// TODO boundary:                 throw new InvalidDataException($"[TlvItem] ItemExtAttrVals length exceeds maximum of {MaxExtAttrs}.");

// TODO boundary:             if(ItemExtAttrIds.Length != ItemExtAttrVals.Length)
// TODO boundary:                 throw new InvalidDataException($"[TlvItem] ItemExtAttrIds length ({ItemExtAttrIds.Length}) does not match ItemExtAttrVals length ({ItemExtAttrVals.Length}).");

            // --- SERIALIZATION ---

            WriteTlvInt64(buffer, 2, ItemId);
            WriteTlvInt32(buffer, 3, ItemType);
            WriteTlvByte(buffer, 4, PosColumn);
            WriteTlvInt16(buffer, 5, PosGrid);
            WriteTlvInt16(buffer, 6, Count);
            WriteTlvByte(buffer, 7, Bind);

            // The C++ client explicitly checks `if (AttrCount > 0)` before writing the arrays
            // Re-inject the array length directly
            WriteTlvByte(buffer, 8, (byte)ItemExtAttrIds.Length);

            if (ItemExtAttrIds.Length > 0)
            {
                WriteTlvByteArr(buffer, 10, ItemExtAttrIds);
                WriteTlvInt32Arr(buffer, 11, ItemExtAttrVals);
            }
        }
    }
}