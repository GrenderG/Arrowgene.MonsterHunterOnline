using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for pet system data (unlock, pets, battle/farm slots).
    /// C++ Reader: crygame.dll+sub_101A6A90 (UnkTlv0186)
    /// C++ Printer: crygame.dll+sub_101A7810
    /// </summary>
    public class TlvPetSystemData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: Unlock = reader.ReadByte(); break;
                    case 3: ID = reader.ReadInt(); break;
                    case 4: reader.ReadByte(); break;
                    case 5: Data = ReadTlvList<TlvPetBattleData>(reader); break;
                    case 6: OwnedNumMax = reader.ReadByte(); break;
                    case 7: reader.ReadShort(); break;
                    case 8: { int len = reader.ReadInt(); if (len > 0 && len <= MaxSlots) Battle = reader.ReadBytes(len); } break;
                    case 9: reader.ReadShort(); break;
                    case 10: { int len = reader.ReadInt(); if (len > 0 && len <= MaxSlots) Farm = reader.ReadBytes(len); } break;
                    case 11: SupportSlot = reader.ReadByte(); break;
                    case 12: BattleSlot = reader.ReadByte(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Data?.Count ?? 0) > MaxData)
                throw new InvalidDataException($"[TlvPetSystemData] Data exceeds {MaxData}.");
            if ((Battle?.Length ?? 0) > MaxSlots)
                throw new InvalidDataException($"[TlvPetSystemData] Battle exceeds {MaxSlots}.");
            if ((Farm?.Length ?? 0) > MaxSlots)
                throw new InvalidDataException($"[TlvPetSystemData] Farm exceeds {MaxSlots}.");

            WriteTlvByte(writer, 2, Unlock);
            WriteTlvInt(writer, 3, ID);
            WriteTlvByte(writer, 4, Count);
            WriteTlvList(writer, 5, Data);
            WriteTlvByte(writer, 6, OwnedNumMax);
            WriteTlvShort(writer, 7, BattleNum);
            WriteTlvByteArray(writer, 8, Battle, BattleNum);
            WriteTlvShort(writer, 9, FarmNum);
            WriteTlvByteArray(writer, 10, Farm, FarmNum);
            WriteTlvByte(writer, 11, SupportSlot);
            WriteTlvByte(writer, 12, BattleSlot);
        }
    }
}
