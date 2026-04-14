using Arrowgene.Buffers;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Structures
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
