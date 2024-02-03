
using Gopet.Util;
using System.Text.Json.Serialization;

public class GameObject
{

    public int  hp, mp, maxHp, maxMp;

    protected int atk, def;

    public int lvl { get; set; } = 1;
    public GameObject()
    {

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

    public virtual int getAgi()
    {
        return 0;
    }

    public virtual int getInt()
    {
        return 0;
    }

    public virtual int getStr()
    {
        return 0;
    }

    [JsonIgnore]
    public virtual PetTemplate Template
    {
        get;
        set;
    }

    public virtual int getDef()
    {
        return def + getAgi() / 3;
    }

    public virtual int getAtk()
    {
        switch (Template.nclass)
        {
            case GopetManager.Archer:
            case GopetManager.Fighter:
                return atk + (getStr() / 3) + 5;
            case GopetManager.Demon:
            case GopetManager.Assassin:
                return atk + (getAgi() / 3) + 5;
            case GopetManager.Angel:
            case GopetManager.Wizard:
                return atk + (getInt() / 3) + 5;
        }
        return 0;
    }

    public virtual int getHpViaPrice()
    {
        return lvl * 3 + getStr() * 4 + 20;
    }

    public virtual int getMpViaPrice()
    {
        return lvl * 3 + getInt() * 5 + 20;
    }
    public virtual float SkipPercent
    {
        get
        {
            return 15 + getAgi() / 1000;
        }
    }

    public virtual float AccuracyPercent
    {
        get
        {
            return 100 + getAgi() / 1000;
        }
    }

    public virtual float HitRate(GameObject B)
    {
        return this.AccuracyPercent - B.SkipPercent;
    }


    public bool IsMiss(GameObject B)
    {
        return Utilities.NextFloatPer() > HitRate(B);
    }

    public virtual bool isCrit()
    {
        switch (Template.nclass)
        {
            case GopetManager.Archer:
            case GopetManager.Fighter:
                return Utilities.NextFloatPer() < (getStr() + getAgi() * 2) / 4000 + (4 / 100);
            case GopetManager.Demon:
            case GopetManager.Assassin:
                return Utilities.NextFloatPer() < (getInt() + getAgi() * 2) / 4000 + (4 / 100);
            case GopetManager.Angel:
            case GopetManager.Wizard:
                return Utilities.NextFloatPer() < (getAgi() * 2) / 2200 + (4 / 100);
        }
        return false;
    }
}
