using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for NPC organization/preferences container.
    /// C++ Reader: crygame.dll+sub_1022A980 (UnkTlv0265)
    /// C++ Printer: crygame.dll+sub_1022B490
    /// </summary>
    public class TlvNpcOrgPrefsContainer : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

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
                    case 2: NpcAtdPkg = ReadTlvList<TlvNpcAttitude>(reader); break;
                    case 3: reader.ReadInt(); break;
                    case 4: NpcOrgPkg = ReadTlvList<TlvNpcOrganization>(reader); break;
                    case 5: reader.ReadInt(); break;
                    case 6: NpcPrefersPkg = ReadTlvList<TlvGroupPreference>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((NpcAtdPkg?.Count ?? 0) > MaxEntries) throw new InvalidDataException($"[TlvNpcOrgPrefsContainer] NpcAtdPkg exceeds {MaxEntries}.");
            if ((NpcOrgPkg?.Count ?? 0) > MaxEntries) throw new InvalidDataException($"[TlvNpcOrgPrefsContainer] NpcOrgPkg exceeds {MaxEntries}.");
            if ((NpcPrefersPkg?.Count ?? 0) > MaxEntries) throw new InvalidDataException($"[TlvNpcOrgPrefsContainer] NpcPrefersPkg exceeds {MaxEntries}.");

            WriteTlvInt(writer, 1, Count);
            WriteTlvList(writer, 2, NpcAtdPkg);
            WriteTlvInt(writer, 3, OrgNum);
            WriteTlvList(writer, 4, NpcOrgPkg);
            WriteTlvInt(writer, 5, PreferNum);
            WriteTlvList(writer, 6, NpcPrefersPkg);
        }
    }
}
