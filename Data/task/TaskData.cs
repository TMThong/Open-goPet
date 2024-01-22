/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.task;

import java.util.Arrays;
import manager.GopetManager;

/**
 *
 * @author MINH THONG
 */
public class TaskData {

    public int taskTemplateId;
    public int[][] taskInfo;
    public int[] task;
    public int[][] gift;
    public long timeTask;
    public bool wasShowDialog = false;

    public TaskData(TaskTemplate taskTemplate) {
        this.taskTemplateId = taskTemplate.getTaskId();
        this.taskInfo = taskTemplate.getTask();
        this.gift = taskTemplate.getGift();
        this.timeTask = taskTemplate.getTimeTask();
        this.task = new int[taskInfo.length];
        Arrays.fill(this.task, 0);
    }

    public TaskTemplate getTemplate() {
        return GopetManager.taskTemplate.get(taskTemplateId);
    }
}
