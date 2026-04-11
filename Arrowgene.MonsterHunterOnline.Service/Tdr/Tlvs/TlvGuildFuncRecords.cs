using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvOperationLog.
    /// C++ Reader: crygame.dll+sub_10131790 (UnkTlv0045)
    /// </summary>
    public class TlvGuildFuncRecords : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from GuildFuncRecordInfosPkg).
        /// Field ID: 1
        /// </summary>
        public int Count => GuildFuncRecordInfosPkg?.Count ?? 0;

        /// <summary>
        /// List of TlvOperationLog.
        /// Field ID: 2
        /// </summary>
        public List<TlvOperationLog> GuildFuncRecordInfosPkg { get; set; }

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
                    case 2: GuildFuncRecordInfosPkg = ReadTlvList<TlvOperationLog>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, GuildFuncRecordInfosPkg);
        }
    }
}
