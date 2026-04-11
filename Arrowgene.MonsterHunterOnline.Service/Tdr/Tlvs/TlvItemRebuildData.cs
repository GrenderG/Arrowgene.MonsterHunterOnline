using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for item rebuild data with limit tracking.
    /// C++ Reader: crygame.dll+sub_1017BA00 (UnkTlv0137)
    /// C++ Printer: crygame.dll+sub_1017BDC0
    /// </summary>
    public class TlvItemRebuildData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxRebuildTypes = 8;
        public const int MaxTracks = 10;

        /// <summary>
        /// Item rebuild type count (derived from arrays).
        /// Field ID: 5
        /// </summary>
        public byte ItemRebuildTypeCount => (byte)(ItemRebuildLimitId?.Length ?? 0);

        /// <summary>
        /// Last item rebuild time.
        /// Field ID: 6
        /// </summary>
        public long LastItemRebuildTime { get; set; }

        /// <summary>
        /// Item rebuild limit IDs (byte array).
        /// Field ID: 7
        /// </summary>
        public byte[] ItemRebuildLimitId { get; set; }

        /// <summary>
        /// Item rebuild limit counts (int array).
        /// Field ID: 8
        /// </summary>
        public int[] ItemRebuildLimitCount { get; set; }

        /// <summary>
        /// Tracks count (derived from list).
        /// Field ID: 9
        /// </summary>
        public byte TracksCount => (byte)(TracksSet?.Count ?? 0);

        /// <summary>
        /// Tracks set entries.
        /// Field ID: 10
        /// </summary>
        public List<TlvTypeCountArgs> TracksSet { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 5: reader.ReadByte(); break; // itemRebuildTypeCount, derived
                    case 6: LastItemRebuildTime = reader.ReadLong(); break;
                    case 7:
                        int len7 = reader.ReadInt();
                        if (len7 > 0 && len7 <= MaxRebuildTypes)
                            ItemRebuildLimitId = reader.ReadBytes(len7);
                        break;
                    case 8: ItemRebuildLimitCount = ReadTlvIntArray(reader); break;
                    case 9: reader.ReadByte(); break; // tracksCount, derived
                    case 10: TracksSet = ReadTlvList<TlvTypeCountArgs>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemRebuildLimitId?.Length ?? 0) > MaxRebuildTypes)
                throw new InvalidDataException($"[TlvItemRebuildData] ItemRebuildLimitId exceeds the maximum of {MaxRebuildTypes} elements.");
            if ((TracksSet?.Count ?? 0) > MaxTracks)
                throw new InvalidDataException($"[TlvItemRebuildData] TracksSet exceeds the maximum of {MaxTracks} elements.");

            WriteTlvByte(writer, 5, ItemRebuildTypeCount);
            WriteTlvLong(writer, 6, LastItemRebuildTime);
            WriteTlvByteArray(writer, 7, ItemRebuildLimitId, ItemRebuildTypeCount);
            WriteTlvIntArray(writer, 8, ItemRebuildLimitCount, ItemRebuildTypeCount);
            WriteTlvByte(writer, 9, TracksCount);
            WriteTlvList(writer, 10, TracksSet);
        }
    }
}
