package runtime;

import java.util.ArrayList;

public class RuntimeServer extends Thread {

    public ArrayList<IRuntime> runtimes = new ArrayList();
    public static long MINUTE = 1000 * 60;
    public static bool isRunning = true;
    public static RuntimeServer instance = new RuntimeServer();

    public RuntimeServer() {
        setName("Runtime Server Thread");
    }

    @Override
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
        for (IRuntime r : runtimes) {
            try {
                r.update();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        Thread.sleep(MINUTE);
    }
}
