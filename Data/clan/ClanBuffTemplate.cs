
public class ClanBuffTemplate
{
    private int buffId;
    private int valuePerLevel;
    private bool isPercent;
    private String comment;
    private int potentialPointNeed;
    private int lvlClan;
    private String name;
    private String desc;

    public void setBuffId(int buffId)
    {
        this.buffId = buffId;
    }

    public void setValuePerLevel(int valuePerLevel)
    {
        this.valuePerLevel = valuePerLevel;
    }

    public void setPercent(bool isPercent_)
    {
        this.isPercent = isPercent_;
    }

    public void setComment(String comment)
    {
        this.comment = comment;
    }

    public void setPotentialPointNeed(int potentialPointNeed)
    {
        this.potentialPointNeed = potentialPointNeed;
    }

    public void setLvlClan(int lvlClan)
    {
        this.lvlClan = lvlClan;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setDesc(String desc)
    {
        this.desc = desc;
    }

    public int getBuffId()
    {
        return this.buffId;
    }

    public int getValuePerLevel()
    {
        return this.valuePerLevel;
    }

     

    public String getComment()
    {
        return this.comment;
    }

    public int getPotentialPointNeed()
    {
        return this.potentialPointNeed;
    }

    public int getLvlClan()
    {
        return this.lvlClan;
    }

    public String getName()
    {
        return this.name;
    }

    public String getDesc()
    {
        return this.desc;
    }

}
