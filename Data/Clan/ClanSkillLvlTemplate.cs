using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Clan
{
    public record ClanSkillLvlTemplate(int id, int skillId, int lvl, PetSkillInfo[] skillInfo);
}
