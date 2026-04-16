using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for typed base/bonus variant value.
    /// C++ Reader: crygame.dll+sub_101B0290 (UnkTlv0191)
    /// C++ Printer: crygame.dll+sub_101B0710
    /// </summary>
    public class TlvTypedBaseOrBonus : Structure, ITlvStructure
    {
        /// <summary>
        /// Type.
        /// Field ID: 1
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Value (base or bonus variant).
        /// Field ID: 2
        /// </summary>
        public TlvBaseOrBonus Value { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, Type);
            WriteTlvSubStructure(buffer, 2, Value);
        }
    }
}
