
using Gopet.Util;

namespace Gopet.Data.GopetItem
{
    public class ItemTemplate
    {

        public int itemId;
        public string name;
        public string description;
        public int requireStr;
        public int requireAgi;
        public int requireInt;
        public int type;
       
        public int[] option = new int[0];
       
        public int[] optionValue = new int[0];
       
        public int[] hpRange;
       
        public int[] mpRange;
       
        public int[] atkRange;
       
        public int[] defRange;
        public bool isStackable;
        public string frameImgPath;
        public string iconPath;
        public sbyte gender;
        public long expire;
        public bool isOnSky;
        public bool canTrade;
        public sbyte petNClass;
        public int iconId;
        public sbyte element;

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
            this.petNClass = NClass;
        }

        public void setIconId(int iconId)
        {
            this.iconId = iconId;
        }

        public void setElement(sbyte element)
        {
            this.element = element;
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
            return petNClass;
        }

        public int getIconId()
        {
            return iconId;
        }

        public sbyte getElement()
        {
            return element;
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
                //return Utilities.Format("%s chỉ số (%s(atk) %s(def) %s(hp) %s(mp)) Yêu cầu (%s(str) %s(int) %s(agi))", name, atk, def, hp, mp, requireStr, requireInt, requireAgi);
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


        public string getRange(string icon, int[] range)
        {
            if (range == null) return "";

            if (range.Length == 0) return string.Empty;

            if (range.Length == 1) return range[0].ToString() + " (atk) ";

            return string.Format("[{0}({2})-{1}({2})]", range[0], range[1], icon);
        }

        internal string getAtk()
        {
            return getRange("atk", this.atkRange);
        }

        internal object getDef()
        {
            return getRange("def", this.defRange);
        }

        internal object getHp()
        {
            return getRange("hp", this.hpRange);
        }

        internal object getMp()
        {
            return getRange("mp", this.mpRange);
        }
    }
}