using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvGroupTypeTime.
    /// C++ Reader: crygame.dll+sub_1016EE00 (UnkTlv0118)
    /// </summary>
    public class TlvGroupTypeTimeList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Data).
        /// Field ID: 1
        /// </summary>
        public int Count => Data?.Count ?? 0;

        /// <summary>
        /// List of TlvGroupTypeTime.
        /// Field ID: 2
        /// </summary>
        public List<TlvGroupTypeTime> Data { get; set; }

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
                    case 2: Data = ReadTlvList<TlvGroupTypeTime>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Data);
        }
    }
}
