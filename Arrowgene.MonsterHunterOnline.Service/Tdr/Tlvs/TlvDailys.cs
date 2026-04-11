using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvRefreshLibTasks.
    /// C++ Reader: crygame.dll+sub_10228260 (UnkTlv0260)
    /// </summary>
    public class TlvDailys : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from Dailys).
        /// Field ID: 1
        /// </summary>
        public int Count => Dailys?.Count ?? 0;

        /// <summary>
        /// List of TlvRefreshLibTasks.
        /// Field ID: 2
        /// </summary>
        public List<TlvRefreshLibTasks> Dailys { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: reader.ReadInt(); break;
                    case 2: Dailys = ReadTlvList<TlvRefreshLibTasks>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, Dailys);
        }
    }
}
