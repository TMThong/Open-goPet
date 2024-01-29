
using Gopet.Util;
using System.Diagnostics;

public class PetTemplate
{

    public int petId, str, _int, agi;
    public sbyte type, nclass, element;
    public string name, icon, frameImg;

    public void setPetId(int petId)
    {
        this.petId = petId;
    }


    public void setStr(int str)
    {
        this.str = str;
    }

    public void set_int(int _int)
    {
        this._int = _int;
    }

    public void setAgi(int agi)
    {
        this.agi = agi;
    }

    public void setType(sbyte type)
    {
        this.type = type;
    }

    public void setNclass(sbyte nclass)
    {
        this.nclass = nclass;
    }

    public void setElement(sbyte element)
    {
        this.element = element;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setIcon(String icon)
    {
        this.icon = icon;
    }

    public void setFrameImg(String frameImg)
    {
        this.frameImg = frameImg;
    }


    public int getPetId()
    {
        return this.petId;
    }



    public int getStr()
    {
        return this.str;
    }

    public int get_int()
    {
        return this._int;
    }

    public int getAgi()
    {
        return this.agi;
    }

    public sbyte getType()
    {
        return this.type;
    }

    public sbyte getNclass()
    {
        return this.nclass;
    }

    public sbyte getElement()
    {
        return this.element;
    }

    public String getName()
    {
        return this.name;
    }

    public String getIcon()
    {
        return this.icon;
    }

    public String getFrameImg()
    {
        return this.frameImg;
    }



    public void setInt(int i)
    {
        this._int = i;
    }

    public int getInt()
    {
        return this._int;
    }

    public String getDesc()
    {
        return Utilities.Format("(str) %s (int) %s (agi) %s", getStr(), getInt(), getAgi());
    }

    public int getHp()
    {
        return 1 * 3 + str * 4 + 20;
    }

    public int getMp()
    {
        return 1 * 2 + agi * 5 + 20;
    }
}
