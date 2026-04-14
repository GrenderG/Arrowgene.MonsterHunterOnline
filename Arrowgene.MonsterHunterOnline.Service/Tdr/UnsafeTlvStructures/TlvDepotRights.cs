using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for depot rights and fetch count.
    /// C++ Reader: crygame.dll+sub_1011D790 (UnkTlv0019)
    /// C++ Printer: crygame.dll+sub_1011D960
    /// </summary>
    public class TlvDepotRights : Structure, ITlvStructure
    {
        /// <summary>
        /// Depot value.
        /// Field ID: 1
        /// </summary>
        public int Depot { get; set; }

        /// <summary>
        /// Rights value.
        /// Field ID: 2
        /// </summary>
        public int Rights { get; set; }

        /// <summary>
        /// Fetch count.
        /// Field ID: 3
        /// </summary>
        public int FetchCount { get; set; }

        /// <summary>
        /// Current fetch count.
        /// Field ID: 4
        /// </summary>
        public int CurFetchCount { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Depot);
            WriteTlvInt32(buffer, 2, Rights);
            WriteTlvInt32(buffer, 3, FetchCount);
            WriteTlvInt32(buffer, 4, CurFetchCount);
        }
    }
}
