using Gopet.Data.Collections;
using Gopet.Data.GopetItem;

namespace Gopet.Battle
{
    public class PetBattleInfo
    {

        private int turn = 0;
        private JArrayList<Buff> buffs = new();
        private HashMap<int, int> skill_cooldown = new();
        private bool isPlayer = false;
        private Player player;

        public PetBattleInfo()
        {

        }

        public PetBattleInfo(Player player)
        {
            this.player = player;
            setIsPlayer(player != null);
        }

        public Player getPlayer()
        {
            return player;
        }

        public void setPlayer(Player player)
        {
            this.player = player;
        }

        public int getTurn()
        {
            return turn;
        }

        public void setTurn(int turn)
        {
            this.turn = turn;
        }

        public JArrayList<Buff> getBuffs()
        {
            return buffs;
        }

        public void addBuff(Buff buff)
        {
            buffs.add(buff);
        }

        public void setBuffs(JArrayList<Buff> buffs)
        {
            this.buffs = buffs;
        }

        public bool isIsPlayer()
        {
            return isPlayer;
        }

        public void setIsPlayer(bool isPlayer)
        {
            this.isPlayer = isPlayer;
        }

        public void nextTurn()
        {
            foreach (Buff buff in buffs.ToArray())
            {
                buff.turn--;
                if (buff.turn <= 0)
                {
                    buffs.Remove(buff);
                }
            }
            turn++;
            JArrayList<int> skill_cooldownArrayList = new();
            foreach (var entry in skill_cooldown)
            {
                int key = entry.Key;
                int val = entry.Value;
                if (val - 1 <= 0)
                {
                    skill_cooldownArrayList.add(key);
                }
                else
                {
                    skill_cooldown.put(key, val - 1);
                }
            }
            foreach (var i in skill_cooldownArrayList)
            {
                skill_cooldown.Remove(i);
            }
        }

        public ItemInfo[] getBuff()
        {
            ItemInfo[] itemInfos = new ItemInfo[GopetManager.itemInfoName.Count()];
            for (int i = 0; i < itemInfos.Length; i++)
            {
                itemInfos[i] = new ItemInfo(i, 0);
            }
            foreach (Buff buff in buffs)
            {
                foreach (ItemInfo info in buff.infos)
                {
                    itemInfos[info.id].value += info.value;
                }
            }
            return itemInfos;
        }

        public bool isCoolDown(int skillId)
        {
            return skill_cooldown.ContainsKey(skillId);
        }

        public void addSkillCoolDown(int skillId, int turn)
        {
            skill_cooldown.put(skillId, turn);
        }

        public int getTurnCoolDown(int skillId)
        {
            if (!isCoolDown(skillId))
            {
                return 0;
            }
            return skill_cooldown.get(skillId);
        }
    }
}