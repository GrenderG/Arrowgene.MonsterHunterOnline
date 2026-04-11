using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvIdStartStopTime.
    /// C++ Reader: crygame.dll+sub_102472A0 (UnkTlv0293)
    /// </summary>
    public class TlvStartStopTimes : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Times).
        /// Field ID: 1
        /// </summary>
        public int Count => Times?.Count ?? 0;

        /// <summary>
        /// List of TlvIdStartStopTime.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdStartStopTime> Times { get; set; }

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
                    case 2: Times = ReadTlvList<TlvIdStartStopTime>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Times);
        }
    }
}
