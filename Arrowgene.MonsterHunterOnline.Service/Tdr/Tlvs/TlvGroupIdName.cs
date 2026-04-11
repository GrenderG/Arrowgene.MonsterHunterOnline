using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for group ID with name.
    /// C++ Reader: crygame.dll+sub_1016D790 (UnkTlv0115)
    /// C++ Printer: crygame.dll+sub_1016D890
    /// </summary>
    public class TlvGroupIdName : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxGroupNameLength = 32;

        /// <summary>
        /// Group ID.
        /// Field ID: 1
        /// </summary>
        public byte GroupId { get; set; }

        /// <summary>
        /// Group name.
        /// Field ID: 2
        /// </summary>
        public string GroupName { get; set; } = string.Empty;

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: GroupId = reader.ReadByte(); break;
                    case 2:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxGroupNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            GroupName = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(GroupName) && Encoding.UTF8.GetByteCount(GroupName) >= MaxGroupNameLength)
                throw new InvalidDataException($"[TlvGroupIdName] GroupName exceeds or equals the maximum of {MaxGroupNameLength} bytes.");

            WriteTlvByte(writer, 1, GroupId);
            WriteTlvString(writer, 2, GroupName);
        }
    }
}
