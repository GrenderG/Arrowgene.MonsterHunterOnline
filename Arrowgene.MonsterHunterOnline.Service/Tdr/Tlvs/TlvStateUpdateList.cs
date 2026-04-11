using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvIdStateUpdate.
    /// C++ Reader: crygame.dll+sub_10244C20 (UnkTlv0289)
    /// </summary>
    public class TlvStateUpdateList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from State).
        /// Field ID: 1
        /// </summary>
        public int Count => State?.Count ?? 0;

        /// <summary>
        /// List of TlvIdStateUpdate.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdStateUpdate> State { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break;
                    case 2: State = ReadTlvList<TlvIdStateUpdate>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, State);
        }
    }
}
