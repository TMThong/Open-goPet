
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.Mob;
using Gopet.Util;

public class TaskCalculator
{

    public const int TASK_TYPE_MAIN = 0;
    public const int TASK_TYPE_LOOP = 1;
    public const int TASK_TYPE_CLAN = 2;
    public const int TASK_TYPE_DAILY = 3;

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
    private Player player;

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
                    if (!playerData.wasTask.Contains(taskTemplate.getTaskId()) && !playerData.tasking.Contains(taskTemplate.getTaskId()))
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

    public static String getTaskText(int[] task, int[][] taskInfo, long timeTask)
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
                    taskText.Add(Utilities.Format("Tiêu diệt %s %s / %s", GopetManager.PETTEMPLATE_HASH_MAP.get(taskI[2]).name, task[i], taskI[1]));
                    break;
                case REQUEST_PET_LVL:
                    taskText.Add(Utilities.Format("Pet đạt đạt cấp %s / %s", task[i], taskI[1]));
                    break;
                case REQUEST_LEARN_SKILL_PET:
                    taskText.Add(Utilities.Format("Học %s / %s kỹ năng cho thú cưng", task[i], taskI[1]));
                    break;
                case REQUEST_LEARN_SKILL2_PET:
                    taskText.Add(Utilities.Format("Học %s / %s kỹ năng 2 cho thú cưng", task[i], taskI[1]));
                    break;
                case REQUEST_BUY_RANDOM_WEAPON:
                    taskText.Add(Utilities.Format("Mua %s / %s vũ khí bất kì của cửa hàng ở Tp Linh Thú", task[i], taskI[1]));
                    break;
                case REQUEST_UP_SKILL_PET:
                    taskText.Add(Utilities.Format("Nâng cấp %s / %s kỹ năng pet lên cấp %s", task[i], taskI[1], taskI[2]));
                    break;
                case REQUEST_KILL_SPECIAL_BOSS:
                case REQUEST_KILL_BOSS:
                    taskText.Add(Utilities.Format("Tiêu diệt %s(tinh anh) %s / %s", GopetManager.boss.get(taskI[2]).name, task[i], taskI[1]));
                    break;
                case REQUEST_UP_TIER_ITEM:
                    taskText.Add(Utilities.Format("Tiến hóa trang bị thú cưng %s / %s lần lên đời %s", task[i], taskI[1], taskI[2]));
                    break;
                case REQUEST_ENCHANT_ITEM:
                    taskText.Add(Utilities.Format("Cường hóa trang bị thú cưng %s / %s lần lên cấp %s", task[i], taskI[1], taskI[2]));
                    break;
                case REQUEST_NEED_TASK:
                    taskText.Add(Utilities.Format("Hoàn thành nhiệm vụ %s %s / %s", GopetManager.taskTemplate.get(taskI[2]).getName(), task[i], taskI[1]));
                    break;
                case REQUEST_ATTACK_BOSS:
                    taskText.Add(Utilities.Format("Tấn công %s(tinh anh) %s / %s", GopetManager.boss.get(taskI[2]).name, task[i], taskI[1]));
                    break;
                case REQUEST_ITEM:
                    taskText.Add(Utilities.Format("Vật phẩm %s %s / %s", GopetManager.itemTemplate.get(taskI[2]).getName(), task[i], taskI[1]));
                    break;
                case REQUEST_CHALLENGE_PLACE:
                    taskText.Add(Utilities.Format("Vượt ải Đấu Trường tới ải %s / %s", task[i], taskI[1]));
                    break;
                case REQUEST_ITEM_AND_PLUS:
                    taskText.Add(Utilities.Format("Vật phẩm %s %s / %s (vật phẩm sẽ tự trừ)", GopetManager.itemTemplate.get(taskI[2]).getName(), task[i], taskI[1]));
                    break;
                case REQUEST_UP_TIER_PET:
                    taskText.Add(Utilities.Format("Tiến hóa thú cưng %s / %s lần", task[i], taskI[1]));
                    break;
                case REQUEST_PLUS_GYM_POINT:
                    taskText.Add(Utilities.Format("Dùng điểm gym %s / %s lần", task[i], taskI[1]));
                    break;
                case REQUEST_MEET_NPC:
                    taskText.Add(Utilities.Format($"Gặp {GopetManager.npcTemplate[taskI[2]].name} và trò chuyện %s / %s lần", task[i], taskI[1]));
                    break;
                case REUQEST_BET_PLAYER_WIN:
                    taskText.Add($"Thách đấu thắng người chơi {task[i]}/{taskI[1]} lần");
                    break;
                case REQUEST_WAIT_NEW_TASK:
                    taskText.Add("Con hãy chờ nhiệm vụ mới nhé !!!");
                    break;
            }
        }
        return "\n  ---- Yêu cầu ----\n" + String.Join("\n", taskText);
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
                        taskData.task[i]++;
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

        {
            this.onAllTaskUpdate(REQUEST_ENCHANT_ITEM, item.lvl);
        }
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

    public void onUpdateSkillPet(Pet pet, int skillLv)
    {
        if (pet == null)
        {
            return;
        }

        {
            this.onAllTaskUpdate(REQUEST_UP_SKILL_PET, skillLv);
        }
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
                player.playerData.wasTask.add(taskData.taskTemplateId);
                break;
        }
        getTaskDatas().remove(taskData);
        player.playerData.tasking.remove(taskData.taskTemplateId);
        JArrayList<Popup> list = player.controller.onReiceiveGift(taskData.gift);
        JArrayList<String> txtInfo = new();
        foreach (Popup petBattleText in list)
        {
            txtInfo.add(petBattleText.getText());
        }
        player.okDialog(Utilities.Format("Chức mừng bạn hoàn thành nhiệm vụ %s nhận được in\n%s", taskData.getTemplate().getName(), String.Join(",", txtInfo)));
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
