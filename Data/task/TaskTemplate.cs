/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.task;

import lombok.Getter;
import lombok.Setter;

/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
public class TaskTemplate {

    private int taskId;
    private int type;
    private String name;
    private String description;
    private String guide = "";
    private int[][] task;
    private int[][] gift;
    private int fromNpc;
    private int[] taskNeed;
    private long timeTask = -1;
}
