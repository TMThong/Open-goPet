
public class BossTemplate : GameObject
{

    private int bossId;
    private String name;
    private int[][] gift;
    private sbyte typeBoss;
    private PetTemplate petTemplate;
    private int lvl;

    public void setBossId(int bossId)
    {
        this.bossId = bossId;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setGift(int[][] gift)
    {
        this.gift = gift;
    }

    public void setTypeBoss(sbyte typeBoss)
    {
        this.typeBoss = typeBoss;
    }

    public void setPetTemplate(PetTemplate petTemplate)
    {
        this.petTemplate = petTemplate;
    }

    public void setLvl(int lvl)
    {
        this.lvl = lvl;
    }

    public int getBossId()
    {
        return this.bossId;
    }

    public String getName()
    {
        return this.name;
    }

    public int[][] getGift()
    {
        return this.gift;
    }

    public sbyte getTypeBoss()
    {
        return this.typeBoss;
    }

    public PetTemplate getPetTemplate()
    {
        return this.petTemplate;
    }

    public int getLvl()
    {
        return this.lvl;
    }

}
