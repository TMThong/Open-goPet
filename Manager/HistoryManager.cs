 
public class HistoryManager extends Thread {

    public static HistoryManager instance = new HistoryManager();
    private CopyOnWriteArrayList<History> historys = new CopyOnWriteArrayList<>();

    public HistoryManager() {
        setName("History thread");
    }

    public void add(History history) {
        historys.add(history);
    }

    public static void addHistory(History history) {
        instance.add(history);
    }

     
    public void run() {
        try {
            while (true) {

                try {
                    MySqlConnection MySqlConnection = MYSQLManager.createWebMySqlConnection();
                    MySqlConnection gameMySqlConnection = MYSQLManager.create();
                    while (historys.Count > 0) {
                        History history = historys.get(0);
                        historys.remove(history);
                        try {
                            MYSQLManager.updateSql(Utilities.Format("INSERT INTO `history`(`historyId` , `targetId`, `currentTime`, `dateSave`, `log`, `obj`, `charname`) VALUES (NULL, '%s' ,'%s','%s','%s','%s','%s')",
                                    history.getUser_id(),
                                    history.getCurrentTime(),
                                    Utilities.dateFormatVI.format(history.getDate()),
                                    history.getLog(),
                                    JsonManager.ToJson(history.getObj()),
                                    history.charName(gameMySqlConnection)
                            ), MySqlConnection);
                        } catch (Exception e) {
                            e.printStackTrace();
                            System.err.println("History Log Error " + history.getLog());
                        }
                    }
                    MySqlConnection.close();
                } catch (Exception e) {
                    e.printStackTrace();
                }

                Thread.Sleep(2000);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
