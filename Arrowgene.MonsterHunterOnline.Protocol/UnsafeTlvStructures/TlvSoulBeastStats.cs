using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for soul beast stats.
    /// C++ Reader: crygame.dll+sub_10206FE0 (UnkTlv0219)
    /// C++ Printer: crygame.dll+sub_10207430
    /// </summary>
    public class TlvSoulBeastStats : Structure, ITlvStructure
    {
        /// <summary>
        /// Soul beast GID.
        /// Field ID: 1
        /// </summary>
        public ulong SoulBeastGid { get; set; }

        /// <summary>
        /// Character level.
        /// Field ID: 2
        /// </summary>
        public int CharLevel { get; set; }

        /// <summary>
        /// Character experience.
        /// Field ID: 4
        /// </summary>
        public int CharExp { get; set; }

        /// <summary>
        /// Character gluttony.
        /// Field ID: 5
        /// </summary>
        public int CharGlut { get; set; }

        /// <summary>
        /// Evolve stage.
        /// Field ID: 6
        /// </summary>
        public int EvolveStage { get; set; }

        /// <summary>
        /// Image.
        /// Field ID: 7
        /// </summary>
        public int Image { get; set; }

        /// <summary>
        /// Follow flag.
        /// Field ID: 8
        /// </summary>
        public int Follow { get; set; }

        /// <summary>
        /// Feed time.
        /// Field ID: 9
        /// </summary>
        public int FeedTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt64(buffer, 1, (long)SoulBeastGid);
            WriteTlvInt32(buffer, 2, CharLevel);
            WriteTlvInt32(buffer, 4, CharExp);
            WriteTlvInt32(buffer, 5, CharGlut);
            WriteTlvInt32(buffer, 6, EvolveStage);
            WriteTlvInt32(buffer, 7, Image);
            WriteTlvInt32(buffer, 8, Follow);
            WriteTlvInt32(buffer, 9, FeedTime);
        }
    }
}
