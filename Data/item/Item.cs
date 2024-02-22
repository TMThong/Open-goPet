using Gopet.Data.Collections;
using Gopet.Util;
using Newtonsoft.Json;

namespace Gopet.Data.GopetItem
{
    public class Item
    {

        public int itemId;

        public int itemTemplateId = -1;

        public string itemUID = Utilities.GetUID();

        public int count = 0;

        public int petEuipId = -1;

        public int lvl = 0;

        public int[] option = new int[0];

        public int[] optionValue = new int[0];

        public long expire = -1;

        public int def;

        public int atk;

        public int hp;

        public int mp;

        public float[] gemOptionValue = null;

        public ItemGem gemInfo = null;

        public bool wasSell = false;
        public bool canTrade { get; set; } = true;


        public Item(int itemTemplateId, int count_ = 0, bool isSkipRandom = false)
        {
            this.count = count_;
            this.itemTemplateId = itemTemplateId;
            option = getTemp().getOption();
            optionValue = getTemp().getOptionValue();
            ItemTemplate itemTemplate = getTemp();
            if (!isSkipRandom)
            {
                atk = RandomInfoItem(itemTemplate.atkRange);
                def = RandomInfoItem(itemTemplate.defRange);
                hp = RandomInfoItem(itemTemplate.hpRange);
                mp = RandomInfoItem(itemTemplate.mpRange);
            }
        }

        public static int RandomInfoItem(int[] range)
        {
            if (range != null)
            {
                if (range.Length > 0)
                {
                    if (range.Length >= 2)
                    {
                        return Utilities.nextInt(range[0], range[1]);
                    }
                    return range[0];
                }
            }
            return 0;
        }

        public static int GetMaxOption(int[] range)
        {
            if (range != null)
            {
                if (range.Length > 0)
                {
                    if (range.Length >= 2)
                    {
                        return range[1];
                    }
                    return range[0];
                }
            }
            return 0;
        }

        public ItemTemplate getTemp()
        {
            return GopetManager.itemTemplate.get(itemTemplateId);
        }

        public ItemTemplate Template
        {
            get
            {
                return GopetManager.itemTemplate.get(itemTemplateId);
            }
        }

        public string getDescription(Player player)
        {
            ItemTemplate itemTemplate = getTemp();
            switch (itemTemplate.getType())
            {
                case GopetManager.SKIN_ITEM:
                    return Utilities.Format("+%s (atk) +%s (def) +%s (hp) +%s (mp)", atk, def, hp, mp) + " (HSD: " + Utilities.ToDateString(Utilities.GetDate(expire)) + " )";
                case GopetManager.WING_ITEM:
                    string strExpire = "";
                    if (expire > 0)
                    {
                        strExpire = " (HSD: " + Utilities.GetFormatNumber(expire) + " )";
                    }
                    else
                    {
                        strExpire = "Hạn sử dụng đến : vĩnh viễn .";
                    }
                    return strExpire + " Chỉ số áp dụng" + Utilities.Format(" +%s (atk) +%s (def) +%s (hp) +%s (mp)", getAtk(), getDef(), getHp(), getMp());
            }

            if (expire > 0)
            {
                return getTemp().getDescription() + " (HSD: " + Utilities.GetFormatNumber(expire) + " )";
            }
            return getTemp().getDescription();
        }

