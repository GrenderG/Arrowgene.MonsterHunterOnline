using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for item rebuild data with limit tracking.
    /// C++ Reader: crygame.dll+sub_1017BA00 (UnkTlv0137)
    /// C++ Printer: crygame.dll+sub_1017BDC0
    /// </summary>
    public class TlvItemRebuildData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((ItemRebuildLimitId?.Length ?? 0) > MaxRebuildTypes)
                throw new InvalidDataException($"[TlvItemRebuildData] ItemRebuildLimitId exceeds the maximum of {MaxRebuildTypes} elements.");
            if ((TracksSet?.Count ?? 0) > MaxTracks)
                throw new InvalidDataException($"[TlvItemRebuildData] TracksSet exceeds the maximum of {MaxTracks} elements.");

            WriteTlvByte(buffer, 5, ItemRebuildTypeCount);
            WriteTlvInt64(buffer, 6, LastItemRebuildTime);
            WriteTlvByteArr(buffer, 7, ItemRebuildLimitId);
            WriteTlvInt32Arr(buffer, 8, ItemRebuildLimitCount);
            WriteTlvByte(buffer, 9, TracksCount);
            WriteTlvSubStructureList(buffer, 10, TracksSet.Count, TracksSet);
        }
    }
}
