using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Inventory / Equipment Item).
    /// C++ Writer: crygame.dll+sub_1010E010
    /// C++ Printer: crygame.dll+sub_1010E990
    /// </summary>
    public class TlvItem : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: ItemId = reader.ReadLong(); break;
                    case 3: ItemType = reader.ReadInt(); break;
                    case 4: PosColumn = reader.ReadByte(); break;
                    case 5: PosGrid = reader.ReadShort(); break;
                    case 6: Count = reader.ReadShort(); break;
                    case 7: Bind = reader.ReadByte(); break;
                    case 8: reader.ReadByte(); break; // Discard AttrCount
                    case 10: ItemExtAttrIds = reader.ReadBytes(reader.ReadInt()); break;
                    case 11: ItemExtAttrVals = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECKS ---
            if (AttrCount > MaxExtAttrs)
                throw new InvalidDataException($"[TlvItem] AttrCount ({AttrCount}) exceeds maximum of {MaxExtAttrs}.");
            if (ItemExtAttrIds.Length > MaxExtAttrs)
                throw new InvalidDataException($"[TlvItem] ItemExtAttrIds length exceeds maximum of {MaxExtAttrs}.");
            if (ItemExtAttrVals.Length > MaxExtAttrs)
                throw new InvalidDataException($"[TlvItem] ItemExtAttrVals length exceeds maximum of {MaxExtAttrs}.");

            if(ItemExtAttrIds.Length != ItemExtAttrVals.Length)
                throw new InvalidDataException($"[TlvItem] ItemExtAttrIds length ({ItemExtAttrIds.Length}) does not match ItemExtAttrVals length ({ItemExtAttrVals.Length}).");

            // --- SERIALIZATION ---

            WriteTlvLong(writer, 2, ItemId);
            WriteTlvInt(writer, 3, ItemType);
            WriteTlvByte(writer, 4, PosColumn);
            WriteTlvShort(writer, 5, PosGrid);
            WriteTlvShort(writer, 6, Count);
            WriteTlvByte(writer, 7, Bind);

            // The C++ client explicitly checks `if (AttrCount > 0)` before writing the arrays
            // Re-inject the array length directly
            WriteTlvByte(writer, 8, (byte)ItemExtAttrIds.Length);

            if (ItemExtAttrIds.Length > 0)
            {
                WriteTlvByteArray(writer, 10, ItemExtAttrIds, ItemExtAttrIds.Length);
                WriteTlvIntArray(writer, 11, ItemExtAttrVals, ItemExtAttrVals.Length);
            }
        }
    }
}