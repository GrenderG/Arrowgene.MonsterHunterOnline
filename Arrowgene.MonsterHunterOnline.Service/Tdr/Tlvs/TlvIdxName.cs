using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for index with name.
    /// C++ Reader: crygame.dll+sub_10128D30 (UnkTlv0034)
    /// C++ Printer: crygame.dll+sub_10128E40
    /// </summary>
    public class TlvIdxName : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 8;

        /// <summary>
        /// Index value.
        /// Field ID: 1
        /// </summary>
        public byte Idx { get; set; }

        /// <summary>
        /// Name string.
        /// Field ID: 2
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
                    case 1: Idx = reader.ReadByte(); break;
                    case 2:
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
                throw new InvalidDataException($"[TlvIdxName] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvByte(writer, 1, Idx);
            WriteTlvString(writer, 2, Name);
        }
    }
}
