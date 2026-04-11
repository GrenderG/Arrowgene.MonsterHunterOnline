using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for depot rights and fetch count.
    /// C++ Reader: crygame.dll+sub_1011D790 (UnkTlv0019)
    /// C++ Printer: crygame.dll+sub_1011D960
    /// </summary>
    public class TlvDepotRights : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Depot value.
        /// Field ID: 1
        /// </summary>
        public int Depot { get; set; }

        /// <summary>
        /// Rights value.
        /// Field ID: 2
        /// </summary>
        public int Rights { get; set; }

        /// <summary>
        /// Fetch count.
        /// Field ID: 3
        /// </summary>
        public int FetchCount { get; set; }

        /// <summary>
        /// Current fetch count.
        /// Field ID: 4
        /// </summary>
        public int CurFetchCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Depot = reader.ReadInt(); break;
                    case 2: Rights = reader.ReadInt(); break;
                    case 3: FetchCount = reader.ReadInt(); break;
                    case 4: CurFetchCount = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Depot);
            WriteTlvInt(writer, 2, Rights);
            WriteTlvInt(writer, 3, FetchCount);
            WriteTlvInt(writer, 4, CurFetchCount);
        }
    }
}
