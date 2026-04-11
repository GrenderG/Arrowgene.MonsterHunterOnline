using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvTargetIdxData.
    /// C++ Reader: crygame.dll+sub_102188C0 (UnkTlv0238)
    /// </summary>
    public class TlvCardTargets : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from TargetList).
        /// Field ID: 1
        /// </summary>
        public int Count => TargetList?.Count ?? 0;

        /// <summary>
        /// List of TlvTargetIdxData.
        /// Field ID: 2
        /// </summary>
        public List<TlvTargetIdxData> TargetList { get; set; }

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
                    case 2: TargetList = ReadTlvList<TlvTargetIdxData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, TargetList);
        }
    }
}
