
using Gopet.Data.Collections;
using Gopet.Util;

public class PetTatto
{

    public int tattooTemplateId;
    public int tattoId;
    public int hp, mp, atk, def;
    public int lvl = 0;

    public PetTatto(int tattooTemplateId)
    {
        this.tattooTemplateId = tattooTemplateId;
        PetTattoTemplate tattoTemplate = getTemp();
        setAtk(tattoTemplate.getAtk());
        setDef(tattoTemplate.getDef());
        setHp(tattoTemplate.getHp());
        setMp(tattoTemplate.getMp());
    }


    public static int getInfo(int info, int lvl)
    {
        return info + (int)Utilities.GetValueFromPercent(info, lvl * 10f);
    }

    public int getHp()
    {
        return getInfo(hp, lvl);
    }

    public void setHp(int hp)
    {
        this.hp = hp;
    }

    public int getMp()
    {
        return getInfo(mp, lvl);
    }

    public void setMp(int mp)
    {
        this.mp = mp;
    }

    public int getAtk()
    {
        return getInfo(atk, lvl);
    }

    public void setAtk(int atk)
    {
        this.atk = atk;
    }

    public int getDef()
    {
        return getInfo(def, lvl);
    }

    public void setDef(int def)
    {
        this.def = def;
    }

    public PetTattoTemplate getTemp()
    {
        return GopetManager.tattos.get(tattooTemplateId);
    }

    public String getName()
    {
        JArrayList<String> infoStrings = new JArrayList<String>();

        if (lvl > 0)
        {
            infoStrings.add($" cấp {lvl}");
        }

        if (getAtk() > 0)
        {
            infoStrings.add(getAtk() + " (atk) ");
        }
        if (getDef() > 0)
        {
            infoStrings.add(getDef() + " (def) ");
        }
        if (getHp() > 0)
        {
            infoStrings.add(getHp() + " (hp) ");
        }
        if (getMp() > 0)
        {
            infoStrings.add(getMp() + " (mp) ");
        }

        return getTemp().getName() + "  " + String.Join(" ", infoStrings);
    }
}
