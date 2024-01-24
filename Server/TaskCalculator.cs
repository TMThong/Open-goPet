
using Gopet.Data.Collections;

public class TaskCalculator {

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
    private Player player;

    private HashMap<int, ArrayList<TaskTemplate>> cacheTask = new();

    public TaskCalculator(Player player) {
        this.player = player;
    }

    public void update() {
        this.cacheTask.Clear();
        PlayerData playerData = player.playerData;
        if (playerData == null) {
            return;
        }

        for (Map.Entry<int, NpcTemplate> entry : GopetManager.npcTemplate ) {
            int key = entry.Key;
            NpcTemplate val = entry.Value;
            ArrayList<TaskTemplate> taskFromNPC = GopetManager.taskTemplateByNpcId.get(key);
            if (taskFromNPC != null) {
                for (TaskTemplate taskTemplate : taskFromNPC) {
                    if (!playerData.wasTask.Contains(taskTemplate.getTaskId()) && !playerData.tasking.Contains(taskTemplate.getTaskId())) {
                        bool flag = true;
                        for (int taskIdNeed : taskTemplate.getTaskNeed()) {
                            if (!playerData.wasTask.Contains(taskIdNeed)) {
                                flag = false;
                                break;
                            }
                        }

                        if (flag) {
                            if (!this.cacheTask.ContainsKey(key)) {
                                this.cacheTask.put(key, new());
                            }
                            this.cacheTask.get(key).add(taskTemplate);
                        }
                    }
                }
            }
        }
    }

    public ArrayList<TaskTemplate> getTaskTemplate(int npcId) {
        ArrayList<TaskTemplate> taskTemplates = this.cacheTask.get(npcId);
        if (taskTemplates == null) {
            return new();
        }
        return taskTemplates;
    }

    public static String getTaskText(int[] task, int[][] taskInfo, long timeTask) {
        if (task == null) {
            task = new int[taskInfo.Length];
            Array.Fill(task, 0);
        }
        ArrayList<String> taskText = new();
        for (int i = 0; i < taskInfo.Length; i++) {
            int[] taskI = taskInfo[i];
            switch (taskI[0]) {
                case REQUEST_KILL_MOB:
                    taskText.add(Utilities.Format("Tiêu diệt %s %s / %s", GopetManager.PETTEMPLATE_HASH_MAP.get(taskI[2]).getName(), task[i], taskI[1]));
                    break;
                case REQUEST_PET_LVL:
                    taskText.add(Utilities.Format("Pet đạt đạt cấp %s / %s", task[i], taskI[1]));
                    break;
                case REQUEST_LEARN_SKILL_PET:
                    taskText.add(Utilities.Format("Học %s / %s kỹ năng cho thú cưng", task[i], taskI[1]));
                    break;
                case REQUEST_BUY_RANDOM_WEAPON:
                    taskText.add(Utilities.Format("Mua %s / %s vũ khí bất kì của cửa hàng ở Tp Linh Thú", task[i], taskI[1]));
                    break;
                case REQUEST_UP_SKILL_PET:
                    taskText.add(Utilities.Format("Nâng cấp %s / %s kỹ năng pet lên cấp %s", task[i], taskI[1], taskI[2]));
                    break;
                case REQUEST_KILL_BOSS:
                    taskText.add(Utilities.Format("Tiêu diệt %s(tinh anh) %s / %s", GopetManager.boss.get(taskI[2]).getName(), task[i], taskI[1]));
                    break;
                case REQUEST_UP_TIER_ITEM:
                    taskText.add(Utilities.Format("Tiến hóa trang bị thú cưng %s / %s lần lên đời %s", task[i], taskI[1], taskI[2]));
                    break;
                case REQUEST_ENCHANT_ITEM:
                    taskText.add(Utilities.Format("Cường hóa trang bị thú cưng %s / %s lần lên cấp %s", task[i], taskI[1], taskI[2]));
                    break;
                case REQUEST_NEED_TASK:
                    taskText.add(Utilities.Format("Hoàn thành nhiệm vụ %s %s / %s", GopetManager.taskTemplate.get(taskI[2]).getName(), task[i], taskI[1]));
                    break;
                case REQUEST_ATTACK_BOSS:
                    taskText.add(Utilities.Format("Tấn công %s(tinh anh) %s / %s", GopetManager.boss.get(taskI[2]).getName(), task[i], taskI[1]));
                    break;
                case REQUEST_ITEM:
                    taskText.add(Utilities.Format("Vật phẩm %s %s / %s", GopetManager.itemTemplate.get(taskI[2]).getName(), task[i], taskI[1]));
                    break;
                case REQUEST_CHALLENGE_PLACE:
                    taskText.add(Utilities.Format("Vượt ải Đấu Trường tới ải %s / %s", task[i], taskI[1]));
                    break;
                case REQUEST_ITEM_AND_PLUS:
                    taskText.add(Utilities.Format("Vật phẩm %s %s / %s (vật phẩm sẽ tự trừ)", GopetManager.itemTemplate.get(taskI[2]).getName(), task[i], taskI[1]));
                    break;
                case REQUEST_UP_TIER_PET:
                    taskText.add(Utilities.Format("Tiến hóa thú cưng %s / %s lần", task[i], taskI[1]));
                    break;
            }
        }
        return "\n  ---- Yêu cầu ----\n" + String.Join("\n", taskText);
    }

