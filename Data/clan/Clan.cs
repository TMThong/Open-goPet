
using Gopet.Data.Collections;

public class Clan {

    private int clanId;
    private int curMember;
    private int maxMember;
    private String name;
    private int leaderId;
    private long fund = 0;
    private long growthPoint = 0;
    private int lvl = 1;
    private int skillHouseLvl = 1;
    private int baseMarketLvl = 0;
    private int potentialPoint = 0;
    private CopyOnWriteArrayList<ClanMember> members = new CopyOnWriteArrayList<ClanMember>();
    private CopyOnWriteArrayList<ClanRequestJoin> requestJoin = new CopyOnWriteArrayList<>();
    private CopyOnWriteArrayList<ClanBuff> clanBuffs = new CopyOnWriteArrayList<>();
    private CopyOnWriteArrayList<int> bannedJoinRequestId = new CopyOnWriteArrayList<>();
    private CopyOnWriteArrayList<ClanChat> clanChats = new CopyOnWriteArrayList<>();
    private CopyOnWriteArrayList<ClanPotentialSkill> clanPotentialSkills = new CopyOnWriteArrayList<>();
    private ClanPlace clanPlace;
    private String slogan = "HTCGOPET";
    private ShopClan shopClan;
    private Object LOCKObject = new Object();
    public const sbyte TYPE_LEADER = 0;
    public const sbyte TYPE_DEPUTY_LEADER = 1;
    public const sbyte TYPE_SENIOR = 2;
    public const sbyte TYPE_NORMAL = 3;

    public Clan() {

    }

    public Clan(String name, int leaderId, String leaderName)   {
        this.name = name;
        this.leaderId = leaderId;
        ClanTemplate clanTemplate = GopetManager.clanTemp.get(lvl);
        this.maxMember = clanTemplate.getMaxMember();
        addMember(leaderId, leaderName);
        initClan();
    }

    public Clan(int clanId)   {
        this.clanId = clanId;
        initClan();
    }

    public   ClanTemplate getTemp() {
        return GopetManager.clanTemp.get(lvl);
    }

    public   void addPotentialPoint(int value) {
        this.potentialPoint += value;
    }

    public   void sendMessage(Message m) {
        for (ClanMember member : members) {
            Player onlinePlayer = PlayerManager.get(member.user_id);
            if (onlinePlayer != null) {
                onlinePlayer.session.sendMessage(m);
            }
        }
    }

    public String getClanDesc() {
        ArrayList<String> clanInfo = new();
        clanInfo.add(Utilities.Format(" Cấp: %s ", lvl));
        clanInfo.add(Utilities.Format(" Thành viên: %s/%s ", curMember, maxMember));
        clanInfo.add(Utilities.Format(" Shop bảo vật cấp: %s ", baseMarketLvl));
        clanInfo.add(Utilities.Format(" Nhà kỹ năng bang hội cấp: %s ", skillHouseLvl));
        return String.Join(",", clanInfo.toArray(new String[0]));
    }

    private void initClan()   {
        clanPlace = new ClanPlace(MapManager.maps.get(30), this.clanId);
    }

    public void addFund(long value, ClanMember clanMember) {
        if (members.contains(clanMember)) {
            this.fund += value;
            clanMember.fundDonate += value;
        }
    }

    public void addGrowthPoint(long value, ClanMember clanMember) {
        if (members.contains(clanMember)) {
            this.growthPoint += value;
            clanMember.growthPointDonate += value;
        }
    }

    public bool checkFund(long value)   {
        return this.fund >= value;
    }

    public bool checkGrowthPoint(long value)   {
        return this.growthPoint >= value;
    }

    public void mineFund(long value) {
        this.fund -= value;
    }

    public void mineGrowthPoint(long value) {
        this.growthPoint -= value;
    }

    public void notEnoughFund(Player player)   {
        player.redDialog("Quỹ không đủ");
    }

    public void mineGrowthPoint(Player player)   {
        player.redDialog("Điểm phát triển không đủ không đủ");
    }

    public bool checkDuty(sbyte typeDuty) {
        int maxOfDutye = getTemp().getPermission()[typeDuty];
        int cur = 0;
        for (ClanMember member : members) {
            if (member.duty == typeDuty) {
                cur++;
            }
        }
        return maxOfDutye > cur;
    }

    public void showFullDuty(Player player)   {
        player.redDialog("Chức vụ này đã dành cho người khác rồi hoặc do cấp bang hội quá thấp");
    }

    public void addMember(int user_id, String name)   {
        Player player = PlayerManager.get(user_id);
        ClanMember clanMember = new ClanMember();
        clanMember.setClan(this);
        clanMember.name = name;
        clanMember.user_id = user_id;
        clanMember.fundDonate = 0l;
        clanMember.growthPointDonate = 0l;
        clanMember.duty = (user_id == leaderId ? TYPE_LEADER : TYPE_NORMAL);
        if (player != null) {
            clanMember.avatarPath = player.playerData.avatarPath;
        }
        members.add(clanMember);
        curMember++;
        members.sort(new Comparator<ClanMember>() {
             
            public int compare(ClanMember o1, ClanMember o2) {
                return o1.user_id - o2.user_id;
            }
        });
    }

