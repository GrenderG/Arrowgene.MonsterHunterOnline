using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// Reconstructed TLV Structure (Supply / Item Pouch Loadout Plan).
    /// C++ Reader: crygame.dll+sub_10113D40
    /// C++ Printer: crygame.dll+sub_10114140
    /// </summary>
    public class TlvSupplyPlan : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxNameLength = 20; // 0x14
        public const int MaxSupplyItems = 30; // 0x1E

        public int SupplyPlanId { get; set; }
        public string Name { get; set; } = string.Empty;

        // The SupplyCnt is dynamically calculated based on the length of these arrays.
        // They must all be perfectly synchronized in length (up to 30).
        public int[] ItemType { get; set; } = new int[0];
        public int[] ItemCount { get; set; } = new int[0];
        public int[] PosGrid { get; set; } = new int[0];

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY & SYNC CHECKS ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvSupplyPlan] Name exceeds or equals the strict maximum of {MaxNameLength} bytes.");

            int supplyCnt = ItemType.Length;

// TODO boundary:             if (supplyCnt > MaxSupplyItems)
// TODO boundary:                 throw new InvalidDataException($"[TlvSupplyPlan] Array lengths ({supplyCnt}) exceed maximum of {MaxSupplyItems}.");

// TODO boundary:             if (ItemCount.Length != supplyCnt || PosGrid.Length != supplyCnt)
// TODO boundary:                 throw new InvalidDataException("[TlvSupplyPlan] ItemType, ItemCount, and PosGrid arrays must all have the exact same length.");

            // --- SERIALIZATION ---
            WriteTlvInt32(buffer, 1, SupplyPlanId);
            WriteTlvString(buffer, 2, Name);

            // Re-inject the dynamically calculated Count directly as Field 3
            WriteTlvInt32(buffer, 3, supplyCnt);

            if (supplyCnt > 0)
            {
                // Write the parallel arrays (assuming you have a 4-arg WriteTlvInt32Arr overload that takes 'count')
                WriteTlvInt32Arr(buffer, 4, ItemType);
                WriteTlvInt32Arr(buffer, 5, ItemCount);
                WriteTlvInt32Arr(buffer, 6, PosGrid);
            }
        }
    }
}