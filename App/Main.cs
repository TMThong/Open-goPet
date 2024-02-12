
using Gopet.APIs;
using Gopet.Data.GopetClan;
using Gopet.Manager;
using Gopet.Util;
 
namespace Gopet.App
{
    public class Main
    {

        public static Server server;
        public static int PORT_SERVER = ServerSetting.instance.portGopetServer;
        public static bool isNetBeans = true;
        public static int HTTP_PORT = ServerSetting.instance.portHttpServer;
        public static HttpServer APIServer ;

        /**
         * hàm chính
         *
         * @param args
         * @ 
         */
        public static void StartServer(string[] args)
        {
            Thread.CurrentThread.Name = "MAIN THREAD (GOPET)";
            if (ServerSetting.instance.initLog)
            {
                initLog();
            }

            //        AutoMaintenance autoMaintenance = new AutoMaintenance();
            //        autoMaintenance.start(ServerSetting.instance.getHourMaintenance(), ServerSetting.instance.getMinMaintenance());
            MYSQLManager.init();
            GopetManager.init();
            HistoryManager.instance.start();
            MapManager.init();
            ClanManager.init();
            GopetManager.loadMarket();
            BXHManager.instance.start();
            MenuController.init();
            FieldManager.Init();
            initRuntime();
            RuntimeServer.instance.start();
            APIServer = new HttpServer(HTTP_PORT);
            APIServer.Start();
            server = new Server(PORT_SERVER);
            server.start();
        }

        public static void initRuntime()
        {
            RuntimeServer.instance.runtimes.add(new AutoSave());
            RuntimeServer.instance.runtimes.add(Maintenance.gI());
        }


        public static void initLog()
        {

        }

        public static void shutdown()
        {
            MapManager.stopUpdate();
            GopetManager.saveMarket();
            server.stopServer();
            RuntimeServer.isRunning = false;
            Thread.Sleep(1000);

            foreach (Player player in PlayerManager.players)
            {
                player.session.Close();
                player.onDisconnected();
                PlayerManager.players.remove(player);
            }

            foreach (Clan clan in ClanManager.clans)
            {
                try
                {
                    clan.save();
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        }

        public static void SocketLog(Exception e)
        {

        }
    }

}