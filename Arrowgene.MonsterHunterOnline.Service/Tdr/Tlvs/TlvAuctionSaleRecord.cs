using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for auction sale record with item data.
    /// C++ Reader: crygame.dll+sub_102364B0 (UnkTlv0279)
    /// C++ Printer: crygame.dll+sub_10236D70
    /// </summary>
    public class TlvAuctionSaleRecord : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;
        public const int MaxItemDataLength = 200;

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
        /// Sale time.
        /// Field ID: 4
        /// </summary>
        public byte SaleTime { get; set; }

        /// <summary>
        /// Expire time.
        /// Field ID: 5
        /// </summary>
        public uint ExpireTime { get; set; }

        /// <summary>
        /// Initial price.
        /// Field ID: 6
        /// </summary>
        public uint InitPrice { get; set; }

        /// <summary>
        /// Maximum price.
        /// Field ID: 7
        /// </summary>
        public uint MaxPrice { get; set; }

        /// <summary>
        /// Current price.
        /// Field ID: 8
        /// </summary>
        public uint CurPrice { get; set; }

        /// <summary>
        /// Bid database ID.
        /// Field ID: 9
        /// </summary>
        public ulong BidDbId { get; set; }

        /// <summary>
        /// Bid role name.
        /// Field ID: 10
        /// </summary>
        public string BidRoleName { get; set; } = string.Empty;

        /// <summary>
        /// Item data size (derived from ItemData).
        /// Field ID: 11
        /// </summary>
        public short ItemDataSize => (short)(ItemData?.Length ?? 0);

        /// <summary>
        /// Item data bytes.
        /// Field ID: 12
        /// </summary>
        public byte[] ItemData { get; set; }

        /// <summary>
        /// Bid level.
        /// Field ID: 13
        /// </summary>
        public uint BidLevel { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: RecordId = (ulong)reader.ReadLong(); break;
                    case 2: DbId = (ulong)reader.ReadLong(); break;
                    case 3:
                    {
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            RoleName = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    }
                    case 4: SaleTime = reader.ReadByte(); break;
                    case 5: ExpireTime = (uint)reader.ReadInt(); break;
                    case 6: InitPrice = (uint)reader.ReadInt(); break;
                    case 7: MaxPrice = (uint)reader.ReadInt(); break;
                    case 8: CurPrice = (uint)reader.ReadInt(); break;
                    case 9: BidDbId = (ulong)reader.ReadLong(); break;
                    case 10:
                    {
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            BidRoleName = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    }
                    case 11: reader.ReadShort(); break;
                    case 12:
                        int dataLen = reader.ReadInt();
                        if (dataLen > 0 && dataLen <= MaxItemDataLength)
                            ItemData = reader.ReadBytes(dataLen);
                        break;
                    case 13: BidLevel = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if (!string.IsNullOrEmpty(RoleName) && Encoding.UTF8.GetByteCount(RoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvAuctionSaleRecord] RoleName exceeds max of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(BidRoleName) && Encoding.UTF8.GetByteCount(BidRoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvAuctionSaleRecord] BidRoleName exceeds max of {MaxNameLength} bytes.");

            WriteTlvLong(writer, 1, (long)RecordId);
            WriteTlvLong(writer, 2, (long)DbId);
            WriteTlvString(writer, 3, RoleName);
            WriteTlvByte(writer, 4, SaleTime);
            WriteTlvInt(writer, 5, (int)ExpireTime);
            WriteTlvInt(writer, 6, (int)InitPrice);
            WriteTlvInt(writer, 7, (int)MaxPrice);
            WriteTlvInt(writer, 8, (int)CurPrice);
            WriteTlvLong(writer, 9, (long)BidDbId);
            WriteTlvString(writer, 10, BidRoleName);
            WriteTlvShort(writer, 11, ItemDataSize);
            WriteTlvByteArray(writer, 12, ItemData, ItemDataSize);
            WriteTlvInt(writer, 13, (int)BidLevel);
        }
    }
}
