
public class ClanMemberDonateInfo : DataVersion
{
    private sbyte priceType;
    private long price;
    private long growthPoint;
    private long fund;
    private int curDonate = 0;
    private int maxDonate;

    public ClanMemberDonateInfo()
    {
    }


    public ClanMemberDonateInfo(sbyte priceType, long price, long growthPoint, long fund, int maxDonate)
    {
        this.priceType = priceType;
        this.price = price;
        this.growthPoint = growthPoint;
        this.fund = fund;
        this.maxDonate = maxDonate;
    }
    public void setPriceType(sbyte priceType)
    {
        this.priceType = priceType;
    }

    public void setPrice(long price)
    {
        this.price = price;
    }

    public void setGrowthPoint(long growthPoint)
    {
        this.growthPoint = growthPoint;
    }

    public void setFund(long fund)
    {
        this.fund = fund;
    }

    public void setCurDonate(int curDonate)
    {
        this.curDonate = curDonate;
    }

    public void setMaxDonate(int maxDonate)
    {
        this.maxDonate = maxDonate;
    }

    public sbyte getPriceType()
    {
        return this.priceType;
    }

    public long getPrice()
    {
        return this.price;
    }

    public long getGrowthPoint()
    {
        return this.growthPoint;
    }

    public long getFund()
    {
        return this.fund;
    }

    public int getCurDonate()
    {
        return this.curDonate;
    }

    public int getMaxDonate()
    {
        return this.maxDonate;
    }


}
