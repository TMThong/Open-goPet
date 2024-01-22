 
public class PetBattleInfo {

    private int turn = 0;
    private ArrayList<Buff> buffs = new();
    private HashMap<int, int> skill_cooldown = new();
    private bool isPlayer = false;
    private Player player;

    public PetBattleInfo() {

    }

    public PetBattleInfo(Player player) {
        this.player = player;
        this.setIsPlayer(player != null);
    }

    public Player getPlayer() {
        return player;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public int getTurn() {
        return turn;
    }

    public void setTurn(int turn) {
        this.turn = turn;
    }

    public ArrayList<Buff> getBuffs() {
        return buffs;
    }

    public   void addBuff(Buff buff) {
        buffs.add(buff);
    }

    public void setBuffs(ArrayList<Buff> buffs) {
        this.buffs = buffs;
    }

    public bool isIsPlayer() {
        return isPlayer;
    }

    public void setIsPlayer(bool isPlayer) {
        this.isPlayer = isPlayer;
    }

    public void nextTurn() {
        for (Iterator<Buff> iterator = buffs.iterator(); iterator.hasNext();) {
            Buff buff = iterator.next();
            buff.turn--;
            if (buff.turn <= 0) {
                iterator.remove();
            }
        }
        turn++;
        ArrayList<int> skill_cooldownArrayList = new();
        for (Map.Entry<int, int> entry : skill_cooldown.entrySet()) {
            int key = entry.getKey();
            int val = entry.getValue();
            if (val - 1 <= 0) {
                skill_cooldownArrayList.add(key);
            } else {
                skill_cooldown.put(key, val - 1);
            }
        }
        for (int int : skill_cooldownArrayList) {
            skill_cooldown.remove(int);
        }
    }

    public ItemInfo[] getBuff() {
        ItemInfo[] itemInfos = new ItemInfo[GopetManager.itemInfoName.size()];
        for (int i = 0; i < itemInfos.Length; i++) {
            itemInfos[i] = new ItemInfo(i, 0);
        }
        for (Buff buff : buffs) {
            for (ItemInfo info : buff.infos) {
                itemInfos[info.id].value += info.value;
            }
        }
        return itemInfos;
    }

    public bool isCoolDown(int skillId) {
        return skill_cooldown.containsKey(skillId);
    }

    public void addSkillCoolDown(int skillId, int turn) {
        this.skill_cooldown.put(skillId, turn);
    }

    public int getTurnCoolDown(int skillId) {
        if (!isCoolDown(skillId)) {
            return 0;
        }
        return this.skill_cooldown.get(skillId);
    }
}
