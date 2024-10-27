
using System.Numerics;

public class TaskTemplate
{

    public int taskId;
    public int type;
    public string name;
    public string description;
    public int[][] task;
    public int[][] gift;
    public int fromNPC;
    public int[] taskNeed;
    public long timeTask = -1;

    public int getTaskId()
    {
        return this.taskId;
    }

    public int getType()
    {
        return this.type;
    }

    public String getName(Player player)
    {
        return player.Language.TaskNameLanguage[this.taskId];
    }

    public String getDescription(Player player)
    {
        return player.Language.TaskDescLanguage[this.taskId];
    }

    public int[][] getTask(Player player)
    {
        return this.task;
    }

    public int[][] getGift()
    {
        return this.gift;
    }

    public int getFromNpc()
    {
        return this.fromNPC;
    }

    public int[] getTaskNeed()
    {
        return this.taskNeed;
    }

    public long getTimeTask()
    {
        return this.timeTask;
    }
}
