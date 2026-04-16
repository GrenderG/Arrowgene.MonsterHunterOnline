using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for global/level/mode stat data container.
    /// C++ Reader: crygame.dll+sub_10148A50 (UnkTlv0072)
    /// C++ Printer: crygame.dll+sub_10149410
    /// </summary>
    public class TlvGlobalLevelStatContainer : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxGlobalStats = 10;
        public const int MaxLevelData = 5000;
        public const int MaxModeData = 10;

        /// <summary>
        /// Global stat count (derived from arrays).
        /// Field ID: 1
        /// </summary>
        public byte GlobalStatCnt => (byte)(GlobalStatDataType?.Length ?? 0);

        /// <summary>
        /// Global stat data types (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] GlobalStatDataType { get; set; }

        /// <summary>
        /// Global stat data values (int array).
        /// Field ID: 3
        /// </summary>
        public int[] GlobalStatDataVal { get; set; }

        /// <summary>
        /// Level data count (derived from list).
        /// Field ID: 4
        /// </summary>
        public short LevelDataCnt => (short)(LevelStatDataInfo?.Count ?? 0);

        /// <summary>
        /// Level stat data entries.
        /// Field ID: 5
        /// </summary>
        public List<TlvLevelData> LevelStatDataInfo { get; set; }

        /// <summary>
        /// Level mode data count (derived from list).
        /// Field ID: 6
        /// </summary>
        public byte LevelModeDataCnt => (byte)(LevelModeStatDataInfo?.Count ?? 0);

        /// <summary>
        /// Level mode stat data entries.
        /// Field ID: 7
        /// </summary>
        public List<TlvLevelModeStat> LevelModeStatDataInfo { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((GlobalStatDataType?.Length ?? 0) > MaxGlobalStats)
                throw new InvalidDataException($"[TlvGlobalLevelStatContainer] GlobalStatDataType exceeds the maximum of {MaxGlobalStats} elements.");
            if ((LevelStatDataInfo?.Count ?? 0) > MaxLevelData)
                throw new InvalidDataException($"[TlvGlobalLevelStatContainer] LevelStatDataInfo exceeds the maximum of {MaxLevelData} elements.");
            if ((LevelModeStatDataInfo?.Count ?? 0) > MaxModeData)
                throw new InvalidDataException($"[TlvGlobalLevelStatContainer] LevelModeStatDataInfo exceeds the maximum of {MaxModeData} elements.");

            WriteTlvByte(buffer, 1, GlobalStatCnt);
            WriteTlvByteArr(buffer, 2, GlobalStatDataType);
            WriteTlvInt32Arr(buffer, 3, GlobalStatDataVal);
            WriteTlvInt16(buffer, 4, LevelDataCnt);
            WriteTlvSubStructureList(buffer, 5, LevelStatDataInfo.Count, LevelStatDataInfo);
            WriteTlvByte(buffer, 6, LevelModeDataCnt);
            WriteTlvSubStructureList(buffer, 7, LevelModeStatDataInfo.Count, LevelModeStatDataInfo);
        }
    }
}
