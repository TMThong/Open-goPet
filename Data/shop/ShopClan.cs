
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Util;

public class ShopClan : ShopTemplate {

    private Clan clan;
    private long timeRefresh;

    public ShopClan(Clan clan_) : base(MenuController.SHOP_CLAN)
    {
         
        this.clan = clan_;
        this.refresh();
    }

    public   void refresh()   {
        timeRefresh = Utilities.CurrentTimeMillis;
        this.shopTemplateItems.Clear();
        JArrayList<ShopClanTemplate> list = GopetManager.shopClanByLvl.get(clan.getbaseMarketLvl());
        if (list != null) {
            foreach (ShopClanTemplate shopClanTemplate in list) {
                this.shopTemplateItems.AddRange(shopClanTemplate.next());
            }
        }

        for (int i = 0; i < shopTemplateItems.Count; i++) {
            ShopTemplateItem get = shopTemplateItems.get(i);
            get.setHasId(true);
            get.setMenuId(i);
        }
    }

    public   ShopTemplateItem getShopTemplateItem(int mneuId)   {
        int left = 0;
        int right = shopTemplateItems.Count - 1;
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

    internal long GetTimeMillisRefresh()
    {
        return this.timeRefresh;
    }
}
