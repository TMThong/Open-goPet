

using Gopet.Battle;
using Gopet.Data.Collections;
using Gopet.Data.Map;
using Gopet.IO;

public abstract class Place
{

    public CopyOnWriteArrayList<Player> players = new();
    public int maxPlayer = 20;
    public GopetMap map;
    public int zoneID;

    public int numPlayer
    {
        get
        {
            return this.players.Count;
        }
    }

    public Place(GopetMap m, int ID)
    {
        map = m;
        zoneID = ID;

    }

    public abstract void add(Player player);

    public virtual void remove(Player player)
    {
        players.remove(player);
        player.setPlace(null);
        sendRemove(player);
        PetBattle petBattle = player.controller.getPetBattle();
        if (petBattle != null)
        {
            petBattle.Close(player);
        }
    }

    public virtual bool canAdd(Player player)
    {
        return this.players.Count + 1 < maxPlayer;
    }

    public virtual void update()
    {
        foreach (Player player in players)
        {
            player.update();
        }
    }

    public void sendMessage(Message ms)
    {
        foreach (Player player in players)
        {
            player.session.sendMessage(ms);
        }
    }

    public void sendMessage(Message ms, Player ex)
    {
        foreach (Player player in players)
        {
            if (player != ex)
                player.session.sendMessage(ms);
        }
    }

    public void sendMessageWithCheckVersion(Dictionary<Message, Func<Version, bool>> messages)
    {
        List<Player> sentPlayer = new List<Player>();

        foreach (Player player in players)
        {
            if (sentPlayer.Contains(player))
                continue;

            foreach (var item in messages)
            {
                if (item.Value.Invoke(player.ApplicationVersion))
                {
                    player.session.sendMessage(item.Key);
                    sentPlayer.Add(player);
                }
            }
        }
        sentPlayer.Clear();
        sentPlayer = null;
    }

    public virtual void loadInfo(Player player)
    {

    }

    public virtual void sendMove(int channelID, int userID, sbyte lastDir, short[][] points)
    {
        Message ms = new Message((sbyte)108);
        ms.putsbyte(9);
        ms.putsbyte(5);
        ms.putInt(channelID);
        ms.putInt(userID);
        ms.putsbyte(lastDir);
        ms.putInt(points.Length);
        foreach (short[] point in points)
        {
            ms.putShort(point[0]);
            ms.putShort(point[1]);
        }
        ms.cleanup();
        sendMessage(ms);
    }

    public virtual void sendNewPlayer(Player player)
    {

    }

    public virtual void sendRemove(Player player)
    {
        Message ms = new Message((sbyte)108);
        ms.putsbyte(9);
        ms.putsbyte(4);
        ms.putInt(zoneID);
        ms.putInt(player.user.user_id);
        ms.cleanup();
        sendMessage(ms);
    }

    public virtual void chat(Player player, String text)
    {
        Message ms = new Message((sbyte)108);
        ms.putsbyte(9);
        ms.putsbyte(6);
        ms.putInt(zoneID);
        ms.putInt(player.user.user_id);
        ms.putUTF(player.playerData.name);
        ms.putUTF(text);
        ms.cleanup();
        sendMessage(ms);
    }

    public virtual void chat(int user_id, String name, String text)
    {
        Message ms = new Message((sbyte)108);
        ms.putsbyte(9);
        ms.putsbyte(6);
        ms.putInt(zoneID);
        ms.putInt(user_id);
        ms.putUTF(name);
        ms.putUTF(text);
        ms.cleanup();
        sendMessage(ms);
    }

    public virtual bool needRemove()
    {
        return false;
    }

    public static Message messagePetService(sbyte subCmd)
    {
        Message message = new Message(GopetCMD.PET_SERVICE);
        message.putsbyte(subCmd);
        return message;
    }

    public virtual void removeAllPlayer()
    {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }
}
