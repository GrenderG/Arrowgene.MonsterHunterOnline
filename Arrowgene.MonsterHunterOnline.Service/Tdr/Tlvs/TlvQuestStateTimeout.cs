using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for quest state with timeout.
    /// C++ Reader: crygame.dll+sub_1021EB90 (UnkTlv0246)
    /// C++ Printer: crygame.dll+sub_1021EF20
    /// </summary>
    public class TlvQuestStateTimeout : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Quest ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quest state.
        /// Field ID: 2
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Accept time.
        /// Field ID: 3
        /// </summary>
        public uint AcceptTime { get; set; }

        /// <summary>
        /// Timeout value.
        /// Field ID: 4
        /// </summary>
        public int Timeout { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadInt(); break;
                    case 2: State = reader.ReadInt(); break;
                    case 3: AcceptTime = (uint)reader.ReadInt(); break;
                    case 4: Timeout = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvInt(writer, 2, State);
            WriteTlvInt(writer, 3, (int)AcceptTime);
            WriteTlvInt(writer, 4, Timeout);
        }
    }
}
