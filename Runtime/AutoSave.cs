package runtime;

import data.clan.Clan;
import data.user.History;
import manager.ClanManager;
import manager.HistoryManager;
import manager.PlayerManager;
import server.Player;

public class AutoSave implements IRuntime {

    public static long lastTime = System.currentTimeMillis() + 60 * 1000;
    public static long lastTimeSaveClan = System.currentTimeMillis() + (60 * 1000 * 30);

    @Override
    public void update()   {
        if (lastTime < System.currentTimeMillis()) {
            for (Player player : PlayerManager.players) {
                if (player.session.isConnected()) {
                    if (player.timeSaveDelta < System.currentTimeMillis()) {
                        if (player.playerData != null) {
                            player.playerData.save();
                            player.Popup("Dữ liệu của bạn đã được máy chủ lưu dự phòng thành công");
                            HistoryManager.addHistory(new History(player).setLog("Backup dữ liệu thành công").setObj(player.playerData));
                        }
                        player.timeSaveDelta = System.currentTimeMillis() + Player.TIME_SAVE_DATA;
                    }
                }
            }
            lastTime = System.currentTimeMillis() + 60 * 1000;
        }
        if (lastTimeSaveClan < System.currentTimeMillis()) {
            for (Clan clan : ClanManager.clans) {
                try {
                    clan.save();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
            lastTimeSaveClan = System.currentTimeMillis() + (60 * 1000 * 30);
        }
    }
}
