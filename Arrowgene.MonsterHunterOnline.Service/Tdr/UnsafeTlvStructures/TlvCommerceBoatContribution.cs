using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for commerce boat with contribution data.
    /// C++ Reader: crygame.dll+sub_1011FB00 (UnkTlv0023)
    /// C++ Printer: crygame.dll+sub_101200E0
    /// </summary>
    public class TlvCommerceBoatContribution : Structure, ITlvStructure
    {
        /// <summary>Commerce boat info. Field ID: 1</summary>
        public TlvCommerceBoat CommerceBoatInfo { get; set; } = new();

        /// <summary>Contribute resource point. Field ID: 2</summary>
        public int ContributeResPoint { get; set; }

        /// <summary>Challenge times. Field ID: 3</summary>
        public byte ChallengeTimes { get; set; }

        /// <summary>Refresh timestamp. Field ID: 4</summary>
        public int RefreshTimestamp { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 1, CommerceBoatInfo);
            WriteTlvInt32(buffer, 2, ContributeResPoint);
            WriteTlvByte(buffer, 3, ChallengeTimes);
            WriteTlvInt32(buffer, 4, RefreshTimestamp);
        }
    }
}
