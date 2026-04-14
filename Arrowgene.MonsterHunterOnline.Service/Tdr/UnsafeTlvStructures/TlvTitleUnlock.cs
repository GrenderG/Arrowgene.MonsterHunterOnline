using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for title unlock information.
    /// C++ Reader: crygame.dll+sub_10174840 (UnkTlv0126)
    /// C++ Printer: crygame.dll+sub_10174900
    /// </summary>
    public class TlvTitleUnlock : Structure, ITlvStructure
    {
        /// <summary>
        /// Title ID.
        /// Field ID: 1
        /// </summary>
        public int TitleId { get; set; }

        /// <summary>
        /// Unlock time value.
        /// Field ID: 2
        /// </summary>
        public uint UnlockTime { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, TitleId);
            WriteTlvInt32(buffer, 2, (int)UnlockTime);
        }
    }
}
