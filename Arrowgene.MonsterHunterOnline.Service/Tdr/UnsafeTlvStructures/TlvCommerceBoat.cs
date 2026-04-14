using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for commerce boat information.
    /// C++ Reader: crygame.dll+sub_1011B450 (UnkTlv0017)
    /// C++ Printer: crygame.dll+sub_1011B670
    /// </summary>
    public class TlvCommerceBoat : Structure, ITlvStructure
    {
        /// <summary>
        /// Commerce boat identifier.
        /// Field ID: 1
        /// </summary>
        public uint CommerceBoatId { get; set; }

        /// <summary>
        /// Commerce boat start time.
        /// Field ID: 2
        /// </summary>
        public int CommerceBoatStartTime { get; set; }

        /// <summary>
        /// Commerce boat status.
        /// Field ID: 3
        /// </summary>
        public int CommerceBoatStatus { get; set; }

        /// <summary>
        /// Level identifier.
        /// Field ID: 4
        /// </summary>
        public int LevelId { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)CommerceBoatId);
            WriteTlvInt32(buffer, 2, CommerceBoatStartTime);
            WriteTlvInt32(buffer, 3, CommerceBoatStatus);
            WriteTlvInt32(buffer, 4, LevelId);
        }
    }
}
