 
public class PetTattoTemplate {
    private int tattooId;
    private String name, iconPath;
    private int atk, def, hp, mp;
    private float percent;
    private int type;

    public void setTattooId(int tattooId)
    {
        this.tattooId = tattooId;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setIconPath(String iconPath)
    {
        this.iconPath = iconPath;
    }

    public void setAtk(int atk)
    {
        this.atk = atk;
    }

    public void setDef(int def)
    {
        this.def = def;
    }

    public void setHp(int hp)
    {
        this.hp = hp;
    }

    public void setMp(int mp)
    {
        this.mp = mp;
    }

    public void setPercent(float percent)
    {
        this.percent = percent;
    }

    public void setType(int type)
    {
        this.type = type;
    }

    public int getTattooId()
    {
        return this.tattooId;
    }

    public String getName()
    {
        return this.name;
    }

    public String getIconPath()
    {
        return this.iconPath;
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

    public float getPercent()
    {
        return this.percent;
    }

    public int getType()
    {
        return this.type;
    }

}
