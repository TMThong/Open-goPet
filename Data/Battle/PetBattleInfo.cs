 
public class PetBattleInfo {

    private int turn = 0;
    private ArrayList<Buff> buffs = new ArrayList<>();
    private HashMap<Integer, Integer> skill_cooldown = new HashMap<>();
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

    public final void addBuff(Buff buff) {
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
        ArrayList<Integer> skill_cooldownArrayList = new ArrayList<>();
        for (Map.Entry<Integer, Integer> entry : skill_cooldown.entrySet()) {
            Integer key = entry.getKey();
            Integer val = entry.getValue();
            if (val - 1 <= 0) {
                skill_cooldownArrayList.add(key);
            } else {
                skill_cooldown.put(key, val - 1);
            }
        }
        for (Integer integer : skill_cooldownArrayList) {
            skill_cooldown.remove(integer);
        }
    }

    public ItemInfo[] getBuff() {
        ItemInfo[] itemInfos = new ItemInfo[GopetManager.itemInfoName.size()];
        for (int i = 0; i < itemInfos.length; i++) {
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