        public int getDef()
        {
            int value = def;
            if (value == 0) return 0;
            if (Template.type == GopetManager.WING_ITEM)
            {
                return value + Utilities.round(Utilities.GetValueFromPercent(value, lvl * GopetManager.PERCENT_ADD_WHEN_ENCHANT_WING));
            }
            int info = value + (int)((value * 4 + 50f) / 100 * 5 / 2) * lvl;
            return info + Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_DEF)));
        }


        public int getAtk()
        {
            int value = atk;
            if (value == 0) return 0;
            if (Template.type == GopetManager.WING_ITEM)
            {
                return value + Utilities.round(Utilities.GetValueFromPercent(value, lvl * GopetManager.PERCENT_ADD_WHEN_ENCHANT_WING));
            }
            int info = value + (int)((value * 4 + 50f) / 100 * 5 / 2) * lvl;
            return info + Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_ATK)));
        }


        public int getHp()
        {
            int value = hp;
            if (value == 0) return 0;
            if (Template.type == GopetManager.WING_ITEM)
            {
                return value + Utilities.round(Utilities.GetValueFromPercent(value, lvl * GopetManager.PERCENT_ADD_WHEN_ENCHANT_WING));
            }
            int info = value + (int)((value * 4 + 50f) / 100 * 5 / 2) * lvl;
            return info + Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_HP)));
        }


        public int getMp()
        {
            int value = mp;
            if (value == 0) return 0;
            if (Template.type == GopetManager.WING_ITEM)
            {
                return value + Utilities.round(Utilities.GetValueFromPercent(value, lvl * GopetManager.PERCENT_ADD_WHEN_ENCHANT_WING));
            }
            int info = value + (int)((value * 4 + 50f) / 100 * 5 / 2) * lvl;
            return info + Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_MP)));
        }


        public static CopyOnWriteArrayList<Item> search(int type, CopyOnWriteArrayList<Item> listNeedSearchItems)
        {
            CopyOnWriteArrayList<Item> arrayList = new();
            foreach (Item item in listNeedSearchItems)
            {
                if (item.getTemp().getType() == type)
                {
                    arrayList.add(item);
                }
            }
            return arrayList;
        }

        public static CopyOnWriteArrayList<Item> search(JArrayList<int> types, CopyOnWriteArrayList<Item> listNeedSearchItems)
        {

            if(types == null) return listNeedSearchItems;

            CopyOnWriteArrayList<Item> arrayList = new();
            foreach (Item item in listNeedSearchItems)
            {
                if (types.Contains(item.getTemp().getType()))
                {
                    arrayList.add(item);
                }
            }
            return arrayList;
        }

        public string getName()
        {
            if (getTemp().isStackable)
            {
                return getTemp().getName() + " x" + count + (canTrade ? "" : " (Khóa)");
            }
            if (Template.type == GopetManager.WING_INVENTORY)
            {
                return getTemp().getName() + " cấp " + lvl + (canTrade ? "" : " (Khóa)");
            }
            return getTemp().getName() + (canTrade ? "" : " (Khóa)");
        }

        public string getEquipName()
        {
            JArrayList<string> infoStrings = new();
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

            switch (getTemp().getType())
            {
                case GopetManager.ITEM_GEM:
                    {
                        if (gemOptionValue == null)
                        {
                            updateGemOption();
                        }
                        for (int i = 0; i < option.Length; i++)
                        {
                            int j = option[i];
                            float info = gemOptionValue[i];
                            switch (j)
                            {
                                case ItemInfo.OptionType.PERCENT_HP:
                                    infoStrings.add(Utilities.Format("Tăng %s/ ", info).Replace('/', '%') + " (hp) ");
                                    break;
                                case ItemInfo.OptionType.PERCENT_MP:
                                    infoStrings.add(Utilities.Format("Tăng %s/ ", info).Replace('/', '%') + " (mp) ");
                                    break;
                                case ItemInfo.OptionType.PERCENT_ATK:
                                    infoStrings.add(Utilities.Format("Tăng %s/ ", info).Replace('/', '%') + " (atk) ");
                                    break;
                                case ItemInfo.OptionType.PERCENT_DEF:
                                    infoStrings.add(Utilities.Format("Tăng %s/ ", info).Replace('/', '%') + " (def) ");
                                    break;
                            }
                        }
                    }
                    break;
            }

            if (getTemp().getNClass() >= 0)
            {
                if (!getTemp().isOnSky)
                {
                    switch (getTemp().getNClass())
                    {
                        case GopetManager.Fighter:
                            infoStrings.add(" Dành cho chiến binh");
                            break;
                        case GopetManager.Assassin:
                            infoStrings.add(" Dành cho sát thủ");
                            break;
                        case GopetManager.Wizard:
                            infoStrings.add(" Dành cho pháp sư");
                            break;
                    }
                }
            }
            return getName() + "  " + getTemp().description + " " + Utilities.Format("up: %s ", lvl) + string.Join(" ", infoStrings) + (gemInfo == null ? "" : " " + gemInfo.getElementIcon());
        }

        public void updateGemOption()
        {
            if (gemInfo != null)
            {
                gemOptionValue = new float[gemInfo.getOption().Length];
                for (int i = 0; i < gemOptionValue.Length; i++)
                {
                    gemOptionValue[i] = gemInfo.getOptionValue()[i] / 100f + (gemInfo.getOptionValue()[i] / 100f * 4 + 80) / 100 * 4 / 2 * lvl;
                }
            }
            else if (getTemp().getType() == GopetManager.ITEM_GEM)
            {
                gemOptionValue = new float[option.Length];
                for (int i = 0; i < option.Length; i++)
                {
                    gemOptionValue[i] = optionValue[i] / 100f + (optionValue[i] / 100f * 2 + 80) / 100 * 4 / 2 * lvl;
                }
            }
            else
            {
                gemOptionValue = null;
            }
        }

        private float getPercentGemBuff(int idoption)
        {
            if (gemInfo != null)
            {
                if (gemInfo.getOption() != null)
                    for (int i = 0; i < gemInfo.getOption().Length; i++)
                    {
                        int j = gemInfo.getOption()[i];
                        if (j == idoption)
                        {
                            return gemOptionValue[i];
                        }
                    }
            }
            return 0;
        }
    }
}