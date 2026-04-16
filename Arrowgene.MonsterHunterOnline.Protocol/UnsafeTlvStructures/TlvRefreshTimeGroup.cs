using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for refresh time with group.
    /// C++ Reader: crygame.dll+sub_10226B60 (UnkTlv0258)
    /// C++ Printer: crygame.dll+sub_10226D90
    /// </summary>
    public class TlvRefreshTimeGroup : Structure, ITlvStructure
    {
        /// <summary>
        /// Refresh time.
        /// Field ID: 1
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Group.
        /// Field ID: 2
        /// </summary>
        public int Group { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)RefreshTime);
            WriteTlvInt32(buffer, 2, Group);
        }
    }
}
