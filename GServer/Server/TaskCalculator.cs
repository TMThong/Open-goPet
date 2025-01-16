
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.Mob;
using Gopet.Util;

public class TaskCalculator
{
    /// <summary>
    /// Nhiệm vụ chính
    /// </summary>
    public const int TASK_TYPE_MAIN = 0;
    /// <summary>
    /// Nhiệm vụ cho phép lập lại nhiều lần
    /// </summary>
    public const int TASK_TYPE_LOOP = 1;
    /// <summary>
    /// Nhiệm vụ bang hội
    /// </summary>
    public const int TASK_TYPE_CLAN = 2;
    /// <summary>
    /// Nhiệm vụ đến hạn là hết ngày
    /// Qua ngày là xóa
    /// </summary>
    public const int TASK_TYPE_DAILY = 3;
    /// <summary>
    /// Nhiệm vụ diệt quái
    /// </summary>
    public const int REQUEST_KILL_MOB = 0;
    public const int REQUEST_PET_LVL = 1;
    public const int REQUEST_LEARN_SKILL_PET = 2;
    public const int REQUEST_UP_SKILL_PET = 3;
    public const int REQUEST_BUY_RANDOM_WEAPON = 4;
    public const int REQUEST_KILL_BOSS = 5;
    public const int REQUEST_UP_TIER_ITEM = 6;
    public const int REQUEST_ENCHANT_ITEM = 7;
    public const int REQUEST_NEED_TASK = 8;
    public const int REQUEST_ATTACK_BOSS = 9;
    public const int REQUEST_ITEM = 10;
    public const int REQUEST_CHALLENGE_PLACE = 11;
    public const int REQUEST_ITEM_AND_PLUS = 12;
    public const int REQUEST_UP_TIER_PET = 13;
    public const int REQUEST_PLUS_GYM_POINT = 14;
    public const int REQUEST_LEARN_SKILL2_PET = 15;
    public const int REQUEST_MEET_NPC = 16;
    public const int REQUEST_KILL_SPECIAL_BOSS = 17;
    public const int REQUEST_WAIT_NEW_TASK = 18;
    public const int REUQEST_BET_PLAYER_WIN = 19;
    public const int REQUEST_NUM_OF_FUND_CLAN = 20;
    public const int REQUEST_HIẾN_TẾ_THÚ_CƯNG = 21;
    public const int REQUEST_KILL_ELITE_BOSS = 22;
    public Player player { get; }

    private HashMap<int, JArrayList<TaskTemplate>> cacheTask = new();

    public TaskCalculator(Player player)
    {
        this.player = player;
    }

