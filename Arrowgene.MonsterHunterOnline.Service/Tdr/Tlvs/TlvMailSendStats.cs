using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for mail sending statistics.
    /// C++ Reader: crygame.dll+sub_10171130 (UnkTlv0121)
    /// C++ Printer: crygame.dll+sub_10171270
    /// </summary>
    public class TlvMailSendStats : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Account mail send times.
        /// Field ID: 1
        /// </summary>
        public int AccMailSendTimes { get; set; }

        /// <summary>
        /// Passerby send times.
        /// Field ID: 2
        /// </summary>
        public int PasserbySendTimes { get; set; }

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
                    case 1: AccMailSendTimes = reader.ReadInt(); break;
                    case 2: PasserbySendTimes = reader.ReadInt(); break;
                    case 3: RefreshTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, AccMailSendTimes);
            WriteTlvInt(writer, 2, PasserbySendTimes);
            WriteTlvInt(writer, 3, (int)RefreshTime);
        }
    }
}
