using System;
using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;

namespace Arrowgene.MonsterHunterOnline.Service.Tdr
{
    public sealed class TlvDebug : Structure, ITlvStructure
    {
        private readonly int? _debugMagic;

        public TlvDebug(int? debugMagic = null)
        {
            _debugMagic = debugMagic;
        }

        public override TlvMagic Magic => _debugMagic.HasValue ? (TlvMagic)_debugMagic.Value : TlvMagic.Debug;
        public void ReadTlv(IBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteTlv(IBuffer buffer)
        {

        }
    }
}
