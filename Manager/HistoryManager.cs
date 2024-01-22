 
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

    @Override
    public void run() {
        try {
            while (true) {

                try {
                    Connection connection = MYSQLManager.createWebConnection();
                    Connection gameConnection = MYSQLManager.create();
                    while (historys.size() > 0) {
                        History history = historys.get(0);
                        historys.remove(history);
                        try {
                            MYSQLManager.updateSql(String.format("INSERT INTO `history`(`historyId` , `targetId`, `currentTime`, `dateSave`, `log`, `obj`, `charname`) VALUES (NULL, '%s' ,'%s','%s','%s','%s','%s')",
                                    history.getUser_id(),
                                    history.getCurrentTime(),
                                    Utilities.dateFormatVI.format(history.getDate()),
                                    history.getLog(),
                                    JsonManager.ToJson(history.getObj()),
                                    history.charName(gameConnection)
                            ), connection);
                        } catch (Exception e) {
                            e.printStackTrace();
                            System.err.println("History Log Error " + history.getLog());
                        }
                    }
                    connection.close();
                } catch (Exception e) {
                    e.printStackTrace();
                }

                Thread.sleep(2000);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
