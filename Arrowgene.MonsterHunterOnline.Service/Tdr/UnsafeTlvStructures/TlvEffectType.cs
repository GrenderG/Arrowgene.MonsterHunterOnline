using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for effect type with parameters.
    /// C++ Reader: crygame.dll+sub_101FBB30 (UnkTlv0206)
    /// C++ Printer: crygame.dll+sub_101FBD20
    /// </summary>
    public class TlvEffectType : Structure, ITlvStructure
    {
        /// <summary>
        /// Effect type.
        /// Field ID: 1
        /// </summary>
        public int EffectType { get; set; }

        /// <summary>
        /// Parameter 1.
        /// Field ID: 2
        /// </summary>
        public int Param1 { get; set; }

        /// <summary>
        /// Parameter 2.
        /// Field ID: 3
        /// </summary>
        public int Param2 { get; set; }

        /// <summary>
        /// Parameter 3.
        /// Field ID: 4
        /// </summary>
        public int Param3 { get; set; }

        /// <summary>
        /// Parameter 4.
        /// Field ID: 5
        /// </summary>
        public int Param4 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, EffectType);
            WriteTlvInt32(buffer, 2, Param1);
            WriteTlvInt32(buffer, 3, Param2);
            WriteTlvInt32(buffer, 4, Param3);
            WriteTlvInt32(buffer, 5, Param4);
        }
    }
}
