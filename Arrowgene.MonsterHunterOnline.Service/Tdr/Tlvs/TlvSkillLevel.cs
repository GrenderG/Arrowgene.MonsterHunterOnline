using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for skill level info.
    /// C++ Reader: crygame.dll+sub_10126FD0 (UnkTlv0032)
    /// C++ Printer: crygame.dll+sub_101270B0
    /// </summary>
    public class TlvSkillLevel : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Skill identifier (short).
        /// Field ID: 1
        /// </summary>
        public short Skill { get; set; }

        /// <summary>
        /// Level value.
        /// Field ID: 2
        /// </summary>
        public byte Level { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Skill = reader.ReadShort(); break;
                    case 2: Level = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvShort(writer, 1, Skill);
            WriteTlvByte(writer, 2, Level);
        }
    }
}
