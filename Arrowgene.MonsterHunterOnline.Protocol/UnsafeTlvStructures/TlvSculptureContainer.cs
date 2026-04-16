using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for sculpture container (id, refreshTime, sculptures list).
    /// C++ Reader: crygame.dll+sub_1013F840 (UnkTlv0059)
    /// C++ Printer: crygame.dll+sub_10140430
    /// </summary>
    public class TlvSculptureContainer : Structure, ITlvStructure
    {
        public const int MaxSculptures = 10;

        /// <summary>Field ID: 1</summary>
        public int Id { get; set; }

        /// <summary>Field ID: 2</summary>
        public int RefreshTime { get; set; }

        /// <summary>Count (derived). Field ID: 3</summary>
        public int Count => Sculptures?.Count ?? 0;

        /// <summary>Sculpture data entries. Field ID: 4</summary>
        public List<TlvSculptureData> Sculptures { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Sculptures?.Count ?? 0) > MaxSculptures)
                throw new InvalidDataException($"[TlvSculptureContainer] Sculptures exceeds {MaxSculptures}.");

            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, RefreshTime);
            WriteTlvInt32(buffer, 3, Count);
            WriteTlvSubStructureList(buffer, 4, Sculptures.Count, Sculptures);
        }
    }
}
