using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for ID with int array + byte array + lastUpdate.
    /// C++ Reader: crygame.dll+sub_102456A0 (UnkTlv0290)
    /// C++ Printer: crygame.dll+sub_10245E50
    /// </summary>
    public class TlvIdVarData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = (uint)reader.ReadInt(); break;
                    case 2: Vars = ReadTlvIntArray(reader); break;
                    case 3: reader.ReadInt(); break;
                    case 4:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxByteElements)
                            Data = reader.ReadBytes(byteLen);
                        break;
                    case 5: LastUpdate = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Vars?.Length ?? 0) > MaxIntElements)
                throw new InvalidDataException($"[TlvIdVarData] Vars exceeds the maximum of {MaxIntElements} elements.");
            if ((Data?.Length ?? 0) > MaxByteElements)
                throw new InvalidDataException($"[TlvIdVarData] Data exceeds the maximum of {MaxByteElements} bytes.");

            WriteTlvInt(writer, 1, (int)Id);
            WriteTlvIntArray(writer, 2, Vars);
            WriteTlvInt(writer, 3, Length);
            WriteTlvByteArray(writer, 4, Data, Length);
            WriteTlvInt(writer, 5, (int)LastUpdate);
        }
    }
}
