using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for chapter progress tracking.
    /// C++ Reader: crygame.dll+sub_1018EF10 (UnkTlv0162)
    /// C++ Printer: crygame.dll+sub_1018F060
    /// </summary>
    public class TlvChapterProgress : Structure, ITlvStructure
    {
        /// <summary>
        /// Chapter identifier.
        /// Field ID: 1
        /// </summary>
        public int ChapterId { get; set; }

        /// <summary>
        /// Get reward count.
        /// Field ID: 2
        /// </summary>
        public int GetRewardCount { get; set; }

        /// <summary>
        /// Is not new flag.
        /// Field ID: 3
        /// </summary>
        public byte IsNotNew { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, ChapterId);
            WriteTlvInt32(buffer, 2, GetRewardCount);
            WriteTlvByte(buffer, 3, IsNotNew);
        }
    }
}
