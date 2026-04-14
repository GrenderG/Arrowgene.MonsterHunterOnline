using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for VIP information.
    /// C++ Reader: crygame.dll+sub_1019CDE0 (UnkTlv0173)
    /// C++ Printer: crygame.dll+sub_1019D220
    /// </summary>
    public class TlvVipInfo : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, VipLevel);
            WriteTlvInt32(buffer, 2, VipExp);
            WriteTlvInt32(buffer, 3, QqGameMhoVipLevel);
            WriteTlvInt32(buffer, 4, QqGameMhoVipEndTime);
            WriteTlvInt32(buffer, 5, QqGameMhoVipLastUpdateTime);
            WriteTlvInt32(buffer, 6, VipBaseLatestEndTime);
            WriteTlvByte(buffer, 7, VipPayReq);
            WriteTlvInt32(buffer, 8, VipOpenPeriod);
            WriteTlvInt32(buffer, 10, LastEnterOrExitTime);
            WriteTlvByte(buffer, 11, FinishRoleVipMerge);
            WriteTlvByte(buffer, 12, CreatedRole);
            WriteTlvInt32(buffer, 13, LastAddVipExpDaily);
            WriteTlvByte(buffer, 14, VipPaySuccCount);
            WriteTlvByte(buffer, 15, DelRoleCount);
            WriteTlvInt32(buffer, 16, SanctionEndTime);
            WriteTlvInt32(buffer, 17, FaceCount);
            WriteTlvByte(buffer, 18, SanctionRatio);
            WriteTlvInt32(buffer, 19, ChgSexCount);
            WriteTlvByte(buffer, 20, IsNewbie);
            WriteTlvSubStructure(buffer, 21, SelfUIOption);
            WriteTlvInt32(buffer, 22, ParentControlStartTime);
            WriteTlvInt32(buffer, 23, ParentControlEndTime);
        }
    }
}
