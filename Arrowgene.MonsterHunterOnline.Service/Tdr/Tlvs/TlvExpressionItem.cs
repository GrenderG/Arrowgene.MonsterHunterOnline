using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// Reconstructed TLV Structure.
    /// C++ Writer: crygame.dll+sub_101EEE50
    /// C++ Reader: crygame.dll+sub_XXXXX
    /// C++ Printer: crygame.dll+sub_101EF2F0
    /// </summary>
    public class TlvExpressionItem : TlvStructure
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int Point { get; set; }
        public int CollectLevel { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                switch (tag >> 4)
                {
                    case 1: Id = reader.ReadVarInt(); break;
                    case 2: Level = reader.ReadVarInt(); break;
                    case 3: Point = reader.ReadVarInt(); break;
                    case 4: CollectLevel = reader.ReadVarInt(); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvInt(writer, 2, Level);
            WriteTlvInt(writer, 3, Point);
            WriteTlvInt(writer, 4, CollectLevel);
        }
    }
}