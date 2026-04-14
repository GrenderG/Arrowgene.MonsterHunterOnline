using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for type + count + three int arrays.
    /// C++ Reader: crygame.dll+sub_10179DA0 (UnkTlv0135)
    /// C++ Printer: crygame.dll+sub_1017A3D0
    /// </summary>
    public class TlvTypeCountArgs : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxArgs = 5;

        /// <summary>
        /// Type byte.
        /// Field ID: 1
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public int Count => Arg1?.Length ?? 0;

        /// <summary>
        /// Arg1 int array.
        /// Field ID: 4
        /// </summary>
        public int[] Arg1 { get; set; }

        /// <summary>
        /// Arg2 int array.
        /// Field ID: 5
        /// </summary>
        public int[] Arg2 { get; set; }

        /// <summary>
        /// Arg3 int array.
        /// Field ID: 6
        /// </summary>
        public int[] Arg3 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Arg1?.Length ?? 0) > MaxArgs)
                throw new InvalidDataException($"[TlvTypeCountArgs] Arg1 exceeds the maximum of {MaxArgs} elements.");
            if ((Arg2?.Length ?? 0) > MaxArgs)
                throw new InvalidDataException($"[TlvTypeCountArgs] Arg2 exceeds the maximum of {MaxArgs} elements.");
            if ((Arg3?.Length ?? 0) > MaxArgs)
                throw new InvalidDataException($"[TlvTypeCountArgs] Arg3 exceeds the maximum of {MaxArgs} elements.");

            WriteTlvByte(buffer, 1, Type);
            WriteTlvInt32(buffer, 2, Count);
            WriteTlvInt32Arr(buffer, 4, Arg1);
            WriteTlvInt32Arr(buffer, 5, Arg2);
            WriteTlvInt32Arr(buffer, 6, Arg3);
        }
    }
}
