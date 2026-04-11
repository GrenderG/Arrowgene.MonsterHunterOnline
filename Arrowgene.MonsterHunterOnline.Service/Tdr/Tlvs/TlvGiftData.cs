using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for gift data with nested gift attributes.
    /// C++ Reader: crygame.dll+sub_1024CA40 (UnkTlv0299)
    /// C++ Printer: crygame.dll+sub_1024CF20
    /// </summary>
    public class TlvGiftData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxGifts = 100;

        /// <summary>
        /// Gift attributes (nested sub-structure).
        /// Field ID: 2
        /// </summary>
        public TlvOnlineConsecRefresh GiftAttr { get; set; } = new();

        /// <summary>
        /// Gift count (derived from arrays).
        /// Field ID: 3
        /// </summary>
        public byte GiftNum => (byte)(GiftId?.Length ?? 0);

        /// <summary>
        /// Gift IDs (int array).
        /// Field ID: 4
        /// </summary>
        public int[] GiftId { get; set; }

        /// <summary>
        /// Gift states (byte array).
        /// Field ID: 5
        /// </summary>
        public byte[] GiftState { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: GiftAttr = ReadTlvObject<TlvOnlineConsecRefresh>(reader); break;
                    case 3: reader.ReadByte(); break; // giftNum, derived
                    case 4: GiftId = ReadTlvIntArray(reader); break;
                    case 5:
                        int byteLen = reader.ReadInt();
                        if (byteLen > 0 && byteLen <= MaxGifts)
                            GiftState = reader.ReadBytes(byteLen);
                        break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((GiftId?.Length ?? 0) > MaxGifts)
                throw new InvalidDataException($"[TlvGiftData] GiftId exceeds the maximum of {MaxGifts} elements.");
            if ((GiftState?.Length ?? 0) > MaxGifts)
                throw new InvalidDataException($"[TlvGiftData] GiftState exceeds the maximum of {MaxGifts} elements.");

            WriteTlvObject(writer, 2, GiftAttr);
            WriteTlvByte(writer, 3, GiftNum);
            WriteTlvIntArray(writer, 4, GiftId, GiftNum);
            WriteTlvByteArray(writer, 5, GiftState, GiftNum);
        }
    }
}
