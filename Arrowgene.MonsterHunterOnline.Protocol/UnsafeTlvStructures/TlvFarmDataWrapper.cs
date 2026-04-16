using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for farm data wrapper.
    /// C++ Reader: crygame.dll+sub_10206340 (UnkTlv0218)
    /// C++ Printer: crygame.dll+sub_10206660
    /// </summary>
    public class TlvFarmDataWrapper : Structure, ITlvStructure
    {
        /// <summary>Farm data. Field ID: 2</summary>
        public TlvFarmData Farm { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 2, Farm);
        }
    }
}
