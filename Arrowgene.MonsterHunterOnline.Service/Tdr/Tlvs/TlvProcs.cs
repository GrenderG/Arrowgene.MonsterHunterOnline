using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvTypeProcData.
    /// C++ Reader: crygame.dll+sub_10153410 (UnkTlv0082)
    /// </summary>
    public class TlvProcs : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Procs).
        /// Field ID: 1
        /// </summary>
        public int Count => Procs?.Count ?? 0;

        /// <summary>
        /// List of TlvTypeProcData.
        /// Field ID: 2
        /// </summary>
        public List<TlvTypeProcData> Procs { get; set; }

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
                    case 2: Procs = ReadTlvList<TlvTypeProcData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Procs);
        }
    }
}
