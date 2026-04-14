using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for target index with data.
    /// C++ Reader: crygame.dll+sub_10218090 (UnkTlv0237)
    /// C++ Printer: crygame.dll+sub_10218360
    /// </summary>
    public class TlvTargetIdxData : Structure, ITlvStructure
    {
        /// <summary>
        /// Target index.
        /// Field ID: 1
        /// </summary>
        public byte TargetIdx { get; set; }

        /// <summary>
        /// Target data.
        /// Field ID: 2
        /// </summary>
        public uint TargetData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, TargetIdx);
            WriteTlvInt32(buffer, 2, (int)TargetData);
        }
    }
}
