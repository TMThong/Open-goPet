 
public class ShopArena : ShopTemplate {

    private long timeGem = System.currentTimeMillis();
    public int numResetFree = GopetManager.DEFAULT_FREE_RESET_ARENA_SHOP;
    private int numReset = 0;

    public ShopArena(byte type) {
        super(type);
    }

    public final void nextWhenNewDay() {
        Date timeServerDate = new Date(System.currentTimeMillis());
        Date timeGenDate = new Date(timeGem);
        if ((timeGenDate.getDay() != timeServerDate.getDay()) || (timeGenDate.getMonth() != timeServerDate.getMonth()) || (timeGenDate.getYear() != timeServerDate.getYear())) {
            numResetFree = GopetManager.DEFAULT_FREE_RESET_ARENA_SHOP;
            numReset = 0;
            nextArena();
        }
    }

    public void nextArena() {
        this.timeGem = System.currentTimeMillis();
        this.shopTemplateItems.clear();
        int numSlot = GopetManager.MAX_SLOT_SHOP_ARENA + 1;
        int priceReset = (int) (numResetFree > 0 ? 0 : Math.round( Math.pow(GopetManager.PRICE_RESET_SHOP_ARENA, numReset - GopetManager.DEFAULT_FREE_RESET_ARENA_SHOP + 2)));
        ShopTemplateItem resetShopArenaItem = new ShopTemplateItem();
        resetShopArenaItem.setSpceialType(ShopTemplateItem.TYPE_RESET_SHOP_ARENA);
        resetShopArenaItem.setSpceial(true);
        resetShopArenaItem.setNameSpeceial("Reset vật phẩm shop");
        resetShopArenaItem.setDescriptionSpeceial(String.format("Dùng %s (vang) để đổi các vật phẩm khác", Utilities.formatNumber(priceReset)));
        resetShopArenaItem.setPrice(new int[]{priceReset});
        resetShopArenaItem.setMoneyType(new byte[]{GopetManager.MONEY_TYPE_GOLD});
        this.shopTemplateItems.add(resetShopArenaItem);
        long timeGen = System.currentTimeMillis() + 10000;
        while (timeGen > System.currentTimeMillis()) {
            if (this.shopTemplateItems.size() < numSlot) {
                ShopArenaTemplate shopArenaTemplate = GopetManager.SHOP_ARENA_TEMPLATE.get(Utilities.nextInt(GopetManager.SHOP_ARENA_TEMPLATE.size()));
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
