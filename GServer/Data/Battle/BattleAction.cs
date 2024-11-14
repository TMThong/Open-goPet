using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Battle
{
    public class BattleAction
    {
        public bool IsNormalAttack { get; set; } = false;

        public int SkillId { get; set; } = 0;

        public Player Player { get; set; }
    }
}
