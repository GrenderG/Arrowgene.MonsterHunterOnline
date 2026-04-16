using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for buff information.
    /// C++ Reader: crygame.dll+sub_101FBF80 (UnkTlv0207)
    /// C++ Printer: crygame.dll+sub_101FC250
    /// </summary>
    public class TlvBuffInfo : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxEffects = 10;

        /// <summary>
        /// Buff ID.
        /// Field ID: 1
        /// </summary>
        public int BuffId { get; set; }

        /// <summary>
        /// Unique ID.
        /// Field ID: 2
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// Owner ID.
        /// Field ID: 3
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Retain time.
        /// Field ID: 4
        /// </summary>
        public int RetainTime { get; set; }

        /// <summary>
        /// Expire time.
        /// Field ID: 5
        /// </summary>
        public int ExpireTime { get; set; }

        /// <summary>
        /// Count.
        /// Field ID: 6
        /// </summary>
        public short Count { get; set; }

        /// <summary>
        /// Stack.
        /// Field ID: 7
        /// </summary>
        public short Stack { get; set; }

        /// <summary>
        /// From.
        /// Field ID: 8
        /// </summary>
        public short From { get; set; }

        /// <summary>
        /// Effect number (derived from list).
        /// Field ID: 9
        /// </summary>
        public short EffectNum => (short)(EffectData?.Count ?? 0);

        /// <summary>
        /// Effect data list (max 10).
        /// Field ID: 10
        /// </summary>
        public List<TlvEffectType> EffectData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, BuffId);
            WriteTlvInt32(buffer, 2, UId);
            WriteTlvInt32(buffer, 3, OwnerId);
            WriteTlvInt32(buffer, 4, RetainTime);
            WriteTlvInt32(buffer, 5, ExpireTime);
            WriteTlvInt16(buffer, 6, Count);
            WriteTlvInt16(buffer, 7, Stack);
            WriteTlvInt16(buffer, 8, From);
            WriteTlvInt16(buffer, 9, EffectNum);
            WriteTlvSubStructureList(buffer, 10, EffectData.Count, EffectData);
        }
    }
}
