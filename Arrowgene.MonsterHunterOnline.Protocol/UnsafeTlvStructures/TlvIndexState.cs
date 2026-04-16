using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for index state tracking.
    /// C++ Reader: crygame.dll+sub_10193260 (UnkTlv0169)
    /// C++ Printer: crygame.dll+sub_10193320
    /// </summary>
    public class TlvIndexState : Structure, ITlvStructure
    {
        /// <summary>
        /// Index value.
        /// Field ID: 1
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// State value.
        /// Field ID: 2
        /// </summary>
        public int State { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Index);
            WriteTlvInt32(buffer, 2, State);
        }
    }
}
