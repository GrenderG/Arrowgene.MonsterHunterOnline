using Arrowgene.Buffers;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Old.Structures;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{

    /// <summary>
    /// 获取猎团团员集
    /// </summary>
    public class GetGuilders : Structure, ICsStructure
    {
        private static readonly ILogger Logger = LogProvider.Logger(typeof(S2CGetGuilders));

        public GetGuilders()
        {
            GuildersCount = 0;
            Pages = 0;
            Page = 0;
            Guilders = new CSGuilders();
        }

        /// <summary>
        /// 团员集
        /// </summary>
        public int GuildersCount;

        /// <summary>
        /// 页集
        /// </summary>
        public int Pages;

        /// <summary>
        /// 页
        /// </summary>
        public int Page;

        /// <summary>
        /// 团员集
        /// </summary>
        public CSGuilders Guilders;

        public void WriteCs(IBuffer buffer)
        {
            WriteInt32(buffer, GuildersCount);
            WriteInt32(buffer, Pages);
            WriteInt32(buffer, Page);
            Guilders.WriteCs(buffer);
        }

        public void ReadCs(IBuffer buffer)
        {
            GuildersCount = ReadInt32(buffer);
            Pages = ReadInt32(buffer);
            Page = ReadInt32(buffer);
            Guilders.ReadCs(buffer);
        }

    }
}
