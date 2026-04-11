using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for channel name with flags.
    /// C++ Reader: crygame.dll+sub_10167140 (UnkTlv0106)
    /// C++ Printer: crygame.dll+sub_101674B0
    /// </summary>
    public class TlvChannelNameFlags : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 21;

        /// <summary>
        /// Channel name.
        /// Field ID: 1
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Channel flags.
        /// Field ID: 2
        /// </summary>
        public uint ChannelFlags { get; set; }

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
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            Name = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    case 2: ChannelFlags = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvChannelNameFlags] Name exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvString(writer, 1, Name);
            WriteTlvInt(writer, 2, (int)ChannelFlags);
        }
    }
}
