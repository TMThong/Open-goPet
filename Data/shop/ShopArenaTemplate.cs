
using Gopet.Data.Collections;
using Gopet.Util;

public class ShopArenaTemplate
{
    private int Id;
    private int[][] option;
    private String comment;
    private float percent;
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

    public ShopTemplateItem next()
    {
        ShopTemplateItem shopTemplateItem = new ShopTemplateItem();
        shopTemplateItem.setNeedRemove(true);
        shopTemplateItem.setCloseScreenAfterClick(true);
        for (int i = 0; i < option.Length; i++)
        {
            int[] optionInfo = option[i];
            switch (optionInfo[0])
            {
                case ITEM:
                    shopTemplateItem.setItemTempalteId(optionInfo[1]);
                    shopTemplateItem.setCount(optionInfo[2]);
                    shopTemplateItem.setInventoryType((sbyte)optionInfo[3]);
                    shopTemplateItem.setMoneyType(new sbyte[] { (sbyte)optionInfo[4] });
                    shopTemplateItem.setPrice(new int[] { optionInfo[5] });
                    break;
                case ITEM_PART_PET:
                    {
                        int typePart = optionInfo[1];
                        shopTemplateItem.setCount(optionInfo[2]);
                        shopTemplateItem.setInventoryType((sbyte)optionInfo[3]);
                        shopTemplateItem.setMoneyType(new sbyte[] { (sbyte)optionInfo[4] });
                        shopTemplateItem.setPrice(new int[] { optionInfo[5] });
                        ArrayList<ItemTemplate> partPet = GopetManager.mergeItemPet.get(typePart);
                        shopTemplateItem.setItemTempalteId(partPet.get(Utilities.nextInt(partPet.Count)).getItemId());
                    }
                    break;
                case ITEM_PART_ITEM:
                    {
                        int typePart = optionInfo[1];
                        shopTemplateItem.setCount(optionInfo[2]);
                        shopTemplateItem.setInventoryType((sbyte)optionInfo[3]);
                        shopTemplateItem.setMoneyType(new sbyte[] { (sbyte)optionInfo[4] });
                        shopTemplateItem.setPrice(new int[] { optionInfo[5] });
                        ArrayList<ItemTemplate> partItem = GopetManager.mergeItemItem.get(typePart);
                        shopTemplateItem.setItemTempalteId(partItem.get(Utilities.nextInt(partItem.Count())).getItemId());
                    }
                    break;
            }
            if (shopTemplateItem.getMoneyType() != null)
            {
                break;
            }
        }
        return shopTemplateItem;
    }
}