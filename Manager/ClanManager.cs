
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

    /// <summary>
    /// Thêm danh hiệu đệ nhất bang 
    /// Giá 2,500 vàng
    /// </summary>
    public const int OPTION_ADD_TITLE_9 = 0;
    /// <summary>
    /// Thêm danh hiệu đệ nhất bang 
    /// Giá 2,500 vàng
    /// </summary>
    public const int OPTION_ADD_TITLE_10 = 1;

    public static void addClan(Clan clan)
    {
        clans.Add(clan);
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
                    if (member.duty == Clan.TYPE_LEADER && clan.leaderId != member.user_id)
                    {
                        clan.leaderId = member.user_id;
                    }
                }
                clan.setShopClan(new ShopClan(clan));
                clan.initClan();
                clan.removeClanMemberDuplicate();
                addClan(clan);
            }
        }
    }
}
