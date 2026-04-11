using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for online time, consecutive days, and refresh time.
    /// C++ Reader: crygame.dll+sub_1024BFF0 (UnkTlv0298)
    /// C++ Printer: crygame.dll+sub_1024C240
    /// </summary>
    public class TlvOnlineConsecRefresh : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Online time.
        /// Field ID: 1
        /// </summary>
        public uint OnlineTime { get; set; }

        /// <summary>
        /// Consecutive days.
        /// Field ID: 2
        /// </summary>
        public uint ConsecDays { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: OnlineTime = (uint)reader.ReadInt(); break;
                    case 2: ConsecDays = (uint)reader.ReadInt(); break;
                    case 3: RefreshTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)OnlineTime);
            WriteTlvInt(writer, 2, (int)ConsecDays);
            WriteTlvInt(writer, 3, (int)RefreshTime);
        }
    }
}
