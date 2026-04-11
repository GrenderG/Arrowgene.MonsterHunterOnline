using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for item count with short array.
    /// C++ Reader: crygame.dll+sub_101648B0 (UnkTlv0102)
    /// C++ Printer: crygame.dll+sub_10164D10
    /// </summary>
    public class TlvItemList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxItems = 200;

        /// <summary>
        /// Item count (derived from Items array).
        /// Field ID: 2
        /// </summary>
        public short ItemCount => (short)(ItemList?.Length ?? 0);

        /// <summary>
        /// Item IDs (short array).
        /// Field ID: 3
        /// </summary>
        public short[] ItemList { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: reader.ReadShort(); break; // itemCount, derived from array
                    case 3: ItemList = ReadTlvShortArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemList?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvItemList] ItemList exceeds the maximum of {MaxItems} items.");

            WriteTlvShort(writer, 2, ItemCount);
            WriteTlvShortArray(writer, 3, ItemList);
        }
    }
}
