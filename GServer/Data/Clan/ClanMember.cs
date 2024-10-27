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
        public sbyte duty;
        public DateTime timeJoin = DateTime.Now;
        public DateTime timeResetData = DateTime.Now;
        public Dictionary<int, DateTime> shopData = new Dictionary<int, DateTime>();
        public int curQuest = 0;
        [JsonIgnore]
        public Clan clan;

        public void reset()
        {
            timeResetData = DateTime.Now;
            curQuest = 0;
        }

        public bool needReset()
        {
            DateTime dateTime = timeResetData.AddDays(1);
            return dateTime < DateTime.Now;
        }

        public string getDutyName(Player player)
        {
            switch (duty)
            {
                case Clan.TYPE_NORMAL:
                    return player.Language.ClanDutyMember;
                case Clan.TYPE_LEADER:
                    return player.Language.ClanLeaderMember;
                case Clan.TYPE_DEPUTY_LEADER:
                    return player.Language.ClanDeputyLeaderMember;
                case Clan.TYPE_SENIOR:
                    return player.Language.ClanSeniorMember;
            }
            return $"Không xác định <{duty}>";
        }

        [JsonIgnore]
        public bool IsLeader
        {
            get
            {
                return duty == Clan.TYPE_LEADER || duty == Clan.TYPE_DEPUTY_LEADER;
            }
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