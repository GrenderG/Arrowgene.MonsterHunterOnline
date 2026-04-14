using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for single ID.
    /// C++ Reader: crygame.dll+sub_101EDBB0 (UnkTlv0198)
    /// C++ Printer: crygame.dll+sub_101EDDA0
    /// </summary>
    public class TlvSingleId : Structure, ITlvStructure
    {
        /// <summary>
        /// ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
        }
    }
}
