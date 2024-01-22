/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.mob;

import lombok.Getter;
import lombok.Setter;
import manager.GopetManager;

/**
 *
 * @author MINH THONG
 */
@Getter

public class Boss extends Mob {

    private BossTemplate bossTemplate;
    @Setter
    private bool isTimeOut = false;
    @Setter
    private long timeoutMilis = 0L;

    public Boss(int bossTemplateId, MobLocation mobLocation) {
        this.bossTemplate = GopetManager.boss.get(bossTemplateId);
        this.setPetTemplate(bossTemplate.getPetTemplate());
        this.setMobLocation(mobLocation);
        this.setDef(bossTemplate.getDef());
        this.setMobLvInfo(new MobLvInfo() {
            @Override
            public int getLvl() {
                return bossTemplate.getLvl();
            }

            @Override
            public int getHp() {
                return bossTemplate.getHp();
            }

            @Override
            public int getExp() {
                return 0;
            }

            @Override
            public int getStrength() {
                return bossTemplate.getAtk();
            }
        });
        this.initMob();
    }

    @Override
    public void initMob() {
        super.initMob(); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
        this.mp = 500 + (this.mobLvInfo.getLvl() * 100);
        this.maxMp = 500 + (this.mobLvInfo.getLvl() * 100);
    }

    @Override
    public String getName() {
        return bossTemplate.getName();
    }
}
