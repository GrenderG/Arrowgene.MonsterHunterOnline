using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for guild member list.
    /// C++ Reader: crygame.dll+sub_10122920 (UnkTlv0025)
    /// C++ Printer: crygame.dll+sub_10122B60
    /// </summary>
    public class TlvGuildMemberList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public const int MaxGuilders = 256;

        /// <summary>Count (derived). Field ID: 1</summary>
        public int Count => Guilders?.Count ?? 0;

        /// <summary>Guild member entries. Field ID: 2</summary>
        public List<TlvGuildMemberData> Guilders { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break;
                    case 2: Guilders = ReadTlvList<TlvGuildMemberData>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Guilders?.Count ?? 0) > MaxGuilders)
                throw new InvalidDataException($"[TlvGuildMemberList] Guilders exceeds {MaxGuilders}.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Guilders);
        }
    }
}
