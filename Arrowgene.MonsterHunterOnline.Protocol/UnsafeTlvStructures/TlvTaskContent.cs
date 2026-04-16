using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Text;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for task content with arguments.
    /// C++ Reader: crygame.dll+sub_1013F3D0 (UnkTlv0058)
    /// C++ Printer: crygame.dll+sub_1013F770
    /// </summary>
    public class TlvTaskContent : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, Id);
            WriteTlvInt32(buffer, 2, Lib);
            WriteTlvByte(buffer, 3, Content);
            WriteTlvInt32(buffer, 4, Arg1);
            WriteTlvInt32(buffer, 5, Arg2);
            WriteTlvInt32(buffer, 6, Arg3);
            WriteTlvInt32(buffer, 7, Npc);
            WriteTlvString(buffer, 8, Name);
            WriteTlvString(buffer, 9, Note);
            WriteTlvString(buffer, 10, Icon);
            WriteTlvString(buffer, 11, Best);
            WriteTlvByte(buffer, 12, StatisticsType);
            WriteTlvInt32(buffer, 13, ItemPrize);
            WriteTlvString(buffer, 14, Note1);
            WriteTlvString(buffer, 15, Note2);
            WriteTlvInt32(buffer, 16, ObjType);
        }
    }
}
