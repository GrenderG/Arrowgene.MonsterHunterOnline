using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for date day with higher value.
    /// C++ Reader: crygame.dll+sub_101A5800 (UnkTlv0184)
    /// C++ Printer: crygame.dll+sub_101A58E0
    /// </summary>
    public class TlvDateDayHigher : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Date day.
        /// Field ID: 1
        /// </summary>
        public int DateDay { get; set; }

        /// <summary>
        /// Current higher value.
        /// Field ID: 2
        /// </summary>
        public int CurHigher { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: DateDay = reader.ReadInt(); break;
                    case 2: CurHigher = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, DateDay);
            WriteTlvInt(writer, 2, CurHigher);
        }
    }
}
