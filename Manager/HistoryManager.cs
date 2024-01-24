
using Gopet.Data.Collections;
using Gopet.Util;
using MySql.Data.MySqlClient;

public class HistoryManager
{

    public static HistoryManager instance = new HistoryManager();
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
        historys.add(history);
    }

    public static void addHistory(History history)
    {
        instance.add(history);
    }


    public void run()
    {
        try
        {
            while (true)
            {

                try
                {
                    MySqlConnection conn = MYSQLManager.createWebMySqlConnection();
                    MySqlConnection gameMySqlConnection = MYSQLManager.create();
                    while (historys.Count > 0)
                    {
                        History history = historys.get(0);
                        historys.remove(history);
                        try
                        {
                            MYSQLManager.updateSql(Utilities.Format("INSERT INTO `history`(`historyId` , `targetId`, `currentTime`, `dateSave`, `log`, `obj`, `charname`) VALUES (NULL, '%s' ,'%s','%s','%s','%s','%s')",
                                    history.getUser_id(),
                                    history.getCurrentTime(),
                                    Utilities.ToDateString(history.getDate()),
                                    history.getLog(),
                                    JsonManager.ToJson(history.getObj()),
                                    history.charName(gameMySqlConnection)
                            ), conn);
                        }
                        catch (Exception e)
                        {
                            e.printStackTrace();
                            //System.err.println("History Log Error " + history.getLog());
                        }
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    e.printStackTrace();
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
