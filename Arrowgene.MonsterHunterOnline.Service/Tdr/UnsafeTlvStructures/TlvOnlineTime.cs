using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for online time tracking.
    /// C++ Reader: crygame.dll+sub_10156EA0 (UnkTlv0088)
    /// C++ Printer: crygame.dll+sub_10157090
    /// </summary>
    public class TlvOnlineTime : Structure, ITlvStructure
    {
        /// <summary>
        /// Online time.
        /// Field ID: 1
        /// </summary>
        public uint OnlineTime { get; set; }

        /// <summary>
        /// Last update time.
        /// Field ID: 2
        /// </summary>
        public uint LastUpdateTime { get; set; }

        /// <summary>
        /// Activity identifier.
        /// Field ID: 3
        /// </summary>
        public uint ActivityId { get; set; }

        /// <summary>
        /// Hour value.
        /// Field ID: 4
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Minute value.
        /// Field ID: 5
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Second value.
        /// Field ID: 6
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// Offset value.
        /// Field ID: 7
        /// </summary>
        public int Offset { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)OnlineTime);
            WriteTlvInt32(buffer, 2, (int)LastUpdateTime);
            WriteTlvInt32(buffer, 3, (int)ActivityId);
            WriteTlvInt32(buffer, 4, Hour);
            WriteTlvInt32(buffer, 5, Min);
            WriteTlvInt32(buffer, 6, Second);
            WriteTlvInt32(buffer, 7, Offset);
        }
    }
}
