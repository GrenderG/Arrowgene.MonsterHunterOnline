using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for daily task stats with short array.
    /// C++ Reader: crygame.dll+sub_10223460 (UnkTlv0254)
    /// C++ Printer: crygame.dll+sub_10223A30
    /// </summary>
    public class TlvDailyTaskStats : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxDaily = 64;

        /// <summary>
        /// Daily count (derived from Daily array).
        /// Field ID: 1
        /// </summary>
        public short DailyCount => (short)(Daily?.Length ?? 0);

        /// <summary>
        /// Daily task IDs (short array).
        /// Field ID: 2
        /// </summary>
        public short[] Daily { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Refresh level.
        /// Field ID: 4
        /// </summary>
        public int RefreshLevel { get; set; }

        /// <summary>
        /// Complete count.
        /// Field ID: 5
        /// </summary>
        public int CompleteCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Daily?.Length ?? 0) > MaxDaily)
                throw new InvalidDataException($"[TlvDailyTaskStats] Daily exceeds the maximum of {MaxDaily} elements.");

            WriteTlvInt16(buffer, 1, DailyCount);
            WriteTlvInt16Arr(buffer, 2, Daily);
            WriteTlvInt32(buffer, 3, (int)RefreshTime);
            WriteTlvInt32(buffer, 4, RefreshLevel);
            WriteTlvInt32(buffer, 5, CompleteCount);
        }
    }
}
