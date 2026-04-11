using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for global/level/mode stat data container.
    /// C++ Reader: crygame.dll+sub_10148A50 (UnkTlv0072)
    /// C++ Printer: crygame.dll+sub_10149410
    /// </summary>
    public class TlvGlobalLevelStatContainer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadByte(); break; // globalStatCnt, derived
                    case 2:
                        int len2 = reader.ReadInt();
                        if (len2 > 0 && len2 <= MaxGlobalStats)
                            GlobalStatDataType = reader.ReadBytes(len2);
                        break;
                    case 3: GlobalStatDataVal = ReadTlvIntArray(reader); break;
                    case 4: reader.ReadShort(); break; // levelDataCnt, derived
                    case 5: LevelStatDataInfo = ReadTlvList<TlvLevelData>(reader); break;
                    case 6: reader.ReadByte(); break; // levelModeDataCnt, derived
                    case 7: LevelModeStatDataInfo = ReadTlvList<TlvLevelModeStat>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((GlobalStatDataType?.Length ?? 0) > MaxGlobalStats)
                throw new InvalidDataException($"[TlvGlobalLevelStatContainer] GlobalStatDataType exceeds the maximum of {MaxGlobalStats} elements.");
            if ((LevelStatDataInfo?.Count ?? 0) > MaxLevelData)
                throw new InvalidDataException($"[TlvGlobalLevelStatContainer] LevelStatDataInfo exceeds the maximum of {MaxLevelData} elements.");
            if ((LevelModeStatDataInfo?.Count ?? 0) > MaxModeData)
                throw new InvalidDataException($"[TlvGlobalLevelStatContainer] LevelModeStatDataInfo exceeds the maximum of {MaxModeData} elements.");

            WriteTlvByte(writer, 1, GlobalStatCnt);
            WriteTlvByteArray(writer, 2, GlobalStatDataType, GlobalStatCnt);
            WriteTlvIntArray(writer, 3, GlobalStatDataVal, GlobalStatCnt);
            WriteTlvShort(writer, 4, LevelDataCnt);
            WriteTlvList(writer, 5, LevelStatDataInfo);
            WriteTlvByte(writer, 6, LevelModeDataCnt);
            WriteTlvList(writer, 7, LevelModeStatDataInfo);
        }
    }
}
