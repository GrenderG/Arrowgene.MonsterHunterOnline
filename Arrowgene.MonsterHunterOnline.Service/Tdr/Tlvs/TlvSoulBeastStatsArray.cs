using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for soul beast stats arrays (7 elements each).
    /// C++ Reader: crygame.dll+sub_10207850 (UnkTlv0220)
    /// C++ Printer: crygame.dll+sub_10208030
    /// </summary>
    public class TlvSoulBeastStatsArray : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int ExactSize = 7;

        /// <summary>
        /// Character levels (int array, exactly 7).
        /// Field ID: 2
        /// </summary>
        public int[] CharLevel { get; set; } = new int[ExactSize];

        /// <summary>
        /// Character experience (int array, exactly 7).
        /// Field ID: 4
        /// </summary>
        public int[] CharExp { get; set; } = new int[ExactSize];

        /// <summary>
        /// Character gluttony (int array, exactly 7).
        /// Field ID: 5
        /// </summary>
        public int[] CharGlut { get; set; } = new int[ExactSize];

        /// <summary>
        /// Evolve stage (int array, exactly 7).
        /// Field ID: 6
        /// </summary>
        public int[] EvolveStage { get; set; } = new int[ExactSize];

        /// <summary>
        /// Image (int array, exactly 7).
        /// Field ID: 7
        /// </summary>
        public int[] Image { get; set; } = new int[ExactSize];

        /// <summary>
        /// Follow flag (int array, exactly 7).
        /// Field ID: 8
        /// </summary>
        public int[] Follow { get; set; } = new int[ExactSize];

        /// <summary>
        /// Feed time (int array, exactly 7).
        /// Field ID: 9
        /// </summary>
        public int[] FeedTime { get; set; } = new int[ExactSize];

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: CharLevel = ReadTlvIntArray(reader); break;
                    case 4: CharExp = ReadTlvIntArray(reader); break;
                    case 5: CharGlut = ReadTlvIntArray(reader); break;
                    case 6: EvolveStage = ReadTlvIntArray(reader); break;
                    case 7: Image = ReadTlvIntArray(reader); break;
                    case 8: Follow = ReadTlvIntArray(reader); break;
                    case 9: FeedTime = ReadTlvIntArray(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteStrictIntArray(writer, 2, CharLevel, ExactSize);
            WriteStrictIntArray(writer, 4, CharExp, ExactSize);
            WriteStrictIntArray(writer, 5, CharGlut, ExactSize);
            WriteStrictIntArray(writer, 6, EvolveStage, ExactSize);
            WriteStrictIntArray(writer, 7, Image, ExactSize);
            WriteStrictIntArray(writer, 8, Follow, ExactSize);
            WriteStrictIntArray(writer, 9, FeedTime, ExactSize);
        }
    }
}
