using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvIdBuyTimes.
    /// C++ Reader: crygame.dll+sub_1022E940 (UnkTlv0271)
    /// </summary>
    public class TlvDragonBoxShopItems : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from DragonBoxShopItems).
        /// Field ID: 1
        /// </summary>
        public int Count => DragonBoxShopItems?.Count ?? 0;

        /// <summary>
        /// List of TlvIdBuyTimes.
        /// Field ID: 2
        /// </summary>
        public List<TlvIdBuyTimes> DragonBoxShopItems { get; set; }

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
                    case 2: DragonBoxShopItems = ReadTlvList<TlvIdBuyTimes>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, DragonBoxShopItems);
        }
    }
}
