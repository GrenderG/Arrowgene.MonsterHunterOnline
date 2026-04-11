using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for item rebuild limit data.
    /// C++ Reader: crygame.dll+sub_10178850 (UnkTlv0133)
    /// C++ Printer: crygame.dll+sub_10178F00
    /// </summary>
    public class TlvItemRebuildLimitData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxLimits = 8;

        /// <summary>
        /// Item rebuild limit count (derived from list).
        /// Field ID: 1
        /// </summary>
        public int ItemRebuildLimitCount => ItemRebuildLimitInfo?.Count ?? 0;

        /// <summary>
        /// Last item rebuild time.
        /// Field ID: 2
        /// </summary>
        public long LastItemRebuildTime { get; set; }

        /// <summary>
        /// Item rebuild limit info list.
        /// Field ID: 3
        /// </summary>
        public List<TlvLimitCount> ItemRebuildLimitInfo { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // itemRebuildLimitCount, derived
                    case 2: LastItemRebuildTime = reader.ReadLong(); break;
                    case 3: ItemRebuildLimitInfo = ReadTlvList<TlvLimitCount>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemRebuildLimitInfo?.Count ?? 0) > MaxLimits)
                throw new InvalidDataException($"[TlvItemRebuildLimitData] ItemRebuildLimitInfo exceeds the maximum of {MaxLimits} elements.");

            WriteTlvInt(writer, 1, ItemRebuildLimitCount);
            WriteTlvLong(writer, 2, LastItemRebuildTime);
            WriteTlvList(writer, 3, ItemRebuildLimitInfo);
        }
    }
}
