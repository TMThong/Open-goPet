 
public class PetTier {
    public int tierId {  get; private set; }
    public int petTemplateId1 { get; private set; }
    public int petTemplateId2 { get; private set; }
    public int petTemplateIdNeed { get; private set; }

    public void setTierId(int tierId)
    {
        this.tierId = tierId;
    }

    public void setPetTemplateId1(int petTemplateId1)
    {
        this.petTemplateId1 = petTemplateId1;
    }

    public void setPetTemplateId2(int petTemplateId2)
    {
        this.petTemplateId2 = petTemplateId2;
    }

    public void setPetTemplateIdNeed(int petTemplateIdNeed)
    {
        this.petTemplateIdNeed = petTemplateIdNeed;
    }

    public int getTierId()
    {
        return this.tierId;
    }

    public int getPetTemplateId1()
    {
        return this.petTemplateId1;
    }

    public int getPetTemplateId2()
    {
        return this.petTemplateId2;
    }

    public int getPetTemplateIdNeed()
    {
        return this.petTemplateIdNeed;
    }

}
