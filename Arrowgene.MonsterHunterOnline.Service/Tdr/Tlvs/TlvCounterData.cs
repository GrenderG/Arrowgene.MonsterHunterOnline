using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvPhaseCounter.
    /// C++ Reader: crygame.dll+sub_10153F00 (UnkTlv0084)
    /// </summary>
    public class TlvCounterData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from CounterData).
        /// Field ID: 1
        /// </summary>
        public int Count => CounterData?.Count ?? 0;

        /// <summary>
        /// List of TlvPhaseCounter.
        /// Field ID: 2
        /// </summary>
        public List<TlvPhaseCounter> CounterData { get; set; }

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
                    case 2: CounterData = ReadTlvList<TlvPhaseCounter>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, CounterData);
        }
    }
}
