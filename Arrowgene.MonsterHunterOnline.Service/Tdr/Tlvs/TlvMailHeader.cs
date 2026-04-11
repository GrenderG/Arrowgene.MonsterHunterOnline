using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for mail header info.
    /// C++ Reader: crygame.dll+sub_102349C0 (UnkTlv0277)
    /// C++ Printer: crygame.dll+sub_10234E70
    /// </summary>
    public class TlvMailHeader : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Source UID.
        /// Field ID: 1
        /// </summary>
        public ulong SrcUid { get; set; }

        /// <summary>
        /// Order.
        /// Field ID: 2
        /// </summary>
        public byte Order { get; set; }

        /// <summary>
        /// Destination UID.
        /// Field ID: 3
        /// </summary>
        public ulong DstUid { get; set; }

        /// <summary>
        /// Destination name.
        /// Field ID: 4
        /// </summary>
        public string DstName { get; set; } = string.Empty;

        /// <summary>
        /// Destination server.
        /// Field ID: 5
        /// </summary>
        public uint DstSvr { get; set; }

        /// <summary>
        /// Create time.
        /// Field ID: 6
        /// </summary>
        public uint CreateTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: SrcUid = (ulong)reader.ReadLong(); break;
                    case 2: Order = reader.ReadByte(); break;
                    case 3: DstUid = (ulong)reader.ReadLong(); break;
                    case 4:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            DstName = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    case 5: DstSvr = (uint)reader.ReadInt(); break;
                    case 6: CreateTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(DstName) && Encoding.UTF8.GetByteCount(DstName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvMailHeader] DstName exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvLong(writer, 1, (long)SrcUid);
            WriteTlvByte(writer, 2, Order);
            WriteTlvLong(writer, 3, (long)DstUid);
            WriteTlvString(writer, 4, DstName);
            WriteTlvInt(writer, 5, (int)DstSvr);
            WriteTlvInt(writer, 6, (int)CreateTime);
        }
    }
}
