using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for level score data.
    /// C++ Reader: crygame.dll+sub_1014C6F0 (UnkTlv0077)
    /// C++ Printer: crygame.dll+sub_1014D300
    /// </summary>
    public class TlvLevelScoreData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxLevels = 5000;

        /// <summary>
        /// Level count (derived from arrays).
        /// Field ID: 1
        /// </summary>
        public short LevelCnt => (short)(LevelID?.Length ?? 0);

        /// <summary>
        /// Level IDs (int array).
        /// Field ID: 2
        /// </summary>
        public int[] LevelID { get; set; }

        /// <summary>
        /// Best scores (short array).
        /// Field ID: 3
        /// </summary>
        public short[] TheBestScore { get; set; }

        /// <summary>
        /// States (byte array).
        /// Field ID: 4
        /// </summary>
        public byte[] State { get; set; }

        /// <summary>
        /// History final ranks (byte array).
        /// Field ID: 5
        /// </summary>
        public byte[] HistoryFinalRank { get; set; }

        /// <summary>
        /// Gain reward flags (byte array).
        /// Field ID: 6
        /// </summary>
        public byte[] GainRewardFlag { get; set; }

        /// <summary>
        /// Last times (int array).
        /// Field ID: 7
        /// </summary>
        public int[] LastTm { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadShort(); break; // levelCnt, derived
                    case 2: LevelID = ReadTlvIntArray(reader); break;
                    case 3: TheBestScore = ReadTlvShortArray(reader); break;
                    case 4:
                        int len4 = reader.ReadInt();
                        if (len4 > 0 && len4 <= MaxLevels)
                            State = reader.ReadBytes(len4);
                        break;
                    case 5:
                        int len5 = reader.ReadInt();
                        if (len5 > 0 && len5 <= MaxLevels)
                            HistoryFinalRank = reader.ReadBytes(len5);
                        break;
                    case 6:
                        int len6 = reader.ReadInt();
                        if (len6 > 0 && len6 <= MaxLevels)
                            GainRewardFlag = reader.ReadBytes(len6);
                        break;
                    case 7: LastTm = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((LevelID?.Length ?? 0) > MaxLevels)
                throw new InvalidDataException($"[TlvLevelScoreData] LevelID exceeds the maximum of {MaxLevels} elements.");

            WriteTlvShort(writer, 1, LevelCnt);
            WriteTlvIntArray(writer, 2, LevelID, LevelCnt);
            WriteTlvShortArray(writer, 3, TheBestScore);
            WriteTlvByteArray(writer, 4, State, LevelCnt);
            WriteTlvByteArray(writer, 5, HistoryFinalRank, LevelCnt);
            WriteTlvByteArray(writer, 6, GainRewardFlag, LevelCnt);
            WriteTlvIntArray(writer, 7, LastTm, LevelCnt);
        }
    }
}
