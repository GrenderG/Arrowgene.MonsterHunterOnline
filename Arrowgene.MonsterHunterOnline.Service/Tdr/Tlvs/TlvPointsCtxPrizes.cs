using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for points, context info and prize IDs.
    /// C++ Reader: crygame.dll+sub_10175EB0 (UnkTlv0129)
    /// C++ Printer: crygame.dll+sub_101767E0
    /// </summary>
    public class TlvPointsCtxPrizes : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxCtx = 200;
        public const int MaxPrizes = 100;

        /// <summary>
        /// Points.
        /// Field ID: 1
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Context count (derived from CtxInfo).
        /// Field ID: 6
        /// </summary>
        public int CtxCount => CtxInfo?.Length ?? 0;

        /// <summary>
        /// Context info (int array).
        /// Field ID: 7
        /// </summary>
        public int[] CtxInfo { get; set; }

        /// <summary>
        /// Prizes count (derived from PrizesID).
        /// Field ID: 8
        /// </summary>
        public int PrizesCount => PrizesID?.Length ?? 0;

        /// <summary>
        /// Prize IDs (int array).
        /// Field ID: 9
        /// </summary>
        public int[] PrizesID { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Points = reader.ReadInt(); break;
                    case 6: reader.ReadInt(); break; // ctxCount, derived
                    case 7: CtxInfo = ReadTlvIntArray(reader); break;
                    case 8: reader.ReadInt(); break; // prizesCount, derived
                    case 9: PrizesID = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((CtxInfo?.Length ?? 0) > MaxCtx)
                throw new InvalidDataException($"[TlvPointsCtxPrizes] CtxInfo exceeds the maximum of {MaxCtx} elements.");
            if ((PrizesID?.Length ?? 0) > MaxPrizes)
                throw new InvalidDataException($"[TlvPointsCtxPrizes] PrizesID exceeds the maximum of {MaxPrizes} elements.");

            WriteTlvInt(writer, 1, Points);
            WriteTlvInt(writer, 6, CtxCount);
            WriteTlvIntArray(writer, 7, CtxInfo, CtxCount);
            WriteTlvInt(writer, 8, PrizesCount);
            WriteTlvIntArray(writer, 9, PrizesID, PrizesCount);
        }
    }
}
