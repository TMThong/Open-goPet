
using Gopet.Data.Collections;
using Gopet.Data.top;
using Gopet.Util;

public class BXHManager
{

    private long lastTime = 0;
    public static BXHManager instance = new BXHManager();
    public bool isRunning = false;
    public Thread BXHThread { get; private set; }
    public BXHManager()
    {
        BXHThread = new Thread(run);
        BXHThread.IsBackground = true;
        BXHThread.Name = "BXH Thread";
    }

    public void start()
    {
        BXHThread.Start();
    }

    public void run()
    {
        if (!isRunning)
        {
            try
            {
                isRunning = true;
                update();
            }
            catch (Exception ex)
            {
                ex.printStackTrace();
            }
        }
    }

    public void update()
    {
        while (isRunning)
        {
            if (lastTime < Utilities.CurrentTimeMillis)
            {
                foreach (Top next in listTop)
                {
                    next.Update();
                }
                lastTime = Utilities.CurrentTimeMillis + 1000 * 60 * 15;
            }
            Thread.Sleep(60000);
        }
    }

    public static CopyOnWriteArrayList<Top> listTop = new();

    static BXHManager()
    {
        listTop.Add(TopGold.Instance);
        listTop.Add(TopPet.Instance);
        listTop.Add(TopGem.Instance);
        listTop.Add(TopLVLClan.Instance);
        listTop.Add(TopSpendGold.Instance);
        listTop.Add(TopAccumulatedPoint.Instance);
        listTop.Add(TopEvent.Instance);
        listTop.Add(TopChallenge.Instance);
    }
}
