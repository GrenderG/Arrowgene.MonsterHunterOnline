using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for dual int arrays (20 max each).
    /// C++ Reader: crygame.dll+sub_1017F930 (UnkTlv0143)
    /// C++ Printer: crygame.dll+sub_1017FE40
    /// </summary>
    public class TlvWeaponRecord : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 20;

        /// <summary>
        /// Weapon record data.
        /// Field ID: 1
        /// </summary>
        public int[] WeaponRecord { get; set; }

        /// <summary>
        /// Weapon record time data.
        /// Field ID: 2
        /// </summary>
        public int[] WeaponRecordTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((WeaponRecord?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvWeaponRecord] WeaponRecord exceeds the maximum of {MaxElements} elements.");
            if ((WeaponRecordTime?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvWeaponRecord] WeaponRecordTime exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt32Arr(buffer, 1, WeaponRecord);
            WriteTlvInt32Arr(buffer, 2, WeaponRecordTime);
        }
    }
}
