using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for level data with byte and int arrays.
    /// C++ Reader: crygame.dll+sub_10146FC0 (UnkTlv0070)
    /// C++ Printer: crygame.dll+sub_101474D0
    /// </summary>
    public class TlvLevelData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxDataCount = 10;

        /// <summary>
        /// Level ID.
        /// Field ID: 1
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Data count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public byte DataCnt => (byte)(DataType?.Length ?? 0);

        /// <summary>
        /// Data type bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] DataType { get; set; }

        /// <summary>
        /// Data values (int array).
        /// Field ID: 4
        /// </summary>
        public int[] DataVal { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LevelId = reader.ReadInt(); break;
                    case 2: reader.ReadByte(); break; // dataCnt, derived from arrays
                    case 3:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxDataCount)
                            DataType = reader.ReadBytes(byteLen);
                        break;
                    case 4: DataVal = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((DataType?.Length ?? 0) > MaxDataCount)
                throw new InvalidDataException($"[TlvLevelData] DataType exceeds the maximum of {MaxDataCount} elements.");
            if ((DataVal?.Length ?? 0) > MaxDataCount)
                throw new InvalidDataException($"[TlvLevelData] DataVal exceeds the maximum of {MaxDataCount} elements.");

            WriteTlvInt(writer, 1, LevelId);
            WriteTlvByte(writer, 2, DataCnt);
            WriteTlvByteArray(writer, 3, DataType, DataCnt);
            WriteTlvIntArray(writer, 4, DataVal, DataCnt);
        }
    }
}
