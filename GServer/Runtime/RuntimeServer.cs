
using Gopet.Data.Collections;
using Gopet.Util;
public class RuntimeServer
{

    public JArrayList<IRuntime> runtimes = new();
    public static int TIME_WAIT = 5000;
    public static bool isRunning = true;
    public static RuntimeServer instance = new RuntimeServer();
    public Thread MyThread;
    public RuntimeServer()
    {
        MyThread = new Thread(run);
        MyThread.IsBackground = true;
        MyThread.Name = "Runtime Server Thread";
    }

    public void start()
    {
        this.MyThread.Start();
    }

    public void run()
    {
        try
        {
            while (isRunning)
            {
                update();
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

    public void update()
    {
        foreach (IRuntime r in runtimes)
        {
            try
            {
                r.Update();
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
        }
        Thread.Sleep(TIME_WAIT);
    }
}
