using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for ID with start/stop time.
    /// C++ Reader: crygame.dll+sub_10246B10 (UnkTlv0292)
    /// C++ Printer: crygame.dll+sub_10246D60
    /// </summary>
    public class TlvIdStartStopTime : Structure, ITlvStructure
    {
        /// <summary>
        /// ID.
        /// Field ID: 1
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Start time.
        /// Field ID: 2
        /// </summary>
        public uint StartTime { get; set; }

        /// <summary>
        /// Stop time.
        /// Field ID: 3
        /// </summary>
        public uint StopTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)Id);
            WriteTlvInt32(buffer, 2, (int)StartTime);
            WriteTlvInt32(buffer, 3, (int)StopTime);
        }
    }
}
