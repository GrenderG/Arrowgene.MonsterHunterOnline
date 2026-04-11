using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for sculpture library data (sculpture containers + task content libs).
    /// C++ Reader: crygame.dll+sub_10140F60 (UnkTlv0060)
    /// C++ Printer: crygame.dll+sub_101412A0
    /// </summary>
    public class TlvSculptureLibData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break;
                    case 2: Cfgs = ReadTlvList<TlvSculptureContainer>(reader); break;
                    case 3: reader.ReadInt(); break;
                    case 4: Libs = ReadTlvList<TlvTaskContent>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Cfgs?.Count ?? 0) > MaxCfgs)
                throw new InvalidDataException($"[TlvSculptureLibData] Cfgs exceeds {MaxCfgs}.");
            if ((Libs?.Count ?? 0) > MaxLibs)
                throw new InvalidDataException($"[TlvSculptureLibData] Libs exceeds {MaxLibs}.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Cfgs);
            WriteTlvInt(writer, 3, CfgCount);
            WriteTlvList(writer, 4, Libs);
        }
    }
}
