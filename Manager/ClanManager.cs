
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.User;
using Newtonsoft.Json;
using Dapper;

public class ClanManager
{

    public static CopyOnWriteArrayList<Clan> clans = new();
    public static ConcurrentHashMap<int, Clan> clanHashMap = new();
    public static ConcurrentHashMap<String, Clan> clanHashMapName = new();

    public static void addClan(Clan clan)
    {
        clans.add(clan);
        clanHashMap.put(clan.getClanId(), clan);
        clanHashMapName.put(clan.getName(), clan);
    }

    public static Clan getClanById(int clanId)
    {
        return clanHashMap.get(clanId);
    }

    public static Clan getClanByName(String name)
    {
        return clanHashMapName.get(name);
    }

    public static void init()
    {
        using(var conn = MYSQLManager.create())
        {
            IEnumerable<Clan> clans = conn.Query<Clan>("SELECT * FROM `clan`");
            foreach(Clan clan in clans)
            {
                foreach (ClanMember member in clan.getMembers())
                {
                    member.clan = clan;
                }
                clan.setShopClan(new ShopClan(clan));
                addClan(clan);
            }
        }
    }
}
