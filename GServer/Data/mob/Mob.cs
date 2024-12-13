
using Gopet.Battle;
using Gopet.Util;
using System.Drawing;

namespace Gopet.Data.Mob
{
    public class Mob : Pet
    {
        private GopetPlace place;
        protected MobLvInfo mobLvInfo;
        protected MobLvlMap mobLvlMap;
        protected MobLocation mobLocation;
        private int mobId;

        private PetBattle petBattle;
        public Mutex Mutex { get; } = new Mutex();

        public DateTime? TimeEndUpdate { get; set; } = null;

        public Rectangle bound;

        public virtual PetBattle getPetBattle(Player player)
        {
            return petBattle;
        }

        public virtual void setPetBattle(PetBattle petBattle, Player player)
        {

            this.petBattle = petBattle;
        }

        public virtual bool HasBattle
        {
            get
            {
                return petBattle != null;
            }
        }

        public virtual MobLvlMap getMobLvlMap()
        {
            return mobLvlMap;
        }

        public virtual void setMobLvlMap(MobLvlMap mobLvlMap)
        {
            this.mobLvlMap = mobLvlMap;
        }

        public virtual int getMobId()
        {
            return mobId;
        }

        public void setMobId(int mobId)
        {
            this.mobId = mobId;
        }

        public MobLocation getMobLocation()
        {
            return mobLocation;
        }

        public Mob(PetTemplate petTemplate, GopetPlace place, MobLvlMap mobLvlMap, MobLocation mobLocation) : this()
        {
            this.petIdTemplate = petTemplate.petId;
            this.place = place;
            this.mobLvlMap = mobLvlMap;
            this.mobLocation = mobLocation;
            initMob();
        }

        public Mob(PetTemplate petTemplate, GopetPlace place, MobLvlMap mobLvlMap, MobLocation mobLocation, MobLvInfo mobLvInfo) : this()
        {

            this.petIdTemplate = petTemplate.petId;
            this.place = place;
            this.mobLvlMap = mobLvlMap;
            this.mobLocation = mobLocation;
            this.mobLvInfo = mobLvInfo;
            initMob();
        }

        public Mob()
        {
            maxHp = 1;
            maxMp = 1;
            mp = 1;
        }


        public void setMobLocation(MobLocation mobLocation)
        {
            this.mobLocation = mobLocation;
        }


        public GopetPlace getPlace()
        {
            return place;
        }

        public MobLvInfo getMobLvInfo()
        {
            return mobLvInfo;
        }

        public int getHp()
        {
            return hp;
        }

        public void setHp(int hp)
        {
            this.hp = hp;
        }



        public virtual void initMob()
        {
            if (mobLvInfo == null)
            {
                mobLvInfo = GopetManager.MOBLVLINFO_HASH_MAP.get(Utilities.nextInt(mobLvlMap.getLvlFrom(), mobLvlMap.getLvlTo()));
            }
            this.lvl = mobLvInfo.lvl;
            hp = mobLvInfo.hp;
            maxHp = getHpViaPrice();
            mp = getMpViaPrice();
            maxMp = getMpViaPrice();
            int xTile = 4;
            bound = new Rectangle(mobLocation.getX() - 24 * xTile, mobLocation.getY() - 24 * xTile, 24 * xTile * 2, 24 * xTile * 2);
        }

        public virtual void addHp(int damage, Player player)
        {
            hp -= damage;
            if (hp < 0)
            {
                hp = 0;
            }
        }

        public virtual void SetWinnerIfHpZero(Player player)
        {

        }

        public virtual Player getLastHitPlayer() => null;


        public void setMobLvInfo(MobLvInfo mobLvInfo)
        {
            this.mobLvInfo = mobLvInfo;
        }


        public string getName(Player player)
        {
            return getPetTemplate().getName(player);
        }

        public override int getAgi()
        {
            return this.mobLvInfo.agi;
        }

        public override int getInt()
        {
            return this.mobLvInfo._int;
        }

        public override int getStr()
        {
            return this.mobLvInfo.str;
        }

        public override int getAtk()
        {
            if (this.mobLvInfo.atk.HasValue)
            {
                return this.mobLvInfo.atk.Value;
            }
            return base.getAtk();
        }
    }
}