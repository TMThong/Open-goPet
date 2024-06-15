using Dapper;
using Gopet.Data.Collections;
using Gopet.Data.User;
using Gopet.IO;
using Gopet.Util;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Gopet.Data.GopetClan
{
    public class Clan
    {
        public int clanId;
        public int curMember;
        public int maxMember;
        public String name;
        public int leaderId;
        public long fund = 0;
        public int lvl = 1;
        public int potentialSkill = 0;
        public CopyOnWriteArrayList<ClanMember> members = new CopyOnWriteArrayList<ClanMember>();
        public CopyOnWriteArrayList<ClanRequestJoin> requestJoin = new();
        public CopyOnWriteArrayList<int> bannedJoinRequestId = new();
        public CopyOnWriteArrayList<ClanChat> clanChats = new();
        public CopyOnWriteArrayList<ClanPotentialSkill> clanPotentialSkills = new();
        public ClanPlace clanPlace;
        public String slogan = "GOPET T";
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
            this.maxMember = clanTemplate.getMaxMember();
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

        public void setCurMember(int curMember)
        {
            this.curMember = curMember;
        }

        public void setMaxMember(int maxMember)
        {
            this.maxMember = maxMember;
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
            this.requestJoin = requestJoin;
        }


        public void setBannedJoinRequestId(CopyOnWriteArrayList<int> bannedJoinRequestId)
        {
            this.bannedJoinRequestId = bannedJoinRequestId;
        }

        public void setClanChats(CopyOnWriteArrayList<ClanChat> clanChats)
        {
            this.clanChats = clanChats;
        }

        public void setClanPotentialSkills(CopyOnWriteArrayList<ClanPotentialSkill> clanPotentialSkills)
        {
            this.clanPotentialSkills = clanPotentialSkills;
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

        public int getCurMember()
        {
            return this.curMember;
        }

        public int getMaxMember()
        {
            return this.maxMember;
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
            return this.requestJoin;
        }



        public CopyOnWriteArrayList<int> getBannedJoinRequestId()
        {
            return this.bannedJoinRequestId;
        }

        public CopyOnWriteArrayList<ClanChat> getClanChats()
        {
            return this.clanChats;
        }

        public CopyOnWriteArrayList<ClanPotentialSkill> getClanPotentialSkills()
        {
            return this.clanPotentialSkills;
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

        public static void mineGrowthPoint(Player player)
        {
            player.redDialog("Điểm phát triển không đủ không đủ");
        }

        public bool checkDuty(sbyte typeDuty)
        {
            int[] permission = new int[] { 1, 1, 5, this.getTemp().maxMember - 7};
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
            Player player = PlayerManager.get(user_id);
            ClanMember clanMember = new ClanMember();
            clanMember.clan = this;
            clanMember.name = name;
            clanMember.user_id = user_id;
            clanMember.fundDonate = 0l;
            clanMember.growthPointDonate = 0l;
            clanMember.duty = (user_id == leaderId ? TYPE_LEADER : TYPE_NORMAL);
            if (player != null)
            {
                clanMember.avatarPath = player.playerData.avatarPath;
            }
            members.add(clanMember);
            curMember++;

            members.Sort(new ClanMemeberComparer());
        }

        public ClanRequestJoin getJoinRequestByUserId(int user_id)
        {
            int left = 0;
            int right = requestJoin.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                ClanRequestJoin midRequest = requestJoin.get(mid);
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
            requestJoin.add(clanRequestJoin);
            requestJoin.Sort(new ClanRequestJoinComparer());
        }

        public void kick(int user_id)
        {
            ClanMember clanMember = getMemberByUserId(user_id);
            if (clanMember != null)
            {
                members.remove(clanMember);
                curMember--;
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

           

            if (shopClan.GetTimeMillisRefresh() + (1000l * 60 * 60 * 24 * 7) <= Utilities.CurrentTimeMillis)
            {
                shopClan.refresh();
            }
        }

        public void create()
        {
            using(var connection = MYSQLManager.create())
            {
                connection.Execute("INSERT INTO `clan`( `name`, `curMember`, `maxMember`, `leaderId`, `members`) VALUES (@name, @curMember, @maxMember, @leaderId, @members)", new { name = name, curMember  = curMember, maxMember = maxMember , leaderId  = leaderId , members = members });
                var clanData = connection.QueryFirstOrDefault("SELECT * FROM `clan` WHERE leaderId = @leaderId", new { leaderId = leaderId });
                if(clanData != null)
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

        public void save()
        {
            using(MySqlConnection MySqlConnection = MYSQLManager.create())
            {
                
            }
        }

        public void setTemplate(ClanTemplate clanTemplate)
        {
            this.lvl = clanTemplate.getLvl();
            this.maxMember = clanTemplate.getMaxMember();
        }

        public void outClan(ClanMember clanMember)
        {
            if (members.Contains(clanMember))
            {
                members.remove(clanMember);
                this.curMember = members.Count;
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
            clanChats.add(clanChat);
        }

        sealed class ClanPotentialSkillComparer : IComparer<ClanPotentialSkill>
        {
            public int Compare(ClanPotentialSkill? o1, ClanPotentialSkill? o2)
            {
                return o1.getBuffId() - o2.getBuffId();
            }
        }

        public ClanPotentialSkill getClanPotentialSkillOrCreate(int buffId)
        {
            int left = 0;
            int right = clanPotentialSkills.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                ClanPotentialSkill midP = clanPotentialSkills.get(mid);
                if (midP.getBuffId() == buffId)
                {
                    return midP;
                }
                if (midP.getBuffId() < buffId)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            ClanPotentialSkill clanPotentialSkill = new ClanPotentialSkill();
            clanPotentialSkill.setBuffId(buffId);
            clanPotentialSkill.setPoint(0);
            this.clanPotentialSkills.addIfAbsent(clanPotentialSkill);
            this.clanPotentialSkills.Sort(new ClanPotentialSkillComparer());

            return clanPotentialSkill;
        }

        public static void notEngouhPermission(Player player)
        {
            player.redDialog("Bạn không đủ quyền");
        }
    }
}