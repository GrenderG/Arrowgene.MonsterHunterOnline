using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for typed variant value (wType + stValue).
    /// C++ Reader: crygame.dll+sub_101AECC0 (UnkTlv0188)
    /// C++ Printer: crygame.dll+sub_101AEEB0
    /// </summary>
    public class TlvTypedVariant : Structure, ITlvStructure
    {
        /// <summary>
        /// Variant wire type (1=int, 2=float, 4=bool/short, 6=ulong).
        /// Field ID: 1
        /// </summary>
        public byte WType { get; set; }

        /// <summary>
        /// Variant value.
        /// Field ID: 2
        /// </summary>
        public TlvVariantArgs StValue { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, WType);
            WriteTlvSubStructure(buffer, 2, StValue);
        }
    }
}
