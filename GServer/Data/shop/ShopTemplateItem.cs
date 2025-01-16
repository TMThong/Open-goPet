
using Gopet.Data.GopetItem;
using Gopet.Util;

public class ShopTemplateItem
{

    public sbyte shopId;
    public int itemTemTempleId;
    public int count;
    public sbyte[] moneyType;
    public int[] price;
    public bool isSpceial = false;
    public String nameSpeceial, descriptionSpeceial;
    public bool needRemove = false;
    public int spceialType = -1;
    public bool CloseScreenAfterClick = false;
    public int clanLvl;
    public int perCount = 0;
    public const int TYPE_RESET_SHOP_ARENA = 1;
    public bool hasId = false;
    public int menuId;
    public int petId;
    public bool isSellItem = true;
    public bool isLock = false;
    public TimeSpan? TimeNeedReset { get; set; } = null;
    public int NeedFund = 0;
    public void setShopId(sbyte shopId)
    {
        this.shopId = shopId;
    }

    public void setItemTempalteId(int itemTempalteId)
    {
        this.itemTemTempleId = itemTempalteId;
    }

    public void setCount(int count)
    {
        this.count = count;
    }

    public void setMoneyType(sbyte[] moneyType)
    {
        this.moneyType = moneyType;
    }

    public void setPrice(int[] price)
    {
        this.price = price;
    }




    public void setNameSpeceial(String nameSpeceial)
    {
        this.nameSpeceial = nameSpeceial;
    }

    public void setDescriptionSpeceial(String descriptionSpeceial)
    {
        this.descriptionSpeceial = descriptionSpeceial;
    }

    public void setNeedRemove(bool needRemove)
    {
        this.needRemove = needRemove;
    }

    public void setSpceialType(int spceialType)
    {
        this.spceialType = spceialType;
    }

    public void setCloseScreenAfterClick(bool CloseScreenAfterClick)
    {
        this.CloseScreenAfterClick = CloseScreenAfterClick;
    }

    public void setClanLvl(int clanLvl)
    {
        this.clanLvl = clanLvl;
    }

    public void setPerCount(int perCount)
    {
        this.perCount = perCount;
    }

    public void setHasId(bool hasId)
    {
        this.hasId = hasId;
    }

    public void setMenuId(int menuId)
    {
        this.menuId = menuId;
    }

    public void setPetId(int petId)
    {
        this.petId = petId;
    }



    public int getShopId()
    {
        return this.shopId;
    }

    public int getItemTempalteId()
    {
        return this.itemTemTempleId;
    }

    public int getCount()
    {
        return this.count;
    }

    public sbyte[] getMoneyType()
    {
        return this.moneyType;
    }

    public int[] getPrice()
    {
        return this.price;
    }

    public String getNameSpeceial()
    {
        return this.nameSpeceial;
    }

    public String getDescriptionSpeceial()
    {
        return this.descriptionSpeceial;
    }

    public bool isNeedRemove()
    {
        return this.needRemove;
    }

    public int getSpceialType()
    {
        return this.spceialType;
    }

    public bool isCloseScreenAfterClick()
    {
        return this.CloseScreenAfterClick;
    }

    public int getClanLvl()
    {
        return this.clanLvl;
    }

    public int getPerCount()
    {
        return this.perCount;
    }

    public bool isHasId()
    {
        return this.hasId;
    }

    public int getMenuId()
    {
        return this.menuId;
    }

    public int getPetId()
    {
        return this.petId;
    }




    public ItemTemplate getItemTemplate()
    {
        return GopetManager.itemTemplate.get(itemTemTempleId);
    }

    public PetTemplate getPetTemplate()
    {
        return GopetManager.PETTEMPLATE_HASH_MAP.get(petId);
    }

    public String getIconPath()
    {
        if (isSellItem)
        {
            return getItemTemplate().getIconPath();
        }

        return getPetTemplate().icon;
    }

