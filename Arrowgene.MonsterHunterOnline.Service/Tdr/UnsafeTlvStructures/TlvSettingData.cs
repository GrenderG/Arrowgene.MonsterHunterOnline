using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for setting data pair.
    /// C++ Reader: crygame.dll+sub_10168160 (UnkTlv0108)
    /// C++ Printer: crygame.dll+sub_10168240
    /// </summary>
    public class TlvSettingData : Structure, ITlvStructure
    {
        /// <summary>
        /// First setting data.
        /// Field ID: 1
        /// </summary>
        public uint FirstSettingData { get; set; }

        /// <summary>
        /// Second setting data.
        /// Field ID: 2
        /// </summary>
        public uint SecondSettingData { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)FirstSettingData);
            WriteTlvInt32(buffer, 2, (int)SecondSettingData);
        }
    }
}
