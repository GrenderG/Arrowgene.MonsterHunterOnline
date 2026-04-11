using Rathalos.Core.Utils.IO;
using System.IO;
using System.Text;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for task content with arguments.
    /// C++ Reader: crygame.dll+sub_1013F3D0 (UnkTlv0058)
    /// C++ Printer: crygame.dll+sub_1013F770
    /// </summary>
    public class TlvTaskContent : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxStringLen = 32;

        /// <summary>
        /// Task identifier.
        /// Field ID: 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Library identifier.
        /// Field ID: 2
        /// </summary>
        public int Lib { get; set; }

        /// <summary>
        /// Content byte.
        /// Field ID: 3
        /// </summary>
        public byte Content { get; set; }

        /// <summary>
        /// Argument 1.
        /// Field ID: 4
        /// </summary>
        public int Arg1 { get; set; }

        /// <summary>
        /// Argument 2.
        /// Field ID: 5
        /// </summary>
        public int Arg2 { get; set; }

        /// <summary>
        /// Argument 3.
        /// Field ID: 6
        /// </summary>
        public int Arg3 { get; set; }

        /// <summary>
        /// NPC identifier.
        /// Field ID: 7
        /// </summary>
        public int Npc { get; set; }

        /// <summary>
        /// Name string.
        /// Field ID: 8
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Note string.
        /// Field ID: 9
        /// </summary>
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Icon string.
        /// Field ID: 10
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// Best string.
        /// Field ID: 11
        /// </summary>
        public string Best { get; set; } = string.Empty;

        /// <summary>
        /// Statistics type.
        /// Field ID: 12
        /// </summary>
        public byte StatisticsType { get; set; }

        /// <summary>
        /// Item prize.
        /// Field ID: 13
        /// </summary>
        public int ItemPrize { get; set; }

        /// <summary>
        /// Note 1 string.
        /// Field ID: 14
        /// </summary>
        public string Note1 { get; set; } = string.Empty;

        /// <summary>
        /// Note 2 string.
        /// Field ID: 15
        /// </summary>
        public string Note2 { get; set; } = string.Empty;

        /// <summary>
        /// Object type.
        /// Field ID: 16
        /// </summary>
        public int ObjType { get; set; }

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
                    case 2: Lib = reader.ReadInt(); break;
                    case 3: Content = reader.ReadByte(); break;
                    case 4: Arg1 = reader.ReadInt(); break;
                    case 5: Arg2 = reader.ReadInt(); break;
                    case 6: Arg3 = reader.ReadInt(); break;
                    case 7: Npc = reader.ReadInt(); break;
                    case 8:
                        int nameLen = reader.ReadInt();
                        if (nameLen > 0 && nameLen < MaxStringLen)
                            Name = Encoding.UTF8.GetString(reader.ReadBytes(nameLen)).TrimEnd('\0');
                        break;
                    case 9:
                        int noteLen = reader.ReadInt();
                        if (noteLen > 0 && noteLen < MaxStringLen)
                            Note = Encoding.UTF8.GetString(reader.ReadBytes(noteLen)).TrimEnd('\0');
                        break;
                    case 10:
                        int iconLen = reader.ReadInt();
                        if (iconLen > 0 && iconLen < MaxStringLen)
                            Icon = Encoding.UTF8.GetString(reader.ReadBytes(iconLen)).TrimEnd('\0');
                        break;
                    case 11:
                        int bestLen = reader.ReadInt();
                        if (bestLen > 0 && bestLen < MaxStringLen)
                            Best = Encoding.UTF8.GetString(reader.ReadBytes(bestLen)).TrimEnd('\0');
                        break;
                    case 12: StatisticsType = reader.ReadByte(); break;
                    case 13: ItemPrize = reader.ReadInt(); break;
                    case 14:
                        int note1Len = reader.ReadInt();
                        if (note1Len > 0 && note1Len < MaxStringLen)
                            Note1 = Encoding.UTF8.GetString(reader.ReadBytes(note1Len)).TrimEnd('\0');
                        break;
                    case 15:
                        int note2Len = reader.ReadInt();
                        if (note2Len > 0 && note2Len < MaxStringLen)
                            Note2 = Encoding.UTF8.GetString(reader.ReadBytes(note2Len)).TrimEnd('\0');
                        break;
                    case 16: ObjType = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Id);
            WriteTlvInt(writer, 2, Lib);
            WriteTlvByte(writer, 3, Content);
            WriteTlvInt(writer, 4, Arg1);
            WriteTlvInt(writer, 5, Arg2);
            WriteTlvInt(writer, 6, Arg3);
            WriteTlvInt(writer, 7, Npc);
            WriteTlvString(writer, 8, Name);
            WriteTlvString(writer, 9, Note);
            WriteTlvString(writer, 10, Icon);
            WriteTlvString(writer, 11, Best);
            WriteTlvByte(writer, 12, StatisticsType);
            WriteTlvInt(writer, 13, ItemPrize);
            WriteTlvString(writer, 14, Note1);
            WriteTlvString(writer, 15, Note2);
            WriteTlvInt(writer, 16, ObjType);
        }
    }
}
