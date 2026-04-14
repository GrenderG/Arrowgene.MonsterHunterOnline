using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for quality with finish time (VarUInt variant).
    /// C++ Reader: crygame.dll+sub_10216BA0 (inner of UnkTlv0236)
    /// C++ Printer: crygame.dll+sub_10216C90 (via DebugFormat)
    /// </summary>
    public class TlvQualityFinishTimeVar : Structure, ITlvStructure
    {
        /// <summary>
        /// Quality value.
        /// Field ID: 1
        /// </summary>
        public byte Quality { get; set; }

        /// <summary>
        /// Finish time (stored as VarUInt).
        /// Field ID: 2
        /// </summary>
        public uint FinishTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvByte(buffer, 1, Quality);
            WriteTlvVarUInt32(buffer, 2, FinishTime);
        }
    }
}
