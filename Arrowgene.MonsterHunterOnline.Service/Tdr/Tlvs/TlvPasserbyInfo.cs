using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for passerby role info.
    /// C++ Reader: crygame.dll+sub_1016C710 (UnkTlv0113)
    /// C++ Printer: crygame.dll+sub_1016C990
    /// </summary>
    public class TlvPasserbyInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxNameLength = 32;

        /// <summary>
        /// Role database ID.
        /// Field ID: 1
        /// </summary>
        public ulong RoleDbId { get; set; }

        /// <summary>
        /// Level.
        /// Field ID: 2
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Role name.
        /// Field ID: 3
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// Meet way.
        /// Field ID: 4
        /// </summary>
        public byte MeetWay { get; set; }

        /// <summary>
        /// HR Level.
        /// Field ID: 5
        /// </summary>
        public int HrLevel { get; set; }

        /// <summary>
        /// Server ID.
        /// Field ID: 6
        /// </summary>
        public int SvrId { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: RoleDbId = (ulong)reader.ReadLong(); break;
                    case 2: Level = reader.ReadInt(); break;
                    case 3:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxNameLength)
                        {
                            byte[] nameBytes = reader.ReadBytes(nameLen);
                            RoleName = Encoding.UTF8.GetString(nameBytes).TrimEnd('\0');
                        }
                        break;
                    case 4: MeetWay = reader.ReadByte(); break;
                    case 5: HrLevel = reader.ReadInt(); break;
                    case 6: SvrId = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if (!string.IsNullOrEmpty(RoleName) && Encoding.UTF8.GetByteCount(RoleName) >= MaxNameLength)
                throw new InvalidDataException($"[TlvPasserbyInfo] RoleName exceeds or equals the maximum of {MaxNameLength} bytes.");

            WriteTlvLong(writer, 1, (long)RoleDbId);
            WriteTlvInt(writer, 2, Level);
            WriteTlvString(writer, 3, RoleName);
            WriteTlvByte(writer, 4, MeetWay);
            WriteTlvInt(writer, 5, HrLevel);
            WriteTlvInt(writer, 6, SvrId);
        }
    }
}
