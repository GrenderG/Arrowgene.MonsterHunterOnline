using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for stat index with value (short fields).
    /// C++ Reader: crygame.dll+sub_1021AF30 (UnkTlv0242)
    /// C++ Printer: crygame.dll+sub_1021B1C0
    /// </summary>
    public class TlvStatIdxValue : Structure, ITlvStructure
    {
        /// <summary>
        /// Stat index.
        /// Field ID: 1
        /// </summary>
        public short StatIdx { get; set; }

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
            WriteTlvInt16(buffer, 1, StatIdx);
            WriteTlvInt32(buffer, 2, (int)StatValue);
        }
    }
}
