using Rathalos.Core.Utils.IO;
using System.Collections.Generic;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Container for list of TlvMailHeader.
    /// C++ Reader: crygame.dll+sub_10235470 (UnkTlv0278)
    /// </summary>
    public class TlvFriendInsts : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        /// <summary>
        /// Count (derived from FriendInsts).
        /// Field ID: 1
        /// </summary>
        public int Count => FriendInsts?.Count ?? 0;

        /// <summary>
        /// List of TlvMailHeader.
        /// Field ID: 2
        /// </summary>
        public List<TlvMailHeader> FriendInsts { get; set; }

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
                    case 2: FriendInsts = ReadTlvList<TlvMailHeader>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, FriendInsts);
        }
    }
}
