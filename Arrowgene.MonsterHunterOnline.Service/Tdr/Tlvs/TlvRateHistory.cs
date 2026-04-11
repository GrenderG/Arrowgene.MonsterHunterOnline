using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for rate and credit/money history.
    /// C++ Reader: crygame.dll+sub_1023B630 (UnkTlv0285)
    /// C++ Printer: crygame.dll+sub_1023BCC0
    /// </summary>
    public class TlvRateHistory : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxHistory = 10;

        /// <summary>
        /// Rate.
        /// Field ID: 1
        /// </summary>
        public short Rate { get; set; }

        /// <summary>
        /// History count (derived from History).
        /// Field ID: 2
        /// </summary>
        public short HistoryCount => (short)(History?.Count ?? 0);

        /// <summary>
        /// History entries.
        /// Field ID: 3
        /// </summary>
        public List<TlvCreditMoneyTime> History { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Rate = reader.ReadShort(); break;
                    case 2: reader.ReadShort(); break; // historyCount, derived
                    case 3: History = ReadTlvList<TlvCreditMoneyTime>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((History?.Count ?? 0) > MaxHistory)
                throw new InvalidDataException($"[TlvRateHistory] History exceeds the maximum of {MaxHistory} elements.");

            WriteTlvShort(writer, 1, Rate);
            WriteTlvShort(writer, 2, HistoryCount);
            WriteTlvList(writer, 3, History);
        }
    }
}
