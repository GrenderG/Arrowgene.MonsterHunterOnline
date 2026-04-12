using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for relic boxes container.
    /// C++ Reader: crygame.dll+sub_10231720 (UnkTlv0274)
    /// C++ Printer: crygame.dll+sub_10231C80
    /// </summary>
    public class TlvRelicBoxesContainer : Structure, ITlvStructure
    {
        public const int MaxBoxes = 5;

        /// <summary>Count (derived). Field ID: 1</summary>
        public byte Count => (byte)(Boxes?.Count ?? 0);

        /// <summary>Box entries. Field ID: 2</summary>
        public List<TlvRelicChessData> Boxes { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Boxes?.Count ?? 0) > MaxBoxes)
                throw new InvalidDataException($"[TlvRelicBoxesContainer] Boxes exceeds {MaxBoxes}.");

            WriteTlvByte(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, Boxes.Count, Boxes);
        }
    }
}
