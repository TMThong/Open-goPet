
using Gopet.Data.Collections;

public class RuntimeServer   {

    public ArrayList<IRuntime> runtimes = new  ();
    public static int MINUTE = 1000 * 60;
    public static bool isRunning = true;
    public static RuntimeServer instance = new RuntimeServer();
    public Thread MyThread;
    public RuntimeServer() {
        MyThread = new Thread(run);
        MyThread.IsBackground = true;
        MyThread.Name = "Runtime Server Thread";
    }

    
    public void run() {
        try {
            while (isRunning) {
                update();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void update()   {
        foreach (IRuntime r in runtimes) {
            try {
                r.update();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        Thread.Sleep(MINUTE);
    }
}
