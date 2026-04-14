using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for task state entry with VarInt arguments.
    /// C++ Reader: crygame.dll+sub_1021EC60 (UnkTlv0257 internal)
    /// </summary>
    public class TlvTaskStateVarEntry : Structure, ITlvStructure
    {
        /// <summary>Field ID: 1</summary>
        public short Id { get; set; }

        /// <summary>Field ID: 2</summary>
        public byte State { get; set; }

        /// <summary>Field ID: 3</summary>
        public uint Arg { get; set; }

        /// <summary>Field ID: 4</summary>
        public int Arg2 { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvVarInt16(buffer, 1, Id);
            WriteTlvByte(buffer, 2, State);
            WriteTlvVarUInt32(buffer, 3, Arg);
            WriteTlvVarInt32(buffer, 4, Arg2);
        }
    }
}
