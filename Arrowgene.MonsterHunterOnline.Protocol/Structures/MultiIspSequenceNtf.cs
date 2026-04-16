using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures;

public class MultiIspSequenceNtf : Structure, ICsStructure
{
    /// <summary>
    /// 双线机房运营商顺序
    /// </summary>
    public MultiIspSequenceNtf()
    {
        Sequence = 0;
    }

    /// <summary>
    /// 双线机房运营商顺序
    /// </summary>
    public int Sequence { get; set; }


    public  void WriteCs(IBuffer buffer)
    {
        WriteInt32(buffer, Sequence);
    }

    public void ReadCs(IBuffer buffer)
    {
        Sequence = ReadInt32(buffer);
    }
}