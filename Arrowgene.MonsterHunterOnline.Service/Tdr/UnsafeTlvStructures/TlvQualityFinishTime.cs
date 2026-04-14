using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for quality with finish time.
    /// C++ Reader: crygame.dll+sub_10209880 (UnkTlv0235)
    /// C++ Printer: crygame.dll+sub_10209960
    /// </summary>
    public class TlvQualityFinishTime : Structure, ITlvStructure
    {
        /// <summary>
        /// Quality value.
        /// Field ID: 1
        /// </summary>
        public byte Quality { get; set; }

        /// <summary>
        /// Finish time.
        /// Field ID: 2
        /// </summary>
        public uint FinishTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, Quality);
            WriteTlvInt32(buffer, 2, (int)FinishTime);
        }
    }
}
