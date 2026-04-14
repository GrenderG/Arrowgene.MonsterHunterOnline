using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for card ID with finish time.
    /// C++ Reader: crygame.dll+sub_102191C0 (UnkTlv0239)
    /// C++ Printer: crygame.dll+sub_10219450
    /// </summary>
    public class TlvCardFinishTime : Structure, ITlvStructure
    {
        /// <summary>
        /// Card ID.
        /// Field ID: 1
        /// </summary>
        public int CardId { get; set; }

        /// <summary>
        /// Finish time.
        /// Field ID: 2
        /// </summary>
        public uint FinishTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, CardId);
            WriteTlvInt32(buffer, 2, (int)FinishTime);
        }
    }
}
