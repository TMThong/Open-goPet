
using Gopet.Util;

public class ShopArena : ShopTemplate
{

    public DateTime timeGem = DateTime.Now;
    public int numReset = 0;

    public ShopArena() : base(MenuController.SHOP_ARENA)
    {

    }

    public void nextWhenNewDay()
    {
        DateTime timeServerDateTime = Utilities.GetCurrentDate();
        DateTime timeGenDateTime =  timeGem;
        if ((timeGenDateTime.Day != timeServerDateTime.Day) || (timeGenDateTime.Month != timeServerDateTime.Month) || (timeGenDateTime.Year != timeServerDateTime.Year))
        {
            numReset = 0;
            nextArena();
        }
    }

    public void nextArena()
    {
        this.timeGem = DateTime.Now;
        this.shopTemplateItems.Clear();
        int numSlot = GopetManager.MAX_SLOT_SHOP_ARENA + 1;
        int priceReset = Utilities.round(GopetManager.PRICE_RESET_SHOP_ARENA * (numReset + 1));
        ShopTemplateItem resetShopArenaItem = new ShopTemplateItem();
        resetShopArenaItem.setSpceialType(ShopTemplateItem.TYPE_RESET_SHOP_ARENA);
        resetShopArenaItem.setSpceial(true);
        resetShopArenaItem.setNameSpeceial("Reset vật phẩm shop");
        resetShopArenaItem.setDescriptionSpeceial(Utilities.Format("Dùng %s (vang) để đổi các vật phẩm khác", Utilities.FormatNumber(priceReset)));
        resetShopArenaItem.setPrice(new int[] { priceReset });
        resetShopArenaItem.setMoneyType(new sbyte[] { GopetManager.MONEY_TYPE_GOLD });
        if(!(numReset  >= GopetManager.MAX_RESET_SHOP_ARENA)) this.shopTemplateItems.add(resetShopArenaItem);
        long timeGen = Utilities.CurrentTimeMillis + 10000;
        while (timeGen > Utilities.CurrentTimeMillis)
        {
            if (this.shopTemplateItems.Count < numSlot)
            {
                ShopArenaTemplate shopArenaTemplate = Utilities.RandomArray(GopetManager.SHOP_ARENA_TEMPLATE);
                if (shopArenaTemplate.percent > Utilities.NextFloatPer() && !this.shopTemplateItems.Any(x => x.itemTemTempleId == shopArenaTemplate.itemTemTempleId))
                {
                    this.shopTemplateItems.add(shopArenaTemplate.ToShopTemplateItem(MenuController.SHOP_ARENA));
                }
            }
            else
            {
                break;
            }
        }
        this.numReset++;
    }

    public int getNumReset()
    {
        return this.numReset;
    }
}
