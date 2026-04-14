using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for dual int arrays (4 max each).
    /// C++ Reader: crygame.dll+sub_10201480 (UnkTlv0214)
    /// C++ Printer: crygame.dll+sub_10201930
    /// </summary>
    public class TlvPetIdStartTime : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 4;

        /// <summary>
        /// Pet IDs.
        /// Field ID: 1
        /// </summary>
        public int[] PetId { get; set; }

        /// <summary>
        /// Start times.
        /// Field ID: 2
        /// </summary>
        public int[] StartTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((PetId?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvPetIdStartTime] PetId exceeds the maximum of {MaxElements} elements.");
            if ((StartTime?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvPetIdStartTime] StartTime exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt32Arr(buffer, 1, PetId);
            WriteTlvInt32Arr(buffer, 2, StartTime);
        }
    }
}