    public ClanRequestJoin getJoinRequestByUserId(int user_id) {
        int left = 0;
        int right = requestJoin.Count - 1;
        while (left <= right) {
            int mid = left + (right - left) / 2;
            ClanRequestJoin midRequest = requestJoin.get(mid);
            if (midRequest.user_id == user_id) {
                return midRequest;
            }
            if (midRequest.user_id < user_id) {
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }
        return null;
    }

    public void addJoinRequest(int user_id, String name, String avatarPath)   {
        ClanRequestJoin clanRequestJoin = new ClanRequestJoin(user_id, name, Utilities.CurrentTimeMillis);
        clanRequestJoin.avatarPath = avatarPath;
        requestJoin.add(clanRequestJoin);
        requestJoin.sort(new Comparator<ClanRequestJoin>() {
             
            public int compare(ClanRequestJoin o1, ClanRequestJoin o2) {
                return o1.user_id - o2.user_id;
            }
        });
    }

    public void kick(int user_id)   {
        ClanMember clanMember = getMemberByUserId(user_id);
        if (clanMember != null) {
            members.remove(clanMember);
            curMember--;
        }

        Player player = PlayerManager.get(user_id);
        if (player != null) {
            Place place = player.getPlace();
            if (place == this.clanPlace) {
                MapManager.maps.get(11).addRandom(player);
            }
            player.redDialog("Bạn đã bị đá ra khỏi bang");
        }
    }

    public ClanMember getMemberByUserId(int user_id) {
        int left = 0;
        int right = members.Count - 1;
        while (left <= right) {
            int mid = left + (right - left) / 2;
            ClanMember midClanMem = members.get(mid);
            if (midClanMem.user_id == user_id) {
                return midClanMem;
            }
            if (midClanMem.user_id < user_id) {
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }
        return null;
    }

    public void update()   {
        this.clanPlace.update();
        for (ClanMember member : members) {
            if (member.needReset()) {
                member.reset();
            }
        }

        for (ClanBuff clanBuff : clanBuffs) {
            if (clanBuff.getTimeEndBuff() < Utilities.CurrentTimeMillis) {
                clanBuffs.remove(clanBuff);
            }
        }

        if (shopClan.getTimeRefresh() + (1000l * 60 * 60 * 24 * 7) <= Utilities.CurrentTimeMillis) {
            shopClan.refresh();
        }
    }

    public void create()   {
        MySqlConnection MySqlConnection = MYSQLManager.create();
        MYSQLManager.updateSql(Utilities.Format("INSERT INTO `clan`(`clanId`, `name`, `lvl`, `curMember`, `maxMember`, `leaderId`, `members`, `fund`, `growthPoint`, `skillHouseLvl`, `baseMarketLvl`) "
                + "VALUES (NULL,'%s','%s','%s','%s','%s','%s','%s','%s','%s','%s')", name, lvl, curMember, maxMember, leaderId, JsonManager.ToJson(members), fund, growthPoint, skillHouseLvl, baseMarketLvl), MySqlConnection);
        ResultSet resultSet = MYSQLManager.jquery(Utilities.Format("SELECT * FROM `clan` WHERE leaderId = %s", leaderId), MySqlConnection);
        if (resultSet.next()) {
            setClanId(resultSet.getInt("clanId"));
        } else {
            throw new NullPointerException("Không tìm thấy clan có người lãnh đạo này");
        }
        resultSet.close();
        MySqlConnection.close();
    }

    public void save()   {
        MySqlConnection MySqlConnection = MYSQLManager.create();
        MYSQLManager.updateSql(Utilities.Format("UPDATE `clan` set `lvl` = %s , `curMember` = %s , `maxMember` =%s , `leaderId` =%s , `members` = '%s' , `fund` =%s, `growthPoint` =%s , `skillHouseLvl` = %s , `baseMarketLvl` =%s , `joinRequest` = '%s' WHERE `clanId` =%s", lvl, curMember, maxMember, leaderId, JsonManager.ToJson(members), fund, growthPoint, skillHouseLvl, baseMarketLvl, JsonManager.ToJson(requestJoin), this.clanId), MySqlConnection);
        MySqlConnection.close();
    }

    public void setTemplate(ClanTemplate clanTemplate) {
        this.lvl = clanTemplate.getLvl();
        this.maxMember = clanTemplate.getMaxMember();
    }

    public void outClan(ClanMember clanMember)   {
        if (members.contains(clanMember)) {
            members.remove(clanMember);
            this.curMember = members.Count;
        }
    }

    public bool canAddNewMember() {
        return this.curMember < this.maxMember;
    }

    public void addChat(ClanChat clanChat) {
        if (clanChats.Count >= 50) {
            clanChats.remove(0);
        }
        clanChats.add(clanChat);
    }

    public   ClanPotentialSkill getClanPotentialSkillOrCreate(int buffId) {
        int left = 0;
        int right = clanPotentialSkills.Count - 1;
        while (left <= right) {
            int mid = left + (right - left) / 2;
            ClanPotentialSkill midP = clanPotentialSkills.get(mid);
            if (midP.getBuffId() == buffId) {
                return midP;
            }
            if (midP.getBuffId() < buffId) {
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }

        ClanPotentialSkill clanPotentialSkill = new ClanPotentialSkill();
        clanPotentialSkill.setBuffId(buffId);
        clanPotentialSkill.setPoint(0);
        this.clanPotentialSkills.addIfAbsent(clanPotentialSkill);
        this.clanPotentialSkills.sort(new Comparator<ClanPotentialSkill>() {
             
            public int compare(ClanPotentialSkill o1, ClanPotentialSkill o2) {
                return o1.getBuffId() - o2.getBuffId();
            }
        });
        return clanPotentialSkill;
    }

    public   ClanBuff getBuff(int index) {
        if (index >= 0 && index < clanBuffs.Count) {
            return clanBuffs.get(index);
        }
        return null;
    }

    public   ClanBuff getBuffByIdBuff(int buffId) {
        for (ClanBuff clanBuff : clanBuffs) {
            if (clanBuff.getBuffId() == buffId) {
                return clanBuff;
            }
        }
        return new ClanBuff(buffId, 0, Long.MAX_VALUE);
    }

    public void notEngouhPermission(Player player)   {
        player.redDialog("Bạn không đủ quyền");
    }
}
