using Gopet.Data.Collections;
using Gopet.Util;
using Newtonsoft.Json;

namespace Gopet.Data.GopetClan
{
    public class ClanMember
    {
        public string avatarPath;
        public int user_id;
        public string name;
        public long fundDonate = 0;
        public long growthPointDonate = 0;
        public sbyte duty = Clan.TYPE_NORMAL;
        public DateTime timeJoin = DateTime.Now;
        public DateTime timeResetData = DateTime.Now;
        public int curQuest = 0;
        public CopyOnWriteArrayList<ClanMemberDonateInfo> clanMemberDonateInfos;
        [JsonIgnore]
        public Clan clan;

        public void reset()
        {
            timeResetData = DateTime.Now;
            clanMemberDonateInfos = new(new ClanMemberDonateInfo[]
            {
            new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_COIN, 10000l, 0, 10, 3),
            new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_COIN, 100000l, 0, 110, 3),
            new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_GOLD, 1000l, 20, 100, 3),
            new ClanMemberDonateInfo(GopetManager.MONEY_TYPE_GOLD, 5000l, 100, 550, 3)

        });
            curQuest = 0;
        }

        public bool needReset()
        {
            DateTime resetDateTime =  timeResetData;
            DateTime serer = Utilities.GetCurrentDate();
            return resetDateTime.Day != serer.Day || resetDateTime.Month != serer.Month || resetDateTime.Year != serer.Year;
        }

        public string getDutyName()
        {
            switch (duty)
            {
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

        public string getAvatar()
        {
            if (avatarPath == null)
            {
                return "npcs/gopet.png";
            }
            return avatarPath;
        }

        internal Clan getClan()
        {
            return clan;
        }
    }
}