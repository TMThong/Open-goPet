
 
public class ClanTemplate {
    private int maxMember;
    private int tiemnangPoint;
    private long fundNeed;
    private long growthPointNeed;
    private int[] permission;
    private int lvl;

    public void setMaxMember(int maxMember)
    {
        this.maxMember = maxMember;
    }

    public void setTiemnangPoint(int tiemnangPoint)
    {
        this.tiemnangPoint = tiemnangPoint;
    }

    public void setFundNeed(long fundNeed)
    {
        this.fundNeed = fundNeed;
    }

    public void setGrowthPointNeed(long growthPointNeed)
    {
        this.growthPointNeed = growthPointNeed;
    }

    public void setPermission(int[] permission)
    {
        this.permission = permission;
    }

    public void setLvl(int lvl)
    {
        this.lvl = lvl;
    }

    public int getMaxMember()
    {
        return this.maxMember;
    }

    public int getTiemnangPoint()
    {
        return this.tiemnangPoint;
    }

    public long getFundNeed()
    {
        return this.fundNeed;
    }

    public long getGrowthPointNeed()
    {
        return this.growthPointNeed;
    }

    public int[] getPermission()
    {
        return this.permission;
    }

    public int getLvl()
    {
        return this.lvl;
    }

}
