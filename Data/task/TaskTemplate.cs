
public class TaskTemplate
{

    public int taskId;
    public int type;
    public String name;
    public String description;
    public String guide = "";
    public int[][] task;
    public int[][] gift;
    public int fromNpc;
    public int[] taskNeed;
    public long timeTask = -1;

    public void setTaskId(int taskId)
    {
        this.taskId = taskId;
    }

    public void setType(int type)
    {
        this.type = type;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setDescription(String description)
    {
        this.description = description;
    }

    public void setGuide(String guide)
    {
        this.guide = guide;
    }

    public void setTask(int[][] task)
    {
        this.task = task;
    }

    public void setGift(int[][] gift)
    {
        this.gift = gift;
    }

    public void setFromNpc(int fromNpc)
    {
        this.fromNpc = fromNpc;
    }

    public void setTaskNeed(int[] taskNeed)
    {
        this.taskNeed = taskNeed;
    }

    public void setTimeTask(long timeTask)
    {
        this.timeTask = timeTask;
    }

    public int getTaskId()
    {
        return this.taskId;
    }

    public int getType()
    {
        return this.type;
    }

    public String getName()
    {
        return this.name;
    }

    public String getDescription()
    {
        return this.description;
    }

    public String getGuide()
    {
        return this.guide;
    }

    public int[][] getTask()
    {
        return this.task;
    }

    public int[][] getGift()
    {
        return this.gift;
    }

    public int getFromNpc()
    {
        return this.fromNpc;
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
