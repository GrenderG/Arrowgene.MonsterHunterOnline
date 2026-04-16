using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for script/proc/online time data.
    /// C++ Reader: crygame.dll+sub_10158750 (UnkTlv0090)
    /// C++ Printer: crygame.dll+sub_10158990
    /// </summary>
    public class TlvScriptProcData : Structure, ITlvStructure
    {
        /// <summary>Fetch procs data. Field ID: 1</summary>
        public TlvProcs FetchProcs { get; set; } = new();

        /// <summary>Script vars data. Field ID: 2</summary>
        public TlvActivityDataList ScriptVars { get; set; } = new();

        /// <summary>Online time data. Field ID: 3</summary>
        public TlvAlarmTimeData OnlineTime { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 1, FetchProcs);
            WriteTlvSubStructure(buffer, 2, ScriptVars);
            WriteTlvSubStructure(buffer, 3, OnlineTime);
        }
    }
}
