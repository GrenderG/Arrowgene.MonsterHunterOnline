using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for ID with start/stop time.
    /// C++ Reader: crygame.dll+sub_10246B10 (UnkTlv0292)
    /// C++ Printer: crygame.dll+sub_10246D60
    /// </summary>
    public class TlvIdStartStopTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// ID.
        /// Field ID: 1
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Start time.
        /// Field ID: 2
        /// </summary>
        public uint StartTime { get; set; }

        /// <summary>
        /// Stop time.
        /// Field ID: 3
        /// </summary>
        public uint StopTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = (uint)reader.ReadInt(); break;
                    case 2: StartTime = (uint)reader.ReadInt(); break;
                    case 3: StopTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)Id);
            WriteTlvInt(writer, 2, (int)StartTime);
            WriteTlvInt(writer, 3, (int)StopTime);
        }
    }
}
