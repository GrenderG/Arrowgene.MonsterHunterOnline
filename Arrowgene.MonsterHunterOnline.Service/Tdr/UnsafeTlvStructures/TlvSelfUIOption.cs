using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for self UI option data.
    /// C++ Reader: crygame.dll+sub_1019BD60 (inner of UnkTlv0173)
    /// C++ Printer: crygame.dll+sub_1019BEB0
    /// </summary>
    public class TlvSelfUIOption : Structure, ITlvStructure
    {
        /// <summary>
        /// Entrust UI step.
        /// Field ID: 1
        /// </summary>
        public int EntrustUIStep { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, EntrustUIStep);
        }
    }
}
