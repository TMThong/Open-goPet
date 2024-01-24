 
public class GameObject  : DataVersion {

    public int atk, def, hp, mp, maxHp, maxMp ;

    public GameObject() {
        
    }

    public virtual void setAtk(int atk)
    {
        this.atk = atk;
    }

    public virtual void setDef(int def)
    {
        this.def = def;
    }

    public virtual void setHp(int hp)
    {
        this.hp = hp;
    }

    public virtual void setMp(int mp)
    {
        this.mp = mp;
    }

    public virtual void setMaxHp(int maxHp)
    {
        this.maxHp = maxHp;
    }

    public virtual void setMaxMp(int maxMp)
    {
        this.maxMp = maxMp;
    }

    public int getAtk()
    {
        return this.atk;
    }

    public int getDef()
    {
        return this.def;
    }

    public int getHp()
    {
        return this.hp;
    }

    public int getMp()
    {
        return this.mp;
    }

    public int getMaxHp()
    {
        return this.maxHp;
    }

    public int getMaxMp()
    {
        return this.maxMp;
    }

}
