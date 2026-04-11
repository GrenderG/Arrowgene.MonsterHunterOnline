using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for snapshot item data.
    /// C++ Reader: crygame.dll+sub_10186820 (UnkTlv0152)
    /// C++ Printer: crygame.dll+sub_10186E90
    /// </summary>
    public class TlvSnapItemData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: HasFlag = reader.ReadByte(); break;
                    case 2: reader.ReadInt(); break; // snapCnt, derived
                    case 3: ItemType = ReadTlvIntArray(reader); break;
                    case 4:
                        int len4 = reader.ReadInt();
                        if (len4 > 0 && len4 <= MaxItems)
                            BindType = reader.ReadBytes(len4);
                        break;
                    case 5: ItemCount = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemType?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvSnapItemData] ItemType exceeds the maximum of {MaxItems} elements.");
            if ((BindType?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvSnapItemData] BindType exceeds the maximum of {MaxItems} elements.");
            if ((ItemCount?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvSnapItemData] ItemCount exceeds the maximum of {MaxItems} elements.");

            WriteTlvByte(writer, 1, HasFlag);
            WriteTlvInt(writer, 2, SnapCnt);
            WriteTlvIntArray(writer, 3, ItemType, SnapCnt);
            WriteTlvByteArray(writer, 4, BindType, SnapCnt);
            WriteTlvIntArray(writer, 5, ItemCount, SnapCnt);
        }
    }
}
