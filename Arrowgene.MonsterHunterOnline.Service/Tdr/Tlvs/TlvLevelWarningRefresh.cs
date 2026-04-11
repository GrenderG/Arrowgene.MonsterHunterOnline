using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for level warning refresh data.
    /// C++ Reader: crygame.dll+sub_1014A730 (UnkTlv0075)
    /// C++ Printer: crygame.dll+sub_1014B090
    /// </summary>
    public class TlvLevelWarningRefresh : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxWarnings = 10;

        /// <summary>
        /// Last refresh time.
        /// Field ID: 1
        /// </summary>
        public uint LastRefreshTm { get; set; }

        /// <summary>
        /// Reward count.
        /// Field ID: 2
        /// </summary>
        public byte RewardCnt { get; set; }

        /// <summary>
        /// Level count (derived from WarningData).
        /// Field ID: 3
        /// </summary>
        public byte LevelCnt => (byte)(WarningData?.Count ?? 0);

        /// <summary>
        /// Warning data list.
        /// Field ID: 4
        /// </summary>
        public List<TlvLevelWarning> WarningData { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LastRefreshTm = (uint)reader.ReadInt(); break;
                    case 2: RewardCnt = reader.ReadByte(); break;
                    case 3: reader.ReadByte(); break; // levelCnt, derived
                    case 4: WarningData = ReadTlvList<TlvLevelWarning>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((WarningData?.Count ?? 0) > MaxWarnings)
                throw new InvalidDataException($"[TlvLevelWarningRefresh] WarningData exceeds the maximum of {MaxWarnings} elements.");

            WriteTlvInt(writer, 1, (int)LastRefreshTm);
            WriteTlvByte(writer, 2, RewardCnt);
            WriteTlvByte(writer, 3, LevelCnt);
            WriteTlvList(writer, 4, WarningData);
        }
    }
}
