/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.clan;

import data.shop.ShopClan;
import java.sql.Connection;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.concurrent.CopyOnWriteArrayList;
import lombok.Getter;
import lombok.Setter;
import manager.GopetManager;
import manager.JsonManager;
import manager.MYSQLManager;
import manager.MapManager;
import manager.PlayerManager;
import place.ClanPlace;
import place.Place;
import server.Player;
import server.io.Message;

/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
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
    private int superMarketLvl = 0;
    private int potentialPoint = 0;
    private CopyOnWriteArrayList<ClanMember> members = new CopyOnWriteArrayList<ClanMember>();
    private CopyOnWriteArrayList<ClanRequestJoin> requestJoin = new CopyOnWriteArrayList<>();
    private CopyOnWriteArrayList<ClanBuff> clanBuffs = new CopyOnWriteArrayList<>();
    private CopyOnWriteArrayList<Integer> bannedJoinRequestId = new CopyOnWriteArrayList<>();
    private CopyOnWriteArrayList<ClanChat> clanChats = new CopyOnWriteArrayList<>();
    private CopyOnWriteArrayList<ClanPotentialSkill> clanPotentialSkills = new CopyOnWriteArrayList<>();
    private ClanPlace clanPlace;
    private String slogan = "HTCGOPET";
    private ShopClan shopClan;
    private Object LOCKObject = new Object();
    public const byte TYPE_LEADER = 0;
    public const byte TYPE_DEPUTY_LEADER = 1;
    public const byte TYPE_SENIOR = 2;
    public const byte TYPE_NORMAL = 3;

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

    public final ClanTemplate getTemp() {
        return GopetManager.clanTemp.get(lvl);
    }

    public final void addPotentialPoint(int value) {
        this.potentialPoint += value;
    }

    public final void sendMessage(Message m) {
        for (ClanMember member : members) {
            Player onlinePlayer = PlayerManager.get(member.user_id);
            if (onlinePlayer != null) {
                onlinePlayer.session.sendMessage(m);
            }
        }
    }

    public String getClanDesc() {
        ArrayList<String> clanInfo = new ArrayList<>();
        clanInfo.add(String.format(" Cấp: %s ", lvl));
        clanInfo.add(String.format(" Thành viên: %s/%s ", curMember, maxMember));
        clanInfo.add(String.format(" Shop bảo vật cấp: %s ", superMarketLvl));
        clanInfo.add(String.format(" Nhà kỹ năng bang hội cấp: %s ", skillHouseLvl));
        return String.join(",", clanInfo.toArray(new String[0]));
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

    public bool checkDuty(byte typeDuty) {
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
            @Override
            public int compare(ClanMember o1, ClanMember o2) {
                return o1.user_id - o2.user_id;
            }
        });
    }

    public ClanRequestJoin getJoinRequestByUserId(int user_id) {
        int left = 0;
        int right = requestJoin.size() - 1;
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
        ClanRequestJoin clanRequestJoin = new ClanRequestJoin(user_id, name, System.currentTimeMillis());
        clanRequestJoin.avatarPath = avatarPath;
        requestJoin.add(clanRequestJoin);
        requestJoin.sort(new Comparator<ClanRequestJoin>() {
            @Override
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
        int right = members.size() - 1;
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
            if (clanBuff.getTimeEndBuff() < System.currentTimeMillis()) {
                clanBuffs.remove(clanBuff);
            }
        }

        if (shopClan.getTimeRefresh() + (1000l * 60 * 60 * 24 * 7) <= System.currentTimeMillis()) {
            shopClan.refresh();
        }
    }

    public void create()   {
        Connection connection = MYSQLManager.create();
        MYSQLManager.updateSql(String.format("INSERT INTO `clan`(`clanId`, `name`, `lvl`, `curMember`, `maxMember`, `leaderId`, `members`, `fund`, `growthPoint`, `skillHouseLvl`, `superMarketLvl`) "
                + "VALUES (NULL,'%s','%s','%s','%s','%s','%s','%s','%s','%s','%s')", name, lvl, curMember, maxMember, leaderId, JsonManager.ToJson(members), fund, growthPoint, skillHouseLvl, superMarketLvl), connection);
        ResultSet resultSet = MYSQLManager.jquery(String.format("SELECT * FROM `clan` WHERE leaderId = %s", leaderId), connection);
        if (resultSet.next()) {
            setClanId(resultSet.getInt("clanId"));
        } else {
            throw new NullPointerException("Không tìm thấy clan có người lãnh đạo này");
        }
        resultSet.close();
        connection.close();
    }

    public void save()   {
        Connection connection = MYSQLManager.create();
        MYSQLManager.updateSql(String.format("UPDATE `clan` set `lvl` = %s , `curMember` = %s , `maxMember` =%s , `leaderId` =%s , `members` = '%s' , `fund` =%s, `growthPoint` =%s , `skillHouseLvl` = %s , `superMarketLvl` =%s , `joinRequest` = '%s' WHERE `clanId` =%s", lvl, curMember, maxMember, leaderId, JsonManager.ToJson(members), fund, growthPoint, skillHouseLvl, superMarketLvl, JsonManager.ToJson(requestJoin), this.clanId), connection);
        connection.close();
    }

    public void setTemplate(ClanTemplate clanTemplate) {
        this.lvl = clanTemplate.getLvl();
        this.maxMember = clanTemplate.getMaxMember();
    }

    public void outClan(ClanMember clanMember)   {
        if (members.contains(clanMember)) {
            members.remove(clanMember);
            this.curMember = members.size();
        }
    }

    public bool canAddNewMember() {
        return this.curMember < this.maxMember;
    }

    public void addChat(ClanChat clanChat) {
        if (clanChats.size() >= 50) {
            clanChats.remove(0);
        }
        clanChats.add(clanChat);
    }

    public final ClanPotentialSkill getClanPotentialSkillOrCreate(int buffId) {
        int left = 0;
        int right = clanPotentialSkills.size() - 1;
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
            @Override
            public int compare(ClanPotentialSkill o1, ClanPotentialSkill o2) {
                return o1.getBuffId() - o2.getBuffId();
            }
        });
        return clanPotentialSkill;
    }

    public final ClanBuff getBuff(int index) {
        if (index >= 0 && index < clanBuffs.size()) {
            return clanBuffs.get(index);
        }
        return null;
    }

    public final ClanBuff getBuffByIdBuff(int buffId) {
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