    public void update()
    {
        this.cacheTask.Clear();
        PlayerData playerData = player.playerData;
        if (playerData == null)
        {
            return;
        }

        foreach (var entry in GopetManager.npcTemplate)
        {
            int key = entry.Key;
            NpcTemplate val = entry.Value;
            JArrayList<TaskTemplate> taskFromNPC = GopetManager.taskTemplateByNpcId.get(key);
            if (taskFromNPC != null)
            {
                foreach (TaskTemplate taskTemplate in taskFromNPC)
                {
                    if (taskTemplate.type == TASK_TYPE_CLAN && player.controller.getClan() == null)
                    {
                        continue;
                    }

                    if (!playerData.wasTask.Contains(taskTemplate.getTaskId()) && !playerData.ClanTasked.Contains(taskTemplate.getTaskId()) && !playerData.tasking.Contains(taskTemplate.getTaskId()) && (taskTemplate.type == TASK_TYPE_MAIN || taskTemplate.type == TASK_TYPE_CLAN))
                    {
                        bool flag = true;
                        foreach (int taskIdNeed in taskTemplate.getTaskNeed())
                        {
                            if (!playerData.wasTask.Contains(taskIdNeed))
                            {
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            if (!this.cacheTask.ContainsKey(key))
                            {
                                this.cacheTask.put(key, new());
                            }
                            this.cacheTask.get(key).add(taskTemplate);
                        }
                    }
                }
            }
        }
    }

    public JArrayList<TaskTemplate> getTaskTemplate(int npcId)
    {
        JArrayList<TaskTemplate> taskTemplates = this.cacheTask.get(npcId);
        if (taskTemplates == null)
        {
            return new();
        }
        return taskTemplates;
    }

    public static String getTaskText(int[] task, int[][] taskInfo, long timeTask, Player player)
    {
        if (task == null)
        {
            task = new int[taskInfo.Length];
            Array.Fill(task, 0);
        }
        List<String> taskText = new();
        for (int i = 0; i < taskInfo.Length; i++)
        {
            int[] taskI = taskInfo[i];
            switch (taskI[0])
            {
                case REQUEST_KILL_MOB:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_KILL_MOB, GopetManager.PETTEMPLATE_HASH_MAP.get(taskI[2]).getName(player), task[i], taskI[1]));
                    break;
                case REQUEST_PET_LVL:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_PET_LVL, task[i], taskI[1]));
                    break;
                case REQUEST_LEARN_SKILL_PET:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_LEARN_SKILL_PET, task[i], taskI[1]));
                    break;
                case REQUEST_LEARN_SKILL2_PET:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_LEARN_SKILL2_PET, task[i], taskI[1]));
                    break;
                case REQUEST_BUY_RANDOM_WEAPON:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_BUY_RANDOM_WEAPON, task[i], taskI[1]));
                    break;
                case REQUEST_UP_SKILL_PET:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_UP_SKILL_PET, task[i], taskI[1], taskI[2]));
                    break;
                case REQUEST_KILL_SPECIAL_BOSS:
                case REQUEST_KILL_BOSS:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_KILL_BOSS, GopetManager.boss.get(taskI[2]).getName(player), task[i], taskI[1]));
                    break;
                case REQUEST_UP_TIER_ITEM:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_UP_TIER_ITEM, task[i], taskI[1], taskI[2]));
                    break;
                case REQUEST_ENCHANT_ITEM:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_ENCHANT_ITEM, task[i], taskI[1], taskI[2]));
                    break;
                case REQUEST_NEED_TASK:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_NEED_TASK, GopetManager.taskTemplate.get(taskI[2]).getName(player), task[i], taskI[1]));
                    break;
                case REQUEST_ATTACK_BOSS:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_ATTACK_BOSS, GopetManager.boss.get(taskI[2]).getName(player), task[i], taskI[1]));
                    break;
                case REQUEST_ITEM:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_ITEM, GopetManager.itemTemplate.get(taskI[2]).getName(player), task[i], taskI[1]));
                    break;
                case REQUEST_CHALLENGE_PLACE:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_CHALLENGE_PLACE, task[i], taskI[1]));
                    break;
                case REQUEST_ITEM_AND_PLUS:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_ITEM_AND_PLUS, GopetManager.itemTemplate.get(taskI[2]).getName(player), task[i], taskI[1]));
                    break;
                case REQUEST_UP_TIER_PET:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_UP_TIER_PET, task[i], taskI[1]));
                    break;
                case REQUEST_PLUS_GYM_POINT:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_PLUS_GYM_POINT, task[i], taskI[1]));
                    break;
                case REQUEST_MEET_NPC:
                    taskText.Add(Utilities.Format(player.Language.TASK_REQUEST_MEET_NPC, GopetManager.npcTemplate[taskI[2]].getName(player), task[i], taskI[1]));
                    break;
                case REUQEST_BET_PLAYER_WIN:
                    taskText.Add(string.Format(player.Language.TASK_REUQEST_BET_PLAYER_WIN, task[i], taskI[1]));
                    break;
                case REQUEST_NUM_OF_FUND_CLAN:
                    taskText.Add(string.Format(player.Language.REQUEST_NUM_OF_FUND_CLAN, task[i], taskI[1]));
                    break;
                case REQUEST_WAIT_NEW_TASK:
                    taskText.Add(player.Language.TASK_REQUEST_WAIT_NEW_TASK);
                    break;
                case REQUEST_HIẾN_TẾ_THÚ_CƯNG:
                    taskText.Add(string.Format(player.Language.TASK_REQUEST_SACRIFICE_PET, taskI[2], task[i], taskI[1]));
                    break;
                case REQUEST_KILL_ELITE_BOSS:
                    taskText.Add(string.Format(player.Language.REQUEST_KILL_ELITE_BOSS, task[i], taskI[1]));
                    break;
            }
        }
        return "\n  ---- " + player.Language.Request + " ----\n" + String.Join("\n", taskText);
    }

    public void onTaskUpdate(TaskData taskData, int taskRequestType, params object[] dObjects)
    {
        for (int i = 0; i < taskData.taskInfo.Length; i++)
        {
            if (taskData.task[i] < taskData.taskInfo[i][1] && taskData.taskInfo[i][0] == taskRequestType)
            {
                switch (taskRequestType)
                {
                    case REQUEST_KILL_MOB:
                        {
                            int mobId = (int)dObjects[0];
                            if (taskData.taskInfo[i][2] == mobId)
                            {
                                taskData.task[i]++;
                            }
                        }
                        break;

                    case REQUEST_PET_LVL:
                        {
                            Pet pet = (Pet)dObjects[0];
                            if (taskData.task[i] < pet.lvl)
                            {
                                taskData.task[i] = pet.lvl;
                            }
                        }
                        break;
                    case REQUEST_LEARN_SKILL2_PET:
                    case REQUEST_BUY_RANDOM_WEAPON:
                    case REQUEST_LEARN_SKILL_PET:
                        taskData.task[i]++;
                        break;

                    case REQUEST_UP_SKILL_PET:
                        {
                            int skillLv = (int)dObjects[0];
                            if (skillLv >= taskData.taskInfo[i][2])
                            {
                                taskData.task[i]++;
                            }
                        }
                        break;
                    case REQUEST_KILL_SPECIAL_BOSS:
                    case REQUEST_KILL_BOSS:
                        {
                            Boss boss = (Boss)dObjects[0];
                            if (taskData.taskInfo[i][2] == boss.Template.bossId)
                            {
                                taskData.task[i]++;
                            }
                        }
                        break;

                    case REQUEST_UP_TIER_ITEM:
                        {
                            int tier = (int)dObjects[0];
                            if (tier >= taskData.taskInfo[i][2])
                            {
                                taskData.task[i]++;
                            }
                        }
                        break;

                    case REQUEST_MEET_NPC:
                        {
                            int npc = (int)dObjects[0];
                            if (npc == taskData.taskInfo[i][2] && taskData.taskInfo[i][1] > taskData.task[i])
                            {
                                taskData.task[i]++;
                            }
                        }
                        break;

                    case REQUEST_ENCHANT_ITEM:
                        {
                            int lvl = (int)dObjects[0];
                            if (lvl >= taskData.taskInfo[i][2])
                            {
                                taskData.task[i]++;
                            }
                        }
                        break;

                    case REQUEST_NEED_TASK:
                        {
                            int taskId = (int)dObjects[0];
                            if (taskData.taskInfo[i][2] == taskId)
                            {
                                taskData.task[i]++;
                            }
                        }
                        break;

                    case REQUEST_ATTACK_BOSS:
                        {
                            Boss boss = (Boss)dObjects[0];
                            if (taskData.taskInfo[i][2] == boss.Template.bossId)
                            {
                                taskData.task[i]++;
                            }
                        }
                        break;

                    case REQUEST_ITEM:
                        {
                            Item item = (Item)dObjects[0];
                            if (taskData.taskInfo[i][2] == item.itemTemplateId)
                            {
                                if (taskData.task[i] < item.count)
                                {
                                    taskData.task[i] = taskData.taskInfo[i][1];
                                }
                                else
                                {
                                    taskData.task[i] = item.count;
                                }
                            }
                        }
                        break;

                    case REQUEST_CHALLENGE_PLACE:
                        {
                            int turn = (int)dObjects[0];
                            if (turn > taskData.task[i])
                            {
                                taskData.task[i] = turn;
                            }
                        }
                        break;
                    case REUQEST_BET_PLAYER_WIN:
                    case REQUEST_PLUS_GYM_POINT:
                    case REQUEST_UP_TIER_PET:
                    case REQUEST_NUM_OF_FUND_CLAN:
                        taskData.task[i]++;
                        break;
                    case REQUEST_HIẾN_TẾ_THÚ_CƯNG:
                        {
                            int lvl = (int)dObjects[0];
                            if (taskData.taskInfo[i][2] <= lvl)
                            {
                                taskData.task[i]++;
                            }
                        }
                        break;
                }
            }
        }
    }

    public void onAllTaskUpdate(int taskRequestType, params object[] dObjects)
    {
        foreach (TaskData taskData in getTaskDatas())
        {
            this.onTaskUpdate(taskData, taskRequestType, dObjects);
        }
    }

    public bool TryCheckPetSacrifice(Pet pet) => getTaskDatas().Any(m => m.taskInfo.Any(t => t[0] == REQUEST_HIẾN_TẾ_THÚ_CƯNG && t[2] <= pet.lvl));

    public void OnSacrifice(int petLvl)
    {
        this.onAllTaskUpdate(REQUEST_HIẾN_TẾ_THÚ_CƯNG, petLvl);
    }


    public void onWinBetBattle()
    {
        this.onAllTaskUpdate(REUQEST_BET_PLAYER_WIN);
    }

    public void onUpTierPet()
    {
        this.onAllTaskUpdate(REQUEST_UP_TIER_PET);
    }

    public void onPlusGymPoint()
    {
        this.onAllTaskUpdate(REQUEST_PLUS_GYM_POINT);
    }

    public void onItemNeed(Item item)
    {
        this.onAllTaskUpdate(REQUEST_ITEM, item);
    }

    public void onAttackBoss(Boss boss)
    {
        this.onAllTaskUpdate(REQUEST_ATTACK_BOSS, boss);
    }

    public void onItemEnchant(Item item)
    {
        this.onAllTaskUpdate(REQUEST_ENCHANT_ITEM, item.lvl);
    }

    public void onUpTierItem(int tier)
    {
        this.onAllTaskUpdate(REQUEST_UP_TIER_ITEM, tier);
    }

    public void onMeetNpc(int npcId)
    {
        this.onAllTaskUpdate(REQUEST_MEET_NPC, npcId);
    }

    public void onKillBoss(Boss boss)
    {
        this.onAllTaskUpdate(REQUEST_KILL_BOSS, boss);
        this.onAllTaskUpdate(REQUEST_KILL_SPECIAL_BOSS, boss);
    }

    public void onDonateFund()
    {
        this.onAllTaskUpdate(REQUEST_NUM_OF_FUND_CLAN);
    }

    public void onUpdateSkillPet(Pet pet, int skillLv)
    {
        if (pet == null)
        {
            return;
        }
        this.onAllTaskUpdate(REQUEST_UP_SKILL_PET, skillLv);
    }

    public void onBuyRandomWeapon()
    {
        this.onAllTaskUpdate(REQUEST_BUY_RANDOM_WEAPON);
    }

    public void onLearnSkillPet()
    {
        this.onAllTaskUpdate(REQUEST_LEARN_SKILL_PET);
    }
    public void onLearnSkillPet2()
    {
        this.onAllTaskUpdate(REQUEST_LEARN_SKILL2_PET);
    }


    public void onPetUpLevel(Pet pet)
    {
        if (pet == null)
        {
            return;
        }

        {
            this.onAllTaskUpdate(REQUEST_PET_LVL, pet);
        }
    }

    private void updatePetLvlViaAll()
    {
        foreach (Pet pet in player.playerData.pets)
        {
            onPetUpLevel(pet);
        }

        foreach (Item item in player.playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY))
        {
            this.onItemNeed(item);
        }

        onPetUpLevel(player.playerData.petSelected);
    }

    public void onNextChellengePlace(int turn)
    {
        this.onAllTaskUpdate(REQUEST_CHALLENGE_PLACE, turn);
    }

    public void onKillMob(int mobId)
    {
        this.onAllTaskUpdate(REQUEST_KILL_MOB, mobId);
    }

    public void onTaskSucces(TaskData taskData)
    {
        switch (taskData.getTemplate().getType())
        {
            case TASK_TYPE_MAIN:
                player.playerData.wasTask.Add(taskData.taskTemplateId);
                break;
        }
        getTaskDatas().remove(taskData);
        player.playerData.tasking.remove(taskData.taskTemplateId);
        switch (taskData.getTemplate().type)
        {
            case TASK_TYPE_CLAN:
                player.playerData.ClanTasked.Add(taskData.taskTemplateId);
                break;
        }
        JArrayList<Popup> list = player.controller.onReiceiveGift(taskData.gift);
        JArrayList<String> txtInfo = new();
        foreach (Popup petBattleText in list)
        {
            txtInfo.add(petBattleText.getText());
        }
        player.okDialog(string.Format(player.Language.OnTaskSuccess, taskData.getTemplate().getName(player), String.Join(",", txtInfo)));
        this.onAllTaskUpdate(REQUEST_NEED_TASK, taskData.taskTemplateId);
    }

    public CopyOnWriteArrayList<TaskData> getTaskDatas()
    {
        if (this.player.playerData == null)
        {
            return new();
        }
        return this.player.playerData.task;
    }

    public void onUpdateTask(TaskData taskData)
    {
        foreach (Pet pet in player.playerData.pets)
        {

            this.onTaskUpdate(taskData, REQUEST_PET_LVL, pet);
            if (pet.skill.Length > 0)
            {
                this.onTaskUpdate(taskData, REQUEST_LEARN_SKILL_PET);
                for (global::System.Int32 i = 0; i < pet.skill.Length; i++)
                {
                    this.onTaskUpdate(taskData, REQUEST_UP_SKILL_PET, pet.skill[i][1]);
                }
            }

            if (pet.skill.Length > 1)
            {
                this.onTaskUpdate(taskData, REQUEST_LEARN_SKILL2_PET);
            }
        }

        Pet currentPet = player.getPet();
        if (currentPet != null)
        {


            this.onTaskUpdate(taskData, REQUEST_PET_LVL, currentPet);
            if (currentPet.skill.Length > 0)
            {
                this.onTaskUpdate(taskData, REQUEST_LEARN_SKILL_PET);
            }
            if (currentPet.skill.Length > 1)
            {
                this.onTaskUpdate(taskData, REQUEST_LEARN_SKILL2_PET);
            }
        }

        foreach (int id in player.playerData.wasTask)
        {
            this.onTaskUpdate(taskData, REQUEST_NEED_TASK, id);
        }

        foreach (var itemKey in player.playerData.items)
        {
            foreach (var item in itemKey.Value)
            {
                this.onAllTaskUpdate(REQUEST_ENCHANT_ITEM, item.lvl);
            }
        }
    }

    public bool taskSuccess(TaskData taskData)
    {
        for (int i = 0; i < taskData.taskInfo.Length; i++)
        {
            int[] taskI = taskData.taskInfo[i];
            if (taskI[1] > taskData.task[i])
            {
                return false;
            }
        }
        return true;
    }
}
