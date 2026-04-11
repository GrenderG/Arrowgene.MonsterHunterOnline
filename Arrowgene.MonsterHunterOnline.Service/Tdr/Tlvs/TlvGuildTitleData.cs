using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild title data.
    /// C++ Reader: crygame.dll+sub_1011E650 (UnkTlv0021)
    /// C++ Printer: crygame.dll+sub_1011ED60
    /// </summary>
    public class TlvGuildTitleData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLen = 32;

        /// <summary>
        /// Title.
        /// Field ID: 1
        /// </summary>
        public int Title { get; set; }

        /// <summary>
        /// Name.
        /// Field ID: 2
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Rights.
        /// Field ID: 3
        /// </summary>
        public int Rights { get; set; }

        /// <summary>
        /// Depot rights data.
        /// Field ID: 4
        /// </summary>
        public TlvDepotRightsList DepotRights { get; set; } = new();

        /// <summary>
        /// Depot operation count.
        /// Field ID: 5
        /// </summary>
        public int DepotOpCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Title = reader.ReadInt(); break;
                    case 2:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLen)
                            Name = Encoding.UTF8.GetString(reader.ReadBytes(nameLen));
                        break;
                    case 3: Rights = reader.ReadInt(); break;
                    case 4: DepotRights = ReadTlvObject<TlvDepotRightsList>(reader); break;
                    case 5: DepotOpCount = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Title);

            byte[] nameBytes = Encoding.UTF8.GetBytes(Name ?? string.Empty);
            if (nameBytes.Length > 0)
            {
                writer.WriteVarUInt((2u << 4) | 5);
                writer.WriteInt(nameBytes.Length);
                writer.WriteBytes(nameBytes);
            }

            WriteTlvInt(writer, 3, Rights);
            WriteTlvObject(writer, 4, DepotRights);
            WriteTlvInt(writer, 5, DepotOpCount);
        }
    }
}
