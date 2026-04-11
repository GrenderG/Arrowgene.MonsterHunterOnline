using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvChannelNameFlags.
    /// C++ Reader: crygame.dll+sub_10167AE0 (UnkTlv0107)
    /// </summary>
    public class TlvChannelTabs : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from AstTabs).
        /// Field ID: 1
        /// </summary>
        public int Count => AstTabs?.Count ?? 0;

        /// <summary>
        /// List of TlvChannelNameFlags.
        /// Field ID: 2
        /// </summary>
        public List<TlvChannelNameFlags> AstTabs { get; set; }

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
                    case 2: AstTabs = ReadTlvList<TlvChannelNameFlags>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, AstTabs);
        }
    }
}
