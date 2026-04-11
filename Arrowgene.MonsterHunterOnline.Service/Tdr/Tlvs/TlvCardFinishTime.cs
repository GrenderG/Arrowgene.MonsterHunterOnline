using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for card ID with finish time.
    /// C++ Reader: crygame.dll+sub_102191C0 (UnkTlv0239)
    /// C++ Printer: crygame.dll+sub_10219450
    /// </summary>
    public class TlvCardFinishTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Card ID.
        /// Field ID: 1
        /// </summary>
        public int CardId { get; set; }

        /// <summary>
        /// Finish time.
        /// Field ID: 2
        /// </summary>
        public uint FinishTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CardId = reader.ReadInt(); break;
                    case 2: FinishTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, CardId);
            WriteTlvInt(writer, 2, (int)FinishTime);
        }
    }
}
