using Arrowgene.Buffers;

namespace Arrowgene.MonsterHunterOnline.Protocol
{
    public interface ICsStructure
    {
        void WriteCs(IBuffer buffer);
        void ReadCs(IBuffer buffer);
    }
}
