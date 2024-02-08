
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Util;

public class ShopArenaTemplate
{
    public int Id { get; private set; }
    public sbyte[] moneyType { get; private set; }
    public int[] price { get; private set; }
    public int itemTemTempleId { get; private set; }
    public int count { get; private set; }
    public float percent { get; private set; }

    public ShopTemplateItem ToShopTemplateItem(sbyte shopid)
    {
        ShopTemplateItem shopTemplateItem = new ShopTemplateItem();
        shopTemplateItem.setNeedRemove(true);
        shopTemplateItem.setCloseScreenAfterClick(true);
        shopTemplateItem.setItemTempalteId(itemTemTempleId);
        shopTemplateItem.setCount(count);
        shopTemplateItem.setMoneyType(moneyType);
        shopTemplateItem.setPrice(price);
        shopTemplateItem.setShopId(shopid);
        return shopTemplateItem;
    }
}