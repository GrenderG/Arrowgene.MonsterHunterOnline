using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for entrust UI step.
    /// C++ Reader: crygame.dll+sub_1019BC10 (UnkTlv0172)
    /// C++ Printer: crygame.dll+sub_1019BE00
    /// </summary>
    public class TlvEntrustUiStep : Structure, ITlvStructure
    {
        /// <summary>
        /// Entrust UI step.
        /// Field ID: 1
        /// </summary>
        public int EntrustUiStep { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, EntrustUiStep);
        }
    }
}
