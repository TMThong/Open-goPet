 
public class ShopTemplateItem : DataVersion {

    public int shopId;
    public int itemTempalteId;
    public int count;
    public sbyte[] moneyType;
    public int[] price;
    public sbyte inventoryType;
    public bool isSpceial = false;
    public String nameSpeceial, descriptionSpeceial;
    public bool needRemove = false;
    public int spceialType = -1;
    public bool closeScreenAfterClick = false;
    public int clanLvl;
    public int perCount = 0;
    public const int TYPE_RESET_SHOP_ARENA = 0;
    public bool hasId;
    public int menuId;
    public int petId;
    public bool isSellItem = true;

    public ItemTemplate getItemTemplate() {
        return GopetManager.itemTemplate.get(itemTempalteId);
    }

    public PetTemplate getPetTemplate() {
        return GopetManager.PETTEMPLATE_HASH_MAP.get(petId);
    }

    public String getIconPath() {
        if (isSellItem) {
            return getItemTemplate().getIconPath();
        }

        return getPetTemplate().getIcon();
    }

    public String getDesc(Player player) {
        if (isSpceial) {
            return descriptionSpeceial;
        }

        if (!isSellItem) {
            PetTemplate petTemplate = getPetTemplate();
            return String.format("( + %s (str) , + %s (agi) , + %s (int) , + %s (hp) , + %s (mp))", petTemplate.getStr(), petTemplate.getAgi(), petTemplate.getInt(), petTemplate.getHp(), petTemplate.getMp());
        }

        ItemTemplate itemTemplate = getItemTemplate();

        if (itemTemplate.getType() == GopetManager.PET_EQUIP_ARMOUR || itemTemplate.getType() == GopetManager.PET_EQUIP_GLOVE || itemTemplate.getType() == GopetManager.PET_EQUIP_HAT || itemTemplate.getType() == GopetManager.PET_EQUIP_SHOE || itemTemplate.getType() == GopetManager.PET_EQUIP_WEAPON) {
            return itemTemplate.getDescription() + String.format("( + %s (atk) , + %s (def) , + %s (hp) , + %s (mp))", itemTemplate.getAtk(), itemTemplate.getDef(), itemTemplate.getHp(), itemTemplate.getMp());
        }

        if (itemTemplate.getType() == GopetManager.SKIN_ITEM) {
            return String.format("+%s (atk) +%s (def) +%s (hp) +%s (mp)", itemTemplate.getAtk(), itemTemplate.getDef(), itemTemplate.getHp(), itemTemplate.getMp());
        }
        return itemTemplate.getDescription();
    }

    public String getName() {
        if (isSpceial) {
            return nameSpeceial;
        }

        if (!isSellItem) {
            return getPetTemplate().getName();
        }
        ItemTemplate itemTemplate = getItemTemplate();

        if (itemTemplate.getType() == GopetManager.PET_EQUIP_ARMOUR || itemTemplate.getType() == GopetManager.PET_EQUIP_GLOVE || itemTemplate.getType() == GopetManager.PET_EQUIP_HAT || itemTemplate.getType() == GopetManager.PET_EQUIP_SHOE || itemTemplate.getType() == GopetManager.PET_EQUIP_WEAPON) {
            return itemTemplate.getName() + String.format("(Yêu cầu   %s (str) ,  %s (agi) ,  %s (int))", itemTemplate.getRequireStr(), itemTemplate.getRequireAgi(), itemTemplate.getRequireInt());
        }

        if (count > 1 && shopId != MenuController.SHOP_CLAN) {
            return itemTemplate.getName() + "  x" + count;
        } else if (count > 1 && shopId == MenuController.SHOP_CLAN) {
            return itemTemplate.getName() + " còn x" + (count - perCount);
        }
        return itemTemplate.getName();
    }

    public void execute(Player player)   {
        switch (spceialType) {
            case TYPE_RESET_SHOP_ARENA: {
                if (player.playerData.shopArena != null) {
                    if (player.playerData.shopArena.getNumReset() + 1 >= GopetManager.MAX_RESET_SHOP_ARENA) {
                        player.redDialog("Reset đạt số lần tối đa trong hôm nay");
                        return;
                    }

                    if (player.playerData.shopArena.numResetFree > 0) {
                        player.playerData.shopArena.numResetFree--;
                    }
                    player.playerData.shopArena.nextArena();
                    MenuController.sendMenu(MenuController.SHOP_ARENA, player);
                    player.okDialog("Reset thành công");
                }
            }
            break;
        }
    }

   
}
