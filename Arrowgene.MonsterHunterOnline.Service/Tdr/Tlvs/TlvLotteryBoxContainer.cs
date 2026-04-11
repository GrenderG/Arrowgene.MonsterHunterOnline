using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for multiple lottery boxes with daily refresh.
    /// C++ Reader: crygame.dll+sub_10192890 (UnkTlv0168)
    /// C++ Printer: crygame.dll+sub_10192D70
    /// </summary>
    public class TlvLotteryBoxContainer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxBoxes = 3;

        /// <summary>
        /// Lottery boxes.
        /// Field ID: 1
        /// </summary>
        public List<TlvLotteryBoxItemPool> LotteryBox { get; set; }

        /// <summary>
        /// Last daily refresh time.
        /// Field ID: 2
        /// </summary>
        public int LastDailyRefreshTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: LotteryBox = ReadTlvList<TlvLotteryBoxItemPool>(reader); break;
                    case 2: LastDailyRefreshTime = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((LotteryBox?.Count ?? 0) > MaxBoxes)
                throw new InvalidDataException($"[TlvLotteryBoxContainer] LotteryBox exceeds the maximum of {MaxBoxes} elements.");

            WriteTlvList(writer, 1, LotteryBox);
            WriteTlvInt(writer, 2, LastDailyRefreshTime);
        }
    }
}
