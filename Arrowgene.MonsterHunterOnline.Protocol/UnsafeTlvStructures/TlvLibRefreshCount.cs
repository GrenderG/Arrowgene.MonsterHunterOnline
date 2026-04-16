using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for library refresh count.
    /// C++ Reader: crygame.dll+sub_102243B0 (UnkTlv0256)
    /// C++ Printer: crygame.dll+sub_102246A0
    /// </summary>
    public class TlvLibRefreshCount : Structure, ITlvStructure
    {
        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Library ID.
        /// Field ID: 2
        /// </summary>
        public int Lib { get; set; }

        /// <summary>
        /// Complete count.
        /// Field ID: 3
        /// </summary>
        public int CompleteCount { get; set; }

        /// <summary>
        /// Remain count.
        /// Field ID: 4
        /// </summary>
        public int RemainCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)RefreshTime);
            WriteTlvInt32(buffer, 2, Lib);
            WriteTlvInt32(buffer, 3, CompleteCount);
            WriteTlvInt32(buffer, 4, RemainCount);
        }
    }
}
