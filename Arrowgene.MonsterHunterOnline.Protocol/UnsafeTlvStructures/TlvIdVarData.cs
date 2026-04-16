using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for ID with int array + byte array + lastUpdate.
    /// C++ Reader: crygame.dll+sub_102456A0 (UnkTlv0290)
    /// C++ Printer: crygame.dll+sub_10245E50
    /// </summary>
    public class TlvIdVarData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxIntElements = 16;
        public const int MaxByteElements = 1024;

        /// <summary>
        /// ID.
        /// Field ID: 1
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Integer variable array.
        /// Field ID: 2
        /// </summary>
        public int[] Vars { get; set; }

        /// <summary>
        /// Data length (derived from Data).
        /// Field ID: 3
        /// </summary>
        public int Length => Data?.Length ?? 0;

        /// <summary>
        /// Byte data array.
        /// Field ID: 4
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Last update time.
        /// Field ID: 5
        /// </summary>
        public uint LastUpdate { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Vars?.Length ?? 0) > MaxIntElements)
                throw new InvalidDataException($"[TlvIdVarData] Vars exceeds the maximum of {MaxIntElements} elements.");
            if ((Data?.Length ?? 0) > MaxByteElements)
                throw new InvalidDataException($"[TlvIdVarData] Data exceeds the maximum of {MaxByteElements} bytes.");

            WriteTlvInt32(buffer, 1, (int)Id);
            WriteTlvInt32Arr(buffer, 2, Vars);
            WriteTlvInt32(buffer, 3, Length);
            WriteTlvByteArr(buffer, 4, Data);
            WriteTlvInt32(buffer, 5, (int)LastUpdate);
        }
    }
}
