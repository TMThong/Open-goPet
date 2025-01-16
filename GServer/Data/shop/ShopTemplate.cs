
using Gopet.Data.Collections;
using System.Xml.Linq;

public class ShopTemplate  {

    public JArrayList<ShopTemplateItem> shopTemplateItems = new();
    private sbyte type;

    public ShopTemplate(sbyte type) {
        this.type = type;
        
    }

    public void nextArena() {

    }

    public JArrayList<ShopTemplateItem> getShopTemplateItems()
    {
        return this.shopTemplateItems;
    }

    public string getName(Player player)
    {
        switch (type)
        {
            case MenuController.SHOP_ARMOUR:
                return player.Language.SHOP_ARMOUR;
            case MenuController.SHOP_SKIN:
                return player.Language.SHOP_SKIN;
            case MenuController.SHOP_HAT:
                return player.Language.SHOP_HAT;
            case MenuController.SHOP_WEAPON:
                return player.Language.SHOP_WEAPON;
            case MenuController.SHOP_THUONG_NHAN:
                return player.Language.SHOP_THUONG_NHAN;
            case MenuController.SHOP_PET:
                return player.Language.SHOP_PET;
            case MenuController.SHOP_FOOD:
                return player.Language.SHOP_FOOD;
            case MenuController.SHOP_ARENA:
                nextArena();
                return player.Language.SHOP_ARENA;
            case MenuController.SHOP_CLAN:
                return player.Language.SHOP_CLAN;
            case MenuController.SHOP_ENERGY:
                return player.Language.SHOP_ENERGY;
            case MenuController.SHOP_GIAN_THUONG:
                return player.Language.SHOP_GIAN_THUONG;
            case MenuController.SHOP_BIRTHDAY_EVENT:
                return player.Language.SHOP_BIRTHDAY_EVENT;
            default:
                return "Error shop type " + type;
        }
    }
}
