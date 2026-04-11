using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for combined stat data with int-value and short-value stat lists.
    /// C++ Reader: crygame.dll+sub_1021B540 (UnkTlv0243)
    /// C++ Printer: crygame.dll+sub_1021BF40
    /// </summary>
    public class TlvStatData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxStatInt = 200;
        public const int MaxStat = 3700;

        /// <summary>
        /// Int-value stat count (derived from list).
        /// Field ID: 1
        /// </summary>
        public short StatNumInt => (short)(StatListInt?.Count ?? 0);

        /// <summary>
        /// Int-value stat list.
        /// Field ID: 2
        /// </summary>
        public List<TlvStatIdxValue> StatListInt { get; set; }

        /// <summary>
        /// Short-value stat count (derived from list).
        /// Field ID: 3
        /// </summary>
        public short StatNum => (short)(StatList?.Count ?? 0);

        /// <summary>
        /// Short-value stat list.
        /// Field ID: 4
        /// </summary>
        public List<TlvStatIdxPair> StatList { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadShort(); break; // statNumInt, derived
                    case 2: StatListInt = ReadTlvList<TlvStatIdxValue>(reader); break;
                    case 3: reader.ReadShort(); break; // statNum, derived
                    case 4: StatList = ReadTlvList<TlvStatIdxPair>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((StatListInt?.Count ?? 0) > MaxStatInt)
                throw new InvalidDataException($"[TlvStatData] StatListInt exceeds the maximum of {MaxStatInt} elements.");
            if ((StatList?.Count ?? 0) > MaxStat)
                throw new InvalidDataException($"[TlvStatData] StatList exceeds the maximum of {MaxStat} elements.");

            WriteTlvShort(writer, 1, StatNumInt);
            WriteTlvList(writer, 2, StatListInt);
            WriteTlvShort(writer, 3, StatNum);
            WriteTlvList(writer, 4, StatList);
        }
    }
}
