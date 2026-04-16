using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for online time, consecutive days, and refresh time.
    /// C++ Reader: crygame.dll+sub_1024BFF0 (UnkTlv0298)
    /// C++ Printer: crygame.dll+sub_1024C240
    /// </summary>
    public class TlvOnlineConsecRefresh : Structure, ITlvStructure
    {
        /// <summary>
        /// Online time.
        /// Field ID: 1
        /// </summary>
        public uint OnlineTime { get; set; }

        /// <summary>
        /// Consecutive days.
        /// Field ID: 2
        /// </summary>
        public uint ConsecDays { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)OnlineTime);
            WriteTlvInt32(buffer, 2, (int)ConsecDays);
            WriteTlvInt32(buffer, 3, (int)RefreshTime);
        }
    }
}
