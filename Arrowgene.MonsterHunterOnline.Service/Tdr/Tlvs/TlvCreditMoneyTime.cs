using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for credit, money and time.
    /// C++ Reader: crygame.dll+sub_1023B050 (UnkTlv0284)
    /// C++ Printer: crygame.dll+sub_1023B2A0
    /// </summary>
    public class TlvCreditMoneyTime : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Total credit.
        /// Field ID: 1
        /// </summary>
        public uint TotalCredit { get; set; }

        /// <summary>
        /// Total money.
        /// Field ID: 2
        /// </summary>
        public uint TotalMoney { get; set; }

        /// <summary>
        /// Last time.
        /// Field ID: 3
        /// </summary>
        public uint LastTime { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: TotalCredit = (uint)reader.ReadInt(); break;
                    case 2: TotalMoney = (uint)reader.ReadInt(); break;
                    case 3: LastTime = (uint)reader.ReadInt(); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, (int)TotalCredit);
            WriteTlvInt(writer, 2, (int)TotalMoney);
            WriteTlvInt(writer, 3, (int)LastTime);
        }
    }
}
