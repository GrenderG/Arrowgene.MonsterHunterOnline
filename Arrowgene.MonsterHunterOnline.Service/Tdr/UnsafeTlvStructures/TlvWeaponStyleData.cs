using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for single int array (20 max).
    /// C++ Reader: crygame.dll+sub_10180360 (UnkTlv0144)
    /// C++ Printer: crygame.dll+sub_101805F0
    /// </summary>
    public class TlvWeaponStyleData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 20;

        /// <summary>
        /// Weapon style data.
        /// Field ID: 1
        /// </summary>
        public int[] WeaponStyleData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((WeaponStyleData?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvWeaponStyleData] WeaponStyleData exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt32Arr(buffer, 1, WeaponStyleData);
        }
    }
}
