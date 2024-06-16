using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Clan
{
    public record ClanSkillTemplate(int id, string name, string description, long expire, sbyte[] moneyType, int[] price, int lvlClanRequire)
    {
        public ClanSkillLvlTemplate[] clanSkillLvlTemplates { get; set; }
    }
}
