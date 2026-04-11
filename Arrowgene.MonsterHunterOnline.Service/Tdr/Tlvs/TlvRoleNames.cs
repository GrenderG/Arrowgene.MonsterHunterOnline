using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for role display names.
    /// C++ Reader: crygame.dll+sub_101514C0 (UnkTlv0080)
    /// C++ Printer: crygame.dll+sub_10151890
    /// </summary>
    public class TlvRoleNames : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Character name.
        /// Field ID: 1
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Guild name.
        /// Field ID: 2
        /// </summary>
        public string Guild { get; set; } = string.Empty;

        /// <summary>
        /// Clan name.
        /// Field ID: 3
        /// </summary>
        public string Clan { get; set; } = string.Empty;

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
                    {
                        int len = reader.ReadInt();
                        if (len > 0 && len < MaxNameLength)
                        {
                            byte[] bytes = reader.ReadBytes(len);
                            Name = Encoding.UTF8.GetString(bytes).TrimEnd('\0');
                        }
                        break;
                    }
                    case 2:
                    {
                        int len = reader.ReadInt();
                        if (len > 0 && len < MaxNameLength)
                        {
                            byte[] bytes = reader.ReadBytes(len);
                            Guild = Encoding.UTF8.GetString(bytes).TrimEnd('\0');
                        }
                        break;
                    }
                    case 3:
                    {
                        int len = reader.ReadInt();
                        if (len > 0 && len < MaxNameLength)
                        {
                            byte[] bytes = reader.ReadBytes(len);
                            Clan = Encoding.UTF8.GetString(bytes).TrimEnd('\0');
                        }
                        break;
                    }
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(Name) && Encoding.UTF8.GetByteCount(Name) >= MaxNameLength)
                throw new InvalidDataException($"[TlvRoleNames] Name exceeds or equals the maximum of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(Guild) && Encoding.UTF8.GetByteCount(Guild) >= MaxNameLength)
                throw new InvalidDataException($"[TlvRoleNames] Guild exceeds or equals the maximum of {MaxNameLength} bytes.");
            if (!string.IsNullOrEmpty(Clan) && Encoding.UTF8.GetByteCount(Clan) >= MaxNameLength)
                throw new InvalidDataException($"[TlvRoleNames] Clan exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvString(writer, 1, Name);
            WriteTlvString(writer, 2, Guild);
            WriteTlvString(writer, 3, Clan);
        }
    }
}
