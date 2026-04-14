using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for commerce timeout tracking.
    /// C++ Reader: crygame.dll+sub_1012F1C0 (UnkTlv0042)
    /// C++ Printer: crygame.dll+sub_1012F2A0
    /// </summary>
    public class TlvCommerceTimeout : Structure, ITlvStructure
    {
        /// <summary>
        /// Commerce identifier.
        /// Field ID: 1
        /// </summary>
        public int CommerceId { get; set; }

        /// <summary>
        /// Timeout date.
        /// Field ID: 2
        /// </summary>
        public uint TimeoutDate { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, CommerceId);
            WriteTlvInt32(buffer, 2, (int)TimeoutDate);
        }
    }
}
