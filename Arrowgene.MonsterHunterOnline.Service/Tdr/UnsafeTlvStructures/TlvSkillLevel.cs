using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for skill level info.
    /// C++ Reader: crygame.dll+sub_10126FD0 (UnkTlv0032)
    /// C++ Printer: crygame.dll+sub_101270B0
    /// </summary>
    public class TlvSkillLevel : Structure, ITlvStructure
    {
        /// <summary>
        /// Skill identifier (short).
        /// Field ID: 1
        /// </summary>
        public short Skill { get; set; }

        /// <summary>
        /// Level value.
        /// Field ID: 2
        /// </summary>
        public byte Level { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt16(buffer, 1, Skill);
            WriteTlvByte(buffer, 2, Level);
        }
    }
}
