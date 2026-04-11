using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for training time and slot.
    /// C++ Reader: crygame.dll+sub_1019F6C0 (UnkTlv0176)
    /// C++ Printer: crygame.dll+sub_1019F7A0
    /// </summary>
    public class TlvTrainTimeSlot : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Training time.
        /// Field ID: 1
        /// </summary>
        public uint TrainTime { get; set; }

        /// <summary>
        /// Training slot.
        /// Field ID: 2
        /// </summary>
        public byte TrainSlot { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: TrainTime = (uint)reader.ReadInt(); break;
                    case 2: TrainSlot = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)TrainTime);
            WriteTlvByte(writer, 2, TrainSlot);
        }
    }
}
