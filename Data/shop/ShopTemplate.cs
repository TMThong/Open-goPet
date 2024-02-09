
using Gopet.Data.Collections;

public class ShopTemplate  {

    private String name;
    public JArrayList<ShopTemplateItem> shopTemplateItems = new();
    private sbyte type;

    public ShopTemplate(sbyte type) {
        this.type = type;
        switch (type) {
            case MenuController.SHOP_ARMOUR:
                name = "Cửa hàng giáp";
                break;
            case MenuController.SHOP_SKIN:
                name = "Cửa hàng trang phục";
                break;
            case MenuController.SHOP_HAT:
                name = "Cửa hàng nón";
                break;
            case MenuController.SHOP_WEAPON:
                name = "Cửa hàng vũ khí";
                break;
            case MenuController.SHOP_THUONG_NHAN:
                name = "Cửa hàng của thương nhân";
                break;
            case MenuController.SHOP_PET:
                name = "Cửa hàng thú cưng";
                break;
            case MenuController.SHOP_FOOD:
                name = "Cửa hàng thức ăn";
                break;
            case MenuController.SHOP_ARENA:
                name = "Cửa hàng đấu trường";
                nextArena();
                break;
            case MenuController.SHOP_CLAN:
                name = "Cửa hàng bang hội";
                break;
            case MenuController.SHOP_ENERGY:
                name = "Cửa hàng năng lượng";
                break;
            default:
                name = "Error shop type " + type;
                break;
        }
    }

    public void nextArena() {

    }

    public JArrayList<ShopTemplateItem> getShopTemplateItems()
    {
        return this.shopTemplateItems;
    }

    public string getName()
    {
        return this.name;
    }
}
