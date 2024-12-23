using Dapper;
using Gopet.Data.Clan;
using Gopet.Data.Collections;
using Gopet.Data.User;
using Gopet.IO;
using Gopet.Util;
using MySqlConnector;
using Newtonsoft.Json;

namespace Gopet.Data.GopetClan
{
    public class Clan
    {
        public int clanId;
        public String name;
        public int leaderId;
        public long fund = 0;
        public int lvl = 1;
        public int slotSkill = 0;
        public int potentialSkill = 0;
        public CopyOnWriteArrayList<ClanMember> members = new CopyOnWriteArrayList<ClanMember>();
        public CopyOnWriteArrayList<ClanRequestJoin> joinRequest = new();
        public CopyOnWriteArrayList<int> bannedJoinRequestId = new();
        public CopyOnWriteArrayList<ClanChat> clanChats = new();
        public CopyOnWriteArrayList<ClanSkill> SkillRent = new CopyOnWriteArrayList<ClanSkill>();
        public Dictionary<int, int> SkillInfo = new Dictionary<int, int>();
        public int[] Options = new int[0];
        public ClanPlace clanPlace;
        public String slogan = "GOPET TAE";
        public ShopClan shopClan;
        public Mutex LOCKObject = new Mutex();
        public const sbyte TYPE_LEADER = 0;
        public const sbyte TYPE_DEPUTY_LEADER = 1;
        public const sbyte TYPE_SENIOR = 2;
        public const sbyte TYPE_NORMAL = 3;

        public Clan()
        {

        }

        public Clan(String name, int leaderId, String leaderName)
        {
            this.name = name;
            this.leaderId = leaderId;
            ClanTemplate clanTemplate = GopetManager.clanTemp.get(lvl);
            addMember(leaderId, leaderName);
            initClan();
        }

        public Clan(int clanId)
        {
            this.clanId = clanId;
            initClan();
        }

        public void setClanId(int clanId)
        {
            this.clanId = clanId;
        }


        public void setName(String name)
        {
            this.name = name;
        }

        public void setLeaderId(int leaderId)
        {
            this.leaderId = leaderId;
        }

        public void setFund(long fund)
        {
            this.fund = fund;
        }

        public void setLvl(int lvl)
        {
            this.lvl = lvl;
        }

        public void setMembers(CopyOnWriteArrayList<ClanMember> members)
        {
            this.members = members;
        }

        public void setRequestJoin(CopyOnWriteArrayList<ClanRequestJoin> requestJoin)
        {
            this.joinRequest = requestJoin;
        }


        public void setBannedJoinRequestId(CopyOnWriteArrayList<int> bannedJoinRequestId)
        {
            this.bannedJoinRequestId = bannedJoinRequestId;
        }

        public void setClanChats(CopyOnWriteArrayList<ClanChat> clanChats)
        {
            this.clanChats = clanChats;
        }


        public void setClanPlace(ClanPlace clanPlace)
        {
            this.clanPlace = clanPlace;
        }

        public void setSlogan(String slogan)
        {
            this.slogan = slogan;
        }

        public void setShopClan(ShopClan shopClan)
        {
            this.shopClan = shopClan;
        }


        public int getClanId()
        {
            return this.clanId;
        }

        public int curMember
        {
            get
            {
                return this.members.Count;
            }
        }


        public int maxMember
        {
            get
            {
                return this.getTemp().maxMember;
            }
        }

        public String getName()
        {
            return this.name;
        }

        public int getLeaderId()
        {
            return this.leaderId;
        }

        public long getFund()
        {
            return this.fund;
        }



        public int getLvl()
        {
            return this.lvl;
        }




        public CopyOnWriteArrayList<ClanMember> getMembers()
        {
            return this.members;
        }

        public CopyOnWriteArrayList<ClanRequestJoin> getRequestJoin()
        {
            return this.joinRequest;
        }



        public CopyOnWriteArrayList<int> getBannedJoinRequestId()
        {
            return this.bannedJoinRequestId;
        }

        public CopyOnWriteArrayList<ClanChat> getClanChats()
        {
            return this.clanChats;
        }

        public ClanPlace getClanPlace()
        {
            return this.clanPlace;
        }

        public String getSlogan()
        {
            return this.slogan;
        }

