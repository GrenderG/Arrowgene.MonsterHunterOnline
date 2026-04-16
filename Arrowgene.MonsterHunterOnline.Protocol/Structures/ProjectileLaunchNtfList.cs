using Arrowgene.Buffers;
using Arrowgene.MonsterHunterOnline.Protocol;
using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.Protocol.Structures
{
    /// <summary>
    /// projectile appear notify list
    /// </summary>
    public class ProjectileLaunchNtfList : Structure, ICsStructure
    {
        public ProjectileLaunchNtfList()
        {
            Appear = new List<ProjectileLaunchNtf>();
        }

        public List<ProjectileLaunchNtf> Appear;

        public void WriteCs(IBuffer buffer)
        {
            int appearCount = (int)Appear.Count;
            WriteInt32(buffer, appearCount);
            for (int i = 0; i < appearCount; i++)
            {
                Appear[i].WriteCs(buffer);
            }
        }

        public void ReadCs(IBuffer buffer)
        {
            Appear.Clear();
            int appearCount = ReadInt32(buffer);
            for (int i = 0; i < appearCount; i++)
            {
                ProjectileLaunchNtf AppearEntry = new ProjectileLaunchNtf();
                AppearEntry.ReadCs(buffer);
                Appear.Add(AppearEntry);
            }
        }
    }
}
