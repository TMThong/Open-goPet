

using Gopet.Data.Collections;

public class Item : DataVersion {
    
    public int itemId;
     
    public int itemTemplateId = -1;
   
    public String itemUID = Utilities.getUID();
    
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

    public Item(int itemTemplateId) {
        this.itemTemplateId = itemTemplateId;
        setHp(getTemp().getHp());
        setMp(getTemp().getMp());
        setAtk(getTemp().getAtk());
        setDef(getTemp().getDef());
        option = getTemp().getOption();
        optionValue = getTemp().getOptionValue();
        ItemTemplate itemTemplate = getTemp();
    }

    public ItemTemplate getTemp() {
        return GopetManager.itemTemplate.get(itemTemplateId);
    }

    public String getDescription(Player player) {
        ItemTemplate itemTemplate = getTemp();
        switch (itemTemplate.getType()) {
            case GopetManager.SKIN_ITEM:

               

                return Utilities.Format("+%s (atk) +%s (def) +%s (hp) +%s (mp)", itemTemplate.getAtk(), itemTemplate.getDef(), itemTemplate.getHp(), itemTemplate.getMp()) + " (HSD: " + Utilities.dateFormatVI.format(expire) + " )";
            case GopetManager.WING_ITEM:
                String strExpire = "";
                if (expire > 0) {
                    strExpire = " (HSD: " + Utilities.dateFormatVI.format(expire) + " )";
                } else {
                    strExpire = "Hạn sử dụng đến : vĩnh viễn .";
                }
                return strExpire + " Chỉ số áp dụng" + Utilities.Format(" +%s (atk) +%s (def) +%s (hp) +%s (mp)", getAtk(), getDef(), getHp(), getMp());
        }

        if (expire > 0) {
            return getTemp().getDescription() + " (HSD: " + Utilities.dateFormatVI.format(expire) + " )";
        }
        return getTemp().getDescription();
    }

    public int getDef() {
        int value = def;
        int info = value + Utilities.round((((value * 4 + 50) / 100) * 5) / 2) * lvl;
        return info + Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_DEF)));
    }

    public void setDef(int def) {
        this.def = def;
    }

    public int getAtk() {
        int value = atk;
        int info = value + Utilities.round((((value * 4 + 50) / 100) * 5) / 2) * lvl;
        return info + Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_ATK)));
    }

    public void setAtk(int atk) {
        this.atk = atk;
    }

    public int getHp() {
        int value = hp;
        int info = value + Utilities.round((((value * 4 + 50) / 100) * 5) / 2) * lvl;
        return info + Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_HP)));
    }

    public void setHp(int hp) {
        this.hp = hp;
    }

    public int getMp() {
        int value = mp;
        int info = value + Utilities.round((((value * 4 + 50) / 100) * 5) / 2) * lvl;
        return info + Utilities.round(Utilities.GetValueFromPercent(info, getPercentGemBuff(ItemInfo.OptionType.PERCENT_MP)));
    }

    public void setMp(int mp) {
        this.mp = mp;
    }

    public static CopyOnWriteArrayList<Item> search(int type, CopyOnWriteArrayList<Item> listNeedSearchItems) {
        CopyOnWriteArrayList<Item> arrayList =new ();
        for (Item item : listNeedSearchItems) {
            if (item.getTemp().getType() == type) {
                arrayList.add(item);
            }
        }
        return arrayList;
    }

    public static CopyOnWriteArrayList<Item> search(ArrayList<int> types, CopyOnWriteArrayList<Item> listNeedSearchItems) {
        CopyOnWriteArrayList<Item> arrayList =new ();
        for (Item item : listNeedSearchItems) {
            if (types.Contains(item.getTemp().getType())) {
                arrayList.add(item);
            }
        }
        return arrayList;
    }

    public String getName() {
        if (getTemp().isStackable) {
            return getTemp().getName() + " x" + count;
        }
        return getTemp().getName();
    }

    public String getEquipName()   {
        ArrayList<String> infoStrings = new();
        if (getAtk() > 0) {
            infoStrings.add(getAtk() + " (atk) ");
        }
        if (getDef() > 0) {
            infoStrings.add(getDef() + " (def) ");
        }
        if (getHp() > 0) {
            infoStrings.add(getHp() + " (hp) ");
        }
        if (getMp() > 0) {
            infoStrings.add(getMp() + " (mp) ");
        }

        switch (getTemp().getType()) {
            case GopetManager.ITEM_GEM: {
                if (gemOptionValue == null) {
                    updateGemOption();
                }
                for (int i = 0; i < option.Length; i++) {
                    int j = option[i];
                    float info = gemOptionValue[i];
                    switch (j) {
                        case ItemInfo.OptionType.PERCENT_HP:
                            infoStrings.add(Utilities.Format("Tăng %s/ ", info).replace('/', '%') + " (hp) ");
                            break;
                        case ItemInfo.OptionType.PERCENT_MP:
                            infoStrings.add(Utilities.Format("Tăng %s/ ", info).replace('/', '%') + " (mp) ");
                            break;
                        case ItemInfo.OptionType.PERCENT_ATK:
                            infoStrings.add(Utilities.Format("Tăng %s/ ", info).replace('/', '%') + " (atk) ");
                            break;
                        case ItemInfo.OptionType.PERCENT_DEF:
                            infoStrings.add(Utilities.Format("Tăng %s/ ", info).replace('/', '%') + " (def) ");
                            break;
                    }
                }
            }
            break;
        }

        if (getTemp().getNClass() >= 0) {
            if (!getTemp().isOnSky()) {
                switch (getTemp().getNClass()) {
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
        return getTemp().getName() + "  " + Utilities.Format("up: %s ", lvl) + String.Join(" ", infoStrings) + (this.gemInfo == null ? "" : " " + this.gemInfo.getElementIcon());
    }

    public void updateGemOption()   {
        if (this.gemInfo != null) {
            this.gemOptionValue = new float[this.gemInfo.getOption().Length];
            for (int i = 0; i < gemOptionValue.Length; i++) {
                this.gemOptionValue[i] = this.gemInfo.getOptionValue()[i] / 100f + (((((this.gemInfo.getOptionValue()[i] / 100f) * 4 + 80) / 100) * 4) / 2) * lvl;
            }
        } else if (getTemp().getType() == GopetManager.ITEM_GEM) {
            this.gemOptionValue = new float[this.option.Length];
            for (int i = 0; i < option.Length; i++) {
                this.gemOptionValue[i] = optionValue[i] / 100f + (((((optionValue[i] / 100f) * 2 + 80) / 100) * 4) / 2) * lvl;
            }
        } else {
            this.gemOptionValue = null;
        }
    }

    private float getPercentGemBuff(int idoption) {
        float percent = 0;
        if (this.gemInfo != null) {
            for (int i = 0; i < this.gemInfo.getOption().Length; i++) {
                int j = this.gemInfo.getOption()[i];
                if (j == idoption) {
                    return this.gemOptionValue[i];
                }
            }
        }
        return percent;
    }
}
