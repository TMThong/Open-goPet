namespace Gopet.Data.GopetItem
{
    public class TierItem
    {

        public int tierId { get; private set; }
        public int itemTemplateIdTier1 { get; private set; }
        public int itemTemplateIdTier2 { get; private set; }
        public float percent { get; private set; }

        public ItemTemplate ItemTemplateOne
        {
            get
            {
                return GopetManager.itemTemplate[itemTemplateIdTier1];
            }
        }
        public ItemTemplate ItemTemplateTwo
        {
            get
            {
                return GopetManager.itemTemplate[itemTemplateIdTier2];
            }
        }
    }
}