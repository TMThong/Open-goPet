namespace Gopet.Data.GopetClan
{
    public class ClanPotentialSkill
    {

        public int buffId;
        public int point;

        public void addPoint(int skillpoint)
        {
            point += skillpoint;
        }

        public void setBuffId(int buffId)
        {
            this.buffId = buffId;
        }

        public void setPoint(int point)
        {
            this.point = point;
        }

        public int getBuffId()
        {
            return buffId;
        }

        public int getPoint()
        {
            return point;
        }
    }
}