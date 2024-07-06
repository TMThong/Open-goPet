
using Gopet.Util;

namespace Gopet.Data.GopetItem
{
    public class ItemTemplate
    {
        public int itemId { get; private set; }
        public string name { get; private set; }
        public string description { get; private set; }
        public int requireStr { get; private set; }
        public int requireAgi { get; private set; }
        public int requireInt { get; private set; }
        public int type { get; private set; }

        public int[] itemOption { get; private set; } = new int[0];

        public int[] itemOptionValue { get; private set; } = new int[0];

        public int[] hpRange { get; private set; }

        public int[] mpRange { get; private set; }
        public int[] atkRange { get; private set; }

        public int[] defRange { get; private set; }
        public bool isStackable { get; private set; }
        public string frameImgPath { get; private set; }
        public string iconPath { get; private set; }
        public sbyte gender { get; private set; }
        public long expire { get; private set; }
        public bool isOnSky { get; private set; }
        public bool canTrade { get; private set; }
        public sbyte petNClass { get; private set; }
        public int iconId { get; private set; }
        public sbyte element { get; private set; }

        public void setIconId(int iconId)
        {
            this.iconId = iconId;
        }





        public int getItemId()
        {
            return itemId;
        }

        public string getName(Player player)
        {
            return player.Language.ItemLanguage[itemId];
        }

        public string getDescription(Player player)
        {
            return player.Language.ItemDescLanguage[itemId];
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
            return itemOption;
        }

        public int[] getOptionValue()
        {
            return itemOptionValue;
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



        public string getNameViaType(Player player)
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
                    return getName(player);
            }
        }

        public string getDescriptionViaType(Player player)
        {
            switch (type)
            {
                case GopetManager.ITEM_PART_PET:
                    {
                        if (itemOptionValue.Length >= 2)
                        {
                            int petId = itemOptionValue[0];
                            int count = itemOptionValue[1];
                            PetTemplate petTemplate = GopetManager.PETTEMPLATE_HASH_MAP.get(petId);
                            if (petTemplate != null)
                            {
                                return Utilities.Format("Dùng %s mảnh sẽ đổi được pet %s", count, petTemplate.getName(player));
                            }
                        }
                        return getDescription(player);
                    }
                case GopetManager.ITEM_PART_ITEM:
                    {
                        if (itemOptionValue.Length >= 2)
                        {
                            int itemidTemp = itemOptionValue[0];
                            int count = itemOptionValue[1];
                            ItemTemplate itemTemplate = GopetManager.itemTemplate.get(itemidTemp);
                            if (itemTemplate != null)
                            {
                                return Utilities.Format("Dùng %s mảnh sẽ đổi được %s", count, itemTemplate.getName(player));
                            }
                        }
                        return getDescription(player);
                    }
                default:
                    return getDescription(player);
            }
        }


        public string getRange(string icon, int[] range)
        {
            if (range == null) return string.Format("[{0} ({2}) -{1} ({2}) ]", 0, 0, icon);

            if (range.Length == 0) return string.Empty;

            if (range.Length == 1) return range[0].ToString() + " (" + icon + ") ";

            return string.Format("[{0} ({2}) -{1} ({2}) ]", range[0], range[1], icon);
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