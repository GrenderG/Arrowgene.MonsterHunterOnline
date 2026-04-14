using System;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for index with dual int arrays.
    /// C++ Reader: crygame.dll+sub_10202E10 (UnkTlv0216)
    /// C++ Printer: crygame.dll+sub_10203470
    /// </summary>
    public class TlvEquipData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxElements = 1256;

        /// <summary>
        /// Index (short).
        /// Field ID: 1
        /// </summary>
        public short Index { get; set; }

        /// <summary>
        /// Data length (derived from DataBytes).
        /// Field ID: 2
        /// </summary>
        public int DataLen => EquipData?.Length ?? 0;

        /// <summary>
        /// Equipment data bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] EquipData { get; set; }

        /// <summary>
        /// Second int array.
        /// Field ID: 4
        /// </summary>
        public int[] DataInts { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((EquipData?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvEquipData] EquipData exceeds the maximum of {MaxElements} elements.");
            if ((DataInts?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvEquipData] DataInts exceeds the maximum of {MaxElements} elements.");

            WriteTlvInt16(buffer, 1, Index);
            WriteTlvInt32(buffer, 2, DataLen);
            WriteTlvByteArr(buffer, 3, EquipData);
            WriteTlvInt32Arr(buffer, 4, DataInts);
        }
    }
}
