using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure (Equipment Slot / Bead Item).
    /// C++ Writer: crygame.dll+sub_1010F880
    /// C++ Printer: crygame.dll+sub_1010FBE0
    /// </summary>
    public class TlvSlotItem : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        public int SlotItemId { get; set; }
        public int BeadLevel { get; set; }
        public int IsLegend { get; set; } // Often treated as a boolean (0 or 1) in memory, but written as Int32

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
                        SlotItemId = reader.ReadInt();
                        break;

                    case 2:
                        BeadLevel = reader.ReadInt();
                        break;

                    case 3:
                        IsLegend = reader.ReadInt();
                        break;

                    default:
                        SkipTlvField(reader, wireType);
                        break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, SlotItemId);
            WriteTlvInt(writer, 2, BeadLevel);
            WriteTlvInt(writer, 3, IsLegend);
        }
    }
}