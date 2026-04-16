using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.UnsafeTlvStructures
{
    /// <summary>
    /// TLV Structure for friend list container.
    /// C++ Reader: crygame.dll+sub_1016E080 (UnkTlv0125)
    /// C++ Printer: crygame.dll+sub_1016EAC0
    /// </summary>
    public class TlvFriendListContainer : Structure, ITlvStructure
    {
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

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((AstFriendData?.Count ?? 0) > MaxFriends) throw new InvalidDataException($"[TlvFriendListContainer] AstFriendData exceeds {MaxFriends}.");
            if ((AstPasserbyData?.Count ?? 0) > MaxPasserby) throw new InvalidDataException($"[TlvFriendListContainer] AstPasserbyData exceeds {MaxPasserby}.");
            if ((AstBlacklistData?.Count ?? 0) > MaxBlacklist) throw new InvalidDataException($"[TlvFriendListContainer] AstBlacklistData exceeds {MaxBlacklist}.");
            if ((AstFriendGroupData?.Count ?? 0) > MaxGroups) throw new InvalidDataException($"[TlvFriendListContainer] AstFriendGroupData exceeds {MaxGroups}.");

            WriteTlvInt32(buffer, 1, IFriendCount);
            WriteTlvSubStructureList(buffer, 2, AstFriendData.Count, AstFriendData);
            WriteTlvInt32(buffer, 3, IPasserbyCount);
            WriteTlvSubStructureList(buffer, 4, AstPasserbyData.Count, AstPasserbyData);
            WriteTlvInt32(buffer, 5, IBlacklistCount);
            WriteTlvSubStructureList(buffer, 6, AstBlacklistData.Count, AstBlacklistData);
            WriteTlvInt32(buffer, 7, IFriendGroupCount);
            WriteTlvSubStructureList(buffer, 8, AstFriendGroupData.Count, AstFriendGroupData);
        }
    }
}
