using Arrowgene.Buffers;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Protocol.Old.Structures;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{

    /// <summary>
    /// 获取猎团信息
    /// </summary>
    public class GetGuildDetail : Structure, ICsStructure
    {
        private static readonly ILogger Logger = LogProvider.Logger(typeof(S2CGetGuildDetail));

        public GetGuildDetail()
        {
            Guild = new CSGuild();
        }

        /// <summary>
        /// 猎团
        /// </summary>
        public CSGuild Guild;

        public void WriteCs(IBuffer buffer)
        {
            Guild.WriteCs(buffer);
        }

        public void ReadCs(IBuffer buffer)
        {
            Guild.ReadCs(buffer);
        }

    }
}
