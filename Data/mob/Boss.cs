 

public class Boss : Mob {

    private BossTemplate bossTemplate;
     
    private bool isTimeOut = false;
     
    private long timeoutMilis = 0L;

    public Boss(int bossTemplateId, MobLocation mobLocation) {
        this.bossTemplate = GopetManager.boss.get(bossTemplateId);
        this.setPetTemplate(bossTemplate.getPetTemplate());
        this.setMobLocation(mobLocation);
        this.setDef(bossTemplate.getDef());
        this.setMobLvInfo(new MobLvInfo() {
             
            public int getLvl() {
                return bossTemplate.getLvl();
            }

             
            public int getHp() {
                return bossTemplate.getHp();
            }

             
            public int getExp() {
                return 0;
            }

             
            public int getStrength() {
                return bossTemplate.getAtk();
            }
        });
        this.initMob();
    }

     
    public  void initMob() {
        base.initMob(); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
        this.mp = 500 + (this.mobLvInfo.getLvl() * 100);
        this.maxMp = 500 + (this.mobLvInfo.getLvl() * 100);
    }

     
    public String getName() {
        return bossTemplate.getName();
    }
}
