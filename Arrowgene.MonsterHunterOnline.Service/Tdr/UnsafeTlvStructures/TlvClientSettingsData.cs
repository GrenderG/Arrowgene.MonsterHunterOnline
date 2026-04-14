using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for client settings data (chat tabs, hunter star, gamepad, silver tips).
    /// C++ Reader: crygame.dll+sub_1016A830 (UnkTlv0111)
    /// C++ Printer: crygame.dll+sub_1016B080
    /// </summary>
    public class TlvClientSettingsData : Structure, ITlvStructure
    {
        /// <summary>Chat tabs data. Field ID: 2</summary>
        public TlvChannelTabs StChatTabs { get; set; } = new();

        /// <summary>Hunter star setting. Field ID: 3</summary>
        public TlvSettingData StHunterStar { get; set; } = new();

        /// <summary>Gamepad custom setting. Field ID: 4</summary>
        public TlvControllerMapping StGamePadCustom { get; set; } = new();

        /// <summary>Silver tips setting. Field ID: 5</summary>
        public TlvTipsRefresh StSilverTips { get; set; } = new();

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvSubStructure(buffer, 2, StChatTabs);
            WriteTlvSubStructure(buffer, 3, StHunterStar);
            WriteTlvSubStructure(buffer, 4, StGamePadCustom);
            WriteTlvSubStructure(buffer, 5, StSilverTips);
        }
    }
}
