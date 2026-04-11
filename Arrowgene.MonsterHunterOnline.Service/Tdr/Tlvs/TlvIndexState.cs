using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for index state tracking.
    /// C++ Reader: crygame.dll+sub_10193260 (UnkTlv0169)
    /// C++ Printer: crygame.dll+sub_10193320
    /// </summary>
    public class TlvIndexState : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Index value.
        /// Field ID: 1
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// State value.
        /// Field ID: 2
        /// </summary>
        public int State { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Index = reader.ReadInt(); break;
                    case 2: State = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Index);
            WriteTlvInt(writer, 2, State);
        }
    }
}
