
using Gopet.Data.Collections;

public class ShopClanTemplate {

    private int Id;
    private int[][] option;
    private String comment;
    private float percent;
    private int needShopClanLvl;
    public const int ITEM = 0;
    public const int ITEM_PART_PET = 1;
    public const int ITEM_PART_ITEM = 2;

    /**
     *
     * Option Id 0 index1: idItemTemp index2: count item index3: inventory type
     * index4: moneyType index5: price index6: perItem
     *
     * @return
     */
    public ArrayList<ShopTemplateItem> next() {
        ArrayList<ShopTemplateItem> shopTemplateItems = new ArrayList<ShopTemplateItem>();
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
                        shopTemplateItem.setInventoryType((sbyte) optionInfo[3]);
                        shopTemplateItem.setMoneyType(new sbyte[]{(sbyte) optionInfo[4]});
                        shopTemplateItem.setPrice(new int[]{optionInfo[5]});
                        shopTemplateItems.add(shopTemplateItem);
                    }
                }
                break;
                case ITEM_PART_PET: {
                    int perItem = optionInfo[6];
                    for (int j = 0; j < perItem; j++) {
                        ShopTemplateItem shopTemplateItem = new ShopTemplateItem();
                        shopTemplateItem.setNeedRemove(true);
                        shopTemplateItem.setCloseScreenAfterClick(true);
                        int typePart = optionInfo[1];
                        shopTemplateItem.setCount(optionInfo[2]);
                        shopTemplateItem.setInventoryType((sbyte) optionInfo[3]);
                        shopTemplateItem.setMoneyType(new sbyte[]{(sbyte) optionInfo[4]});
                        shopTemplateItem.setPrice(new int[]{optionInfo[5]});
                        ArrayList<ItemTemplate> partPet = GopetManager.mergeItemPet.get(typePart);
                        shopTemplateItem.setItemTempalteId(partPet.get(util.Utilities.nextInt(partPet.size())).getItemId());
                        shopTemplateItems.add(shopTemplateItem);
                    }
                }
                break;
                case ITEM_PART_ITEM: {
                    int perItem = optionInfo[6];
                    for (int j = 0; j < perItem; j++) {
                        ShopTemplateItem shopTemplateItem = new ShopTemplateItem();
                        shopTemplateItem.setNeedRemove(true);
                        shopTemplateItem.setCloseScreenAfterClick(true);
                        int typePart = optionInfo[1];
                        shopTemplateItem.setCount(optionInfo[2]);
                        shopTemplateItem.setInventoryType((sbyte) optionInfo[3]);
                        shopTemplateItem.setMoneyType(new sbyte[]{(sbyte) optionInfo[4]});
                        shopTemplateItem.setPrice(new int[]{optionInfo[5]});
                        ArrayList<ItemTemplate> partItem = GopetManager.mergeItemItem.get(typePart);
                        shopTemplateItem.setItemTempalteId(partItem.get(util.Utilities.nextInt(partItem.size())).getItemId());
                        shopTemplateItems.add(shopTemplateItem);
                    }
                }
                break;
            }
        }
        return shopTemplateItems;
    }
}
