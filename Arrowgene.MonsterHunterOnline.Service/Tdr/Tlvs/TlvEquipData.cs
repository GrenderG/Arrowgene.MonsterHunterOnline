using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for index with dual int arrays.
    /// C++ Reader: crygame.dll+sub_10202E10 (UnkTlv0216)
    /// C++ Printer: crygame.dll+sub_10203470
    /// </summary>
    public class TlvEquipData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Index = reader.ReadShort(); break;
                    case 2: reader.ReadInt(); break; // dataLen, derived from array
                    case 3:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxElements)
                            EquipData = reader.ReadBytes(byteLen);
                        break;
                    case 4: DataInts = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((EquipData?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvEquipData] EquipData exceeds the maximum of {MaxElements} elements.");
            if ((DataInts?.Length ?? 0) > MaxElements)
                throw new InvalidDataException($"[TlvEquipData] DataInts exceeds the maximum of {MaxElements} elements.");

            WriteTlvShort(writer, 1, Index);
            WriteTlvInt(writer, 2, DataLen);
            WriteTlvByteArray(writer, 3, EquipData, DataLen);
            WriteTlvIntArray(writer, 4, DataInts);
        }
    }
}
