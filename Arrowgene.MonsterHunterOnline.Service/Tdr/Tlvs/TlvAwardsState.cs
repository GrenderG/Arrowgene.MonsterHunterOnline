using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for awards state with refresh time + byte/int arrays.
    /// C++ Reader: crygame.dll+sub_101777A0 (UnkTlv0131)
    /// C++ Printer: crygame.dll+sub_10177E90
    /// </summary>
    public class TlvAwardsState : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxAwards = 5;

        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Count (derived from arrays).
        /// Field ID: 2
        /// </summary>
        public int Count => AwardsType?.Length ?? 0;

        /// <summary>
        /// Award type bytes.
        /// Field ID: 3
        /// </summary>
        public byte[] AwardsType { get; set; }

        /// <summary>
        /// Award state bytes.
        /// Field ID: 4
        /// </summary>
        public byte[] AwardsState { get; set; }

        /// <summary>
        /// Award ID ints.
        /// Field ID: 5
        /// </summary>
        public int[] AwardsId { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: RefreshTime = (uint)reader.ReadInt(); break;
                    case 2: reader.ReadInt(); break; // count, derived
                    case 3:
                        int len3 = reader.ReadInt();
                        if (len3 > 0 && len3 <= MaxAwards)
                            AwardsType = reader.ReadBytes(len3);
                        break;
                    case 4:
                        int len4 = reader.ReadInt();
                        if (len4 > 0 && len4 <= MaxAwards)
                            AwardsState = reader.ReadBytes(len4);
                        break;
                    case 5: AwardsId = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((AwardsType?.Length ?? 0) > MaxAwards)
                throw new InvalidDataException($"[TlvAwardsState] AwardsType exceeds the maximum of {MaxAwards} elements.");
            if ((AwardsState?.Length ?? 0) > MaxAwards)
                throw new InvalidDataException($"[TlvAwardsState] AwardsState exceeds the maximum of {MaxAwards} elements.");
            if ((AwardsId?.Length ?? 0) > MaxAwards)
                throw new InvalidDataException($"[TlvAwardsState] AwardsId exceeds the maximum of {MaxAwards} elements.");

            WriteTlvInt(writer, 1, (int)RefreshTime);
            WriteTlvInt(writer, 2, Count);
            WriteTlvByteArray(writer, 3, AwardsType, Count);
            WriteTlvByteArray(writer, 4, AwardsState, Count);
            WriteTlvIntArray(writer, 5, AwardsId, Count);
        }
    }
}
