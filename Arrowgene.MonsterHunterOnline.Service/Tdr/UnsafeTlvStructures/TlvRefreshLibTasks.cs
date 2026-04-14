using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for refresh + lib + task short array.
    /// C++ Reader: crygame.dll+sub_10227320 (UnkTlv0259)
    /// C++ Printer: crygame.dll+sub_10227850
    /// </summary>
    public class TlvRefreshLibTasks : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxTasks = 256;

        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Library ID.
        /// Field ID: 2
        /// </summary>
        public int Lib { get; set; }

        /// <summary>
        /// Task count (derived from Tasks array).
        /// Field ID: 3
        /// </summary>
        public short TaskCount => (short)(Tasks?.Length ?? 0);

        /// <summary>
        /// Task IDs (short array).
        /// Field ID: 4
        /// </summary>
        public short[] Tasks { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Tasks?.Length ?? 0) > MaxTasks)
                throw new InvalidDataException($"[TlvRefreshLibTasks] Tasks exceeds the maximum of {MaxTasks} elements.");

            WriteTlvInt32(buffer, 1, (int)RefreshTime);
            WriteTlvInt32(buffer, 2, Lib);
            WriteTlvInt16(buffer, 3, TaskCount);
            WriteTlvInt16Arr(buffer, 4, Tasks);
        }
    }
}
