using Rathalos.Core.Utils.IO;

namespace Rathalos.Core.Protocol.Messages.Custom.Csproto.Classes
{
    public sealed class TlvDebug : TlvStructure
    {
        private readonly int? _debugMagic;

        public TlvDebug(int? debugMagic = null)
        {
            _debugMagic = debugMagic;
        }

        public override TlvMagic Magic => _debugMagic.HasValue ? (TlvMagic)_debugMagic.Value : TlvMagic.Debug;
        protected override void SerializeContent(IDataWriter writer)
        {
        }
        override protected void DeserializeContent(IDataReader reader)
        {
        }
    }
}
