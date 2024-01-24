using Gopet.Battle;
using Gopet.Util;
using System.Drawing;

namespace Gopet.Data.Mob
{
    public class Mob : GameObject
    {

        protected PetTemplate petTemplate;
        private GopetPlace place;
        protected MobLvInfo mobLvInfo;
        protected MobLvlMap mobLvlMap;
        protected MobLocation mobLocation;
        private int mobId;
        private bool elite = false;

        private PetBattle petBattle;

        public Rectangle bound;

        public virtual PetBattle getPetBattle()
        {
            return petBattle;
        }

        public virtual void setPetBattle(PetBattle petBattle)
        {

            this.petBattle = petBattle;
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

            this.petTemplate = petTemplate;
            this.place = place;
            this.mobLvlMap = mobLvlMap;
            this.mobLocation = mobLocation;
            initMob();
        }

        public Mob(PetTemplate petTemplate, GopetPlace place, MobLvlMap mobLvlMap, MobLocation mobLocation, MobLvInfo mobLvInfo) : this()
        {

            this.petTemplate = petTemplate;
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

        public void setPetTemplate(PetTemplate petTemplate)
        {
            this.petTemplate = petTemplate;
        }

        public void setMobLocation(MobLocation mobLocation)
        {
            this.mobLocation = mobLocation;
        }

        public PetTemplate getPetTemplate()
        {
            return petTemplate;
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
            hp = mobLvInfo.getHp();
            maxHp = mobLvInfo.getHp();
            mp = 100 + mobLvInfo.getLvl() * 10;
            maxMp = 100 + mobLvInfo.getLvl() * 10;
            int xTile = 4;
            bound = new Rectangle(mobLocation.getX() - 24 * xTile, mobLocation.getY() - 24 * xTile, 24 * xTile * 2, 24 * xTile * 2);
        }

        public void addHp(int damage)
        {
            hp -= damage;
            if (hp < 0)
            {
                hp = 0;
            }
        }

        public void addHpReal(int hpPlus)
        {
            if (hpPlus + hp > maxHp)
            {
                hp = maxHp;
            }
            else
            {
                hp += hpPlus;
            }
        }

        public void setMobLvInfo(MobLvInfo mobLvInfo)
        {
            this.mobLvInfo = mobLvInfo;
        }

        public bool isElite()
        {
            return elite;
        }

        public void setElite(bool elite)
        {
            this.elite = elite;
        }

        public string getName()
        {
            return getPetTemplate().getName();
        }
    }
}