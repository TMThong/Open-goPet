using Gopet.Util;

namespace Gopet.Data.GopetItem
{
    public class ItemTemplate
    {

        private int itemId;
        private string name;
        private string description;
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
        public bool isStackable;
        private string frameImgPath;
        private string iconPath;
        public sbyte gender;
        public long expire;
        public bool isOnSky;
        private bool canTrade;
        private sbyte NClass;
        private int iconId;
        private sbyte element;
        private int typeTier;

        public void setItemId(int itemId)
        {
            this.itemId = itemId;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setDescription(string description)
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



        public void setFrameImgPath(string frameImgPath)
        {
            this.frameImgPath = frameImgPath;
        }

        public void setIconPath(string iconPath)
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
            return itemId;
        }

        public string getName()
        {
            return name;
        }

        public string getDescription()
        {
            return description;
        }

        public int get_int()
        {
            return _int;
        }

        public int getAgi()
        {
            return agi;
        }

        public int getStr()
        {
            return str;
        }

        public int getDef()
        {
            return def;
        }

        public int getAtk()
        {
            return atk;
        }

        public int getHp()
        {
            return hp;
        }

        public int getMp()
        {
            return mp;
        }

        public int getRequireStr()
        {
            return requireStr;
        }

        public int getRequireAgi()
        {
            return requireAgi;
        }

        public int getRequireInt()
        {
            return requireInt;
        }

        public int getType()
        {
            return type;
        }

        public int[] getOption()
        {
            return option;
        }

        public int[] getOptionValue()
        {
            return optionValue;
        }



        public string getFrameImgPath()
        {
            return frameImgPath;
        }

        public string getIconPath()
        {
            return iconPath;
        }

        public sbyte getGender()
        {
            return gender;
        }

        public long getExpire()
        {
            return expire;
        }



        public bool isCanTrade()
        {
            return canTrade;
        }

        public sbyte getNClass()
        {
            return NClass;
        }

        public int getIconId()
        {
            return iconId;
        }

        public sbyte getElement()
        {
            return element;
        }

        public int getTypeTier()
        {
            return typeTier;
        }



        public string getNameViaType()
        {
            switch (type)
            {
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

        public string getDescriptionViaType()
        {
            switch (type)
            {
                case GopetManager.ITEM_PART_PET:
                    {
                        if (optionValue.Length >= 2)
                        {
                            int petId = optionValue[0];
                            int count = optionValue[1];
                            PetTemplate petTemplate = GopetManager.PETTEMPLATE_HASH_MAP.get(petId);
                            if (petTemplate != null)
                            {
                                return Utilities.Format("Dùng %s mảnh sẽ đổi được pet %s", count, petTemplate.getName());
                            }
                        }
                        return description;
                    }
                case GopetManager.ITEM_PART_ITEM:
                    {
                        if (optionValue.Length >= 2)
                        {
                            int itemidTemp = optionValue[0];
                            int count = optionValue[1];
                            ItemTemplate itemTemplate = GopetManager.itemTemplate.get(itemidTemp);
                            if (itemTemplate != null)
                            {
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
}