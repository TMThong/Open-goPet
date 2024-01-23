 
public class ShopArena : ShopTemplate {

    private long timeGem = Utilities.CurrentTimeMillis;
    public int numResetFree = GopetManager.DEFAULT_FREE_RESET_ARENA_SHOP;
    private int numReset = 0;

    public ShopArena(sbyte type)  : base(type)
    {
         
    }

    public   void nextWhenNewDay() {
        DateTime timeServerDateTime = new DateTime(Utilities.CurrentTimeMillis);
        DateTime timeGenDateTime = new DateTime(timeGem);
        if ((timeGenDateTime.Day != timeServerDateTime.Day) || (timeGenDateTime.Month != timeServerDateTime.Month) || (timeGenDateTime.Year != timeServerDateTime.Year)) {
            numResetFree = GopetManager.DEFAULT_FREE_RESET_ARENA_SHOP;
            numReset = 0;
            nextArena();
        }
    }

    public void nextArena() {
        this.timeGem = Utilities.CurrentTimeMillis;
        this.shopTemplateItems.Clear();
        int numSlot = GopetManager.MAX_SLOT_SHOP_ARENA + 1;
        int priceReset = (int) (numResetFree > 0 ? 0 : Math.round( Math.pow(GopetManager.PRICE_RESET_SHOP_ARENA, numReset - GopetManager.DEFAULT_FREE_RESET_ARENA_SHOP + 2)));
        ShopTemplateItem resetShopArenaItem = new ShopTemplateItem();
        resetShopArenaItem.setSpceialType(ShopTemplateItem.TYPE_RESET_SHOP_ARENA);
        resetShopArenaItem.setSpceial(true);
        resetShopArenaItem.setNameSpeceial("Reset vật phẩm shop");
        resetShopArenaItem.setDescriptionSpeceial(Utilities.Format("Dùng %s (vang) để đổi các vật phẩm khác", Utilities.formatNumber(priceReset)));
        resetShopArenaItem.setPrice(new int[]{priceReset});
        resetShopArenaItem.setMoneyType(new sbyte[]{GopetManager.MONEY_TYPE_GOLD});
        this.shopTemplateItems.add(resetShopArenaItem);
        long timeGen = Utilities.CurrentTimeMillis + 10000;
        while (timeGen > Utilities.CurrentTimeMillis) {
            if (this.shopTemplateItems.Count < numSlot) {
                ShopArenaTemplate shopArenaTemplate = GopetManager.SHOP_ARENA_TEMPLATE.get(Utilities.nextInt(GopetManager.SHOP_ARENA_TEMPLATE.Count));
                if (shopArenaTemplate.getPercent() > Utilities.nextFloatPer()) {
                    this.shopTemplateItems.add(shopArenaTemplate.next());
                }
            } else {
                break;
            }
        }
        this.numReset++;
    }
}
