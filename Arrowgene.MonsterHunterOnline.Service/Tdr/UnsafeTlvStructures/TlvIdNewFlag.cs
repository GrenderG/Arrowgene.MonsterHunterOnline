using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for ID with new flag.
    /// C++ Reader: crygame.dll+sub_101A2660 (UnkTlv0180)
    /// C++ Printer: crygame.dll+sub_101A2740
    /// </summary>
    public class TlvIdNewFlag : Structure, ITlvStructure
    {
        /// <summary>
        /// ID value.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// New flag.
        /// Field ID: 2
        /// </summary>
        public byte NewFlag { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvByte(buffer, 2, NewFlag);
        }
    }
}
