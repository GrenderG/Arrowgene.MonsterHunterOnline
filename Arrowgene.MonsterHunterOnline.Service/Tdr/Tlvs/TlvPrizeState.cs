using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for prize state with refresh time.
    /// C++ Reader: crygame.dll+sub_10172510 (UnkTlv0124)
    /// C++ Printer: crygame.dll+sub_10172AE0
    /// </summary>
    public class TlvPrizeState : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxPrizes = 20;

        /// <summary>
        /// Refresh time.
        /// Field ID: 2
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Count (derived from arrays).
        /// Field ID: 4
        /// </summary>
        public int Count => PrizeId?.Length ?? 0;

        /// <summary>
        /// Prize IDs (int array).
        /// Field ID: 5
        /// </summary>
        public int[] PrizeId { get; set; }

        /// <summary>
        /// State bytes.
        /// Field ID: 6
        /// </summary>
        public byte[] State { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: RefreshTime = (uint)reader.ReadInt(); break;
                    case 4: reader.ReadInt(); break; // count, derived
                    case 5: PrizeId = ReadTlvIntArray(reader); break;
                    case 6:
                        int len6 = reader.ReadInt();
                        if (len6 > 0 && len6 <= MaxPrizes)
                            State = reader.ReadBytes(len6);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((PrizeId?.Length ?? 0) > MaxPrizes)
                throw new InvalidDataException($"[TlvPrizeState] PrizeId exceeds the maximum of {MaxPrizes} elements.");
            if ((State?.Length ?? 0) > MaxPrizes)
                throw new InvalidDataException($"[TlvPrizeState] State exceeds the maximum of {MaxPrizes} bytes.");

            WriteTlvInt(writer, 2, (int)RefreshTime);
            WriteTlvInt(writer, 4, Count);
            WriteTlvIntArray(writer, 5, PrizeId, Count);
            WriteTlvByteArray(writer, 6, State, Count);
        }
    }
}
