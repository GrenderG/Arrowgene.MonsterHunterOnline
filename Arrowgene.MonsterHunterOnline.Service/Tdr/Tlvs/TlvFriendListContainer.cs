using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for friend list container.
    /// C++ Reader: crygame.dll+sub_1016E080 (UnkTlv0125)
    /// C++ Printer: crygame.dll+sub_1016EAC0
    /// </summary>
    public class TlvFriendListContainer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundaries ---
        public const int MaxFriends = 500;
        public const int MaxPasserby = 20;
        public const int MaxBlacklist = 20;
        public const int MaxGroups = 10;

        /// <summary>Friend count (derived). Field ID: 1</summary>
        public int IFriendCount => AstFriendData?.Count ?? 0;

        /// <summary>Friend data entries. Field ID: 2</summary>
        public List<TlvFriendRoleInfo> AstFriendData { get; set; }

        /// <summary>Passerby count (derived). Field ID: 3</summary>
        public int IPasserbyCount => AstPasserbyData?.Count ?? 0;

        /// <summary>Passerby data entries. Field ID: 4</summary>
        public List<TlvPasserbyInfo> AstPasserbyData { get; set; }

        /// <summary>Blacklist count (derived). Field ID: 5</summary>
        public int IBlacklistCount => AstBlacklistData?.Count ?? 0;

        /// <summary>Blacklist data entries. Field ID: 6</summary>
        public List<TlvBasicRoleInfo> AstBlacklistData { get; set; }

        /// <summary>Friend group count (derived). Field ID: 7</summary>
        public int IFriendGroupCount => AstFriendGroupData?.Count ?? 0;

        /// <summary>Friend group data entries. Field ID: 8</summary>
        public List<TlvGroupIdName> AstFriendGroupData { get; set; }

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
                    case 2: AstFriendData = ReadTlvList<TlvFriendRoleInfo>(reader); break;
                    case 3: reader.ReadInt(); break;
                    case 4: AstPasserbyData = ReadTlvList<TlvPasserbyInfo>(reader); break;
                    case 5: reader.ReadInt(); break;
                    case 6: AstBlacklistData = ReadTlvList<TlvBasicRoleInfo>(reader); break;
                    case 7: reader.ReadInt(); break;
                    case 8: AstFriendGroupData = ReadTlvList<TlvGroupIdName>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((AstFriendData?.Count ?? 0) > MaxFriends) throw new InvalidDataException($"[TlvFriendListContainer] AstFriendData exceeds {MaxFriends}.");
            if ((AstPasserbyData?.Count ?? 0) > MaxPasserby) throw new InvalidDataException($"[TlvFriendListContainer] AstPasserbyData exceeds {MaxPasserby}.");
            if ((AstBlacklistData?.Count ?? 0) > MaxBlacklist) throw new InvalidDataException($"[TlvFriendListContainer] AstBlacklistData exceeds {MaxBlacklist}.");
            if ((AstFriendGroupData?.Count ?? 0) > MaxGroups) throw new InvalidDataException($"[TlvFriendListContainer] AstFriendGroupData exceeds {MaxGroups}.");

            WriteTlvInt(writer, 1, IFriendCount);
            WriteTlvList(writer, 2, AstFriendData);
            WriteTlvInt(writer, 3, IPasserbyCount);
            WriteTlvList(writer, 4, AstPasserbyData);
            WriteTlvInt(writer, 5, IBlacklistCount);
            WriteTlvList(writer, 6, AstBlacklistData);
            WriteTlvInt(writer, 7, IFriendGroupCount);
            WriteTlvList(writer, 8, AstFriendGroupData);
        }
    }
}
