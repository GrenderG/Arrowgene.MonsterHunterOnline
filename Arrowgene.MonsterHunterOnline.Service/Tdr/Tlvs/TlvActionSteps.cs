using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for action with steps.
    /// C++ Reader: crygame.dll+sub_1018DAC0 (UnkTlv0160)
    /// C++ Printer: crygame.dll+sub_1018DB80
    /// </summary>
    public class TlvActionSteps : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Action identifier.
        /// Field ID: 1
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// Number of steps.
        /// Field ID: 2
        /// </summary>
        public int Steps { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: ActionId = reader.ReadInt(); break;
                    case 2: Steps = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, ActionId);
            WriteTlvInt(writer, 2, Steps);
        }
    }
}
