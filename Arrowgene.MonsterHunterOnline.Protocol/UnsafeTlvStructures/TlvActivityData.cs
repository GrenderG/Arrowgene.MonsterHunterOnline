using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for activity data.
    /// C++ Reader: crygame.dll+sub_10155590 (UnkTlv0086)
    /// C++ Printer: crygame.dll+sub_10155D40
    /// </summary>
    public class TlvActivityData : Structure, ITlvStructure
    {
        /// <summary>
        /// Activity ID.
        /// Field ID: 1
        /// </summary>
        public int ActivityID { get; set; }

        /// <summary>
        /// Last update time.
        /// Field ID: 2
        /// </summary>
        public int LastUpdate { get; set; }

        /// <summary>
        /// Variable data.
        /// Field ID: 5
        /// </summary>
        public TlvVarData Vars { get; set; } = new();

        /// <summary>
        /// Counter data.
        /// Field ID: 6
        /// </summary>
        public TlvCounterDataList Counters { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, ActivityID);
            WriteTlvInt32(buffer, 2, LastUpdate);
            WriteTlvSubStructure(buffer, 5, Vars);
            WriteTlvSubStructure(buffer, 6, Counters);
        }
    }
}
