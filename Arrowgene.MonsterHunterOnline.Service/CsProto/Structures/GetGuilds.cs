using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
{

    /// <summary>
    /// 获取猎团集
    /// </summary>
    public class GetGuilds : Structure, ICsStructure
    {
        public GetGuilds()
        {
            GuildsCount = 0;
            Pages = 0;
            Page = 0;
            Guilds = new CSGuildOutlines();
        }

        /// <summary>
        /// 猎团集
        /// </summary>
        public int GuildsCount;

        /// <summary>
        /// 页集
        /// </summary>
        public int Pages;

        /// <summary>
        /// 页
        /// </summary>
        public int Page;

        /// <summary>
        /// 猎团集
        /// </summary>
        public CSGuildOutlines Guilds;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, GuildsCount);
            WriteInt32(buffer, Pages);
            WriteInt32(buffer, Page);
            Guilds.WriteCs(buffer);
        }

        public void ReadCs(IBuffer buffer)
        {
            GuildsCount = ReadInt32(buffer);
            Pages = ReadInt32(buffer);
            Page = ReadInt32(buffer);
            Guilds.ReadCs(buffer);
        }

    }
}
