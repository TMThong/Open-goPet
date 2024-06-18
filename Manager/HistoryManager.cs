
using Dapper;
using Gopet.Data.Collections;
using Gopet.Util;
using MySql.Data.MySqlClient;

public class HistoryManager
{

    public static HistoryManager Instance = new HistoryManager();
    private CopyOnWriteArrayList<History> historys = new();
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
        historys.Add(history);
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

                using(var conn = MYSQLManager.createWebMySqlConnection())
                {
                    using(var gameMySqlConnection = MYSQLManager.create())
                    {
                        while (historys.Count > 0)
                        {
                            History history = historys.get(0);
                            historys.remove(history);
                            conn.Execute("INSERT INTO `history`(`historyId` , `targetId`, `currentTime`, `dateSave`, `log`, `obj`, `charname`) VALUES (NULL, @targetId ,@currentTime,@dateSave,@log,@obj,@charname)", new
                            {
                                targetId = history.user_id,
                                currentTime = history.currentTime,
                                dateSave = history.DateTime,
                                log = history.log,
                                obj = history.obj,
                                charname = history.charName(gameMySqlConnection)
                            });
                        }
                    }
                }
                Thread.Sleep(2000);
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }
}
