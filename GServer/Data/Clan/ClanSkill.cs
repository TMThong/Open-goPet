using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Clan
{
    public record ClanSkill(int SkillId, DateTime Expire, int SkillLv)
    {
        [JsonIgnore]
        public ClanSkillTemplate SkillTemplate
        {
            get
            {
                return GopetManager.ClanSkillViaId[SkillId];
            }
        }
        [JsonIgnore]
        public ClanSkillLvlTemplate SkillLvlTemplate
        {
            get
            {
                return SkillTemplate.clanSkillLvlTemplates[SkillLv - 1];
            }
        }
        [JsonIgnore]
        public PetSkillInfo[] PetSkillInfos
        {
            get
            {
                return SkillLvlTemplate.skillInfo;
            }
        }
    }
}
