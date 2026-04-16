using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for activity with fetched flag.
    /// C++ Reader: crygame.dll+sub_1015A4D0 (UnkTlv0093)
    /// C++ Printer: crygame.dll+sub_1015A7A0
    /// </summary>
    public class TlvActivityFetched : Structure, ITlvStructure
    {
        /// <summary>
        /// Activity value.
        /// Field ID: 1
        /// </summary>
        public int Activity { get; set; }

        /// <summary>
        /// Fetched flag.
        /// Field ID: 2
        /// </summary>
        public byte Fetched { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Activity);
            WriteTlvByte(buffer, 2, Fetched);
        }
    }
}
