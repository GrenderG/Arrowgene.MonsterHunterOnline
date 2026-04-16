using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for a list of typed variant values.
    /// C++ Reader: crygame.dll+sub_101AEBA0 (UnkTlv0189)
    /// C++ Printer: crygame.dll+sub_101AF840
    /// </summary>
    public class TlvTypedVariantList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxValues = 7;

        /// <summary>
        /// Value list.
        /// Field ID: 1
        /// </summary>
        public List<TlvTypedVariant> Value { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructureList(buffer, 1, Value.Count, Value);
        }
    }
}
