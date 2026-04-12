using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for store data.
    /// C++ Reader: crygame.dll+sub_10129230 (UnkTlv0035)
    /// C++ Printer: crygame.dll+sub_10129B30
    /// </summary>
    public class TlvStoreData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundaries ---
        public const int MaxStores = 8;
        public const int MaxStoreDataSize = 200000;

        /// <summary>
        /// Current store number.
        /// Field ID: 1
        /// </summary>
        public byte CurNum { get; set; }

        /// <summary>
        /// Store count (derived from list).
        /// Field ID: 2
        /// </summary>
        public byte Count => (byte)(Stores?.Count ?? 0);

        /// <summary>
        /// Store entries (idx + name).
        /// Field ID: 3
        /// </summary>
        public List<TlvIdxName> Stores { get; set; }

        /// <summary>
        /// Store data size (derived from array).
        /// Field ID: 4
        /// </summary>
        public int StoreSize => StoreData?.Length ?? 0;

        /// <summary>
        /// Store data bytes.
        /// Field ID: 5
        /// </summary>
        public byte[] StoreData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            // --- BOUNDARY CHECK ---
            if ((Stores?.Count ?? 0) > MaxStores)
                throw new InvalidDataException($"[TlvStoreData] Stores exceeds the maximum of {MaxStores} elements.");
            if ((StoreData?.Length ?? 0) > MaxStoreDataSize)
                throw new InvalidDataException($"[TlvStoreData] StoreData exceeds the maximum of {MaxStoreDataSize} bytes.");

            WriteTlvByte(buffer, 1, CurNum);
            WriteTlvByte(buffer, 2, Count);
            WriteTlvSubStructureList(buffer, 3, Stores.Count, Stores);
            WriteTlvInt32(buffer, 4, StoreSize);
            WriteTlvByteArr(buffer, 5, StoreData);
        }
    }
}
