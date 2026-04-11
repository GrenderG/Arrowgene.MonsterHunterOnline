using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for setting data pair.
    /// C++ Reader: crygame.dll+sub_10168160 (UnkTlv0108)
    /// C++ Printer: crygame.dll+sub_10168240
    /// </summary>
    public class TlvSettingData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// First setting data.
        /// Field ID: 1
        /// </summary>
        public uint FirstSettingData { get; set; }

        /// <summary>
        /// Second setting data.
        /// Field ID: 2
        /// </summary>
        public uint SecondSettingData { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: FirstSettingData = (uint)reader.ReadInt(); break;
                    case 2: SecondSettingData = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)FirstSettingData);
            WriteTlvInt(writer, 2, (int)SecondSettingData);
        }
    }
}
