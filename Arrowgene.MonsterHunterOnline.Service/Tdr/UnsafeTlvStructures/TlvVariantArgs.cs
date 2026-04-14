using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for variant arguments (tagged union).
    /// Only one field is set at a time based on the tag type.
    /// C++ Reader: crygame.dll+sub_101AE410 (UnkTlv0187)
    /// C++ Printer: crygame.dll+sub_101AE8A0
    /// </summary>
    public class TlvVariantArgs : Structure, ITlvStructure
    {
        /// <summary>
        /// Active variant tag (1=int, 2=float, 4=short, 6=long).
        /// </summary>
        public int TypeTag { get; set; }

        /// <summary>
        /// Integer value.
        /// Field ID: 1
        /// </summary>
        public int IntValue { get; set; }

        /// <summary>
        /// Float value (stored as int bits).
        /// Field ID: 2
        /// </summary>
        public float FloatValue { get; set; }

        /// <summary>
        /// Boolean/short value.
        /// Field ID: 4
        /// </summary>
        public short BoolValue { get; set; }

        /// <summary>
        /// UInt64 value.
        /// Field ID: 6
        /// </summary>
        public ulong UInt64Value { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            switch (TypeTag)
            {
                case 1: WriteTlvInt32(buffer, 1, IntValue); break;
                case 2: WriteTlvFloat(buffer, 2, FloatValue); break;
                case 4: WriteTlvInt16(buffer, 4, BoolValue); break;
                case 6: WriteTlvUInt64(buffer, 6, UInt64Value); break;
            }
        }
    }
}
