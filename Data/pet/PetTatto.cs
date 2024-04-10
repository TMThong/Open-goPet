
using Gopet.Data.Collections;
using Gopet.Util;

public class PetTatto
{

    public int tattooTemplateId;
    public int tattoId;
    public int lvl = 0;

    public PetTatto(int tattooTemplateId)
    {
        this.tattooTemplateId = tattooTemplateId;
    }


    public static int getInfo(int info, int lvl)
    {
        return info + (int)Utilities.GetValueFromPercent(info, lvl * 10f);
    }

    public int getHp()
    {
        return getInfo(Template.hp, lvl);
    }


    public int getMp()
    {
        return getInfo(Template.mp, lvl);
    }


    public int getAtk()
    {
        return getInfo(Template.atk, lvl);
    }


    public int getDef()
    {
        return getInfo(Template.def, lvl);
    }


    public PetTattoTemplate Template
    {
        get
        {
            return GopetManager.tattos.get(tattooTemplateId);
        }
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

        return Template.getName() + "  " + String.Join(" ", infoStrings);
    }
}
