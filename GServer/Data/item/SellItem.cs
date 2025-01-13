using Gopet.Util;
using Newtonsoft.Json;

namespace Gopet.Data.GopetItem
{
    public class SellItem
    {
        [NonSerialized]
        [JsonIgnore]
        public Mutex sellItemMutex = new Mutex();
        public Item ItemSell;
        public int price;
        public long sumVal = 0;
        public bool hasSell = false;
        public bool hasRemoved = false;
        public Pet pet;
        public long expireTime = 0l;
        public int itemId;
        public int user_id = 0;
        public bool IsRetail { get; set; } = false;
        public string AssignedName { get; set; } = null;

        public int TotalCount { get; set; } = 1;

        protected SellItem() { }

        public SellItem(int hoursExpire)
        {
            expireTime = Utilities.TimeHours(hoursExpire);
        }

        public SellItem(Item ItemSell, int price, int hoursExpire) : this(hoursExpire)
        {
            this.ItemSell = ItemSell;
            this.price = price;
            this.TotalCount = ItemSell.count;
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

        public string getName(Player player)
        {
            if (pet != null)
            {
                return pet.getNameWithoutStar(player) + Utilities.Format(" (Id:%s)", itemId);
            }
            return ItemSell.getTemp().getName(player) + Utilities.Format(" (Id:%s)", itemId);
        }

        public string getFrameImgPath()
        {
            if (pet != null)
            {
                return pet.getPetTemplate().frameImg;
            }
            return ItemSell.getTemp().getIconPath();
        }

        public string getDescription(Player player)
        {
            if (pet != null)
            {
                return pet.getPetTemplate().getDesc();
            }
            return ItemSell.getTemp().getDescription(player);
        }

        public int MathPrice
        {
            get
            {
                if (sumVal > 0 && this.ItemSell != null)
                {
                    int price = Math.Max(1, this.price) / Math.Max(1, this.TotalCount);
                    return Math.Max(1, price * this.ItemSell.count);
                }
                return this.price;
            }
        }
    }
}