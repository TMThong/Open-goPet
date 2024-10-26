
using Dapper;
using Gopet.Data.Collections;
using Gopet.Util;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Threading;

public class HistoryManager
{

    public static HistoryManager Instance = new HistoryManager();
    private ConcurrentQueue<History> historys = new();
    private AutoResetEvent AutoResetEvent = new AutoResetEvent(false);
    public Thread HistoryThread;
    public HistoryManager()
    {
        this.HistoryThread = new Thread(run);
        this.HistoryThread.Name = "History thread";
        this.HistoryThread.IsBackground = true;
    }

    public void start()
    {
        this.HistoryThread.Start();
    }

    public void add(History history)
    {
        historys.Enqueue(history);
        AutoResetEvent.Set();
    }

    public static void addHistory(History history)
    {
        Instance.add(history);
    }


    public void run()
    {
        try
        {
            while (true)
            {

                using(var conn = MYSQLManager.createLogConnection())
                {
                    using(var gameMySqlConnection = MYSQLManager.create())
                    {
                        while (this.historys.TryDequeue(out var history))
                        {
                            conn.Execute("INSERT INTO `history`(`targetId`, `log`, `obj`, `charname`) VALUES (@targetId,@log,@obj,@charname)", new
                            {
                                targetId = history.user_id,
                                log = history.log,
                                obj = JsonConvert.SerializeObject(history.obj),
                                charname = history.charName(gameMySqlConnection)
                            });
                        }
                    }
                }
                AutoResetEvent.WaitOne(2000);
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }
}
