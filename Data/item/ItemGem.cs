
public class ItemGem : DataVersion
{

    private int element;
    private String name;
    private int[] option;
    private int[] optionValue;
    private int lvl = 0;
    private int itemTemplateId;
    public long timeUnequip = -1;

    public String getElementIcon()
    {
        switch (element)
        {
            case GopetManager.FIRE_ELEMENT:
                return "(fire)";
            case GopetManager.WATER_ELEMENT:
                return "(water)";
            case GopetManager.ROCK_ELEMENT:
                return "(rock)";
            case GopetManager.THUNDER_ELEMENT:
                return "(thunder)";
            case GopetManager.TREE_ELEMENT:
                return "(tree)";
            case GopetManager.LIGHT_ELEMENT:
                return "(light)";
            case GopetManager.DARK_ELEMENT:
                return "(dark)";
        }
        return "";


    }
    public void setElement(int element)
    {
        this.element = element;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setOption(int[] option)
    {
        this.option = option;
    }

    public void setOptionValue(int[] optionValue)
    {
        this.optionValue = optionValue;
    }

    public void setLvl(int lvl)
    {
        this.lvl = lvl;
    }

    public void setItemTemplateId(int itemTemplateId)
    {
        this.itemTemplateId = itemTemplateId;
    }

    public void setTimeUnequip(long timeUnequip)
    {
        this.timeUnequip = timeUnequip;
    }

    public int getElement()
    {
        return this.element;
    }

    public String getName()
    {
        return this.name;
    }

    public int[] getOption()
    {
        return this.option;
    }

    public int[] getOptionValue()
    {
        return this.optionValue;
    }

    public int getLvl()
    {
        return this.lvl;
    }

    public int getItemTemplateId()
    {
        return this.itemTemplateId;
    }

    public long getTimeUnequip()
    {
        return this.timeUnequip;
    }



}
