using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for level value.
    /// C++ Reader: crygame.dll+sub_101FFB50 (UnkTlv0211)
    /// C++ Printer: crygame.dll+sub_101FFC40
    /// </summary>
    public class TlvLevelValue : Structure, ITlvStructure
    {
        /// <summary>
        /// Level value.
        /// Field ID: 1
        /// </summary>
        public short Level { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, Level);
        }
    }
}
