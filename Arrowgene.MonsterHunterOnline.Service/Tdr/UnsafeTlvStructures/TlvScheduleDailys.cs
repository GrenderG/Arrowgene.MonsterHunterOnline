using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for schedule + dailys container.
    /// C++ Reader: crygame.dll+sub_10228680 (UnkTlv0261)
    /// C++ Printer: crygame.dll+sub_10228A90
    /// </summary>
    public class TlvScheduleDailys : Structure, ITlvStructure
    {
        /// <summary>Schedule data. Field ID: 1</summary>
        public TlvRefreshTimeGroup Schedule { get; set; } = new();

        /// <summary>Dailys data. Field ID: 2</summary>
        public TlvDailys Dailys { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 1, Schedule);
            WriteTlvSubStructure(buffer, 2, Dailys);
        }
    }
}
