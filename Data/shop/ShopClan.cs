 
public class ShopClan : ShopTemplate {

    private Clan clan;
    private long timeRefresh;

    public ShopClan(Clan clan_) throws CloneNotSupportedException {
        base(MenuController.SHOP_CLAN);
        this.clan = clan_;
        this.refresh();
    }

    public   void refresh() throws CloneNotSupportedException {
        timeRefresh = System.currentTimeMillis();
        this.shopTemplateItems.clear();
        ArrayList<ShopClanTemplate> list = GopetManager.shopClanByLvl.get(clan.getbaseMarketLvl());
        if (list != null) {
            for (ShopClanTemplate shopClanTemplate : list) {
                this.shopTemplateItems.addAll(shopClanTemplate.next());
            }
        }

        for (int i = 0; i < shopTemplateItems.size(); i++) {
            ShopTemplateItem get = shopTemplateItems.get(i);
            get.setHasId(true);
            get.setMenuId(i);
        }
    }

    public   ShopTemplateItem getShopTemplateItem(int mneuId)   {
        int left = 0;
        int right = shopTemplateItems.size() - 1;
        while (left <= right) {
            int mid = left + (right - left) / 2;
            ShopTemplateItem midItem = shopTemplateItems.get(mid);
            if (midItem.getMenuId() == mneuId) {
                return midItem;
            }
            if (midItem.getMenuId() < mneuId) {
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }
        return null;
    }
}
