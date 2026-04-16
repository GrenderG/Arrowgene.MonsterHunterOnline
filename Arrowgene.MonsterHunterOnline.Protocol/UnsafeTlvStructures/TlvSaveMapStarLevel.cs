using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for save map + star level container.
    /// C++ Reader: crygame.dll+sub_1019B5E0 (UnkTlv0171)
    /// C++ Printer: crygame.dll+sub_1019B8A0
    /// </summary>
    public class TlvSaveMapStarLevel : Structure, ITlvStructure
    {
        /// <summary>
        /// Save map (level position data).
        /// Field ID: 2
        /// </summary>
        public TlvLevelPosition SaveMap { get; set; } = new();

        /// <summary>
        /// Star level data.
        /// Field ID: 3
        /// </summary>
        public TlvStarLevelData StarLevel { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 2, SaveMap);
            WriteTlvSubStructure(buffer, 3, StarLevel);
        }
    }
}
