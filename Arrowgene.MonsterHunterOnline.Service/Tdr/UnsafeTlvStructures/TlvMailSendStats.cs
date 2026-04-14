using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for mail sending statistics.
    /// C++ Reader: crygame.dll+sub_10171130 (UnkTlv0121)
    /// C++ Printer: crygame.dll+sub_10171270
    /// </summary>
    public class TlvMailSendStats : Structure, ITlvStructure
    {
        /// <summary>
        /// Account mail send times.
        /// Field ID: 1
        /// </summary>
        public int AccMailSendTimes { get; set; }

        /// <summary>
        /// Passerby send times.
        /// Field ID: 2
        /// </summary>
        public int PasserbySendTimes { get; set; }

        /// <summary>
        /// Refresh time.
        /// Field ID: 3
        /// </summary>
        public uint RefreshTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, AccMailSendTimes);
            WriteTlvInt32(buffer, 2, PasserbySendTimes);
            WriteTlvInt32(buffer, 3, (int)RefreshTime);
        }
    }
}
