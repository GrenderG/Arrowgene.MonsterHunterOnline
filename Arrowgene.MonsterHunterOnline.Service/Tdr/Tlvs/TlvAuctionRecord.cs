using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for auction record.
    /// C++ Reader: crygame.dll+sub_102385A0 (UnkTlv0281)
    /// C++ Printer: crygame.dll+sub_10238C30
    /// </summary>
    public class TlvAuctionRecord : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

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
        /// Expire time.
        /// Field ID: 4
        /// </summary>
        public uint ExpireTime { get; set; }

        /// <summary>
        /// Money.
        /// Field ID: 5
        /// </summary>
        public uint Money { get; set; }

        /// <summary>
        /// Credit.
        /// Field ID: 6
        /// </summary>
        public uint Credit { get; set; }

        /// <summary>
        /// Record time.
        /// Field ID: 7
        /// </summary>
        public uint RecordTime { get; set; }

        /// <summary>
        /// Bid database ID.
        /// Field ID: 8
        /// </summary>
        public ulong BidDbId { get; set; }

        /// <summary>
        /// Bid role name.
        /// Field ID: 9
        /// </summary>
        public string BidRoleName { get; set; } = string.Empty;

        /// <summary>
        /// Bid level.
        /// Field ID: 10
        /// </summary>
        public uint BidLevel { get; set; }

        /// <summary>
        /// UIN.
        /// Field ID: 11
        /// </summary>
        public uint Uin { get; set; }

        /// <summary>
        /// Bid UIN.
        /// Field ID: 12
        /// </summary>
        public uint BidUin { get; set; }

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
                    case 4: ExpireTime = (uint)reader.ReadInt(); break;
                    case 5: Money = (uint)reader.ReadInt(); break;
                    case 6: Credit = (uint)reader.ReadInt(); break;
                    case 7: RecordTime = (uint)reader.ReadInt(); break;
                    case 8: BidDbId = (ulong)reader.ReadLong(); break;
                    case 9:
                    {
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            BidRoleName = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    }
                    case 10: BidLevel = (uint)reader.ReadInt(); break;
                    case 11: Uin = (uint)reader.ReadInt(); break;
                    case 12: BidUin = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(RoleName) && Encoding.UTF8.GetByteCount(RoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvAuctionRecord] RoleName exceeds or equals the maximum of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(BidRoleName) && Encoding.UTF8.GetByteCount(BidRoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvAuctionRecord] BidRoleName exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvLong(writer, 1, (long)RecordId);
            WriteTlvLong(writer, 2, (long)DbId);
            WriteTlvString(writer, 3, RoleName);
            WriteTlvInt(writer, 4, (int)ExpireTime);
            WriteTlvInt(writer, 5, (int)Money);
            WriteTlvInt(writer, 6, (int)Credit);
            WriteTlvInt(writer, 7, (int)RecordTime);
            WriteTlvLong(writer, 8, (long)BidDbId);
            WriteTlvString(writer, 9, BidRoleName);
            WriteTlvInt(writer, 10, (int)BidLevel);
            WriteTlvInt(writer, 11, (int)Uin);
            WriteTlvInt(writer, 12, (int)BidUin);
        }
    }
}
