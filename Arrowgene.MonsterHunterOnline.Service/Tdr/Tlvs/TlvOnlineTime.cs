using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for online time tracking.
    /// C++ Reader: crygame.dll+sub_10156EA0 (UnkTlv0088)
    /// C++ Printer: crygame.dll+sub_10157090
    /// </summary>
    public class TlvOnlineTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Online time.
        /// Field ID: 1
        /// </summary>
        public uint OnlineTime { get; set; }

        /// <summary>
        /// Last update time.
        /// Field ID: 2
        /// </summary>
        public uint LastUpdateTime { get; set; }

        /// <summary>
        /// Activity identifier.
        /// Field ID: 3
        /// </summary>
        public uint ActivityId { get; set; }

        /// <summary>
        /// Hour value.
        /// Field ID: 4
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Minute value.
        /// Field ID: 5
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Second value.
        /// Field ID: 6
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// Offset value.
        /// Field ID: 7
        /// </summary>
        public int Offset { get; set; }

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
                    case 2: LastUpdateTime = (uint)reader.ReadInt(); break;
                    case 3: ActivityId = (uint)reader.ReadInt(); break;
                    case 4: Hour = reader.ReadInt(); break;
                    case 5: Min = reader.ReadInt(); break;
                    case 6: Second = reader.ReadInt(); break;
                    case 7: Offset = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)OnlineTime);
            WriteTlvInt(writer, 2, (int)LastUpdateTime);
            WriteTlvInt(writer, 3, (int)ActivityId);
            WriteTlvInt(writer, 4, Hour);
            WriteTlvInt(writer, 5, Min);
            WriteTlvInt(writer, 6, Second);
            WriteTlvInt(writer, 7, Offset);
        }
    }
}
