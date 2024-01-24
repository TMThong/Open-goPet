
public class ClanPotentialSkill
{

    private int buffId;
    private int point;

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
        return this.buffId;
    }

    public int getPoint()
    {
        return this.point;
    }


}
