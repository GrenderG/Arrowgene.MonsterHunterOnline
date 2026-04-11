using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for store data.
    /// C++ Reader: crygame.dll+sub_10129230 (UnkTlv0035)
    /// C++ Printer: crygame.dll+sub_10129B30
    /// </summary>
    public class TlvStoreData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CurNum = reader.ReadByte(); break;
                    case 2: reader.ReadByte(); break; // count, derived
                    case 3: Stores = ReadTlvList<TlvIdxName>(reader); break;
                    case 4: reader.ReadInt(); break; // storeSize, derived
                    case 5:
                        int len5 = reader.ReadInt();
                        if (len5 > 0 && len5 <= MaxStoreDataSize)
                            StoreData = reader.ReadBytes(len5);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Stores?.Count ?? 0) > MaxStores)
                throw new InvalidDataException($"[TlvStoreData] Stores exceeds the maximum of {MaxStores} elements.");
            if ((StoreData?.Length ?? 0) > MaxStoreDataSize)
                throw new InvalidDataException($"[TlvStoreData] StoreData exceeds the maximum of {MaxStoreDataSize} bytes.");

            WriteTlvByte(writer, 1, CurNum);
            WriteTlvByte(writer, 2, Count);
            WriteTlvList(writer, 3, Stores);
            WriteTlvInt(writer, 4, StoreSize);
            WriteTlvByteArray(writer, 5, StoreData, StoreSize);
        }
    }
}
