 
public class ClanMember extends DataVersion {
    public String avatarPath;
    public int user_id;
    public String name;
    public long fundDonate = 0;
    public long growthPointDonate = 0;
    public sbyte duty = Clan.TYPE_NORMAL;
    public long timeJoin = System.currentTimeMillis();
    public long timeResetData = System.currentTimeMillis() / 2L;
    public int curQuest = 0;
    public CopyOnWriteArrayList<ClanMemberDonateInfo> clanMemberDonateInfos;
    @Getter
    @Setter
    private transient Clan clan;

    public void reset() {
        timeResetData = System.currentTimeMillis();
        clanMemberDonateInfos = new CopyOnWriteArrayList<>(new ClanMemberDonateInfo[]{
            new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_COIN, 10000l, 0, 10, 3),
            new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_COIN, 100000l, 0, 110, 3),
            new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_GOLD, 1000l, 20, 100, 3),
            new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_GOLD, 5000l, 100, 550, 3)

        });
        this.curQuest = 0;
    }

    public bool needReset() {
        Date resetDate = new Date(timeResetData);
        Date serer = Utilities.getCurrentDate();
        return resetDate.getDay() != serer.getDay() || resetDate.getMonth() != serer.getMonth() || resetDate.getYear() != serer.getYear();
    }

    public String getDutyName() {
        switch (duty) {
            case Clan.TYPE_NORMAL:
                return "Thành viên";
            case Clan.TYPE_LEADER:
                return "Bang chủ";
            case Clan.TYPE_DEPUTY_LEADER:
                return "Phó bang";
            case Clan.TYPE_SENIOR:
                return "Trưởng lão";
        }
        return "";
    }

    public String getAvatar() {
        if (avatarPath == null) {
            return "npcs/gopet.png";
        }
        return avatarPath;
    }
}
