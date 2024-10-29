
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Util;

public class ShopClan : ShopTemplate
{

    private Clan clan;
    private long timeRefresh;

    public ShopClan(Clan clan_) : base(MenuController.SHOP_CLAN)
    {
        this.clan = clan_;
        this.refresh();
    }

    public void refresh()
    {
        timeRefresh = Utilities.CurrentTimeMillis;
        this.shopTemplateItems.Clear();
        foreach (int item in clan.Options)
        {
            switch (item)
            {
                case ClanManager.OPTION_ADD_TITLE_9:
                    this.shopTemplateItems.Add(new ShopTemplateItem()
                    {
                        itemTemTempleId = 240009,
                        count = 1,
                        isSellItem = true,
                        price = new int[] { 2500 },
                        moneyType = new sbyte[] { GopetManager.MONEY_TYPE_GOLD },
                        isLock = true,
                        hasId = true,
                        menuId = 999991,
                        TimeNeedReset = TimeSpan.FromDays(7),
                        NeedFund = 2000
                    });
                    continue;
                case ClanManager.OPTION_ADD_TITLE_10:
                    this.shopTemplateItems.Add(new ShopTemplateItem()
                    {
                        itemTemTempleId = 240010,
                        count = 1,
                        isSellItem = true,
                        price = new int[] { 2500 },
                        moneyType = new sbyte[] { GopetManager.MONEY_TYPE_GOLD },
                        isLock = true,
                        hasId = true,
                        menuId = 999992,
                        TimeNeedReset = TimeSpan.FromDays(7),
                        NeedFund = 2000
                    });
                    continue;
            }
        }
    }

    public ShopTemplateItem getShopTemplateItem(int mneuId)
    {
        int left = 0;
        int right = shopTemplateItems.Count - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            ShopTemplateItem midItem = shopTemplateItems.get(mid);
            if (midItem.getMenuId() == mneuId)
            {
                return midItem;
            }
            if (midItem.getMenuId() < mneuId)
            {
                left = mid + 1;
            }
            else
            {
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
