 

public class Main {

    public static Server server;
    public static int PORT_SERVER = ServerSetting.instance.getPortGopetServer();
    public static SystemInfo systemInfo;
    public static bool isNetBeans = true;
    public static int HTTP_PORT = ServerSetting.instance.getPortHttpServer();
    public const SpringApp springApp = new SpringApp();

    /**
     * hàm chính
     *
     * @param args
     * @ 
     */
    public static void main(String[] args)   {
        Thread.currentThread().setName("MAIN THREAD (GOPET)");
        if (ServerSetting.instance.isInitLog()) {
            initLog();
        }
        systemInfo = new SystemInfo();
        java.lang.Runtime.getRuntime().addShutdownHook(new Thread(() -> {
            try {
                shutdown();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }));
//        AutoMaintenance autoMaintenance = new AutoMaintenance();
//        autoMaintenance.start(ServerSetting.instance.getHourMaintenance(), ServerSetting.instance.getMinMaintenance());
        MYSQLManager.init();
        GopetManager.init();
        HistoryManager.instance.start();
        DeadlockDetectorManager.instance.start();
        MapManager.init();
        ClanManager.init();
        GopetManager.loadMarket();
        BXHManager.instance.start();
        MenuController.init();
        initRuntime();
        RuntimeServer.instance.start();
        server = new Server(PORT_SERVER);
        server.start();
        springApp.start(args);
    }

    public static void initRuntime()   {
        RuntimeServer.instance.runtimes.add(new AutoSave());
        RuntimeServer.instance.runtimes.add(Maintenance.gI());
    }
    public static PrintStream SocketError;

    public static void initLog()   {
        String targetPath = PlatformHelper.currentDirectory() + "/" + ServerSetting.instance.getErrorFileName();
        File f = new File(targetPath);
        if (!f.exists()) {
            f.createNewFile();
        }
        FileOutputStream fos = new FileOutputStream(f);
        System.setErr(new PrintStream(fos));
        targetPath = PlatformHelper.currentDirectory() + "/" + ServerSetting.instance.getOutputFileName();
        f = new File(targetPath);
        if (!f.exists()) {
            f.createNewFile();
        }
        fos = new FileOutputStream(f);
        System.setOut(new PrintStream(fos));
    }

    public static void shutdown()   {
        MapManager.stopUpdate();
        GopetManager.saveMarket();
        server.stopServer();
        RuntimeServer.isRunning = false;
        Thread.sleep(1000);

        for (Player player : PlayerManager.players) {
            player.session.close();
            player.onDisconnected();
            PlayerManager.players.remove(player);
        }

        for (Clan clan : ClanManager.clans) {
            try {
                clan.save();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public static void SocketLog(Exception e) {
        e.printStackTrace(SocketError);
        SocketError.flush();
    }
}
