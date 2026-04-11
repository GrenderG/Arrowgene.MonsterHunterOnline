using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for VIP information.
    /// C++ Reader: crygame.dll+sub_1019CDE0 (UnkTlv0173)
    /// C++ Printer: crygame.dll+sub_1019D220
    /// </summary>
    public class TlvVipInfo : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// VIP Level.
        /// Field ID: 1
        /// </summary>
        public int VipLevel { get; set; }

        /// <summary>
        /// VIP Experience.
        /// Field ID: 2
        /// </summary>
        public int VipExp { get; set; }

        /// <summary>
        /// QQ Game MHO VIP Level.
        /// Field ID: 3
        /// </summary>
        public int QqGameMhoVipLevel { get; set; }

        /// <summary>
        /// QQ Game MHO VIP End Time.
        /// Field ID: 4
        /// </summary>
        public int QqGameMhoVipEndTime { get; set; }

        /// <summary>
        /// QQ Game MHO VIP Last Update Time.
        /// Field ID: 5
        /// </summary>
        public int QqGameMhoVipLastUpdateTime { get; set; }

        /// <summary>
        /// VIP Base Latest End Time.
        /// Field ID: 6
        /// </summary>
        public int VipBaseLatestEndTime { get; set; }

        /// <summary>
        /// VIP Pay Request.
        /// Field ID: 7
        /// </summary>
        public byte VipPayReq { get; set; }

        /// <summary>
        /// VIP Open Period.
        /// Field ID: 8
        /// </summary>
        public int VipOpenPeriod { get; set; }

        /// <summary>
        /// Last enter or exit time.
        /// Field ID: 10
        /// </summary>
        public int LastEnterOrExitTime { get; set; }

        /// <summary>
        /// Finish role VIP merge.
        /// Field ID: 11
        /// </summary>
        public byte FinishRoleVipMerge { get; set; }

        /// <summary>
        /// Created role.
        /// Field ID: 12
        /// </summary>
        public byte CreatedRole { get; set; }

        /// <summary>
        /// Last add VIP exp daily.
        /// Field ID: 13
        /// </summary>
        public int LastAddVipExpDaily { get; set; }

        /// <summary>
        /// VIP pay success count.
        /// Field ID: 14
        /// </summary>
        public byte VipPaySuccCount { get; set; }

        /// <summary>
        /// Delete role count.
        /// Field ID: 15
        /// </summary>
        public byte DelRoleCount { get; set; }

        /// <summary>
        /// Sanction end time.
        /// Field ID: 16
        /// </summary>
        public int SanctionEndTime { get; set; }

        /// <summary>
        /// Face count.
        /// Field ID: 17
        /// </summary>
        public int FaceCount { get; set; }

        /// <summary>
        /// Sanction ratio.
        /// Field ID: 18
        /// </summary>
        public byte SanctionRatio { get; set; }

        /// <summary>
        /// Change sex count.
        /// Field ID: 19
        /// </summary>
        public int ChgSexCount { get; set; }

        /// <summary>
        /// Is newbie flag.
        /// Field ID: 20
        /// </summary>
        public byte IsNewbie { get; set; }

        /// <summary>
        /// Self UI option data.
        /// Field ID: 21
        /// </summary>
        public TlvSelfUIOption SelfUIOption { get; set; } = new();

        /// <summary>
        /// Parent control start time.
        /// Field ID: 22
        /// </summary>
        public int ParentControlStartTime { get; set; }

        /// <summary>
        /// Parent control end time.
        /// Field ID: 23
        /// </summary>
        public int ParentControlEndTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: VipLevel = reader.ReadInt(); break;
                    case 2: VipExp = reader.ReadInt(); break;
                    case 3: QqGameMhoVipLevel = reader.ReadInt(); break;
                    case 4: QqGameMhoVipEndTime = reader.ReadInt(); break;
                    case 5: QqGameMhoVipLastUpdateTime = reader.ReadInt(); break;
                    case 6: VipBaseLatestEndTime = reader.ReadInt(); break;
                    case 7: VipPayReq = reader.ReadByte(); break;
                    case 8: VipOpenPeriod = reader.ReadInt(); break;
                    case 10: LastEnterOrExitTime = reader.ReadInt(); break;
                    case 11: FinishRoleVipMerge = reader.ReadByte(); break;
                    case 12: CreatedRole = reader.ReadByte(); break;
                    case 13: LastAddVipExpDaily = reader.ReadInt(); break;
                    case 14: VipPaySuccCount = reader.ReadByte(); break;
                    case 15: DelRoleCount = reader.ReadByte(); break;
                    case 16: SanctionEndTime = reader.ReadInt(); break;
                    case 17: FaceCount = reader.ReadInt(); break;
                    case 18: SanctionRatio = reader.ReadByte(); break;
                    case 19: ChgSexCount = reader.ReadInt(); break;
                    case 20: IsNewbie = reader.ReadByte(); break;
                    case 21: SelfUIOption = ReadTlvObject<TlvSelfUIOption>(reader); break;
                    case 22: ParentControlStartTime = reader.ReadInt(); break;
                    case 23: ParentControlEndTime = reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, VipLevel);
            WriteTlvInt(writer, 2, VipExp);
            WriteTlvInt(writer, 3, QqGameMhoVipLevel);
            WriteTlvInt(writer, 4, QqGameMhoVipEndTime);
            WriteTlvInt(writer, 5, QqGameMhoVipLastUpdateTime);
            WriteTlvInt(writer, 6, VipBaseLatestEndTime);
            WriteTlvByte(writer, 7, VipPayReq);
            WriteTlvInt(writer, 8, VipOpenPeriod);
            WriteTlvInt(writer, 10, LastEnterOrExitTime);
            WriteTlvByte(writer, 11, FinishRoleVipMerge);
            WriteTlvByte(writer, 12, CreatedRole);
            WriteTlvInt(writer, 13, LastAddVipExpDaily);
            WriteTlvByte(writer, 14, VipPaySuccCount);
            WriteTlvByte(writer, 15, DelRoleCount);
            WriteTlvInt(writer, 16, SanctionEndTime);
            WriteTlvInt(writer, 17, FaceCount);
            WriteTlvByte(writer, 18, SanctionRatio);
            WriteTlvInt(writer, 19, ChgSexCount);
            WriteTlvByte(writer, 20, IsNewbie);
            WriteTlvObject(writer, 21, SelfUIOption);
            WriteTlvInt(writer, 22, ParentControlStartTime);
            WriteTlvInt(writer, 23, ParentControlEndTime);
        }
    }
}
