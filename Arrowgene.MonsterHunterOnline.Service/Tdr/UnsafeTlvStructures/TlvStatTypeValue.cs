using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for stat type with value.
    /// C++ Reader: crygame.dll+sub_10214CF0 (UnkTlv0232)
    /// C++ Printer: crygame.dll+sub_10214F80
    /// </summary>
    public class TlvStatTypeValue : Structure, ITlvStructure
    {
        /// <summary>
        /// Stat type.
        /// Field ID: 1
        /// </summary>
        public int StatType { get; set; }

        /// <summary>
        /// Stat value.
        /// Field ID: 2
        /// </summary>
        public uint StatValue { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, StatType);
            WriteTlvInt32(buffer, 2, (int)StatValue);
        }
    }
}
