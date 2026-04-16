using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for gathering info.
    /// C++ Reader: crygame.dll+sub_101FF3E0 (UnkTlv0210)
    /// C++ Printer: crygame.dll+sub_101FF650
    /// </summary>
    public class TlvGatherInfo : Structure, ITlvStructure
    {
        /// <summary>
        /// Current experience.
        /// Field ID: 1
        /// </summary>
        public int CurExp { get; set; }

        /// <summary>
        /// Level (short).
        /// Field ID: 2
        /// </summary>
        public short Level { get; set; }

        /// <summary>
        /// Gather count.
        /// Field ID: 3
        /// </summary>
        public short GatherCount { get; set; }

        /// <summary>
        /// Last refresh time.
        /// Field ID: 4
        /// </summary>
        public int LastRefreshTime { get; set; }

        /// <summary>
        /// Pet ID.
        /// Field ID: 5
        /// </summary>
        public int PetId { get; set; }

        /// <summary>
        /// Gather level.
        /// Field ID: 6
        /// </summary>
        public short GatherLevel { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, CurExp);
            WriteTlvInt16(buffer, 2, Level);
            WriteTlvInt16(buffer, 3, GatherCount);
            WriteTlvInt32(buffer, 4, LastRefreshTime);
            WriteTlvInt32(buffer, 5, PetId);
            WriteTlvInt16(buffer, 6, GatherLevel);
        }
    }
}
