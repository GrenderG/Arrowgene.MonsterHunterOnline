using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for counter data list with count.
    /// C++ Reader: crygame.dll+sub_10154870 (inner of UnkTlv0086)
    /// </summary>
    public class TlvCounterDataList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxCounters = 64;

        /// <summary>
        /// Counter count (derived from list).
        /// Field ID: 1
        /// </summary>
        public byte CounterNum => (byte)(CounterData?.Count ?? 0);

        /// <summary>
        /// Counter data entries.
        /// Field ID: 2
        /// </summary>
        public List<TlvCounterData> CounterData { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadByte(); break; // counterNum, derived
                    case 2: CounterData = ReadTlvList<TlvCounterData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((CounterData?.Count ?? 0) > MaxCounters)
                throw new InvalidDataException($"[TlvCounterDataList] CounterData exceeds the maximum of {MaxCounters} elements.");

            WriteTlvByte(writer, 1, CounterNum);
            WriteTlvList(writer, 2, CounterData);
        }
    }
}