    public void onTaskUpdate(TaskData taskData, int taskRequestType, params object[] dObjects) {
        for (int i = 0; i < taskData.taskInfo.Length; i++) {
            if (taskData.task[i] < taskData.taskInfo[i][1] && taskData.taskInfo[i][0] == taskRequestType) {
                switch (taskRequestType) {
                    case REQUEST_KILL_MOB: {
                        int mobId = (int) dObjects[0];
                        if (taskData.taskInfo[i][2] == mobId) {
                            taskData.task[i]++;
                        }
                    }
                    break;

                    case REQUEST_PET_LVL: {
                        Pet pet = (Pet) dObjects[0];
                        if (taskData.task[i] < pet.lvl) {
                            taskData.task[i] = pet.lvl;
                        }
                    }
                    break;

                    case REQUEST_BUY_RANDOM_WEAPON:
                    case REQUEST_LEARN_SKILL_PET:
                        taskData.task[i]++;
                        break;

                    case REQUEST_UP_SKILL_PET: {
                        int skillLv = (int) dObjects[0];
                        if (skillLv >= taskData.taskInfo[i][2]) {
                            taskData.task[i]++;
                        }
                    }
                    break;

                    case REQUEST_KILL_BOSS: {
                        Boss boss = (Boss) dObjects[0];
                        if (taskData.taskInfo[i][2] == boss.getBossTemplate().getBossId()) {
                            taskData.task[i]++;
                        }
                    }
                    break;

                    case REQUEST_UP_TIER_ITEM: {
                        int tier = (int) dObjects[0];
                        if (tier >= taskData.taskInfo[i][2]) {
                            taskData.task[i]++;
                        }
                    }
                    break;

                    case REQUEST_ENCHANT_ITEM: {
                        int lvl = (int) dObjects[0];
                        if (lvl >= taskData.taskInfo[i][2]) {
                            taskData.task[i]++;
                        }
                    }
                    break;

                    case REQUEST_NEED_TASK: {
                        int taskId = (int) dObjects[0];
                        if (taskData.taskInfo[i][2] == taskId) {
                            taskData.task[i]++;
                        }
                    }
                    break;

                    case REQUEST_ATTACK_BOSS: {
                        Boss boss = (Boss) dObjects[0];
                        if (taskData.taskInfo[i][2] == boss.getBossTemplate().getBossId()) {
                            taskData.task[i]++;
                        }
                    }
                    break;

                    case REQUEST_ITEM: {
                        Item item = (Item) dObjects[0];
                        if (taskData.taskInfo[i][2] == item.itemTemplateId) {
                            if (taskData.task[i] < item.count) {
                                taskData.task[i] = taskData.taskInfo[i][1];
                            } else {
                                taskData.task[i] = item.count;
                            }
                        }
                    }
                    break;

                    case REQUEST_CHALLENGE_PLACE: {
                        int turn = (int) dObjects[0];
                        if (turn > taskData.task[i]) {
                            taskData.task[i] = turn;
                        }
                    }
                    break;

                    case REQUEST_UP_TIER_PET:
                        taskData.task[i]++;
                        break;
                }
            }
        }
    }

    public void onAllTaskUpdate(int taskRequestType, Object... dObjects) {
        for (TaskData taskData : getTaskDatas()) {
            this.onTaskUpdate(taskData, taskRequestType, dObjects);
        }
    }