        public ShopClan getShopClan()
        {
            return this.shopClan;
        }

        public Object getLOCKObject()
        {
            return this.LOCKObject;
        }


        public ClanTemplate getTemp()
        {
            return GopetManager.clanTemp.get(lvl);
        }


        public void sendMessage(Message m)
        {
            foreach (ClanMember member in members)
            {
                Player onlinePlayer = PlayerManager.get(member.user_id);
                if (onlinePlayer != null)
                {
                    onlinePlayer.session.sendMessage(m);
                }
            }
        }

        public String getClanDesc()
        {
            JArrayList<String> clanInfo = new();
            clanInfo.add(Utilities.Format(" Cấp: %s ", lvl));
            clanInfo.add(Utilities.Format(" Thành viên: %s/%s ", curMember, maxMember));
            return String.Join(",", clanInfo.ToArray());
        }

        public void initClan()
        {
            clanPlace = new ClanPlace(MapManager.maps.get(30), this.clanId);
        }

        public void addFund(long value, ClanMember clanMember)
        {
            if (members.Contains(clanMember))
            {
                this.fund += value;
                clanMember.fundDonate += value;
            }
        }


        public bool checkFund(long value)
        {
            return this.fund >= value;
        }



        public void mineFund(long value)
        {
            this.fund -= value;
        }


        public static void notEnoughFund(Player player)
        {
            player.redDialog("Quỹ không đủ");
        }

