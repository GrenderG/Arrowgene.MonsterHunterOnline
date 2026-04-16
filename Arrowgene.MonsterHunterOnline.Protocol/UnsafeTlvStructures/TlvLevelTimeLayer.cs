using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for level time tracking with layer.
    /// C++ Reader: crygame.dll+sub_10144CF0 (UnkTlv0067)
    /// C++ Printer: crygame.dll+sub_10144E00
    /// </summary>
    public class TlvLevelTimeLayer : Structure, ITlvStructure
    {
        /// <summary>
        /// Level ID.
        /// Field ID: 1
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Seconds value.
        /// Field ID: 2
        /// </summary>
        public short Seconds { get; set; }

        /// <summary>
        /// Layer value.
        /// Field ID: 3
        /// </summary>
        public short Layer { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, LevelId);
            WriteTlvInt16(buffer, 2, Seconds);
            WriteTlvInt16(buffer, 3, Layer);
        }
    }
}
