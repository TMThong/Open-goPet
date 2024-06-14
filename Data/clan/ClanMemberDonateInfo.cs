namespace Gopet.Data.GopetClan
{
    public class ClanMemberDonateInfo 
    {
        public sbyte priceType;
        public long price;
        public long fund;

        public ClanMemberDonateInfo()
        {
        }


        public ClanMemberDonateInfo(sbyte priceType, long price, long fund)
        {
            this.priceType = priceType;
            this.price = price;
            this.fund = fund;
        }
        public void setPriceType(sbyte priceType)
        {
            this.priceType = priceType;
        }

        public void setPrice(long price)
        {
            this.price = price;
        }


        public void setFund(long fund)
        {
            this.fund = fund;
        }

       

        public sbyte getPriceType()
        {
            return priceType;
        }

        public long getPrice()
        {
            return price;
        }


        public long getFund()
        {
            return fund;
        }

    }
}