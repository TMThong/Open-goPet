
using Gopet.Data.Collections;

public class PetTatto  {

    public int tattooTemplateId;
    public int tattoId;
    public int hp, mp, atk, def;

    public PetTatto(int tattooTemplateId) {
        this.tattooTemplateId = tattooTemplateId;
        PetTattoTemplate tattoTemplate = getTemp();
        setAtk(tattoTemplate.getAtk());
        setDef(tattoTemplate.getDef());
        setHp(tattoTemplate.getHp());
        setMp(tattoTemplate.getMp());
    }

    public int getHp() {
        return hp;
    }

    public void setHp(int hp) {
        this.hp = hp;
    }

    public int getMp() {
        return mp;
    }

    public void setMp(int mp) {
        this.mp = mp;
    }

    public int getAtk() {
        return atk;
    }

    public void setAtk(int atk) {
        this.atk = atk;
    }

    public int getDef() {
        return def;
    }

    public void setDef(int def) {
        this.def = def;
    }

    public PetTattoTemplate getTemp() {
        return GopetManager.tattos.get(tattooTemplateId);
    }

    public String getName() {
        ArrayList<String> infoStrings = new ArrayList<String>();
        if (getAtk() > 0) {
            infoStrings.add(getAtk() + " (atk) ");
        }
        if (getDef() > 0) {
            infoStrings.add(getDef() + " (def) ");
        }
        if (getHp() > 0) {
            infoStrings.add(getHp() + " (hp) ");
        }
        if (getMp() > 0) {
            infoStrings.add(getMp() + " (mp) ");
        }

        return getTemp().getName() + "  " + String.Join(" ", infoStrings);
    }
}
