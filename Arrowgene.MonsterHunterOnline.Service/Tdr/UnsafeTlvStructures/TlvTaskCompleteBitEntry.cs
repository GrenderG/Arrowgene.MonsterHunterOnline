using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for task complete bit entry (id + state + 2 byte args).
    /// C++ Reader: crygame.dll+sub_1021F440 (UnkTlv0257 internal)
    /// </summary>
    public class TlvTaskCompleteBitEntry : Structure, ITlvStructure
    {
        /// <summary>Field ID: 1</summary>
        public short Id { get; set; }

        /// <summary>Field ID: 2</summary>
        public byte State { get; set; }

        /// <summary>Field ID: 3</summary>
        public byte Arg { get; set; }

        /// <summary>Field ID: 4</summary>
        public byte Arg2 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvVarInt16(buffer, 1, Id);
            WriteTlvByte(buffer, 2, State);
            WriteTlvByte(buffer, 3, Arg);
            WriteTlvByte(buffer, 4, Arg2);
        }
    }
}
