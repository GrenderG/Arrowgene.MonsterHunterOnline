using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for seed slot info.
    /// C++ Reader: crygame.dll+sub_10200330 (UnkTlv0212)
    /// C++ Printer: crygame.dll+sub_10200470
    /// </summary>
    public class TlvSeedSlot : Structure, ITlvStructure
    {
        /// <summary>
        /// Open flag.
        /// Field ID: 1
        /// </summary>
        public byte Open { get; set; }

        /// <summary>
        /// Seed ID.
        /// Field ID: 2
        /// </summary>
        public int SeedId { get; set; }

        /// <summary>
        /// Seed time.
        /// Field ID: 3
        /// </summary>
        public int SeedTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, Open);
            WriteTlvInt32(buffer, 2, SeedId);
            WriteTlvInt32(buffer, 3, SeedTime);
        }
    }
}
