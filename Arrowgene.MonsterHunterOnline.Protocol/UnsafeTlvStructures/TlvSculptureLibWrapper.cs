using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for sculpture lib wrapper (single nested lib field).
    /// C++ Reader: crygame.dll+sub_10141750 (UnkTlv0061)
    /// C++ Printer: crygame.dll+sub_10141AD0
    /// </summary>
    public class TlvSculptureLibWrapper : Structure, ITlvStructure
    {
        /// <summary>Libs data. Field ID: 1</summary>
        public TlvSculptureLibData Libs { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 1, Libs);
        }
    }
}
