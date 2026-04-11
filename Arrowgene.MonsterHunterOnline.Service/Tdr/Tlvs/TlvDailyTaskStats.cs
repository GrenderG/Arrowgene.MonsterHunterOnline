using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for daily task stats with short array.
    /// C++ Reader: crygame.dll+sub_10223460 (UnkTlv0254)
    /// C++ Printer: crygame.dll+sub_10223A30
    /// </summary>
    public class TlvDailyTaskStats : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxDaily = 64;

        /// <summary>
        /// Daily count (derived from Daily array).
        /// Field ID: 1
        /// </summary>
        public short DailyCount => (short)(Daily?.Length ?? 0);

        /// <summary>
        /// Daily task IDs (short array).
        /// Field ID: 2
        /// </summary>
        public short[] Daily { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Refresh level.
        /// Field ID: 4
        /// </summary>
        public int RefreshLevel { get; set; }

        /// <summary>
        /// Complete count.
        /// Field ID: 5
        /// </summary>
        public int CompleteCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadShort(); break; // dailyCount, derived from array
                    case 2: Daily = ReadTlvShortArray(reader); break;
                    case 3: RefreshTime = (uint)reader.ReadInt(); break;
                    case 4: RefreshLevel = reader.ReadInt(); break;
                    case 5: CompleteCount = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Daily?.Length ?? 0) > MaxDaily)
                throw new InvalidDataException($"[TlvDailyTaskStats] Daily exceeds the maximum of {MaxDaily} elements.");

            WriteTlvShort(writer, 1, DailyCount);
            WriteTlvShortArray(writer, 2, Daily);
            WriteTlvInt(writer, 3, (int)RefreshTime);
            WriteTlvInt(writer, 4, RefreshLevel);
            WriteTlvInt(writer, 5, CompleteCount);
        }
    }
}
