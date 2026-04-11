using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for cat cuisine data.
    /// C++ Reader: crygame.dll+sub_1017DF80 (UnkTlv0141)
    /// C++ Printer: crygame.dll+sub_1017E490
    /// </summary>
    public class TlvCatCuisineData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxOpenSlots = 7;

        /// <summary>
        /// Cat time.
        /// Field ID: 1
        /// </summary>
        public long CatTime { get; set; }

        /// <summary>
        /// Open slots data (byte array).
        /// Field ID: 2
        /// </summary>
        public byte[] OpenData { get; set; }

        /// <summary>
        /// Open flag.
        /// Field ID: 3
        /// </summary>
        public byte IsOpen { get; set; }

        /// <summary>
        /// Open time.
        /// Field ID: 4
        /// </summary>
        public long OpenTime { get; set; }

        /// <summary>
        /// Tools flag.
        /// Field ID: 5
        /// </summary>
        public byte Tools { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: CatTime = reader.ReadLong(); break;
                    case 2:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxOpenSlots)
                            OpenData = reader.ReadBytes(byteLen);
                        break;
                    case 3: IsOpen = reader.ReadByte(); break;
                    case 4: OpenTime = reader.ReadLong(); break;
                    case 5: Tools = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((OpenData?.Length ?? 0) > MaxOpenSlots)
                throw new InvalidDataException($"[TlvCatCuisineData] OpenData exceeds the maximum of {MaxOpenSlots} elements.");

            WriteTlvLong(writer, 1, CatTime);
            WriteTlvByteArray(writer, 2, OpenData, OpenData?.Length ?? 0);
            WriteTlvByte(writer, 3, IsOpen);
            WriteTlvLong(writer, 4, OpenTime);
            WriteTlvByte(writer, 5, Tools);
        }
    }
}
