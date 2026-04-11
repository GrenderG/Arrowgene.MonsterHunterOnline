using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for relic boxes container.
    /// C++ Reader: crygame.dll+sub_10231720 (UnkTlv0274)
    /// C++ Printer: crygame.dll+sub_10231C80
    /// </summary>
    public class TlvRelicBoxesContainer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public const int MaxBoxes = 5;

        /// <summary>Count (derived). Field ID: 1</summary>
        public byte Count => (byte)(Boxes?.Count ?? 0);

        /// <summary>Box entries. Field ID: 2</summary>
        public List<TlvRelicChessData> Boxes { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadByte(); break;
                    case 2: Boxes = ReadTlvList<TlvRelicChessData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Boxes?.Count ?? 0) > MaxBoxes)
                throw new InvalidDataException($"[TlvRelicBoxesContainer] Boxes exceeds {MaxBoxes}.");

            WriteTlvByte(writer, 1, Count);
            WriteTlvList(writer, 2, Boxes);
        }
    }
}
