using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for monster size/flag data.
    /// C++ Reader: crygame.dll+sub_1014B820 (UnkTlv0076)
    /// C++ Printer: crygame.dll+sub_1014C2E0
    /// </summary>
    public class TlvMonsterSizeData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
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

            WriteTlvInt32(buffer, 1, MonsterCnt);
            WriteTlvInt32Arr(buffer, 3, MonsterId);
            WriteTlvFloatArr(buffer, 4, MaxSize);
            WriteTlvFloatArr(buffer, 5, MinSize);
            WriteTlvByteArr(buffer, 6, MaxFlag);
            WriteTlvByteArr(buffer, 7, MinFlag);
        }
    }
}
