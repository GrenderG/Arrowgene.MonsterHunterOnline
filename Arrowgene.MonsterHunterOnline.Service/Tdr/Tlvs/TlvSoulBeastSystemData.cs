using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for soul beast system data.
    /// C++ Reader: crygame.dll+sub_1018AFE0 (UnkTlv0157)
    /// C++ Printer: crygame.dll+sub_1018BDA0
    /// </summary>
    public class TlvSoulBeastSystemData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxStages = 40;
        public const int MaxAttrs = 200;
        public const int MaxBeasts = 1000;

        /// <summary>Field ID: 1</summary>
        public int Stage { get; set; }

        /// <summary>Field ID: 2</summary>
        public int Level { get; set; }

        /// <summary>Field ID: 3</summary>
        public int AttrPoint { get; set; }

        /// <summary>Field ID: 4</summary>
        public int FailureTimes { get; set; }

        /// <summary>Stage count (derived). Field ID: 5</summary>
        public byte StageCount => (byte)(Stages?.Length ?? 0);

        /// <summary>Stage array. Field ID: 6</summary>
        public int[] Stages { get; set; }

        /// <summary>Attr count (derived). Field ID: 7</summary>
        public byte AttrCount => (byte)(Attrs?.Length ?? 0);

        /// <summary>Attr array. Field ID: 8</summary>
        public int[] Attrs { get; set; }

        /// <summary>Field ID: 9</summary>
        public int AttrLearnCount { get; set; }

        /// <summary>Beast count (derived). Field ID: 10</summary>
        public int BeastCount => Beasts?.Count ?? 0;

        /// <summary>Beast entries. Field ID: 11</summary>
        public List<TlvSoulBeastIdAttrs> Beasts { get; set; }

        /// <summary>Field ID: 12</summary>
        public int FollowBeast { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Stage = reader.ReadInt(); break;
                    case 2: Level = reader.ReadInt(); break;
                    case 3: AttrPoint = reader.ReadInt(); break;
                    case 4: FailureTimes = reader.ReadInt(); break;
                    case 5: reader.ReadByte(); break;
                    case 6: Stages = ReadTlvIntArray(reader); break;
                    case 7: reader.ReadByte(); break;
                    case 8: Attrs = ReadTlvIntArray(reader); break;
                    case 9: AttrLearnCount = reader.ReadInt(); break;
                    case 10: reader.ReadInt(); break;
                    case 11: Beasts = ReadTlvList<TlvSoulBeastIdAttrs>(reader); break;
                    case 12: FollowBeast = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Stages?.Length ?? 0) > MaxStages) throw new InvalidDataException($"[TlvSoulBeastSystemData] Stages exceeds {MaxStages}.");
            if ((Attrs?.Length ?? 0) > MaxAttrs) throw new InvalidDataException($"[TlvSoulBeastSystemData] Attrs exceeds {MaxAttrs}.");
            if ((Beasts?.Count ?? 0) > MaxBeasts) throw new InvalidDataException($"[TlvSoulBeastSystemData] Beasts exceeds {MaxBeasts}.");

            WriteTlvInt(writer, 1, Stage);
            WriteTlvInt(writer, 2, Level);
            WriteTlvInt(writer, 3, AttrPoint);
            WriteTlvInt(writer, 4, FailureTimes);
            WriteTlvByte(writer, 5, StageCount);
            WriteTlvIntArray(writer, 6, Stages, StageCount);
            WriteTlvByte(writer, 7, AttrCount);
            WriteTlvIntArray(writer, 8, Attrs, AttrCount);
            WriteTlvInt(writer, 9, AttrLearnCount);
            WriteTlvInt(writer, 10, BeastCount);
            WriteTlvList(writer, 11, Beasts);
            WriteTlvInt(writer, 12, FollowBeast);
        }
    }
}
