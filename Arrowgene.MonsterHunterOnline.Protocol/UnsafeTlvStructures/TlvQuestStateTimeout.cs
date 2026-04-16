using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for quest state with timeout.
    /// C++ Reader: crygame.dll+sub_1021EB90 (UnkTlv0246)
    /// C++ Printer: crygame.dll+sub_1021EF20
    /// </summary>
    public class TlvQuestStateTimeout : Structure, ITlvStructure
    {
        /// <summary>
        /// Quest ID.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quest state.
        /// Field ID: 2
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Accept time.
        /// Field ID: 3
        /// </summary>
        public uint AcceptTime { get; set; }

        /// <summary>
        /// Timeout value.
        /// Field ID: 4
        /// </summary>
        public int Timeout { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, State);
            WriteTlvInt32(buffer, 3, (int)AcceptTime);
            WriteTlvInt32(buffer, 4, Timeout);
        }
    }
}
