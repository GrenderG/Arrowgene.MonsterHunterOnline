using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for cat cuisine state.
    /// C++ Reader: crygame.dll+sub_10171710 (UnkTlv0122)
    /// C++ Printer: crygame.dll+sub_101717D0
    /// </summary>
    public class TlvCatCuisineState : Structure, ITlvStructure
    {
        /// <summary>
        /// Cat cuisine ID.
        /// Field ID: 1
        /// </summary>
        public int CatCuisineId { get; set; }

        /// <summary>
        /// Cuisine state.
        /// Field ID: 2
        /// </summary>
        public uint State { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, CatCuisineId);
            WriteTlvInt32(buffer, 2, (int)State);
        }
    }
}
