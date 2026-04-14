using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for NPC attitude.
    /// C++ Reader: crygame.dll+sub_10229050 (UnkTlv0262)
    /// C++ Printer: crygame.dll+sub_102292A0
    /// </summary>
    public class TlvNpcAttitude : Structure, ITlvStructure
    {
        /// <summary>
        /// NPC attitude ID.
        /// Field ID: 1
        /// </summary>
        public int NpcAtdId { get; set; }

        /// <summary>
        /// NPC attitude value.
        /// Field ID: 2
        /// </summary>
        public int NpcAtdValue { get; set; }

        /// <summary>
        /// NPC attitude stage.
        /// Field ID: 3
        /// </summary>
        public int NpcAtdStage { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, NpcAtdId);
            WriteTlvInt32(buffer, 2, NpcAtdValue);
            WriteTlvInt32(buffer, 3, NpcAtdStage);
        }
    }
}
