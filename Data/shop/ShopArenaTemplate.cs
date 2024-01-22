 
public class ShopArenaTemplate {
    private int Id;
    private int[][] option;
    private String comment;
    private float percent;
    public const int ITEM = 0;
    public const int ITEM_PART_PET = 1;
    public const int ITEM_PART_ITEM = 2;
    
    public ShopTemplateItem next() {
        ShopTemplateItem shopTemplateItem = new ShopTemplateItem();
        shopTemplateItem.setNeedRemove(true);
        shopTemplateItem.setCloseScreenAfterClick(true);
        for (int i = 0; i < option.Length; i++) {
            int[] optionInfo = option[i];
            switch (optionInfo[0]) {
                case ITEM:
                    shopTemplateItem.setItemTempalteId(optionInfo[1]);
                    shopTemplateItem.setCount(optionInfo[2]);
                    shopTemplateItem.setInventoryType((sbyte) optionInfo[3]);
                    shopTemplateItem.setMoneyType(new sbyte[]{(sbyte) optionInfo[4]});
                    shopTemplateItem.setPrice(new int[]{optionInfo[5]});
                    break;
                case ITEM_PART_PET: {
                    int typePart = optionInfo[1];
                    shopTemplateItem.setCount(optionInfo[2]);
                    shopTemplateItem.setInventoryType((sbyte) optionInfo[3]);
                    shopTemplateItem.setMoneyType(new sbyte[]{(sbyte) optionInfo[4]});
                    shopTemplateItem.setPrice(new int[]{optionInfo[5]});
                    ArrayList<ItemTemplate> partPet = GopetManager.mergeItemPet.get(typePart);
                    shopTemplateItem.setItemTempalteId(partPet.get(util.Utilities.nextInt(partPet.size())).getItemId());
                }
                break;
                case ITEM_PART_ITEM: {
                    int typePart = optionInfo[1];
                    shopTemplateItem.setCount(optionInfo[2]);
                    shopTemplateItem.setInventoryType((sbyte) optionInfo[3]);
                    shopTemplateItem.setMoneyType(new sbyte[]{(sbyte) optionInfo[4]});
                    shopTemplateItem.setPrice(new int[]{optionInfo[5]});
                    ArrayList<ItemTemplate> partItem = GopetManager.mergeItemItem.get(typePart);
                    shopTemplateItem.setItemTempalteId(partItem.get(util.Utilities.nextInt(partItem.size())).getItemId());
                }
                break;
            }
            if (shopTemplateItem.getMoneyType() != null) {
                break;
            }
        }
        return shopTemplateItem;
    }
}