/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package manager;

import java.lang.management.ManagementFactory;
import java.lang.management.ThreadInfo;
import java.lang.management.ThreadMXBean;
import lombok.Getter;
import lombok.Setter;

/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
public class DeadlockDetectorManager extends Thread {

    public static DeadlockDetectorManager instance = new DeadlockDetectorManager();

    private bool isRunning = false;

    public DeadlockDetectorManager() {
        setName("Deadlock detector thread");
    }

    @Override
    public void run() {
        setRunning(true);
        try {
            while (isRunning) {
                ThreadMXBean bean = ManagementFactory.getThreadMXBean();
                long[] threadIds = bean.findMonitorDeadlockedThreads();  

                if (threadIds != null) {
                    ThreadInfo[] infos = bean.getThreadInfo(threadIds);
                    for (ThreadInfo info : infos) {
                        System.err.println("DeadLock thread " + info.getThreadName());
                        StackTraceElement[] stack = info.getStackTrace();                        
                        for (StackTraceElement stackTraceElement : stack) {
                            System.err.println("DeadLock className  " + stackTraceElement.getClassName());
                            System.err.println("DeadLock method " + stackTraceElement.getMethodName());                           
                        }
                    }
                }
                Thread.sleep(60000);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
