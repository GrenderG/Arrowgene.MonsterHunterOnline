using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for hide/enable flags.
    /// C++ Reader: crygame.dll+sub_10163B00 (UnkTlv0099)
    /// C++ Printer: crygame.dll+sub_10163BE0
    /// </summary>
    public class TlvHideEnable : Structure, ITlvStructure
    {
        /// <summary>
        /// Hide flag.
        /// Field ID: 1
        /// </summary>
        public int Hide { get; set; }

        /// <summary>
        /// Enable flag.
        /// Field ID: 2
        /// </summary>
        public int Enable { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Hide);
            WriteTlvInt32(buffer, 2, Enable);
        }
    }
}
