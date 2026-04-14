using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for NPC organization/preferences container.
    /// C++ Reader: crygame.dll+sub_1022A980 (UnkTlv0265)
    /// C++ Printer: crygame.dll+sub_1022B490
    /// </summary>
    public class TlvNpcOrgPrefsContainer : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxEntries = 30;

        /// <summary>Attitude count (derived). Field ID: 1</summary>
        public int Count => NpcAtdPkg?.Count ?? 0;

        /// <summary>NPC attitude entries. Field ID: 2</summary>
        public List<TlvNpcAttitude> NpcAtdPkg { get; set; }

        /// <summary>Organization count (derived). Field ID: 3</summary>
        public int OrgNum => NpcOrgPkg?.Count ?? 0;

        /// <summary>NPC organization entries. Field ID: 4</summary>
        public List<TlvNpcOrganization> NpcOrgPkg { get; set; }

        /// <summary>Preference count (derived). Field ID: 5</summary>
        public int PreferNum => NpcPrefersPkg?.Count ?? 0;

        /// <summary>NPC preference entries. Field ID: 6</summary>
        public List<TlvGroupPreference> NpcPrefersPkg { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((NpcAtdPkg?.Count ?? 0) > MaxEntries) throw new InvalidDataException($"[TlvNpcOrgPrefsContainer] NpcAtdPkg exceeds {MaxEntries}.");
            if ((NpcOrgPkg?.Count ?? 0) > MaxEntries) throw new InvalidDataException($"[TlvNpcOrgPrefsContainer] NpcOrgPkg exceeds {MaxEntries}.");
            if ((NpcPrefersPkg?.Count ?? 0) > MaxEntries) throw new InvalidDataException($"[TlvNpcOrgPrefsContainer] NpcPrefersPkg exceeds {MaxEntries}.");

            WriteTlvInt32(buffer, 1, Count);
            WriteTlvSubStructureList(buffer, 2, NpcAtdPkg.Count, NpcAtdPkg);
            WriteTlvInt32(buffer, 3, OrgNum);
            WriteTlvSubStructureList(buffer, 4, NpcOrgPkg.Count, NpcOrgPkg);
            WriteTlvInt32(buffer, 5, PreferNum);
            WriteTlvSubStructureList(buffer, 6, NpcPrefersPkg.Count, NpcPrefersPkg);
        }
    }
}
