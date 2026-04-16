using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for sculpture library data (sculpture containers + task content libs).
    /// C++ Reader: crygame.dll+sub_10140F60 (UnkTlv0060)
    /// C++ Printer: crygame.dll+sub_101412A0
    /// </summary>
    public class TlvSculptureLibData : Structure, ITlvStructure
    {
        public const int MaxCfgs = 4;
        public const int MaxLibs = 10;

        /// <summary>Count (derived). Field ID: 1</summary>
        public int Count => Cfgs?.Count ?? 0;

        /// <summary>Sculpture container entries. Field ID: 2</summary>
        public List<TlvSculptureContainer> Cfgs { get; set; }

        /// <summary>Cfg count (derived). Field ID: 3</summary>
        public int CfgCount => Libs?.Count ?? 0;

        /// <summary>Task content lib entries. Field ID: 4</summary>
        public List<TlvTaskContent> Libs { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Cfgs?.Count ?? 0) > MaxCfgs)
                throw new InvalidDataException($"[TlvSculptureLibData] Cfgs exceeds {MaxCfgs}.");
            if ((Libs?.Count ?? 0) > MaxLibs)
                throw new InvalidDataException($"[TlvSculptureLibData] Libs exceeds {MaxLibs}.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Cfgs.Count, Cfgs);
            WriteTlvInt32(buffer, 3, CfgCount);
            WriteTlvSubStructureList(buffer, 4, Libs.Count, Libs);
        }
    }
}
