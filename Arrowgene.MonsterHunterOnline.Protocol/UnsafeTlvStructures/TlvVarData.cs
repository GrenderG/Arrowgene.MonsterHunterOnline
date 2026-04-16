using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for activity variables.
    /// C++ Reader: crygame.dll+sub_10154F30 (UnkTlv0085)
    /// C++ Printer: crygame.dll+sub_10155200
    /// </summary>
    public class TlvVarData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxVarCount = 16;

        /// <summary>
        /// Variable count (derived from Vars array length).
        /// Field ID: 1
        /// </summary>
        public byte VarNum => (byte)(VarData?.Length ?? 0);

        /// <summary>
        /// Variable values (int array).
        /// Field ID: 2
        /// </summary>
        public int[] VarData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((VarData?.Length ?? 0) > MaxVarCount)
                throw new InvalidDataException($"[TlvVarData] VarData exceeds the maximum of {MaxVarCount} elements.");

            WriteTlvByte(buffer, 1, VarNum);
            WriteTlvInt32Arr(buffer, 2, VarData);
        }
    }
}
