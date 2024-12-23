using Dapper;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Language;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event.Year2024
{
    /// <summary>
    /// Sự kiện 20/11 Năm 2024
    /// </summary>
    public class TeacherDay2024 : EventBase
    {
        public static readonly TeacherDay2024 Instance = new TeacherDay2024();

        public static readonly Tuple<long, int>[] Data = new Tuple<long, int>[]
        {
            new Tuple<long, int>(500, 30),
            new Tuple<long, int>(20000, 30)
        };

        public static readonly Tuple<int, int, int>[] GiftMilistones = new Tuple<int, int, int>[]
        {
            new Tuple<int, int,int>(5000, 134,2),
            new Tuple<int, int,int>(10000, 140,2),
            new Tuple<int, int,int>(50000, 141,2),
            new Tuple<int, int,int>(100000, 142,2),
        };

        public const int BOÁ_HOA = 240018;

        public override int[] ItemsOfEvent
        {
            get => new int[] { BOÁ_HOA };
        }
        protected TeacherDay2024()
        {
            this.Name = "Sự kiện ngày nhà giáo Việt Nam";
        }

        public override void UseItem(int itemId, Player player)
        {
            if (itemId == BOÁ_HOA)
            {
                player.redDialog(player.Language.PleaseGoToNPCToUseFlower);
            }
        }

        public override void Init()
        {
            if (Condition)
            {
                LanguageData languageData = GopetManager.Language[GopetManager.VI_CODE];
                foreach (var item1 in GopetManager.Language)
                {
                    item1.Value.NpcOptionLanguage[MenuController.OP_TẶNG_HOA_NPC] = item1.Value.GiveFlowerToNPC;
                    item1.Value.NpcOptionLanguage[MenuController.OP_XEM_TOP_FLOWER_GOLD] = item1.Value.ViewTopFlowerGold;
                    item1.Value.NpcOptionLanguage[MenuController.OP_XEM_TOP_FLOWER_GEM] = item1.Value.ViewTopFlowerGem;
                    item1.Value.NpcOptionLanguage[MenuController.OP_NHẬN_QUÀ_MỐC] = item1.Value.GetMilistoneGiftTeacherEvent;
                }
                BXHManager.listTop.Add(TopFlowerGold.Instance);
                BXHManager.listTop.Add(TopFlowerGem.Instance);
                NpcTemplate npcTemplate = GopetManager.npcTemplate[NpcTemplate.TRẦN_CHÂN];
                npcTemplate.optionId = npcTemplate.optionId.Concat(new int[] { MenuController.OP_TẶNG_HOA_NPC, MenuController.OP_XEM_TOP_FLOWER_GOLD, MenuController.OP_XEM_TOP_FLOWER_GEM, MenuController.OP_NHẬN_QUÀ_MỐC }).ToArray();
                npcTemplate.optionName = npcTemplate.optionName.Concat(new string[] { languageData.GiveFlowerToNPC, languageData.ViewTopFlowerGold, languageData.ViewTopFlowerGem, languageData.GetMilistoneGiftTeacherEvent }).ToArray();
            }
        }


        public void ReceiveMilistoneGift(Player player)
        {
            if (Condition)
            {
                int point = Math.Max(player.playerData.NumGiveFlowerGold, player.playerData.NumGiveFlowerGem);
                if (player.playerData.IndexMilistoneTeacherEvent < GiftMilistones.Length)
                {
                    for (int i = GiftMilistones.Length - 1; i >= 0; i--)
                    {
                        Tuple<int, int, int> milistone = GiftMilistones[i];
                        if (point >= milistone.Item1)
                        {
                            Item item = new Item(milistone.Item2, milistone.Item3);
                            player.addItemToInventory(item);
                            player.okDialog(player.Language.GetMilistoneGiftTeacherEventOK, item.getName(player), i + 1);
                            player.playerData.IndexMilistoneTeacherEvent = 100;
                            return;
                        }
                    }
                    player.redDialog(player.Language.GetMilistoneGiftTeacherEventErorr, Utilities.FormatNumber(point), Utilities.FormatNumber(GiftMilistones.First().Item1));
                }
                else player.redDialog(player.Language.InvalidFlowerMilestone);
            }
            else player.redDialog(player.Language.EventHadFinished);
        }


        public override bool Condition
        {
            get
            {
                return DateTime.Now > new DateTime(2024, 11, 17, 0, 0, 0) && DateTime.Now < new DateTime(2024, 12, 24);
            }
        }

        public class TopFlowerGold : Top
        {
            public static readonly TopFlowerGold Instance = new TopFlowerGold();

            protected TopFlowerGold() : base("teacher.2024.flower.gold.top")
            {
                this.name = "TOP Tặng Vàng, Dâng Hoa - Lễ Mừng Ngày Nhà Giáo";
                /* 
                 Prompt
                 Tôi đang làm sự kiện để kỉ niệm ngày nhà giáo việt nam hãy ghi cho tôi tiêu đề cho việc người chơi dùng vàng và hoa để tặng npc
                 */
            }
            public override TopData getMyInfo(Player player)
            {
                var findTop = datas.Where(p => p.id == player.playerData.user_id);
                if (findTop.Any())
                {
                    return findTop.First();
                }
                TopData topData = new TopData();
                topData.id = player.playerData.user_id;
                topData.name = player.playerData.name;
                topData.imgPath = player.playerData.avatarPath;
                topData.title = topData.name;
                topData.desc = $"Hạng chưa có. Bạn đang có {Utilities.FormatNumber(player.playerData.NumGiveFlowerGold)} hoa vàng. Chúc mừng Ngày Nhà Giáo Việt Nam 20/11!";
                return topData;
            }

            public override void Update()
            {
                try
                {
                    lastDatas.Clear();
                    lastDatas.AddRange(datas);
                    datas.Clear();
                    using (var conn = MYSQLManager.create())
                    {
                        var topDataDynamic = conn.Query("SELECT user_id, name,avatarPath,NumGiveFlowerGold  FROM `player` WHERE  isAdmin = 0 ORDER BY `player`.`NumGiveFlowerGold` DESC LIMIT 50");
                        int index = 1;
                        foreach
                            (dynamic data in topDataDynamic)
                        {
                            TopData topData = new TopData();
                            topData.id = data.user_id;
                            topData.name = data.name;
                            topData.imgPath = data.avatarPath;
                            topData.title = topData.name;
                            topData.desc = $"Hạng {index}. Bạn đang có {Utilities.FormatNumber(data.NumGiveFlowerGold)} hoa vàng. Chúc mừng Ngày Nhà Giáo Việt Nam 20/11!";
                            datas.Add(topData);
                            index++;
                        }
                    }
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }

            public override string HrImagePath
            {
                get => "items/240018.png";
            }
        }

        public class TopFlowerGem : Top
        {
            public static readonly TopFlowerGem Instance = new TopFlowerGem();

            protected TopFlowerGem() : base("teacher.2024.flower.gold.top")
            {
                this.name = "TOP Dâng Ngọc Tặng Hoa - Tỏ Lòng Tri Ân Thầy Cô";
                /* 
                 Prompt
                 Tôi đang làm sự kiện để kỉ niệm ngày nhà giáo việt nam hãy ghi cho tôi tiêu đề cho việc người chơi dùng ngọc và hoa để tặng npc
                 */
            }
            public override TopData getMyInfo(Player player)
            {
                var findTop = datas.Where(p => p.id == player.playerData.user_id);
                if (findTop.Any())
                {
                    return findTop.First();
                }
                TopData topData = new TopData();
                topData.id = player.playerData.user_id;
                topData.name = player.playerData.name;
                topData.imgPath = player.playerData.avatarPath;
                topData.title = topData.name;
                topData.desc = $"Hạng chưa có. Bạn đang sở hữu {Utilities.FormatNumber(player.playerData.NumGiveFlowerGem)} hoa và ngọc. Cùng chung tay tặng thầy cô nhân Ngày Nhà Giáo Việt Nam 20/11!";
                return topData;
            }

            public override string HrImagePath
            {
                get => "items/240018.png";
            }

            public override void Update()
            {
                try
                {
                    lastDatas.Clear();
                    lastDatas.AddRange(datas);
                    datas.Clear();
                    using (var conn = MYSQLManager.create())
                    {
                        var topDataDynamic = conn.Query("SELECT user_id, name,avatarPath,NumGiveFlowerGem  FROM `player` WHERE  isAdmin = 0 ORDER BY `player`.`NumGiveFlowerGem` DESC LIMIT 50");
                        int index = 1;
                        foreach
                            (dynamic data in topDataDynamic)
                        {
                            TopData topData = new TopData();
                            topData.id = data.user_id;
                            topData.name = data.name;
                            topData.imgPath = data.avatarPath;
                            topData.title = topData.name;
                            topData.desc = $"Hạng {index}. Bạn đang sở hữu {Utilities.FormatNumber(data.NumGiveFlowerGem)} hoa và ngọc. Cùng chung tay tặng thầy cô nhân Ngày Nhà Giáo Việt Nam 20/11!";
                            datas.Add(topData);
                            index++;
                        }
                    }
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        }
    }
}
