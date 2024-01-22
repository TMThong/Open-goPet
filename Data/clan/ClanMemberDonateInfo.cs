 
public class ClanMemberDonateInfo : DataVersion {
    private sbyte priceType;
    private long price;
    private long growthPoint;
    private long fund;
    private int curDonate = 0;
    private int maxDonate;

    public ClanMemberDonateInfo() {
    }
    
     
    public ClanMemberDonateInfo(sbyte priceType, long price, long growthPoint, long fund, int maxDonate) {
        this.priceType = priceType;
        this.price = price;
        this.growthPoint = growthPoint;
        this.fund = fund;
        this.maxDonate = maxDonate;
    }
    
}
