using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for step state tracking.
    /// C++ Reader: crygame.dll+sub_10166910 (UnkTlv0105)
    /// C++ Printer: crygame.dll+sub_10166AB0
    /// </summary>
    public class TlvStepState : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Step identifier.
        /// Field ID: 1
        /// </summary>
        public byte StepId { get; set; }

        /// <summary>
        /// Step state.
        /// Field ID: 2
        /// </summary>
        public byte StepState { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: StepId = reader.ReadByte(); break;
                    case 2: StepState = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvByte(writer, 1, StepId);
            WriteTlvByte(writer, 2, StepState);
        }
    }
}