    public void onUpTierPet() {
        this.onAllTaskUpdate(REQUEST_UP_TIER_PET);
    }

    public void onItemNeed(Item item) {
        this.onAllTaskUpdate(REQUEST_ITEM, item);
    }

    public void onAttackBoss(Boss boss) {
        this.onAllTaskUpdate(REQUEST_ATTACK_BOSS, boss);
    }

    public void onItemEnchant(Item item) {
        if (!item.wasSell) {
            this.onAllTaskUpdate(REQUEST_ENCHANT_ITEM, item.lvl);
        }
    }

    public void onUpTierItem(int tier) {
        this.onAllTaskUpdate(REQUEST_UP_TIER_ITEM, tier);
    }

    public void onKillBoss(Boss boss) {
        this.onAllTaskUpdate(REQUEST_KILL_BOSS, boss);
    }

    public void onUpdateSkillPet(Pet pet, int skillLv) {
        if (pet == null) {
            return;
        }
        if (!pet.wasSell) {
            this.onAllTaskUpdate(REQUEST_UP_SKILL_PET, skillLv);
        }
    }

    public void onBuyRandomWeapon() {
        this.onAllTaskUpdate(REQUEST_BUY_RANDOM_WEAPON);
    }

    public void onLearnSkillPet() {
        this.onAllTaskUpdate(REQUEST_LEARN_SKILL_PET);
    }

    public void onPetUpLevel(Pet pet) {
        if (pet == null) {
            return;
        }
        if (!pet.wasSell) {
            this.onAllTaskUpdate(REQUEST_PET_LVL, pet);
        }
    }

    private void updatePetLvlViaAll() {
        for (Pet pet : player.playerData.pets) {
            onPetUpLevel(pet);
        }

        for (Item item : player.playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY)) {
            this.onItemNeed(item);
        }

        onPetUpLevel(player.playerData.petSelected);
    }

    public void onNextChellengePlace(int turn) {
        this.onAllTaskUpdate(REQUEST_CHALLENGE_PLACE, turn);
    }

    public void onKillMob(int mobId)   {
        this.onAllTaskUpdate(REQUEST_KILL_MOB, mobId);
    }

    public void onTaskSucces(TaskData taskData)   {
        switch (taskData.getTemplate().getType()) {
            case TASK_TYPE_MAIN:
                player.playerData.wasTask.add(taskData.taskTemplateId);
                break;
        }
        getTaskDatas().remove(taskData);
        player.playerData.tasking.remove((Object) taskData.taskTemplateId);
        ArrayList<Popup> list = player.controller.onReiceiveGift(taskData.gift);
        ArrayList<String> txtInfo = new();
        for (Popup petBattleText : list) {
            txtInfo.add(petBattleText.getText());
        }
        player.okDialog(Utilities.Format("Chức mừng bạn hoàn thành nhiệm vụ %s nhận được :\n%s", taskData.getTemplate().getName(), String.Join(",", txtInfo)));
        this.onAllTaskUpdate(REQUEST_NEED_TASK, taskData.taskTemplateId);
    }

    public CopyOnWriteArrayList<TaskData> getTaskDatas() {
        if (this.player.playerData == null) {
            returnnew ();
        }
        return this.player.playerData.task;
    }

    public void onUpdateTask(TaskData taskData) {
        for (Pet pet : player.playerData.pets) {
            if (!pet.wasSell) {
                this.onTaskUpdate(taskData, REQUEST_PET_LVL, pet);
                if (pet.skill.Length > 0) {
                    this.onTaskUpdate(taskData, REQUEST_LEARN_SKILL_PET);
                }
            }
        }

        Pet currentPet = player.getPet();
        if (currentPet != null) {
            if (!currentPet.wasSell) {
                this.onTaskUpdate(taskData, REQUEST_PET_LVL, currentPet);
               if (currentPet.skill.Length > 0) {
                    this.onTaskUpdate(taskData, REQUEST_LEARN_SKILL_PET);
                }
            }
        }

        for (int int : player.playerData.wasTask) {
            this.onTaskUpdate(taskData, REQUEST_NEED_TASK, int);
        }
    }

    public bool taskSuccess(TaskData taskData) {
        for (int i = 0; i < taskData.taskInfo.Length; i++) {
            int[] taskI = taskData.taskInfo[i];
            if (taskI[1] > taskData.task[i]) {
                return false;
            }
        }
        return true;
    }
}
