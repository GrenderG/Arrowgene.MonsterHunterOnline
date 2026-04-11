using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for group entrust stat data.
    /// C++ Reader: crygame.dll+sub_10145950 (UnkTlv0069)
    /// C++ Printer: crygame.dll+sub_10146230
    /// </summary>
    public class TlvGroupEntrustStatData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxGroupStats = 10;
        public const int MaxLevels = 64;

        /// <summary>
        /// Group ID.
        /// Field ID: 1
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// Group stat count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public short GroupStatCnt => (short)(GroupStatType?.Length ?? 0);

        /// <summary>
        /// Group stat types (byte array).
        /// Field ID: 3
        /// </summary>
        public byte[] GroupStatType { get; set; }

        /// <summary>
        /// Group stat values (int array).
        /// Field ID: 4
        /// </summary>
        public int[] GroupStatValue { get; set; }

        /// <summary>
        /// Group level count (derived from list).
        /// Field ID: 5
        /// </summary>
        public short GroupLevelCnt => (short)(EntrustLevelStat?.Count ?? 0);

        /// <summary>
        /// Entrust level stats.
        /// Field ID: 6
        /// </summary>
        public List<TlvLevelStatData> EntrustLevelStat { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: GroupID = reader.ReadInt(); break;
                    case 2: reader.ReadShort(); break; // groupStatCnt, derived
                    case 3:
                        int len3 = reader.ReadInt();
                        if (len3 > 0 && len3 <= MaxGroupStats)
                            GroupStatType = reader.ReadBytes(len3);
                        break;
                    case 4: GroupStatValue = ReadTlvIntArray(reader); break;
                    case 5: reader.ReadShort(); break; // groupLevelCnt, derived
                    case 6: EntrustLevelStat = ReadTlvList<TlvLevelStatData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((GroupStatType?.Length ?? 0) > MaxGroupStats)
                throw new InvalidDataException($"[TlvGroupEntrustStatData] GroupStatType exceeds the maximum of {MaxGroupStats} elements.");
            if ((EntrustLevelStat?.Count ?? 0) > MaxLevels)
                throw new InvalidDataException($"[TlvGroupEntrustStatData] EntrustLevelStat exceeds the maximum of {MaxLevels} elements.");

            WriteTlvInt(writer, 1, GroupID);
            WriteTlvShort(writer, 2, GroupStatCnt);
            WriteTlvByteArray(writer, 3, GroupStatType, GroupStatCnt);
            WriteTlvIntArray(writer, 4, GroupStatValue, GroupStatCnt);
            WriteTlvShort(writer, 5, GroupLevelCnt);
            WriteTlvList(writer, 6, EntrustLevelStat);
        }
    }
}
