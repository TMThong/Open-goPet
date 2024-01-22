
using System.Drawing;

public class Mob : GameObject {

    protected PetTemplate petTemplate;
    private GopetPlace place;
    protected MobLvInfo mobLvInfo;
    protected MobLvlMap mobLvlMap;
    protected MobLocation mobLocation;
    private int mobId;
    private bool elite = false;

    private  PetBattle petBattle;

    public  Rectangle bound;

    public PetBattle getPetBattle() {
        return petBattle;
    }

    public void setPetBattle(PetBattle petBattle) {

        this.petBattle = petBattle;
    }

    public MobLvlMap getMobLvlMap() {
        return mobLvlMap;
    }

    public void setMobLvlMap(MobLvlMap mobLvlMap) {
        this.mobLvlMap = mobLvlMap;
    }

    public int getMobId() {
        return mobId;
    }

    public void setMobId(int mobId) {
        this.mobId = mobId;
    }

    public MobLocation getMobLocation() {
        return mobLocation;
    }

    public Mob(PetTemplate petTemplate, GopetPlace place, MobLvlMap mobLvlMap, MobLocation mobLocation) : this() {
         
        this.petTemplate = petTemplate;
        this.place = place;
        this.mobLvlMap = mobLvlMap;
        this.mobLocation = mobLocation;
        initMob();
    }

    public Mob(PetTemplate petTemplate, GopetPlace place, MobLvlMap mobLvlMap, MobLocation mobLocation, MobLvInfo mobLvInfo) : this()
    {
         
        this.petTemplate = petTemplate;
        this.place = place;
        this.mobLvlMap = mobLvlMap;
        this.mobLocation = mobLocation;
        this.mobLvInfo = mobLvInfo;
        initMob();
    }

    public Mob() {
        this.maxHp = 1;
        this.maxMp = 1;
        this.mp = 1;
    }

    public void setPetTemplate(PetTemplate petTemplate) {
        this.petTemplate = petTemplate;
    }

    public void setMobLocation(MobLocation mobLocation) {
        this.mobLocation = mobLocation;
    }

    public PetTemplate getPetTemplate() {
        return petTemplate;
    }

    public GopetPlace getPlace() {
        return place;
    }

    public MobLvInfo getMobLvInfo() {
        return mobLvInfo;
    }

    public int getHp() {
        return hp;
    }

    public void setHp(int hp) {
        this.hp = hp;
    }

    public void initMob() {
        if (mobLvInfo == null) {
            this.mobLvInfo = GopetManager.MOBLVLINFO_HASH_MAP.get(Utilities.nextInt(mobLvlMap.getLvlFrom(), mobLvlMap.getLvlTo()));
        }
        this.hp = this.mobLvInfo.getHp();
        this.maxHp = this.mobLvInfo.getHp();
        this.mp = 100 + (this.mobLvInfo.getLvl() * 10);
        this.maxMp = 100 + (this.mobLvInfo.getLvl() * 10);
        int xTile = 4;
        bound = new Rectangle(this.mobLocation.getX() - (24 * xTile), this.mobLocation.getY() - (24 * xTile), 24 * (xTile * 2), 24 * (xTile * 2));
    }

    public void addHp(int damage) {
        hp -= damage;
        if (hp < 0) {
            hp = 0;
        }
    }

    public void addHpReal(int hpPlus) {
        if (hpPlus + hp > maxHp) {
            hp = maxHp;
        } else {
            hp += hpPlus;
        }
    }

    public void setMobLvInfo(MobLvInfo mobLvInfo) {
        this.mobLvInfo = mobLvInfo;
    }

    public bool isElite() {
        return elite;
    }

    public void setElite(bool elite) {
        this.elite = elite;
    }

    public String getName() {
        return getPetTemplate().getName();
    }
}
