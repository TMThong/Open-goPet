package place;

import data.battle.PetBattle;
import data.map.GopetMap;
import java.util.concurrent.CopyOnWriteArrayList;
import server.Player;
import server.io.Message;

public abstract class Place {

    public CopyOnWriteArrayList<Player> players = new CopyOnWriteArrayList();
    public int numPlayer = 0;
    public int maxPlayer = 20;
    public GopetMap map;
    public int zoneID;

    public Place(GopetMap m, int ID) {
        map = m;
        zoneID = ID;

    }

    public abstract void add(Player player)  ;

    public void remove(Player player)   {
        players.remove(player);
        player.setPlace(null);
        sendRemove(player);
        numPlayer--;
        PetBattle petBattle = player.controller.getPetBattle();
        if (petBattle != null) {
            petBattle.close(player);
        }
    }

    public bool canAdd(Player player)   {
        return numPlayer + 1 < maxPlayer;
    }

    public void update()   {
        for (Player player : players) {
            player.update();
        }
    }

    public void sendMessage(Message ms)   {
        for (Player player : players) {
            player.session.sendMessage(ms);
        }
    }

    public void loadInfo(Player player)   {

    }

    public void sendMove(int channelID, int userID, byte lastDir, short[][] points)   {
        Message ms = new Message((byte) 108);
        ms.putByte(9);
        ms.putByte(5);
        ms.putInt(channelID);
        ms.putInt(userID);
        ms.putByte(lastDir);
        ms.putInt(points.length);
        for (short[] point : points) {
            ms.putShort(point[0]);
            ms.putShort(point[1]);
        }
        ms.cleanup();
        sendMessage(ms);
    }

    public void sendNewPlayer(Player player)   {

    }

    public void sendRemove(Player player)   {
        Message ms = new Message((byte) 108);
        ms.putByte(9);
        ms.putByte(4);
        ms.putInt(zoneID);
        ms.putInt(player.user.user_id);
        ms.cleanup();
        sendMessage(ms);
    }

    public void chat(Player player, String text)   {
        Message ms = new Message((byte) 108);
        ms.putByte(9);
        ms.putByte(6);
        ms.putInt(zoneID);
        ms.putInt(player.user.user_id);
        ms.putUTF(player.playerData.name);
        ms.putUTF(text);
        ms.cleanup();
        sendMessage(ms);
    }

    public void chat(int user_id, String name, String text)   {
        Message ms = new Message((byte) 108);
        ms.putByte(9);
        ms.putByte(6);
        ms.putInt(zoneID);
        ms.putInt(user_id);
        ms.putUTF(name);
        ms.putUTF(text);
        ms.cleanup();
        sendMessage(ms);
    }

    public bool needRemove() {
        return false;
    }

    public void removeAllPlayer()   {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }
}
