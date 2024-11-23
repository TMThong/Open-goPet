
using Gopet.Data.Collections;
using Gopet.IO;
using Gopet.Language;
using Gopet.Util;
using MySqlX.XDevAPI;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

public class PlayerManager
{

    public static CopyOnWriteArrayList<Player> players = new CopyOnWriteArrayList<Player>();
    public static ConcurrentHashMap<int, Player> player_ID = new ConcurrentHashMap<int, Player>();
    public static ConcurrentHashMap<String, Player> player_name = new ConcurrentHashMap<String, Player>();
    public static ConcurrentHashMap<int, long> waitLogin = new ConcurrentHashMap<int, long>();
    private static ConcurrentDictionary<string, Tuple<int, DateTime>> WaitLogin = new ConcurrentDictionary<string, Tuple<int, DateTime>>();

    public static Player get(String ID)
    {
        return player_name.get(ID);
    }

    public static Player get(int ID)
    {
        return player_ID.get(ID);
    }

    public static void put(Player player)
    {
        players.Add(player);
        player_ID.put(player.user.user_id, player);
        player_name.put(player.playerData.name, player);
    }

    public static void remove(Player player)
    {

        players.remove(player);
        player_ID.remove(player.user.user_id);
        player_name.remove(player.playerData.name);
        waitLogin.put(player.user.user_id, Utilities.CurrentTimeMillis + 15000);

    }

    public static long GetTimeMillisWaitLogin(int ID)
    {
        long time = waitLogin.get(ID);
        if (time == null)
        {
            return 0;
        }
        else if (Utilities.CurrentTimeMillis - time >= 0)
        {
            waitLogin.remove(ID);
            return -1;
        }
        else
        {
            return time - Utilities.CurrentTimeMillis;
        }
    }

    public static void chatGlobal(string Name, string Text)
    {
        Message message = GameController.messagePetService(GopetCMD.CHAT_GLOBAL);
        message.putString(Name);
        message.putString(Text);
        PlayerManager.sendMessage(message);
    }

    public async static void sendMessage(Message ms)
    {
        foreach (Player player in players)
        {
            player.session.sendMessage(ms);
        }
    }

    public static void crossChat(String who, String text)
    {
        Message ms = new Message((sbyte)108);
        ms.putsbyte(9);
        ms.putsbyte(30);
        ms.putInt(1); // số lượng người chat
        ms.putUTF(who);
        ms.putUTF(text);
        ms.cleanup();
        sendMessage(ms);
    }

    public static void okDialog(String str)
    {
        Message m = new Message(71);
        m.putsbyte(0);
        m.putUTF(str);
        m.writer().flush();
        sendMessage(m);
    }

    public static void crossChat(Player player, String text)
    {
        crossChat(player.playerData.name, text);
    }

    public static void showBannerZ(String text)
    {
        Message m = new Message(45);
        m.putsbyte(1);
        m.putUTF(text);
        m.writer().flush();
        sendMessage(m);

        chatGlobal("Máy chủ", text);
    }

    public static void showBanner(Func<LanguageData, string> func)
    {
        Dictionary<LanguageData, Message> messages = new Dictionary<LanguageData, Message>();
        foreach (var item in GopetManager.Language)
        {
            string text = func.Invoke(item.Value);
            Message m = new Message(45);
            m.putsbyte(1);
            m.putUTF(text);
            m.writer().flush();
            sendMessage(m);
            messages[item.Value] = m;
            if (item.Key == "vi")
            {
                chatGlobal("Máy chủ", text);
            }
        }
        foreach (var p in players)
        {
            p.session.sendMessage(messages[p.Language]);
        }
    }

    public static void Popup(String text)
    {
        Message m = new Message(45);
        m.putsbyte(5);
        m.putUTF(text);
        m.writer().flush();
        sendMessage(m);
    }
}