using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for NPC attitude.
    /// C++ Reader: crygame.dll+sub_10229050 (UnkTlv0262)
    /// C++ Printer: crygame.dll+sub_102292A0
    /// </summary>
    public class TlvNpcAttitude : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// NPC attitude ID.
        /// Field ID: 1
        /// </summary>
        public int NpcAtdId { get; set; }

        /// <summary>
        /// NPC attitude value.
        /// Field ID: 2
        /// </summary>
        public int NpcAtdValue { get; set; }

        /// <summary>
        /// NPC attitude stage.
        /// Field ID: 3
        /// </summary>
        public int NpcAtdStage { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: NpcAtdId = reader.ReadInt(); break;
                    case 2: NpcAtdValue = reader.ReadInt(); break;
                    case 3: NpcAtdStage = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, NpcAtdId);
            WriteTlvInt(writer, 2, NpcAtdValue);
            WriteTlvInt(writer, 3, NpcAtdStage);
        }
    }
}
