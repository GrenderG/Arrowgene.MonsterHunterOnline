using System;
using System.Collections.Generic;
using Arrowgene.Buffers;
using System.IO;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for role buff list.
    /// C++ Reader: crygame.dll+sub_101FD3F0 (UnkTlv0208)
    /// C++ Printer: crygame.dll+sub_101FD8F0
    /// </summary>
    public class TlvRoleBuffList : Structure, ITlvStructure
    {
        // --- Hardcoded Boundary ---
        public const int MaxBuffs = 40;

        /// <summary>Role GID. Field ID: 1</summary>
        public long RoleGID { get; set; }

        /// <summary>Buff count (derived). Field ID: 2</summary>
        public short Count => (short)(Data?.Count ?? 0);

        /// <summary>Buff data entries. Field ID: 3</summary>
        public List<TlvBuffInfo> Data { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            if ((Data?.Count ?? 0) > MaxBuffs)
                throw new InvalidDataException($"[TlvRoleBuffList] Data exceeds {MaxBuffs}.");

            WriteTlvInt64(buffer, 1, RoleGID);
            WriteTlvInt16(buffer, 2, Count);
            WriteTlvSubStructureList(buffer, 3, Data.Count, Data);
        }
    }
}
