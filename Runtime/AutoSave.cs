

using Gopet.Data.GopetClan;
using Gopet.Util;

public class AutoSave : IRuntime
{

    public static long lastTime = Utilities.CurrentTimeMillis + 60 * 1000;
    public static long lastTimeSaveClan = Utilities.CurrentTimeMillis + (60 * 1000 * 30);
    public static long lastTimeSaveMarket = Utilities.CurrentTimeMillis + (60 * 1000 * 2);


    public void update()
    {
        if (lastTime < Utilities.CurrentTimeMillis)
        {
            using(var conn = MYSQLManager.create())
            {
                foreach (Player player in PlayerManager.players)
                {
                    if (player.session.isConnected())
                    {
                        if (player.timeSaveDelta < Utilities.CurrentTimeMillis)
                        {
                            if (player.playerData != null)
                            {
                                PlayerData.saveStatic(player.playerData, conn);
                                player.Popup("Dữ liệu của bạn đã được máy chủ lưu dự phòng thành công");
                                HistoryManager.addHistory(new History(player).setLog("Backup dữ liệu thành công").setObj(player.playerData));
                            }
                            player.timeSaveDelta = Utilities.CurrentTimeMillis + Player.TIME_SAVE_DATA;
                        }
                    }
                }
            }
            lastTime = Utilities.CurrentTimeMillis + 60 * 1000;
        }
        if (lastTimeSaveClan < Utilities.CurrentTimeMillis)
        {
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
            lastTimeSaveClan = Utilities.CurrentTimeMillis + (60 * 1000 * 30);
        }

        if (lastTimeSaveMarket < Utilities.CurrentTimeMillis)
        {
            GopetManager.saveMarket();
            lastTimeSaveMarket = Utilities.CurrentTimeMillis + (60 * 1000 * 2);
        }
    }
}
