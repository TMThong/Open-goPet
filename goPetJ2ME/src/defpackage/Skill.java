package defpackage;

import java.util.Hashtable;

/* renamed from: Class56  reason: default package */
 /* loaded from: gopet_repackage.jar:Class56.class */
public final class Skill {

    public SkillEffect[] SlashEffect;
    public SkillEffect gong;
    public SkillEffect attackEffect;
    private Hashtable skillTemplate = new Hashtable(24);

    public final SkillEffect loadCustomSkill(int i) {
        Integer num = new Integer(i);
        SkillEffect skill = (SkillEffect) this.skillTemplate.get(num);
        if (skill == null) {
            switch (i) {
                case 0:
                    skill = Class48.loadSKill("/pet/battle/skills/SongKich")[0];
                    break;
                case 1:
                    skill = Class48.loadSKill("/pet/battle/skills/Satthuong")[0];
                    break;
                case 2:
                    skill = Class48.loadSKill("/pet/battle/skills/CuongNo")[0];
                    break;
                case 3:
                    skill = Class48.loadSKill("/pet/battle/skills/Bang")[0];
                    break;
                case 4:
                    skill = Class48.loadSKill("/pet/battle/skills/SamSet")[0];
                    break;
                case 5:
                    skill = Class48.loadSKill("/pet/battle/skills/Lua")[0];
                    break;
                case 6:
                    skill = Class48.loadSKill("/pet/battle/skills/hadoc")[0];
                    break;
                case 7:
                    skill = Class48.loadSKill("/pet/battle/skills/daogam")[0];
                    break;
                case 8:
                    skill = Class48.loadSKill("/pet/battle/skills/voanh")[0];
                    break;
                case 9:
                    skill = Class48.loadSKill("/pet/battle/skills/fandame")[0];
                    break;
                case 10:
                    skill = Class48.loadSKill("/pet/battle/skills/hutmau")[0];
                    break;
                case 11:
                    skill = Class48.loadSKill("/pet/battle/skills/manaburn")[0];
                    break;
                case 12:
                    skill = Class48.loadSKill("/pet/battle/skills/thiencanthu")[0];
                    break;
                case 13:
                    skill = Class48.loadSKill("/pet/battle/skills/lienhoancuoc")[0];
                    break;
                case 14:
                    skill = Class48.loadSKill("/pet/battle/skills/lachan")[0];
                    break;
                case 15:
                    skill = Class48.loadSKill("/pet/battle/skills/Tornado")[0];
                    break;
                case 16:
                    skill = Class48.loadSKill("/pet/battle/skills/Xayda")[0];
                    break;
                case 17:
                    skill = Class48.loadSKill("/pet/battle/skills/MoonShine")[0];
                    break;
                case 18:
                    skill = Class48.loadSKill("/pet/battle/skills/Thorns")[0];
                    break;
                case 19:
                    skill = Class48.loadSKill("/pet/battle/skills/ThorHammer")[0];
                    break;
                case 20:
                    skill = Class48.loadSKill("/pet/battle/skills/Sword")[0];
                    break;
                case 21:
                    skill = Class48.loadSKill("/pet/battle/skills/Shuriken")[0];
                    break;
                case 22:
                    skill = Class48.loadSKill("/pet/battle/skills/ZeusWraith")[0];
                    break;
                case 23:
                    skill = Class48.loadSKill("/pet/battle/skills/Meteor")[0];
                    break;
            }
            this.skillTemplate.put(num, skill);
        }
        return skill;
    }

    public final void clear() {
        this.skillTemplate.clear();
    }
}
