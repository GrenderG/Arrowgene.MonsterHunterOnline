using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for fetched flag.
    /// C++ Reader: crygame.dll+sub_1015ABB0 (UnkTlv0094)
    /// C++ Printer: crygame.dll+sub_1015AE20
    /// </summary>
    public class TlvFetchedFlag : Structure, ITlvStructure
    {
        /// <summary>
        /// Fetched flag.
        /// Field ID: 1
        /// </summary>
        public byte Fetched { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, Fetched);
        }
    }
}
