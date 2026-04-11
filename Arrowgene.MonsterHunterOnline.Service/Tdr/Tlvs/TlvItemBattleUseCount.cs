using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Item Use Count / Consumable Tracking).
    /// C++ Reader: crygame.dll+sub_10112590
    /// C++ Printer: crygame.dll+sub_101126B0
    /// </summary>
    public class TlvItemBattleUseCount : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public int ItemId { get; set; }

        // Parsed manually in C++ as 2 bytes, meaning it's a Short (Int16)
        public short UseCount { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1:
                        ItemId = reader.ReadInt();
                        break;

                    case 2:
                        UseCount = reader.ReadShort();
                        break;

                    default:
                        SkipTlvField(reader, wireType);
                        break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, ItemId);
            WriteTlvShort(writer, 2, UseCount);
        }
    }
}