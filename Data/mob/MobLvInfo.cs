namespace Gopet.Data.Mob
{
    public class MobLvInfo
    {

        public int lvl, hp, strength, exp;

        public virtual int getLvl()
        {
            return lvl;
        }

        public void setLvl(int lvl)
        {
            this.lvl = lvl;
        }

        public virtual int getHp()
        {
            return hp;
        }

        public void setHp(int hp)
        {
            this.hp = hp;
        }

        public virtual int getStrength()
        {
            return strength;
        }

        public void setStrength(int strength)
        {
            this.strength = strength;
        }

        public virtual int getExp()
        {
            return exp;
        }

        public void setExp(int exp)
        {
            this.exp = exp;
        }
    }
}