using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for refresh time only.
    /// C++ Reader: crygame.dll+sub_10223D90 (UnkTlv0255)
    /// C++ Printer: crygame.dll+sub_10223F80
    /// </summary>
    public class TlvRefreshTimeOnly : Structure, ITlvStructure
    {
        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)RefreshTime);
        }
    }
}
