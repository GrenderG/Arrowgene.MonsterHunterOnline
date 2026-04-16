using Arrowgene.Buffers;

namespace Arrowgene.MonsterHunterOnline.Protocol
{
    /// <summary>
    /// Entry point of TLV structures require magic byte:
    /// 'WriteByte(buffer, (byte)TlvMagic.NoVariant);'
    /// however sub structures do not require such.
    /// </summary>
    public interface ITlvStructure
    {
        TlvMagic Magic { get; }
        void WriteTlv(IBuffer buffer);
        void ReadTlv(IBuffer buffer);
    }
}