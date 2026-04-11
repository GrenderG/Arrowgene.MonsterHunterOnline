using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for sculpture container (id, refreshTime, sculptures list).
    /// C++ Reader: crygame.dll+sub_1013F840 (UnkTlv0059)
    /// C++ Printer: crygame.dll+sub_10140430
    /// </summary>
    public class TlvSculptureContainer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public const int MaxSculptures = 10;

        /// <summary>Field ID: 1</summary>
        public int Id { get; set; }

        /// <summary>Field ID: 2</summary>
        public int RefreshTime { get; set; }

        /// <summary>Count (derived). Field ID: 3</summary>
        public int Count => Sculptures?.Count ?? 0;

        /// <summary>Sculpture data entries. Field ID: 4</summary>
        public List<TlvSculptureData> Sculptures { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: Id = reader.ReadInt(); break;
                    case 2: RefreshTime = reader.ReadInt(); break;
                    case 3: reader.ReadInt(); break;
                    case 4: Sculptures = ReadTlvList<TlvSculptureData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Sculptures?.Count ?? 0) > MaxSculptures)
                throw new InvalidDataException($"[TlvSculptureContainer] Sculptures exceeds {MaxSculptures}.");

            WriteTlvInt(writer, 1, Id);
            WriteTlvInt(writer, 2, RefreshTime);
            WriteTlvInt(writer, 3, Count);
            WriteTlvList(writer, 4, Sculptures);
        }
    }
}
