using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for soul beast with ID and attributes.
    /// C++ Reader: crygame.dll+sub_1018A540 (UnkTlv0156)
    /// C++ Printer: crygame.dll+sub_1018A9D0
    /// </summary>
    public class TlvSoulBeastIdAttrs : Structure, ITlvStructure
    {
        /// <summary>
        /// Soul beast identifier.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Soul beast attributes.
        /// Field ID: 2
        /// </summary>
        public TlvSoulBeastStats Attrs { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvSubStructure(buffer, 2, Attrs);
        }
    }
}
