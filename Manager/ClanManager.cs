
using Gopet.Data.Collections;
using Gopet.Data.user;

public class ClanManager {

    public static CopyOnWriteArrayList<Clan> clans = new CopyOnWriteArrayList<>();
    public static ConcurrentHashMap<int, Clan> clanHashMap = new ConcurrentHashMap<>();
    public static ConcurrentHashMap<String, Clan> clanHashMapName = new ConcurrentHashMap<>();

    public static void addClan(Clan clan) {
        clans.add(clan);
        clanHashMap.put(clan.getClanId(), clan);
        clanHashMapName.put(clan.getName(), clan);
    }

    public static Clan getClanById(int clanId) {
        return clanHashMap.get(clanId);
    }

    public static Clan getClanByName(String name) {
        return clanHashMapName.get(name);
    }

    public static void init()   {
        ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `clan`");
        while (resultSet.next()) {
            Clan clan = new Clan(resultSet.getInt("clanId"));
            clan.setName(resultSet.getString("name"));
            clan.setCurMember(resultSet.getInt("curMember"));
            clan.setMaxMember(resultSet.getInt("maxMember"));
            clan.setLeaderId(resultSet.getInt("leaderId"));
            clan.setLvl(resultSet.getInt("lvl"));
            Type arrayType = new TypeToken<CopyOnWriteArrayList<ClanMember>>() {
            }.getType();
            clan.setMembers((CopyOnWriteArrayList<ClanMember>) JsonManager.LoadFromJson(resultSet.getString("members"), arrayType));
            clan.setFund(resultSet.getBigDecimal("fund").longValue());
            clan.setGrowthPoint(resultSet.getBigDecimal("growthPoint").longValue());
            clan.setSkillHouseLvl(resultSet.getInt("skillHouseLvl"));
            clan.setbaseMarketLvl(resultSet.getInt("baseMarketLvl"));
            clan.setSlogan(resultSet.getString("slogan"));
            String joinRequestStr = resultSet.getString("joinRequest");
            if (!resultSet.wasNull()) {
                arrayType = new TypeToken<CopyOnWriteArrayList<ClanRequestJoin>>() {
                }.getType();
                clan.setRequestJoin((CopyOnWriteArrayList<ClanRequestJoin>) JsonManager.LoadFromJson(joinRequestStr, arrayType));
            }
            for (ClanMember member : clan.getMembers()) {
                member.setClan(clan);
            }
            clan.setShopClan(new ShopClan(clan));
            addClan(clan);
        }
    }
}
