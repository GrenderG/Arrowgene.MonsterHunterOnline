using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for client settings data (chat tabs, hunter star, gamepad, silver tips).
    /// C++ Reader: crygame.dll+sub_1016A830 (UnkTlv0111)
    /// C++ Printer: crygame.dll+sub_1016B080
    /// </summary>
    public class TlvClientSettingsData : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>Chat tabs data. Field ID: 2</summary>
        public TlvChannelTabs StChatTabs { get; set; } = new();

        /// <summary>Hunter star setting. Field ID: 3</summary>
        public TlvSettingData StHunterStar { get; set; } = new();

        /// <summary>Gamepad custom setting. Field ID: 4</summary>
        public TlvControllerMapping StGamePadCustom { get; set; } = new();

        /// <summary>Silver tips setting. Field ID: 5</summary>
        public TlvTipsRefresh StSilverTips { get; set; } = new();

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 2: StChatTabs = ReadTlvObject<TlvChannelTabs>(reader); break;
                    case 3: StHunterStar = ReadTlvObject<TlvSettingData>(reader); break;
                    case 4: StGamePadCustom = ReadTlvObject<TlvControllerMapping>(reader); break;
                    case 5: StSilverTips = ReadTlvObject<TlvTipsRefresh>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvObject(writer, 2, StChatTabs);
            WriteTlvObject(writer, 3, StHunterStar);
            WriteTlvObject(writer, 4, StGamePadCustom);
            WriteTlvObject(writer, 5, StSilverTips);
        }
    }
}