        public static void notEnoughFund(Player player, long value)
        {
            player.redDialog($"Quỹ không đủ, cần {value.ToString("###,###")} điểm!");
        }

        public static void mineGrowthPoint(Player player)
        {
            player.redDialog("Điểm phát triển không đủ không đủ");
        }

        public bool checkDuty(sbyte typeDuty)
        {
            int[] permission = new int[] { 1, 1, 5, this.getTemp().maxMember - 7 };
            int maxOfDutye = permission[typeDuty];
            int cur = 0;
            foreach (ClanMember member in members)
            {
                if (member.duty == typeDuty)
                {
                    cur++;
                }
            }
            return maxOfDutye > cur;
        }

        public void showFullDuty(Player player)
        {
            player.redDialog("Chức vụ này đã dành cho người khác rồi hoặc do cấp bang hội quá thấp");
        }

        public void addMember(int user_id, String name)
        {
            if (this.members.Where(p => p.user_id == user_id).Any())
                return;
            Player player = PlayerManager.get(user_id);
            ClanMember clanMember = new ClanMember();
            clanMember.clan = this;
            clanMember.name = name;
            clanMember.user_id = user_id;
            clanMember.fundDonate = 0l;
            clanMember.duty = (user_id == leaderId ? TYPE_LEADER : TYPE_NORMAL);
            if (player != null)
            {
                clanMember.avatarPath = player.playerData.avatarPath;
            }
            members.Add(clanMember);
            members.Sort(new ClanMemeberComparer());
        }

        public void removeClanMemberDuplicate()
        {
            foreach (var mem in members)
            {
                var listQuery = members.Where(p => p.user_id == mem.user_id);
                if (listQuery.Count() > 1)
                {
                    for (global::System.Int32 i = 1; i < listQuery.Count(); i++)
                    {
                        members.remove(listQuery.ElementAt(i));
                    }
                }
            }
        }

        public ClanRequestJoin getJoinRequestByUserId(int user_id)
        {
            int left = 0;
            int right = joinRequest.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                ClanRequestJoin midRequest = joinRequest.get(mid);
                if (midRequest.user_id == user_id)
                {
                    return midRequest;
                }
                if (midRequest.user_id < user_id)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return null;
        }

        public void addJoinRequest(int user_id, String name, String avatarPath)
        {
            ClanRequestJoin clanRequestJoin = new ClanRequestJoin(user_id, name, Utilities.CurrentTimeMillis);
            clanRequestJoin.avatarPath = avatarPath;
            joinRequest.Add(clanRequestJoin);
            joinRequest.Sort(new ClanRequestJoinComparer());
        }

        public void kick(int user_id)
        {
            ClanMember clanMember = getMemberByUserId(user_id);
            if (clanMember != null)
            {
                members.remove(clanMember);
            }
            Player player = PlayerManager.get(user_id);
            if (player != null)
            {
                Place place = player.getPlace();
                if (place == this.clanPlace)
                {
                    MapManager.maps.get(11).addRandom(player);
                }
                player.redDialog("Bạn đã bị đá ra khỏi bang");
            }
        }

        public ClanMember getMemberByUserId(int user_id)
        {
            int left = 0;
            int right = members.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                ClanMember midClanMem = members.get(mid);
                if (midClanMem.user_id == user_id)
                {
                    return midClanMem;
                }
                if (midClanMem.user_id < user_id)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return null;
        }

        public void update()
        {
            this.clanPlace.update();
            foreach (ClanMember member in members)
            {
                if (member.needReset())
                {
                    member.reset();
                }
            }

            foreach (var skillRent_ in SkillRent)
            {
                if (skillRent_.Expire < DateTime.Now)
                    this.SkillRent.Remove(skillRent_);
            }
        }

        public void create()
        {
            using (var connection = MYSQLManager.create())
            {
                connection.Execute("INSERT INTO `clan`( `name`, `leaderId`, `members`) VALUES (@name, @leaderId, @members)", new { name = name, leaderId = leaderId, members = members });
                var clanData = connection.QueryFirstOrDefault("SELECT * FROM `clan` WHERE leaderId = @leaderId", new { leaderId = leaderId });
                if (clanData != null)
                {
                    setClanId(clanData.clanId);
                    setShopClan(new ShopClan(this));
                }
                else
                {
                    throw new NullReferenceException("Không tìm thấy clan có người lãnh đạo này");
                }
            }
        }

        public void save(MySqlConnection conn)
        {
            conn.Execute("UPDATE `clan` SET `lvl`=@lvl,`leaderId`=@leaderId,`members`=@members,`fund`=@fund,`slogan`=@slogan,`joinRequest`=@joinRequest, `potentialSkill` = @potentialSkill, `SkillRent` = @SkillRent, `SkillInfo` = @SkillInfo , `slotSkill` = @slotSkill WHERE clanId= @clanId",
                new { lvl = lvl, leaderId = leaderId, members = members, fund = fund, slogan = slogan, joinRequest = joinRequest, potentialSkill = potentialSkill, SkillRent = SkillRent, SkillInfo = SkillInfo, clanId = clanId, slotSkill = slotSkill });
        }

        public void save()
        {
            using (MySqlConnection MySqlConnection = MYSQLManager.create())
            {
                save(MySqlConnection);
            }
        }


        public void outClan(ClanMember clanMember)
        {
            if (members.Contains(clanMember))
            {
                members.remove(clanMember);
            }
        }

        public bool canAddNewMember()
        {
            return this.curMember < this.maxMember;
        }

        public void addChat(ClanChat clanChat)
        {
            if (clanChats.Count >= 50)
            {
                clanChats.removeAt(0);
            }
            clanChats.Add(clanChat);
        }

        sealed class ClanPotentialSkillComparer : IComparer<ClanPotentialSkill>
        {
            public int Compare(ClanPotentialSkill? o1, ClanPotentialSkill? o2)
            {
                return o1.getBuffId() - o2.getBuffId();
            }
        }

        public static void notEngouhPermission(Player player)
        {
            player.redDialog("Bạn không đủ quyền");
        }

        public PetSkillInfo[] getAllSkillRent
        {
            get
            {
                List<PetSkillInfo> petSkillInfos = new List<PetSkillInfo>();

                foreach (var skill in this.SkillRent)
                {
                    if (skill.Expire < DateTime.Now)
                        continue;
                    foreach (var item1 in skill.PetSkillInfos)
                    {
                        var queryList = petSkillInfos.Where(p => p.id == item1.id);
                        if (queryList.Any())
                        {
                            queryList.First().value += item1.value;
                        }
                        else
                        {
                            petSkillInfos.Add(new PetSkillInfo(item1.id, item1.value));
                        }
                    }
                }

                return petSkillInfos.ToArray();
            }
        }

        public PetSkillInfo Search(int Id)
        {
            var queryList = this.getAllSkillRent.Where(p => p.id == Id);
            if (queryList.Any())
                return queryList.First();

            return new PetSkillInfo(Id, 0);
        }
    }
}