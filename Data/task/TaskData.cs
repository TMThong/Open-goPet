
public class TaskData
{

    public int taskTemplateId;
    public int[][] taskInfo;
    public int[] task;
    public int[][] gift;
    public long timeTask;
    public bool wasShowDialog = false;
    public DateTime? ExpireTask { get; set; } = null;
    public bool CanCancelTask { get; set; } = true;

    public TaskData()
    {

    }

    public TaskData(TaskTemplate taskTemplate)
    {
        this.taskTemplateId = taskTemplate.taskId;
        this.taskInfo = taskTemplate.task;
        this.gift = taskTemplate.gift;
        this.timeTask = taskTemplate.timeTask;
        this.task = new int[taskInfo.Length];
        Array.Fill(this.task, 0);
        switch (taskTemplate.type)
        {
            case TaskCalculator.TASK_TYPE_CLAN:
                this.CanCancelTask = false;
                break;
        }
    }

    public TaskTemplate getTemplate()
    {
        return GopetManager.taskTemplate.get(taskTemplateId);
    }
}
