using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for sculpture round tracking.
    /// C++ Reader: crygame.dll+sub_10141F90 (UnkTlv0062)
    /// C++ Printer: crygame.dll+sub_10142050
    /// </summary>
    public class TlvSculptureRound : Structure, ITlvStructure
    {
        /// <summary>
        /// Sculpture identifier.
        /// Field ID: 1
        /// </summary>
        public int Sculpture { get; set; }

        /// <summary>
        /// Round value.
        /// Field ID: 2
        /// </summary>
        public int Round { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Sculpture);
            WriteTlvInt32(buffer, 2, Round);
        }
    }
}
