using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Clan
{
    public record ClanSkillTemplate(int id, string name, string description, long expire, int[] moneyType, int[] price)
    {
        public ClanSkillLvlTemplate[] clanSkillLvlTemplates { get; set; }
    }
}
