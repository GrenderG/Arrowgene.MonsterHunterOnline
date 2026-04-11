using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for monster size/flag data.
    /// C++ Reader: crygame.dll+sub_1014B820 (UnkTlv0076)
    /// C++ Printer: crygame.dll+sub_1014C2E0
    /// </summary>
    public class TlvMonsterSizeData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxMonsters = 512;

        /// <summary>
        /// Monster count (derived from arrays).
        /// Field ID: 1
        /// </summary>
        public int MonsterCnt => MonsterId?.Length ?? 0;

        /// <summary>
        /// Monster IDs (int array).
        /// Field ID: 3
        /// </summary>
        public int[] MonsterId { get; set; }

        /// <summary>
        /// Max sizes (float array).
        /// Field ID: 4
        /// </summary>
        public float[] MaxSize { get; set; }

        /// <summary>
        /// Min sizes (float array).
        /// Field ID: 5
        /// </summary>
        public float[] MinSize { get; set; }

        /// <summary>
        /// Max flags (byte array).
        /// Field ID: 6
        /// </summary>
        public byte[] MaxFlag { get; set; }

        /// <summary>
        /// Min flags (byte array).
        /// Field ID: 7
        /// </summary>
        public byte[] MinFlag { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break; // monsterCnt, derived
                    case 3: MonsterId = ReadTlvIntArray(reader); break;
                    case 4: MaxSize = ReadTlvFloatArray(reader); break;
                    case 5: MinSize = ReadTlvFloatArray(reader); break;
                    case 6:
                        int len6 = reader.ReadInt();
                        if (len6 > 0 && len6 <= MaxMonsters)
                            MaxFlag = reader.ReadBytes(len6);
                        break;
                    case 7:
                        int len7 = reader.ReadInt();
                        if (len7 > 0 && len7 <= MaxMonsters)
                            MinFlag = reader.ReadBytes(len7);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((MonsterId?.Length ?? 0) > MaxMonsters)
                throw new InvalidDataException($"[TlvMonsterSizeData] MonsterId exceeds the maximum of {MaxMonsters} elements.");
            if ((MaxSize?.Length ?? 0) > MaxMonsters)
                throw new InvalidDataException($"[TlvMonsterSizeData] MaxSize exceeds the maximum of {MaxMonsters} elements.");
            if ((MinSize?.Length ?? 0) > MaxMonsters)
                throw new InvalidDataException($"[TlvMonsterSizeData] MinSize exceeds the maximum of {MaxMonsters} elements.");
            if ((MaxFlag?.Length ?? 0) > MaxMonsters)
                throw new InvalidDataException($"[TlvMonsterSizeData] MaxFlag exceeds the maximum of {MaxMonsters} elements.");
            if ((MinFlag?.Length ?? 0) > MaxMonsters)
                throw new InvalidDataException($"[TlvMonsterSizeData] MinFlag exceeds the maximum of {MaxMonsters} elements.");

            WriteTlvInt(writer, 1, MonsterCnt);
            WriteTlvIntArray(writer, 3, MonsterId, MonsterCnt);
            WriteTlvFloatArray(writer, 4, MaxSize);
            WriteTlvFloatArray(writer, 5, MinSize);
            WriteTlvByteArray(writer, 6, MaxFlag, MonsterCnt);
            WriteTlvByteArray(writer, 7, MinFlag, MonsterCnt);
        }
    }
}
