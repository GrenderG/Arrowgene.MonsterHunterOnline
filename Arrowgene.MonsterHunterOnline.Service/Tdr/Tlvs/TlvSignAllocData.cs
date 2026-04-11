using Rathalos.Core.Utils.IO;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for sign allocation data (tournament bracket).
    /// C++ Reader: crygame.dll+sub_1012D8E0 (UnkTlv0040)
    /// C++ Printer: crygame.dll+sub_1012E4C0
    /// </summary>
    public class TlvSignAllocData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxSign64 = 64;
        public const int MaxSign32 = 32;
        public const int MaxSign16 = 16;
        public const int MaxSign8 = 8;
        public const int MaxSign4 = 4;
        public const int MaxSign2 = 2;

        /// <summary>Field ID: 1</summary>
        public uint AllocID { get; set; }

        /// <summary>Field ID: 2</summary>
        public uint SaveTM { get; set; }

        /// <summary>Field ID: 3</summary>
        public uint State { get; set; }

        /// <summary>Field ID: 4</summary>
        public uint Round { get; set; }

        /// <summary>Count for 64-bracket IDs (derived). Field ID: 5</summary>
        public int Count64 => Sign64IDs?.Length ?? 0;

        /// <summary>64-bracket sign IDs. Field ID: 6</summary>
        public int[] Sign64IDs { get; set; }

        /// <summary>Count for 32-bracket IDs (derived). Field ID: 7</summary>
        public int Count32 => Sign32IDs?.Length ?? 0;

        /// <summary>32-bracket sign IDs. Field ID: 8</summary>
        public int[] Sign32IDs { get; set; }

        /// <summary>Count for 16-bracket IDs (derived). Field ID: 9</summary>
        public int Count16 => Sign16IDs?.Length ?? 0;

        /// <summary>16-bracket sign IDs. Field ID: 10</summary>
        public int[] Sign16IDs { get; set; }

        /// <summary>Count for 8-bracket IDs (derived). Field ID: 11</summary>
        public int Count8 => Sign8IDs?.Length ?? 0;

        /// <summary>8-bracket sign IDs. Field ID: 12</summary>
        public int[] Sign8IDs { get; set; }

        /// <summary>Count for 4-bracket IDs (derived). Field ID: 13</summary>
        public int Count4 => Sign4IDs?.Length ?? 0;

        /// <summary>4-bracket sign IDs. Field ID: 14</summary>
        public int[] Sign4IDs { get; set; }

        /// <summary>Count for 2-bracket IDs (derived). Field ID: 15</summary>
        public int Count2 => Sign2IDs?.Length ?? 0;

        /// <summary>2-bracket sign IDs. Field ID: 16</summary>
        public int[] Sign2IDs { get; set; }

        /// <summary>Winning sign ID. Field ID: 17</summary>
        public uint WinSignID { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: AllocID = (uint)reader.ReadInt(); break;
                    case 2: SaveTM = (uint)reader.ReadInt(); break;
                    case 3: State = (uint)reader.ReadInt(); break;
                    case 4: Round = (uint)reader.ReadInt(); break;
                    case 5: reader.ReadInt(); break;
                    case 6: Sign64IDs = ReadTlvIntArray(reader); break;
                    case 7: reader.ReadInt(); break;
                    case 8: Sign32IDs = ReadTlvIntArray(reader); break;
                    case 9: reader.ReadInt(); break;
                    case 10: Sign16IDs = ReadTlvIntArray(reader); break;
                    case 11: reader.ReadInt(); break;
                    case 12: Sign8IDs = ReadTlvIntArray(reader); break;
                    case 13: reader.ReadInt(); break;
                    case 14: Sign4IDs = ReadTlvIntArray(reader); break;
                    case 15: reader.ReadInt(); break;
                    case 16: Sign2IDs = ReadTlvIntArray(reader); break;
                    case 17: WinSignID = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            // --- BOUNDARY CHECK ---
            if ((Sign64IDs?.Length ?? 0) > MaxSign64) throw new InvalidDataException($"[TlvSignAllocData] Sign64IDs exceeds {MaxSign64}.");
            if ((Sign32IDs?.Length ?? 0) > MaxSign32) throw new InvalidDataException($"[TlvSignAllocData] Sign32IDs exceeds {MaxSign32}.");
            if ((Sign16IDs?.Length ?? 0) > MaxSign16) throw new InvalidDataException($"[TlvSignAllocData] Sign16IDs exceeds {MaxSign16}.");
            if ((Sign8IDs?.Length ?? 0) > MaxSign8) throw new InvalidDataException($"[TlvSignAllocData] Sign8IDs exceeds {MaxSign8}.");
            if ((Sign4IDs?.Length ?? 0) > MaxSign4) throw new InvalidDataException($"[TlvSignAllocData] Sign4IDs exceeds {MaxSign4}.");
            if ((Sign2IDs?.Length ?? 0) > MaxSign2) throw new InvalidDataException($"[TlvSignAllocData] Sign2IDs exceeds {MaxSign2}.");

            WriteTlvInt(writer, 1, (int)AllocID);
            WriteTlvInt(writer, 2, (int)SaveTM);
            WriteTlvInt(writer, 3, (int)State);
            WriteTlvInt(writer, 4, (int)Round);
            WriteTlvInt(writer, 5, Count64);
            WriteTlvIntArray(writer, 6, Sign64IDs, Count64);
            WriteTlvInt(writer, 7, Count32);
            WriteTlvIntArray(writer, 8, Sign32IDs, Count32);
            WriteTlvInt(writer, 9, Count16);
            WriteTlvIntArray(writer, 10, Sign16IDs, Count16);
            WriteTlvInt(writer, 11, Count8);
            WriteTlvIntArray(writer, 12, Sign8IDs, Count8);
            WriteTlvInt(writer, 13, Count4);
            WriteTlvIntArray(writer, 14, Sign4IDs, Count4);
            WriteTlvInt(writer, 15, Count2);
            WriteTlvIntArray(writer, 16, Sign2IDs, Count2);
            WriteTlvInt(writer, 17, (int)WinSignID);
        }
    }
}
