using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for training time and slot.
    /// C++ Reader: crygame.dll+sub_1019F6C0 (UnkTlv0176)
    /// C++ Printer: crygame.dll+sub_1019F7A0
    /// </summary>
    public class TlvTrainTimeSlot : Structure, ITlvStructure
    {
        /// <summary>
        /// Training time.
        /// Field ID: 1
        /// </summary>
        public uint TrainTime { get; set; }

        /// <summary>
        /// Training slot.
        /// Field ID: 2
        /// </summary>
        public byte TrainSlot { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)TrainTime);
            WriteTlvByte(buffer, 2, TrainSlot);
        }
    }
}
