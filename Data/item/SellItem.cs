 
public class SellItem : DataVersion {

    private Object lockObject;
    public Item ItemSell;
    public int price;
    public bool hasSell = false;
    public bool hasRemoved = false;
    public Pet pet;
    public long expireTime = 0l;
    public int itemId;
    public int user_id = 0;

    public SellItem(int hoursExpire) {

        expireTime = Utilities.TimeHours(hoursExpire);
    }

    public SellItem(Item ItemSell, int price, int hoursExpire) {
        this(hoursExpire);
        this.ItemSell = ItemSell;
        this.price = price;
    }

    public SellItem(int price, Pet pet, int hoursExpire) {
        this(hoursExpire);
        this.price = price;
        this.pet = pet;
    }

    public void setHasSell(bool b) {
        hasSell = b;
    }

    public String getName() {
        if (pet != null) {
            return pet.getNameWithoutStar() + String.format(" (Mã định danh:%s)", itemId);
        }
        return ItemSell.getTemp().getName() + String.format(" (Mã định danh:%s)", itemId);
    }

    public String getFrameImgPath() {
        if (pet != null) {
            return pet.getPetTemplate().getFrameImg();
        }
        return ItemSell.getTemp().getIconPath();
    }

    public String getDescription() {
        if (pet != null) {
            return pet.getPetTemplate().getDesc();
        }
        return ItemSell.getTemp().getDescription();
    }

}
