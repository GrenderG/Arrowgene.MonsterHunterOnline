using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for database ID with name.
    /// C++ Reader: crygame.dll+sub_1012B510 (UnkTlv0038)
    /// C++ Printer: crygame.dll+sub_1012B6F0
    /// </summary>
    public class TlvDbIdInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Maximum name length.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Database ID.
        /// Field ID: 1
        /// </summary>
        public ulong DbId { get; set; }

        /// <summary>
        /// QQ value.
        /// Field ID: 2
        /// </summary>
        public uint QQ { get; set; }

        /// <summary>
        /// Name string.
        /// Field ID: 3
        /// </summary>
        public string Name { get; set; } = string.Empty;

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: DbId = (ulong)reader.ReadLong(); break;
                    case 2: QQ = (uint)reader.ReadInt(); break;
                    case 3:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            Name = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvDbIdInfo] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            // --- SERIALIZATION ---
            WriteTlvULong(writer, 1, DbId);
            WriteTlvInt(writer, 2, (int)QQ);
            WriteTlvString(writer, 3, Name);
        }
    }
}
