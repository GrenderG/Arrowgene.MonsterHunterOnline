using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for level warning information.
    /// C++ Reader: crygame.dll+sub_1014A500 (UnkTlv0074)
    /// C++ Printer: crygame.dll+sub_1014A5C0
    /// </summary>
    public class TlvLevelWarning : Structure, ITlvStructure
    {
        /// <summary>
        /// Level identifier.
        /// Field ID: 1
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Warning time value.
        /// Field ID: 2
        /// </summary>
        public uint WarningTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, LevelId);
            WriteTlvInt32(buffer, 2, (int)WarningTime);
        }
    }
}
