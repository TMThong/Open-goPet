 
public class TierItem {

    private int tierId;
    private int itemTemplateIdTier1;
    private int itemTemplateIdTier2;
    private float percent;

    public void setTierId(int tierId)
    {
        this.tierId = tierId;
    }

    public void setItemTemplateIdTier1(int itemTemplateIdTier1)
    {
        this.itemTemplateIdTier1 = itemTemplateIdTier1;
    }

    public void setItemTemplateIdTier2(int itemTemplateIdTier2)
    {
        this.itemTemplateIdTier2 = itemTemplateIdTier2;
    }

    public void setPercent(float percent)
    {
        this.percent = percent;
    }

    public int getTierId()
    {
        return this.tierId;
    }

    public int getItemTemplateIdTier1()
    {
        return this.itemTemplateIdTier1;
    }

    public int getItemTemplateIdTier2()
    {
        return this.itemTemplateIdTier2;
    }

    public float getPercent()
    {
        return this.percent;
    }

}
