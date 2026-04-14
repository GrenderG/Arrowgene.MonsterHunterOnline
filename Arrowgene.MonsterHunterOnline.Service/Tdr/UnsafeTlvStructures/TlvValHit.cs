using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for val/hit byte pair.
    /// C++ Reader: crygame.dll+sub_1022BF00 (UnkTlv0266)
    /// C++ Printer: crygame.dll+sub_1022C230
    /// </summary>
    public class TlvValHit : Structure, ITlvStructure
    {
        /// <summary>
        /// Value byte.
        /// Field ID: 1
        /// </summary>
        public byte Val { get; set; }

        /// <summary>
        /// Hit byte.
        /// Field ID: 2
        /// </summary>
        public byte Hit { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, Val);
            WriteTlvByte(buffer, 2, Hit);
        }
    }
}
