using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for limit count tracking.
    /// C++ Reader: crygame.dll+sub_10178400 (UnkTlv0132)
    /// C++ Printer: crygame.dll+sub_101784C0
    /// </summary>
    public class TlvLimitCount : Structure, ITlvStructure
    {
        /// <summary>
        /// Limit identifier.
        /// Field ID: 1
        /// </summary>
        public int LimitId { get; set; }

        /// <summary>
        /// Count value.
        /// Field ID: 2
        /// </summary>
        public int Count { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, LimitId);
            WriteTlvInt32(buffer, 2, Count);
        }
    }
}
