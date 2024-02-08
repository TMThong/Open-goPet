namespace Gopet.Data.GopetClan
{
    public class ClanTemplate
    {
        public int maxMember;
        public int tiemnangPoint;
        public long fundNeed;
        public long growthPointNeed;
        public int[] permission;
        public int clanLvl;

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
            this.clanLvl = lvl;
        }

        public int getMaxMember()
        {
            return maxMember;
        }

        public int getTiemnangPoint()
        {
            return tiemnangPoint;
        }

        public long getFundNeed()
        {
            return fundNeed;
        }

        public long getGrowthPointNeed()
        {
            return growthPointNeed;
        }

        public int[] getPermission()
        {
            return permission;
        }

        public int getLvl()
        {
            return clanLvl;
        }

    }
}