using Gopet.Battle;
using Gopet.Data.GopetClan;

namespace Gopet.Data.Mob
{
    public class Boss : Mob
    {

        private BossTemplate bossTemplate;

        public bool isTimeOut = false;

        public DateTime TimeOut = DateTime.MinValue;

        public Dictionary<Player, PetBattle> Battle { get; } = new Dictionary<Player, PetBattle>();
        public Dictionary<Player, int> Damage { get; } = new Dictionary<Player, int>();

        public GopetClan.Clan OwnerClan { get; set; }

        public Player[] GetRank()
        {
            return Damage.OrderByDescending(t => t.Value).Reverse().Select(c => c.Key).ToArray();
        }

        private Player lastHit;

        public override void SetWinnerIfHpZero(Player player)
        {
            if (lastHit != null) return;
            if (Battle.ContainsKey(player))
            {
                if (this.hp <= 0)
                {
                    lastHit = player;
                }
            }
        }

        public override void addHp(int damage, Player player)
        {
            if (this.bossTemplate.typeBoss == BossTemplate.TYPE_BIRTHDAY_EVENT)
            {
                if (damage > 0)
                {
                    this.hp -= 1;
                }
                return;
            }
            base.addHp(damage, player);
            if (damage < 0)
            {
                if (this.Damage.ContainsKey(player))
                {
                    this.Damage[player] += Math.Abs(damage);
                }
                else
                {
                    this.Damage[player] = Math.Abs(damage);
                }
            }
        }

        public override Player getLastHitPlayer()
        {
            return lastHit;
        }


        public Boss(int bossTemplateId, MobLocation mobLocation)
        {
            bossTemplate = GopetManager.boss.get(bossTemplateId);
            this.petIdTemplate = bossTemplate.petTemplateId;
            this.setMobLocation(mobLocation);
            this.setMobLvInfo(new MobLvInfoImp(bossTemplate));
            initMob();
        }

        public override PetBattle getPetBattle(Player player)
        {
            if (Battle.TryGetValue(player, out var battle))
            {
                return battle;
            }
            return null;
        }

        public override void setPetBattle(PetBattle petBattle, Player player)
        {
            Battle[player] = petBattle;
        }

        public override bool HasBattle
        {
            get
            {
                return Battle.Keys.Count > 0;
            }
        }

        sealed class MobLvInfoImp : MobLvInfo
        {
            public MobLvInfoImp(BossTemplate bossTemplate)
            {
                this.bossTemplate = bossTemplate;
                this.agi = this.bossTemplate.agi;
                this.lvl = this.bossTemplate.lvl;
                this.exp = this.bossTemplate.exp;
                this._int = this.bossTemplate._int;
                this.str = this.bossTemplate.str;
                this.hp = this.bossTemplate.hp;
                this.atk = this.bossTemplate.atk;
            }



            public BossTemplate bossTemplate { get; }
        }

        internal BossTemplate Template
        {
            get 
            { 
                return bossTemplate; 
            }
        }
    }
}