    public String getDesc(Player player)
    {
        if (isSpceial)
        {
            return descriptionSpeceial;
        }

        if (!isSellItem)
        {
            PetTemplate petTemplate = getPetTemplate();
            if (petTemplate == null)
            {
                throw new NullReferenceException("pet null");
            }
            return Utilities.Format($"Hệ: {GopetManager.GetElementDisplay(petTemplate.element, petTemplate.nclass, player)}. Mô tả:   " + " + %s (str) , + %s (agi) , + %s (int) , + %s (hp) , + %s (mp)", petTemplate.str, petTemplate.agi, petTemplate._int, petTemplate.getHp(), petTemplate.getMp());
        }

        ItemTemplate itemTemplate = getItemTemplate();

        if (itemTemplate.getType() == GopetManager.PET_EQUIP_ARMOUR || itemTemplate.getType() == GopetManager.PET_EQUIP_GLOVE || itemTemplate.getType() == GopetManager.PET_EQUIP_HAT || itemTemplate.getType() == GopetManager.PET_EQUIP_SHOE || itemTemplate.getType() == GopetManager.PET_EQUIP_WEAPON)
        {
            return itemTemplate.getDescription(player) + Utilities.Format("( %s ,  %s,  %s ,  %s )", itemTemplate.getAtk(), itemTemplate.getDef(), itemTemplate.getHp(), itemTemplate.getMp());
        }

        if (itemTemplate.getType() == GopetManager.SKIN_ITEM)
        {
            return Utilities.Format("+%s  +%s +%s  +%s ", itemTemplate.getAtk(), itemTemplate.getDef(), itemTemplate.getHp(), itemTemplate.getMp());
        }
        return itemTemplate.getDescription(player);
    }

    public String getName(Player player)
    {
        if (isSpceial)
        {
            return nameSpeceial;
        }

        if (!isSellItem)
        {
            return getPetTemplate().getName(player);
        }
        ItemTemplate itemTemplate = getItemTemplate();

        if (itemTemplate.getType() == GopetManager.PET_EQUIP_ARMOUR || itemTemplate.getType() == GopetManager.PET_EQUIP_GLOVE || itemTemplate.getType() == GopetManager.PET_EQUIP_HAT || itemTemplate.getType() == GopetManager.PET_EQUIP_SHOE || itemTemplate.getType() == GopetManager.PET_EQUIP_WEAPON)
        {
            return itemTemplate.getName(player) + Utilities.Format("(" + player.Language.Request + "   %s (str) ,  %s (agi) ,  %s (int))", itemTemplate.getRequireStr(), itemTemplate.getRequireAgi(), itemTemplate.getRequireInt());
        }

        if (count > 1 && shopId != MenuController.SHOP_CLAN)
        {
            return itemTemplate.getName(player) + "  x" + count;
        }
        else if (count > 1 && shopId == MenuController.SHOP_CLAN)
        {
            return itemTemplate.getName(player) + " còn x" + (count - perCount);
        }
        return itemTemplate.getName(player);
    }

    public void execute(Player player)
    {
        switch (spceialType)
        {
            case TYPE_RESET_SHOP_ARENA:
                {
                    if (player.playerData.shopArena != null)
                    {
                        if (player.playerData.shopArena.getNumReset() - 1 >= GopetManager.MAX_RESET_SHOP_ARENA)
                        {
                            player.redDialog("Reset đạt số lần tối đa trong hôm nay");
                            return;
                        }
                        player.playerData.shopArena.nextArena();
                        MenuController.sendMenu(MenuController.SHOP_ARENA, player);
                        player.okDialog("Reset thành công");
                    }
                }
                break;
            default:
                GopetManager.ServerMonitor.LogError($"Không có type đặc biệt là: {spceialType}");
                break;
        }

    }

    public void setSpceial(bool v)
    {
        this.isSpceial = v;
    }


    public ShopTemplateItem Clone()
    {
        ShopTemplateItem item = new ShopTemplateItem();
        item.shopId = shopId;
        item.itemTemTempleId = itemTemTempleId;
        item.count = count;
        item.moneyType = moneyType.ToArray();
        item.price = price.ToArray();
        item.isSpceial = isSpceial;
        item.nameSpeceial = nameSpeceial;
        item.descriptionSpeceial = descriptionSpeceial;
        item.needRemove = needRemove;
        item.spceialType = spceialType;
        item.CloseScreenAfterClick = CloseScreenAfterClick;
        item.clanLvl = clanLvl;
        item.perCount = perCount;
        item.hasId = hasId;
        item.menuId = menuId;
        item.petId = petId;
        item.isSellItem = isSellItem;
        item.isLock = isLock;
        item.TimeNeedReset = TimeNeedReset;
        item.NeedFund = NeedFund;
        return item;
    }
}
