using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{
    /// <summary>
    /// 请求使用物品
    /// </summary>
    public class ItemMgrUseItemReq : Structure, ICsStructure
    {
        public ItemMgrUseItemReq()
        {
            ItemID = 0;
            ItemColumn = 0;
            ItemGrid = 0;
            Param1 = 0;
            Param2 = 0;
            Param3 = 0;
            ExtParam = "";
        }

        /// <summary>
        /// 物品实例
        /// </summary>
        public ulong ItemID;

        /// <summary>
        /// 栏位
        /// </summary>
        public byte ItemColumn;

        /// <summary>
        /// 格子
        /// </summary>
        public ushort ItemGrid;

        /// <summary>
        /// 参数1
        /// </summary>
        public uint Param1;

        /// <summary>
        /// 参数2
        /// </summary>
        public uint Param2;

        /// <summary>
        /// 参数3
        /// </summary>
        public uint Param3;

        /// <summary>
        /// 额外参数
        /// </summary>
        public string ExtParam;

        public void WriteCs(IBuffer buffer)
        {
            WriteUInt64(buffer, ItemID);
            WriteByte(buffer, ItemColumn);
            WriteUInt16(buffer, ItemGrid);
            WriteUInt32(buffer, Param1);
            WriteUInt32(buffer, Param2);
            WriteUInt32(buffer, Param3);
            WriteString(buffer, ExtParam);
        }

        public void ReadCs(IBuffer buffer)
        {
            ItemID = ReadUInt64(buffer);
            ItemColumn = ReadByte(buffer);
            ItemGrid = ReadUInt16(buffer);
            Param1 = ReadUInt32(buffer);
            Param2 = ReadUInt32(buffer);
            Param3 = ReadUInt32(buffer);
            ExtParam = ReadString(buffer);
        }

    }
}
