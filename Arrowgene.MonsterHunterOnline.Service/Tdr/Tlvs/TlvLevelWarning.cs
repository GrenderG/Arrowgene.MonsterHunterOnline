using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for level warning information.
    /// C++ Reader: crygame.dll+sub_1014A500 (UnkTlv0074)
    /// C++ Printer: crygame.dll+sub_1014A5C0
    /// </summary>
    public class TlvLevelWarning : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Level identifier.
        /// Field ID: 1
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Warning time value.
        /// Field ID: 2
        /// </summary>
        public uint WarningTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LevelId = reader.ReadInt(); break;
                    case 2: WarningTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, LevelId);
            WriteTlvInt(writer, 2, (int)WarningTime);
        }
    }
}
