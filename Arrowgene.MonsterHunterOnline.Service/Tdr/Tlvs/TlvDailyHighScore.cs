using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for daily high score tracking.
    /// C++ Reader: crygame.dll+sub_101A3830 (UnkTlv0184)
    /// C++ Printer: crygame.dll+sub_101A38F0
    /// </summary>
    public class TlvDailyHighScore : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Date day value.
        /// Field ID: 1
        /// </summary>
        public int DateDay { get; set; }

        /// <summary>
        /// Current higher score.
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
