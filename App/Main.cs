
using Gopet.APIs;
using Gopet.Data.Event;
using Gopet.Data.GopetClan;
using Gopet.Manager;
using Gopet.Server;
using Gopet.Server.IO;
using Gopet.Util;
namespace Gopet.App
{
    public class Main
    {

        public static IServerBase server;
        public static int PORT_SERVER = ServerSetting.instance.portGopetServer;
        public static bool isNetBeans = true;
        public static int HTTP_PORT = ServerSetting.instance.portHttpServer;
        public static HttpServer APIServer;

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
            GopetManager.init();
            HistoryManager.Instance.start();
            MapManager.init();
            ClanManager.init();
            GopetManager.loadMarket();
            BXHManager.instance.start();
            FieldManager.Init();
            initRuntime();
            RuntimeServer.instance.start();
            DailyBossEvent.Instance = new DailyBossEvent();
            //EventManager.AddEvent(DailyBossEvent.Instance);
            EventManager.Start();
            APIServer = new HttpServer(HTTP_PORT);
            APIServer.Start();
            server = new Gopet.MServer.Server(PORT_SERVER);
            server.StartServer();
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
            server.StopServer();
            APIServer.Stop();
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
    }

}