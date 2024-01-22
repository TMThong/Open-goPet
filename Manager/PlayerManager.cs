
using Gopet.Data.Collections;

public class PlayerManager {

    public static CopyOnWriteArrayList<Player> players = new CopyOnWriteArrayList<Player>();
    public static ConcurrentHashMap<int, Player> player_ID = new ConcurrentHashMap<int, Player>();
    public static ConcurrentHashMap<String, Player> player_name = new ConcurrentHashMap<String, Player>();
    public static ConcurrentHashMap<int, long> waitLogin = new ConcurrentHashMap<int, long>();

    public static Player get(String ID) {

        return player_name.get(ID);

    }

    public static Player get(int ID) {

        return player_ID.get(ID);

    }

    public static void put(Player player) {

        players.add(player);
        player_ID.put(player.user.user_id, player);
        player_name.put(player.playerData.name, player);

    }

    public static void remove(Player player) {

        players.remove(player);
        player_ID.remove(player.user.user_id);
        player_name.remove(player.playerData.name);
        waitLogin.put(player.user.user_id, System.currentTimeMillis() + 15000);

    }

    public static long getTimeWaitLogin(Integer ID)   {
        Long time = waitLogin.get(ID);
        if (time == null) {
            return 0;
        } else if (System.currentTimeMillis() - time >= 0) {
            waitLogin.remove(ID);
            return -1;
        } else {
            return time - System.currentTimeMillis();
        }
    }

    public static void sendMessage(Message ms)   {
        for (Player player : players) {
            player.session.sendMessage(ms);
        }
    }

    public static void crossChat(String who, String text)   {
        Message ms = new Message((byte) 108);
        ms.putByte(9);
        ms.putByte(30);
        ms.putInt(1); // số lượng người chat
        ms.putUTF(who);
        ms.putUTF(text);
        ms.cleanup();
        sendMessage(ms);
    }

    public static void crossChat(Player player, String text)   {
        crossChat(player.playerData.name, text);
    }

    public static void showBanner(String text)   {
        Message m = new Message(45);
        m.putByte(1);
        m.putUTF(text);
        m.writer().flush();
        sendMessage(m);
    }

    public static void Popup(String text)   {
        Message m = new Message(45);
        m.putByte(5);
        m.putUTF(text);
        m.writer().flush();
        sendMessage(m);
    }

}
