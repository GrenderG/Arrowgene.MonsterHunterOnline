using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for guild title data.
    /// C++ Reader: crygame.dll+sub_1011E650 (UnkTlv0021)
    /// C++ Printer: crygame.dll+sub_1011ED60
    /// </summary>
    public class TlvGuildTitleData : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxNameLen = 32;

        /// <summary>
        /// Title.
        /// Field ID: 1
        /// </summary>
        public int Title { get; set; }

        /// <summary>
        /// Name.
        /// Field ID: 2
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Rights.
        /// Field ID: 3
        /// </summary>
        public int Rights { get; set; }

        /// <summary>
        /// Depot rights data.
        /// Field ID: 4
        /// </summary>
        public TlvDepotRightsList DepotRights { get; set; } = new();

        /// <summary>
        /// Depot operation count.
        /// Field ID: 5
        /// </summary>
        public int DepotOpCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Title);
            WriteTlvString(buffer, 2, Name);
            WriteTlvInt32(buffer, 3, Rights);
            WriteTlvSubStructure(buffer, 4, DepotRights);
            WriteTlvInt32(buffer, 5, DepotOpCount);
        }
    }
}
