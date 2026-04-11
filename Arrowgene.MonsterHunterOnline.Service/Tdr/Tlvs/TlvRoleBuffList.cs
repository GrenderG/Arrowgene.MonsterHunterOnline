using Rathalos.Core.Utils.IO;
using System.Collections.Generic;
using System.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes.Tlvs
{
    /// <summary>
    /// TLV Structure for role buff list.
    /// C++ Reader: crygame.dll+sub_101FD3F0 (UnkTlv0208)
    /// C++ Printer: crygame.dll+sub_101FD8F0
    /// </summary>
    public class TlvRoleBuffList : TlvStructure
    {
        public override TlvMagic Magic => TlvMagic.Fixed;

        // --- Hardcoded Boundary ---
        public const int MaxBuffs = 40;

        /// <summary>Role GID. Field ID: 1</summary>
        public long RoleGID { get; set; }

        /// <summary>Buff count (derived). Field ID: 2</summary>
        public short Count => (short)(Data?.Count ?? 0);

        /// <summary>Buff data entries. Field ID: 3</summary>
        public List<TlvBuffInfo> Data { get; set; }

        protected override void DeserializeContent(IDataReader reader)
        {
            while (reader.BytesAvailable > 0)
            {
                uint tag = reader.ReadVarUInt();
                uint fieldId = tag >> 4;
                uint wireType = tag & 0xF;

                switch (fieldId)
                {
                    case 1: RoleGID = reader.ReadLong(); break;
                    case 2: reader.ReadShort(); break;
                    case 3: Data = ReadTlvList<TlvBuffInfo>(reader); break;
                    default: SkipTlvField(reader, wireType); break;
                }
            }
        }

        protected override void SerializeContent(IDataWriter writer)
        {
            if ((Data?.Count ?? 0) > MaxBuffs)
                throw new InvalidDataException($"[TlvRoleBuffList] Data exceeds {MaxBuffs}.");

            WriteTlvLong(writer, 1, RoleGID);
            WriteTlvShort(writer, 2, Count);
            WriteTlvList(writer, 3, Data);
        }
    }
}
