namespace Gopet.Data.GopetClan
{
    public class ClanHouseTemplate
    {
        private int lvl;
        private int fundNeed;
        private int growthPointNeed;
        private int needClanLvl = 0;
        public void setLvl(int lvl)
        {
            this.lvl = lvl;
        }

        public void setFundNeed(int fundNeed)
        {
            this.fundNeed = fundNeed;
        }

        public void setGrowthPointNeed(int growthPointNeed)
        {
            this.growthPointNeed = growthPointNeed;
        }

        public void setNeedClanLvl(int needClanLvl)
        {
            this.needClanLvl = needClanLvl;
        }

        public int getLvl()
        {
            return lvl;
        }

        public int getFundNeed()
        {
            return fundNeed;
        }

        public int getGrowthPointNeed()
        {
            return growthPointNeed;
        }

        public int getNeedClanLvl()
        {
            return needClanLvl;
        }

    }
}