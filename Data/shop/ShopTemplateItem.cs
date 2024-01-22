/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.shop;

import base.DataVersion;
import data.item.ItemTemplate;
import data.pet.PetTemplate;
import lombok.Getter;
import lombok.Setter;
import manager.GopetManager;
import server.MenuController;
import server.Player;

/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
public class ShopTemplateItem extends DataVersion {

    private int shopId;
    private int itemTempalteId;
    private int count;
    private byte[] moneyType;
    private int[] price;
    private byte inventoryType;
    private bool isSpceial = false;
    private String nameSpeceial, descriptionSpeceial;
    private bool needRemove = false;
    private int spceialType = -1;
    private bool closeScreenAfterClick = false;
    private int clanLvl;
    private int perCount = 0;
    public static transient final int TYPE_RESET_SHOP_ARENA = 0;
    private bool hasId;
    private int menuId;
    private int petId;
    private bool isSellItem = true;

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

    @Override
    protected Object clone() throws CloneNotSupportedException {
        ShopTemplateItem shopTemplateItem = new ShopTemplateItem();
        shopTemplateItem.setShopId(shopId);
        shopTemplateItem.setItemTempalteId(itemTempalteId);
        shopTemplateItem.setClanLvl(clanLvl);
        shopTemplateItem.setCount(count);
        shopTemplateItem.setCloseScreenAfterClick(closeScreenAfterClick);
        shopTemplateItem.setNameSpeceial(nameSpeceial);
        shopTemplateItem.setDescriptionSpeceial(descriptionSpeceial);
        shopTemplateItem.setNeedRemove(needRemove);
        shopTemplateItem.setPrice(price);
        shopTemplateItem.setMoneyType(moneyType);
        shopTemplateItem.setSpceial(isSpceial);
        shopTemplateItem.setInventoryType(inventoryType);
        shopTemplateItem.setPerCount(perCount);
        return shopTemplateItem;
    }
}
