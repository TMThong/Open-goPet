 
public class ItemTemplate {

    private int itemId;
    private String name;
    private String description;
    private int _int;
    private int agi;
    private int str;
    private int def;
    private int atk;
    private int hp;
    private int mp;
    private int requireStr;
    private int requireAgi;
    private int requireInt;
    private int type;
    private int[] option = new int[0];
    private int[] optionValue = new int[0];
    private bool isStackable;
    private String frameImgPath;
    private String iconPath;
    public sbyte gender;
    public long expire;
    private bool isOnSky;
    private bool canTrade;
    private sbyte NClass;
    private int iconId;
    private sbyte element;
    private int typeTier;

    public String getNameViaType() {
        switch (type) {
            case GopetManager.PET_EQUIP_ARMOUR:
            case GopetManager.PET_EQUIP_GLOVE:
            case GopetManager.PET_EQUIP_SHOE:
            case GopetManager.PET_EQUIP_WEAPON:
            case GopetManager.PET_EQUIP_HAT:
                return String.format("%s chỉ số (%s(atk) %s(def) %s(hp) %s(mp)) Yêu cầu (%s(str) %s(int) %s(agi))", name, atk, def, hp, mp, requireStr, requireInt, requireAgi);
            default:
                return name;
        }
    }

    public String getDescriptionViaType() {
        switch (type) {
            case GopetManager.ITEM_PART_PET: {
                if (this.optionValue.Length >= 2) {
                    int petId = this.optionValue[0];
                    int count = this.optionValue[1];
                    PetTemplate petTemplate = GopetManager.PETTEMPLATE_HASH_MAP.get(petId);
                    if (petTemplate != null) {
                        return String.format("Dùng %s mảnh sẽ đổi được pet %s", count, petTemplate.getName());
                    }
                }
                return description;
            }
            case GopetManager.ITEM_PART_ITEM: {
                if (this.optionValue.Length >= 2) {
                    int itemidTemp = this.optionValue[0];
                    int count = this.optionValue[1];
                    ItemTemplate itemTemplate = GopetManager.itemTemplate.get(itemidTemp);
                    if (itemTemplate != null) {
                        return String.format("Dùng %s mảnh sẽ đổi được %s", count, itemTemplate.getName());
                    }
                }
                return description;
            }
            default:
                return description;
        }
    }
}
