using Gopet.Util;

namespace Gopet.Data.GopetItem
{
    public class SellItem 
    {

        private object lockObject;
        public Item ItemSell;
        public int price;
        public bool hasSell = false;
        public bool hasRemoved = false;
        public Pet pet;
        public long expireTime = 0l;
        public int itemId;
        public int user_id = 0;

        public SellItem(int hoursExpire)
        {

            expireTime = Utilities.TimeHours(hoursExpire);
        }

        public SellItem(Item ItemSell, int price, int hoursExpire) : this(hoursExpire)
        {

            this.ItemSell = ItemSell;
            this.price = price;
        }

        public SellItem(int price, Pet pet, int hoursExpire) : this(hoursExpire)
        {

            this.price = price;
            this.pet = pet;
        }

        public void setHasSell(bool b)
        {
            hasSell = b;
        }

        public string getName()
        {
            if (pet != null)
            {
                return pet.getNameWithoutStar() + Utilities.Format(" (Mã định danh:%s)", itemId);
            }
            return ItemSell.getTemp().getName() + Utilities.Format(" (Mã định danh:%s)", itemId);
        }

        public string getFrameImgPath()
        {
            if (pet != null)
            {
                return pet.getPetTemplate().getFrameImg();
            }
            return ItemSell.getTemp().getIconPath();
        }

        public string getDescription()
        {
            if (pet != null)
            {
                return pet.getPetTemplate().getDesc();
            }
            return ItemSell.getTemp().getDescription();
        }

    }
}