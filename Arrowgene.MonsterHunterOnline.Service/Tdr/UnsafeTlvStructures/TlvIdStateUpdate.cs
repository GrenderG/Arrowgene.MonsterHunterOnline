using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr.TlvStructures
{
    /// <summary>
    /// TLV Structure for ID state with last update.
    /// C++ Reader: crygame.dll+sub_102443F0 (UnkTlv0288)
    /// C++ Printer: crygame.dll+sub_102446E0
    /// </summary>
    public class TlvIdStateUpdate : Structure, ITlvStructure
    {
        /// <summary>
        /// ID.
        /// Field ID: 1
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// State.
        /// Field ID: 2
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// Last update time.
        /// Field ID: 3
        /// </summary>
        public uint LastUpdate { get; set; }

        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {
            WriteTlvInt32(buffer, 1, (int)Id);
            WriteTlvByte(buffer, 2, State);
            WriteTlvInt32(buffer, 3, (int)LastUpdate);
        }
    }
}
