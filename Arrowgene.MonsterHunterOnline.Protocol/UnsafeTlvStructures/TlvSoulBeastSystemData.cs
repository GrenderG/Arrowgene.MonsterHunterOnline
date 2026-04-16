using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for soul beast system data.
    /// C++ Reader: crygame.dll+sub_1018AFE0 (UnkTlv0157)
    /// C++ Printer: crygame.dll+sub_1018BDA0
    /// </summary>
    public class TlvSoulBeastSystemData : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Stages?.Length ?? 0) > MaxStages) throw new InvalidDataException($"[TlvSoulBeastSystemData] Stages exceeds {MaxStages}.");
            if ((Attrs?.Length ?? 0) > MaxAttrs) throw new InvalidDataException($"[TlvSoulBeastSystemData] Attrs exceeds {MaxAttrs}.");
            if ((Beasts?.Count ?? 0) > MaxBeasts) throw new InvalidDataException($"[TlvSoulBeastSystemData] Beasts exceeds {MaxBeasts}.");

            WriteTlvInt32(buffer, 1, Stage);
            WriteTlvInt32(buffer, 2, Level);
            WriteTlvInt32(buffer, 3, AttrPoint);
            WriteTlvInt32(buffer, 4, FailureTimes);
            WriteTlvByte(buffer, 5, StageCount);
            WriteTlvInt32Arr(buffer, 6, Stages);
            WriteTlvByte(buffer, 7, AttrCount);
            WriteTlvInt32Arr(buffer, 8, Attrs);
            WriteTlvInt32(buffer, 9, AttrLearnCount);
            WriteTlvInt32(buffer, 10, BeastCount);
            WriteTlvSubStructureList(buffer, 11, Beasts.Count, Beasts);
            WriteTlvInt32(buffer, 12, FollowBeast);
        }
    }
}
