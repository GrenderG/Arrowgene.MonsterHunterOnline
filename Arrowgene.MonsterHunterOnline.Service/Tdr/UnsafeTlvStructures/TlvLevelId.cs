using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for level ID.
    /// C++ Reader: crygame.dll+sub_10142F90 (UnkTlv0063)
    /// C++ Printer: crygame.dll+sub_10143180
    /// </summary>
    public class TlvLevelId : Structure, ITlvStructure
    {
        /// <summary>
        /// Level ID.
        /// Field ID: 1
        /// </summary>
        public int LevelId { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, LevelId);
        }
    }
}
