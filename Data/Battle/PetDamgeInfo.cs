 
public class PetDamgeInfo {

    private int damge;
    private bool skipDef;
    private int trueDamge = 0;
    private bool skillMiss;
    private int hpRecovery = 0;

    public void setDamge(int damge)
    {
        this.damge = damge;
    }

    public void setSkipDef(bool skipDef)
    {
        this.skipDef = skipDef;
    }

    public void setTrueDamge(int trueDamge)
    {
        this.trueDamge = trueDamge;
    }

    public void setSkillMiss(bool skillMiss)
    {
        this.skillMiss = skillMiss;
    }

    public void setHpRecovery(int hpRecovery)
    {
        this.hpRecovery = hpRecovery;
    }

    public int getDamge()
    {
        return this.damge;
    }

    public bool isSkipDef()
    {
        return this.skipDef;
    }

    public int getTrueDamge()
    {
        return this.trueDamge;
    }

    public bool isSkillMiss()
    {
        return this.skillMiss;
    }

    public int getHpRecovery()
    {
        return this.hpRecovery;
    }

}
