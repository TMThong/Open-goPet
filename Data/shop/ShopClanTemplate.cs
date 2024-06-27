
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Util;

public class ShopClanTemplate {

    private int Id;
    private int[][] option;
    private String comment;
    private float percent;
    private int needShopClanLvl;
    public const int ITEM = 0;
    public const int ITEM_PART_PET = 1;
    public const int ITEM_PART_ITEM = 2;


    public void setId(int Id)
    {
        this.Id = Id;
    }

    public void setOption(int[][] option)
    {
        this.option = option;
    }

    public void setComment(String comment)
    {
        this.comment = comment;
    }

    public void setPercent(float percent)
    {
        this.percent = percent;
    }

    public void setNeedShopClanLvl(int needShopClanLvl)
    {
        this.needShopClanLvl = needShopClanLvl;
    }

    public int getId()
    {
        return this.Id;
    }

    public int[][] getOption()
    {
        return this.option;
    }

    public String getComment()
    {
        return this.comment;
    }

    public float getPercent()
    {
        return this.percent;
    }

    public int getNeedShopClanLvl()
    {
        return this.needShopClanLvl;
    }

    public JArrayList<ShopTemplateItem> next() {
        JArrayList<ShopTemplateItem> shopTemplateItems = new JArrayList<ShopTemplateItem>();
        for (int i = 0; i < option.Length; i++) {
            int[] optionInfo = option[i];
            switch (optionInfo[0]) {
                case ITEM: {
                    int perItem = optionInfo[6];
                    for (int j = 0; j < perItem; j++) {
                        ShopTemplateItem shopTemplateItem = new ShopTemplateItem();
                        shopTemplateItem.setNeedRemove(true);
                        shopTemplateItem.setCloseScreenAfterClick(true);
                        shopTemplateItem.setItemTempalteId(optionInfo[1]);
                        shopTemplateItem.setCount(optionInfo[2]);
                        shopTemplateItem.setMoneyType(new sbyte[]{(sbyte) optionInfo[4]});
                        shopTemplateItem.setPrice(new int[]{optionInfo[5]});
                        shopTemplateItems.add(shopTemplateItem);
                    }
                }
                break;
            }
        }
        return shopTemplateItems;
    }
}
