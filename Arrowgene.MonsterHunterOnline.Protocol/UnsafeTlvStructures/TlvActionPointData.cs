using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for action point data.
    /// C++ Reader: crygame.dll+sub_101660B0 (UnkTlv0104)
    /// C++ Printer: crygame.dll+sub_10166570
    /// </summary>
    public class TlvActionPointData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxResetTimes = 2;

        /// <summary>
        /// Action point values (int array, max 2).
        /// Field ID: 2
        /// </summary>
        public int[] ActionPoint { get; set; }

        /// <summary>
        /// Next reset time.
        /// Field ID: 3
        /// </summary>
        public int NextResetTime { get; set; }

        /// <summary>
        /// Action point flags.
        /// Field ID: 4
        /// </summary>
        public uint ActionPointFlags { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((ActionPoint?.Length ?? 0) > MaxResetTimes)
                throw new InvalidDataException($"[TlvActionPointData] ActionPoint exceeds the maximum of {MaxResetTimes} elements.");

            WriteTlvInt32Arr(buffer, 2, ActionPoint);
            WriteTlvInt32(buffer, 3, NextResetTime);
            WriteTlvInt32(buffer, 4, (int)ActionPointFlags);
        }
    }
}
