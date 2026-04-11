using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Supply / Item Pouch Loadout Plan).
    /// C++ Reader: crygame.dll+sub_10113D40
    /// C++ Printer: crygame.dll+sub_10114140
    /// </summary>
    public class TlvSupplyPlan : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1:
                        SupplyPlanId = reader.ReadInt();
                        break;
                    case 2:
                        int nameLen = reader.ReadInt();
                        Name = Encoding.UTF8.GetString(reader.ReadBytes(nameLen));
                        break;
                    case 3:
                        // Field 3 is the explicit supply count.
                        // Read and discard it. We rely on the array lengths instead.
                        reader.ReadInt();
                        break;
                    case 4:
                        ItemType = ReadTlvIntArray(reader);
                        break;
                    case 5:
                        ItemCount = ReadTlvIntArray(reader);
                        break;
                    case 6:
                        PosGrid = ReadTlvIntArray(reader);
                        break;
                    default:
                        SkipTlvField(reader, wireType);
                        break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY & SYNC CHECKS ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvSupplyPlan] Name exceeds or equals the strict maximum of {MaxNameLength} bytes.");

            int supplyCnt = ItemType.Length;

            if (supplyCnt > MaxSupplyItems)
                throw new InvalidDataException($"[TlvSupplyPlan] Array lengths ({supplyCnt}) exceed maximum of {MaxSupplyItems}.");

            if (ItemCount.Length != supplyCnt || PosGrid.Length != supplyCnt)
                throw new InvalidDataException("[TlvSupplyPlan] ItemType, ItemCount, and PosGrid arrays must all have the exact same length.");

            // --- SERIALIZATION ---
            WriteTlvInt(writer, 1, SupplyPlanId);
            WriteTlvString(writer, 2, Name);

            // Re-inject the dynamically calculated Count directly as Field 3
            WriteTlvInt(writer, 3, supplyCnt);

            if (supplyCnt > 0)
            {
                // Write the parallel arrays (assuming you have a 4-arg WriteTlvIntArray overload that takes 'count')
                WriteTlvIntArray(writer, 4, ItemType, supplyCnt);
                WriteTlvIntArray(writer, 5, ItemCount, supplyCnt);
                WriteTlvIntArray(writer, 6, PosGrid, supplyCnt);
            }
        }
    }
}