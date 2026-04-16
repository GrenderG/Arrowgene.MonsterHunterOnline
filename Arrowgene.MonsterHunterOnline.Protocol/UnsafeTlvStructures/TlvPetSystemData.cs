using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for pet system data (unlock, pets, battle/farm slots).
    /// C++ Reader: crygame.dll+sub_101A6A90 (UnkTlv0186)
    /// C++ Printer: crygame.dll+sub_101A7810
    /// </summary>
    public class TlvPetSystemData : Structure, ITlvStructure
    {
        public const int MaxData = 12;
        public const int MaxSlots = 70;

        /// <summary>Field ID: 2</summary>
        public byte Unlock { get; set; }

        /// <summary>Field ID: 3</summary>
        public int ID { get; set; }

        /// <summary>Count (derived). Field ID: 4</summary>
        public byte Count => (byte)(Data?.Count ?? 0);

        /// <summary>Pet battle data entries. Field ID: 5</summary>
        public List<TlvPetBattleData> Data { get; set; }

        /// <summary>Field ID: 6</summary>
        public byte OwnedNumMax { get; set; }

        /// <summary>Battle num (derived). Field ID: 7</summary>
        public short BattleNum => (short)(Battle?.Length ?? 0);

        /// <summary>Battle slot bytes. Field ID: 8</summary>
        public byte[] Battle { get; set; }

        /// <summary>Farm num (derived). Field ID: 9</summary>
        public short FarmNum => (short)(Farm?.Length ?? 0);

        /// <summary>Farm slot bytes. Field ID: 10</summary>
        public byte[] Farm { get; set; }

        /// <summary>Field ID: 11</summary>
        public byte SupportSlot { get; set; }

        /// <summary>Field ID: 12</summary>
        public byte BattleSlot { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Data?.Count ?? 0) > MaxData)
                throw new InvalidDataException($"[TlvPetSystemData] Data exceeds {MaxData}.");
            if ((Battle?.Length ?? 0) > MaxSlots)
                throw new InvalidDataException($"[TlvPetSystemData] Battle exceeds {MaxSlots}.");
            if ((Farm?.Length ?? 0) > MaxSlots)
                throw new InvalidDataException($"[TlvPetSystemData] Farm exceeds {MaxSlots}.");

            WriteTlvByte(buffer, 2, Unlock);
            WriteTlvInt32(buffer, 3, ID);
            WriteTlvByte(buffer, 4, Count);
            WriteTlvSubStructureList(buffer, 5, Data.Count, Data);
            WriteTlvByte(buffer, 6, OwnedNumMax);
            WriteTlvInt16(buffer, 7, BattleNum);
            WriteTlvByteArr(buffer, 8, Battle);
            WriteTlvInt16(buffer, 9, FarmNum);
            WriteTlvByteArr(buffer, 10, Farm);
            WriteTlvByte(buffer, 11, SupportSlot);
            WriteTlvByte(buffer, 12, BattleSlot);
        }
    }
}
