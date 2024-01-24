

public class Boss : Mob
{

    private BossTemplate bossTemplate;

    public bool isTimeOut = false;

    public long timeoutMilis = 0L;

    public Boss(int bossTemplateId, MobLocation mobLocation)
    {
        this.bossTemplate = GopetManager.boss.get(bossTemplateId);
        this.setPetTemplate(bossTemplate.getPetTemplate());
        this.setMobLocation(mobLocation);
        this.def = bossTemplate.def;
        this.setMobLvInfo(new MobLvInfoImp(bossTemplate));
        this.initMob();
    }

    sealed class MobLvInfoImp : MobLvInfo
    {
        public MobLvInfoImp(BossTemplate bossTemplate)
        {
            this.bossTemplate = bossTemplate;
        }

        public BossTemplate bossTemplate { get; }

        public override int getLvl()
        {
            return bossTemplate.getLvl();
        }


        public override int getHp()
        {
            return bossTemplate.hp;
        }


        public override int getExp()
        {
            return 0;
        }


        public override int getStrength()
        {
            return bossTemplate.atk;
        }
    }
    public void initMob()
    {
        base.initMob(); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
        this.mp = 500 + (this.mobLvInfo.getLvl() * 100);
        this.maxMp = 500 + (this.mobLvInfo.getLvl() * 100);
    }


    public String getName()
    {
        return bossTemplate.getName();
    }

    internal BossTemplate getBossTemplate()
    {
        return this.bossTemplate;
    }
}
