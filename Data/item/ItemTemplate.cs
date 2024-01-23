 
public class ItemTemplate {

    private int itemId;
    private String name;
    private String description;
    private int _int;
    private int agi;
    private int str;
    private int def;
    private int atk;
    private int hp;
    private int mp;
    private int requireStr;
    private int requireAgi;
    private int requireInt;
    private int type;
    private int[] option = new int[0];
    private int[] optionValue = new int[0];
    private bool isStackable;
    private String frameImgPath;
    private String iconPath;
    public sbyte gender;
    public long expire;
    private bool isOnSky;
    private bool canTrade;
    private sbyte NClass;
    private int iconId;
    private sbyte element;
    private int typeTier;

    public void setItemId(int itemId)
    {
        this.itemId = itemId;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setDescription(String description)
    {
        this.description = description;
    }

    public void set_int(int _int)
    {
        this._int = _int;
    }

    public void setAgi(int agi)
    {
        this.agi = agi;
    }

    public void setStr(int str)
    {
        this.str = str;
    }

    public void setDef(int def)
    {
        this.def = def;
    }

    public void setAtk(int atk)
    {
        this.atk = atk;
    }

    public void setHp(int hp)
    {
        this.hp = hp;
    }

    public void setMp(int mp)
    {
        this.mp = mp;
    }

    public void setRequireStr(int requireStr)
    {
        this.requireStr = requireStr;
    }

    public void setRequireAgi(int requireAgi)
    {
        this.requireAgi = requireAgi;
    }

    public void setRequireInt(int requireInt)
    {
        this.requireInt = requireInt;
    }

    public void setType(int type)
    {
        this.type = type;
    }

    public void setOption(int[] option)
    {
        this.option = option;
    }

    public void setOptionValue(int[] optionValue)
    {
        this.optionValue = optionValue;
    }

     

    public void setFrameImgPath(String frameImgPath)
    {
        this.frameImgPath = frameImgPath;
    }

    public void setIconPath(String iconPath)
    {
        this.iconPath = iconPath;
    }

    public void setGender(sbyte gender)
    {
        this.gender = gender;
    }

    public void setExpire(long expire)
    {
        this.expire = expire;
    }

     
    public void setCanTrade(bool canTrade)
    {
        this.canTrade = canTrade;
    }

    public void setNClass(sbyte NClass)
    {
        this.NClass = NClass;
    }

    public void setIconId(int iconId)
    {
        this.iconId = iconId;
    }

    public void setElement(sbyte element)
    {
        this.element = element;
    }

    public void setTypeTier(int typeTier)
    {
        this.typeTier = typeTier;
    }

    public int getItemId()
    {
        return this.itemId;
    }

    public String getName()
    {
        return this.name;
    }

    public String getDescription()
    {
        return this.description;
    }

    public int get_int()
    {
        return this._int;
    }

    public int getAgi()
    {
        return this.agi;
    }

    public int getStr()
    {
        return this.str;
    }

    public int getDef()
    {
        return this.def;
    }

    public int getAtk()
    {
        return this.atk;
    }

    public int getHp()
    {
        return this.hp;
    }

    public int getMp()
    {
        return this.mp;
    }

    public int getRequireStr()
    {
        return this.requireStr;
    }

    public int getRequireAgi()
    {
        return this.requireAgi;
    }

    public int getRequireInt()
    {
        return this.requireInt;
    }

    public int getType()
    {
        return this.type;
    }

    public int[] getOption()
    {
        return this.option;
    }

    public int[] getOptionValue()
    {
        return this.optionValue;
    }

     

    public String getFrameImgPath()
    {
        return this.frameImgPath;
    }

    public String getIconPath()
    {
        return this.iconPath;
    }

    public sbyte getGender()
    {
        return this.gender;
    }

    public long getExpire()
    {
        return this.expire;
    }

    

    public bool isCanTrade()
    {
        return this.canTrade;
    }

    public sbyte getNClass()
    {
        return this.NClass;
    }

    public int getIconId()
    {
        return this.iconId;
    }

    public sbyte getElement()
    {
        return this.element;
    }

    public int getTypeTier()
    {
        return this.typeTier;
    }



    public String getNameViaType() {
        switch (type) {
            case GopetManager.PET_EQUIP_ARMOUR:
            case GopetManager.PET_EQUIP_GLOVE:
            case GopetManager.PET_EQUIP_SHOE:
            case GopetManager.PET_EQUIP_WEAPON:
            case GopetManager.PET_EQUIP_HAT:
                return Utilities.Format("%s chỉ số (%s(atk) %s(def) %s(hp) %s(mp)) Yêu cầu (%s(str) %s(int) %s(agi))", name, atk, def, hp, mp, requireStr, requireInt, requireAgi);
            default:
                return name;
        }
    }

    public String getDescriptionViaType() {
        switch (type) {
            case GopetManager.ITEM_PART_PET: {
                if (this.optionValue.Length >= 2) {
                    int petId = this.optionValue[0];
                    int count = this.optionValue[1];
                    PetTemplate petTemplate = GopetManager.PETTEMPLATE_HASH_MAP.get(petId);
                    if (petTemplate != null) {
                        return Utilities.Format("Dùng %s mảnh sẽ đổi được pet %s", count, petTemplate.getName());
                    }
                }
                return description;
            }
            case GopetManager.ITEM_PART_ITEM: {
                if (this.optionValue.Length >= 2) {
                    int itemidTemp = this.optionValue[0];
                    int count = this.optionValue[1];
                    ItemTemplate itemTemplate = GopetManager.itemTemplate.get(itemidTemp);
                    if (itemTemplate != null) {
                        return Utilities.Format("Dùng %s mảnh sẽ đổi được %s", count, itemTemplate.getName());
                    }
                }
                return description;
            }
            default:
                return description;
        }
    }
}
