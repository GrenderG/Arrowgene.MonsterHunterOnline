using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for fixed times with block arguments.
    /// C++ Reader: crygame.dll+sub_10163300 (UnkTlv0098)
    /// C++ Printer: crygame.dll+sub_10163560
    /// </summary>
    public class TlvFixedTimesBlock : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxCompleteBits = 5;

        /// <summary>
        /// Fixed times value.
        /// Field ID: 1
        /// </summary>
        public int FixedTimes { get; set; }

        /// <summary>
        /// Block argument 1.
        /// Field ID: 2
        /// </summary>
        public int BlockArg1 { get; set; }

        /// <summary>
        /// Block argument 2.
        /// Field ID: 3
        /// </summary>
        public int BlockArg2 { get; set; }

        /// <summary>
        /// Block argument 3.
        /// Field ID: 4
        /// </summary>
        public int BlockArg3 { get; set; }

        /// <summary>
        /// Block argument 4.
        /// Field ID: 5
        /// </summary>
        public int BlockArg4 { get; set; }

        /// <summary>
        /// Complete bit count (derived from array).
        /// Field ID: 6
        /// </summary>
        public int CompleteBitCount => CompleteBit?.Length ?? 0;

        /// <summary>
        /// Complete bit array (max 5).
        /// Field ID: 7
        /// </summary>
        public byte[] CompleteBit { get; set; }

        /// <summary>
        /// Level ID.
        /// Field ID: 8
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Level result.
        /// Field ID: 9
        /// </summary>
        public int LevelResult { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: FixedTimes = reader.ReadInt(); break;
                    case 2: BlockArg1 = reader.ReadInt(); break;
                    case 3: BlockArg2 = reader.ReadInt(); break;
                    case 4: BlockArg3 = reader.ReadInt(); break;
                    case 5: BlockArg4 = reader.ReadInt(); break;
                    case 6: reader.ReadInt(); break; // completeBitCount - derived
                    case 7: CompleteBit = ReadTlvByteArray(reader); break;
                    case 8: LevelId = reader.ReadInt(); break;
                    case 9: LevelResult = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, FixedTimes);
            WriteTlvInt(writer, 2, BlockArg1);
            WriteTlvInt(writer, 3, BlockArg2);
            WriteTlvInt(writer, 4, BlockArg3);
            WriteTlvInt(writer, 5, BlockArg4);
            WriteTlvInt(writer, 6, CompleteBitCount);
            WriteTlvByteArray(writer, 7, CompleteBit, CompleteBitCount);
            WriteTlvInt(writer, 8, LevelId);
            WriteTlvInt(writer, 9, LevelResult);
        }
    }
}
