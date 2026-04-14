using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for phase counter tracking.
    /// C++ Reader: crygame.dll+sub_10154020 (UnkTlv0083)
    /// C++ Printer: crygame.dll+sub_10154120
    /// </summary>
    public class TlvPhaseCounter : Structure, ITlvStructure
    {
        /// <summary>
        /// Identifier.
        /// Field ID: 1
        /// </summary>
        public byte Id { get; set; }

        /// <summary>
        /// Phase value.
        /// Field ID: 2
        /// </summary>
        public byte Phase { get; set; }

        /// <summary>
        /// Counter value.
        /// Field ID: 3
        /// </summary>
        public uint Counter { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, Id);
            WriteTlvByte(buffer, 2, Phase);
            WriteTlvInt32(buffer, 3, (int)Counter);
        }
    }
}
