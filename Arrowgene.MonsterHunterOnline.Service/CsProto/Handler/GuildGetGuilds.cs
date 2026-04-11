using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class GuildGetGuilds : CsProtoStructureHandler<C2SGetGuilds>
{

    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(C2SGetGuilds));
    public override CS_CMD_ID Cmd => CS_CMD_ID.C2S_CMD_GUILD_GETGUILDS;


    public override void Handle(Client client, C2SGetGuilds req)
    {
        CsCsProtoStructurePacket<GetGuilds> getGuilds = CsProtoResponse.GetGuilds;

        getGuilds.Structure.GuildsCount = 1;
        getGuilds.Structure.Pages = 1;
        getGuilds.Structure.Page = req.Page;

        CSGuildOutline guildOutline = new CSGuildOutline();
        guildOutline.Id = 1;
        guildOutline.Name = "GuildName";
        guildOutline.Icon = 1;
        guildOutline.Note = "GuildNote";
        guildOutline.Level = 1;
        guildOutline.Repute = 1;
        guildOutline.Leader = "GuildLeader";
        guildOutline.Guilders = 1;
        guildOutline.GuildersAvgLevel = 1;
        guildOutline.JoinLevel = 1;
        guildOutline.HuntSoul = 1;

        getGuilds.Structure.Guilds.Guilds.Add(guildOutline);

        //client.SendCsProtoStructurePacket(getGuilds);

        CsCsProtoStructurePacket<GetGuildDetail> getGuildDetail = CsProtoResponse.GetGuildDetail;
        getGuildDetail.Structure.Guild.Id = 1;
        getGuildDetail.Structure.Guild.Name = "Yo?";

        client.SendCsProtoStructurePacket(getGuildDetail);

        CsCsProtoStructurePacket<GetGuilders> getGuilders = CsProtoResponse.GetGuilders;

        getGuilders.Structure.Page = 1;
        getGuilders.Structure.Pages = 1;
        getGuilders.Structure.GuildersCount = 1;

        CSGuilder guilder = new CSGuilder();

        CSRole role = new CSRole();
        role.Name = "me?";
        role.Dbid = 1;

        guilder.Id = role;
        guilder.Note = "note";
        getGuilders.Structure.Guilders.Guilders.Add(guilder);
        

        //client.SendCsProtoStructurePacket(getGuilders);
    }
}