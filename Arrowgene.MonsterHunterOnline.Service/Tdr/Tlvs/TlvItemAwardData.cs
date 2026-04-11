using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for item award data.
    /// C++ Reader: crygame.dll+sub_10152970 (UnkTlv0091)
    /// C++ Printer: crygame.dll+sub_10153400
    /// </summary>
    public class TlvItemAwardData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LastTime = reader.ReadLong(); break;
                    case 2: reader.ReadInt(); break; // awardCnt, derived
                    case 3: reader.ReadByte(); break; // itemTypeCnt, derived
                    case 4: ItemType = ReadTlvIntArray(reader); break;
                    case 5: ItemCnt = ReadTlvShortArray(reader); break;
                    case 6:
                        int len6 = reader.ReadInt();
                        if (len6 > 0 && len6 <= MaxItems)
                            ItemBindType = reader.ReadBytes(len6);
                        break;
                    case 7: VipFlag = reader.ReadByte(); break;
                    case 8: FarmLevel = reader.ReadShort(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemType?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvItemAwardData] ItemType exceeds the maximum of {MaxItems} elements.");
            if ((ItemCnt?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvItemAwardData] ItemCnt exceeds the maximum of {MaxItems} elements.");
            if ((ItemBindType?.Length ?? 0) > MaxItems)
                throw new InvalidDataException($"[TlvItemAwardData] ItemBindType exceeds the maximum of {MaxItems} elements.");

            WriteTlvLong(writer, 1, LastTime);
            WriteTlvInt(writer, 2, AwardCnt);
            WriteTlvByte(writer, 3, ItemTypeCnt);
            WriteTlvIntArray(writer, 4, ItemType, AwardCnt);
            WriteTlvShortArray(writer, 5, ItemCnt);
            WriteTlvByteArray(writer, 6, ItemBindType, AwardCnt);
            WriteTlvByte(writer, 7, VipFlag);
            WriteTlvShort(writer, 8, FarmLevel);
        }
    }
}
