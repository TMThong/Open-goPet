
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.User;
using Newtonsoft.Json;

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
        ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `GopetClan`");
        while (resultSet.next())
        {
            Clan clan = new Clan(resultSet.getInt("clanId"));
            clan.setName(resultSet.getString("name"));
            clan.setCurMember(resultSet.getInt("curMember"));
            clan.setMaxMember(resultSet.getInt("maxMember"));
            clan.setLeaderId(resultSet.getInt("leaderId"));
            clan.setLvl(resultSet.getInt("lvl"));
            clan.setMembers(JsonConvert.DeserializeObject<CopyOnWriteArrayList<ClanMember>>(resultSet.getString("members")));
            clan.setFund(resultSet.getBigDecimal("fund").longValue());
            clan.setGrowthPoint(resultSet.getBigDecimal("growthPoint").longValue());
            clan.setSkillHouseLvl(resultSet.getInt("skillHouseLvl"));
            clan.setbaseMarketLvl(resultSet.getInt("baseMarketLvl"));
            clan.setSlogan(resultSet.getString("slogan"));
            String joinRequestStr = resultSet.getString("joinRequest");
            if (!string.IsNullOrEmpty(joinRequestStr))
            {

                clan.setRequestJoin(JsonConvert.DeserializeObject<CopyOnWriteArrayList<ClanRequestJoin>>(joinRequestStr));
            }
            foreach (ClanMember member in clan.getMembers())
            {
                member.clan = clan;
            }
            clan.setShopClan(new ShopClan(clan));
            addClan(clan);
        }
    }
}
