using Dapper;
using Gopet.Data.Collections;
using Gopet.Data.Event.Year2024;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.Mob;
using Gopet.Manager;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event.Year2025
{
    /// <summary>
    /// Sự kiện sinh nhật
    /// </summary>
    public class GameBirthdayEvent : EventBase
    {
        /// <summary>
        /// Sự kiện sinh nhật
        /// </summary>
        public static readonly GameBirthdayEvent Instance = new GameBirthdayEvent();
        /// <summary>
        /// NPC bánh sinh nhật
        /// </summary>
        public const int NPC_BIRTHDAY_CAKE = -41;
        /// <summary>
        /// ID bánh chưng
        /// </summary>
        public const int ID_SQUARE_CAKE = 240019;
        /// <summary>
        /// ID bánh tét
        /// </summary>
        public const int ID_CYLINDRIAL_CAKE = 240020;
        /// <summary>
        /// ID lá dong
        /// </summary>
        public const int ID_PHRYNIUM = 240022;
        /// <summary>
        /// ID gạo nếp
        /// </summary>
        public const int ID_GLUTINOUS = 240021;
        /// <summary>
        /// ID boss bánh sinh nhật
        /// </summary>
        public const int ID_BOSS_BIRTHDAY_CAKE = 23;
        /// <summary>
        /// Dữ liệu làm bánh sự kiện
        /// </summary>
        public static readonly Tuple<long, int>[] Data = new Tuple<long, int>[]
        {
            new Tuple<long, int>(500, 15),
            new Tuple<long, int>(20000, 15)
        };
        /// <summary>
        /// Mốc quà sự kiện
        /// </summary>
        public static readonly Tuple<int, int, int>[] GiftMilistones = new Tuple<int, int, int>[]
        {
            new Tuple<int, int,int>(20000, 134,2),
            new Tuple<int, int,int>(50000, 140,2),
            new Tuple<int, int,int>(200000, 141,2),
            new Tuple<int, int,int>(300000, 142,2),
        };
        /// <summary>
        /// ID Hộp quà Tết 2025
        /// </summary>
        public const int ID_RANDOM_EVENT_BOX = 240023;
        /// <summary>
        /// Dữ liệu hộp quà Tết 2025
        /// </summary>
        public static readonly int[][] GIFT_OPEN_EVENT_BOX_DATA = new int[][] { new int[] { 9, 1, 1, 809, 1, -135, 1, 693, 1, -137, 1, 5161, 1, 5162, 1, 5163, 1, 5164, 1, 5150, 1, 5151, 1, 5152, 1, 5153, 1, 5154, 1, 185, 1, -136, 1, 136, 1, 137, 1, 138, 1, 139, 1, 132, 1, 133, 1, 134, 1, 135 } };

        protected GameBirthdayEvent()
        {
            this.Name = "Sự kiện sinh nhật";
        }
        /// <summary>
        /// Điều kiện sự kiện
        /// </summary>
        public override bool Condition => DateTime.Now < new DateTime(2025, 2, 18);
        /// <summary>
        /// Danh sách item sự kiện
        /// </summary>
        public override int[] ItemsOfEvent { get; set; } = new int[] { ID_SQUARE_CAKE, ID_CYLINDRIAL_CAKE, ID_PHRYNIUM, ID_GLUTINOUS, ID_RANDOM_EVENT_BOX };

        /// <summary>
        /// Khởi tạo sự kiện
        /// </summary>
        /// <exception cref="UnsupportedOperationException"></exception>
        public override void Init()
        {
            if (this.Condition)
            {
                for (global::System.Int32 i = 0; i < 25; i++)
                {
                    ScheduleManager.Instance.AddScheduleItem(new SummonBossSchedule(i, 0));
                }
                ScheduleManager.Instance.ReleaseMutex();
                BXHManager.listTop.Add(TopUseCylindricalStickyRiceCake.Instance);
                BXHManager.listTop.Add(TopUseSquareStickyRiceCake.Instance);
                BXHManager.listTop.Add(TopUseGiftLootBox2025.Instance);
                GopetManager.shopTemplate[MenuController.SHOP_PET].shopTemplateItems.add(new ShopTemplateItem()
                {
                    itemTemTempleId = 865,
                    shopId = MenuController.SHOP_PET,
                    price = new int[] { 1000000 },
                    moneyType = new sbyte[] { GopetManager.MONEY_TYPE_COIN },
                    count = 1,
                    CloseScreenAfterClick = false,
                });
                MapTemplate mapTemplate = GopetManager.mapTemplate[MapTemplate.THÀNH_PHỐ_LINH_THÚ];
                if (!mapTemplate.npc.Contains(NPC_BIRTHDAY_CAKE))
                {
                    mapTemplate.npc = mapTemplate.npc.Concat(new int[] { NPC_BIRTHDAY_CAKE }).ToArray();
                }
                foreach (var shopItemTemp in GopetManager.shopTemplate[MenuController.SHOP_GIAN_THUONG].getShopTemplateItems())
                {
                    ShopTemplateItem shopTemplateItem = shopItemTemp.Clone();
                    for (global::System.Int32 i = 0; i < shopTemplateItem.moneyType.Length; i++)
                    {
                        switch (shopTemplateItem.moneyType[i])
                        {
                            case GopetManager.MONEY_TYPE_FLOWER_COIN:
                                shopTemplateItem.moneyType[i] = GopetManager.MONEY_TYPE_CYLINDRIAL_COIN;
                                break;
                            case GopetManager.MONEY_TYPE_FLOWER_GOLD:
                                shopTemplateItem.moneyType[i] = GopetManager.MONEY_TYPE_SQUARE_COIN;
                                break;
                            default:
                                throw new UnsupportedOperationException();
                        }
                    }
                    GopetManager.shopTemplate[MenuController.SHOP_BIRTHDAY_EVENT].shopTemplateItems.Add(shopTemplateItem);
                }
                IDictionary<int, float> MapPercent = new Dictionary<int, float>();
                MapPercent.Add(MapTemplate.LINH_LÂM, 50f);
                MapPercent.Add(MapTemplate.ĐẠI_LINH_CẢNH, 40f);
                MapPercent.Add(MapTemplate.LINH_MỘC, 30f);
                MapPercent.Add(MapTemplate.ĐƯỜNG_LÊN_ĐỈNH_NÚI, 20f);
                MapPercent.Add(MapTemplate.NÚI_PHỤC_QUANG, 10f);
                foreach (var mapTemp in GopetManager.mapTemplate)
                {
                    float percent = 5f;
                    if (MapPercent.ContainsKey(mapTemp.Key))
                    {
                        percent = MapPercent[mapTemp.Key];
                    }
                    GopetManager.dropItem[mapTemp.Key].Add(new DropItem(mapTemp.Key, -1, ID_GLUTINOUS, percent, new int[] { 0, 99 }, 1));
                    GopetManager.dropItem[mapTemp.Key].Add(new DropItem(mapTemp.Key, -1, ID_PHRYNIUM, percent, new int[] { 0, 99 }, 1));
                }
                TopUseCylindricalStickyRiceCake.Instance.Update();
                TopUseSquareStickyRiceCake.Instance.Update();
                TopUseGiftLootBox2025.Instance.Update();
            }
        }

        public void ReceiveMilistoneGift(Player player)
        {
            if (Condition)
            {
                int point = Math.Max(player.playerData.NumEatSquareStickyRice, player.playerData.NumEatCylindricalStickyRice);
                if (player.playerData.IndexMilistoneBirthdayEvent < GiftMilistones.Length)
                {
                    for (int i = GiftMilistones.Length - 1; i >= 0; i--)
                    {
                        Tuple<int, int, int> milistone = GiftMilistones[i];
                        if (point >= milistone.Item1)
                        {
                            Item item = new Item(milistone.Item2, milistone.Item3);
                            player.addItemToInventory(item);
                            player.okDialog(player.Language.GetMilistoneGiftTeacherEventOK, item.getName(player), i + 1);
                            player.playerData.IndexMilistoneBirthdayEvent = 100;
                            return;
                        }
                    }
                    player.redDialog(player.Language.GetMilistoneGiftBirthdayEventErorr, Utilities.FormatNumber(point), Utilities.FormatNumber(GiftMilistones.First().Item1));
                }
                else player.redDialog(player.Language.InvalidFlowerMilestone);
            }
            else player.redDialog(player.Language.EventHadFinished);
        }


        public override void UseItem(int itemId, Player player)
        {
            if (this.CheckEventStatus(player))
            {
                switch (itemId)
                {
                    case ID_RANDOM_EVENT_BOX:
                        UseEventItem(itemId, player);
                        break;
                    case ID_CYLINDRIAL_CAKE:
                        MenuController.ShowUseItemCountDialog(player, ID_CYLINDRIAL_CAKE);
                        break;
                    case ID_SQUARE_CAKE:
                        MenuController.ShowUseItemCountDialog(player, ID_SQUARE_CAKE);
                        break;
                    default:
                        player.redDialog(player.Language.ThisEventItemIsMaterial);
                        break;
                }
            }
        }

        public override void UseItemCount(int itemId, Player player, int count)
        {
            if (this.CheckEventStatus(player))
            {
                Item item = player.controller.selectItemsbytemp(itemId, GopetManager.NORMAL_INVENTORY);
                if (item != null && GameController.checkCount(item, count))
                {
                    switch (itemId)
                    {
                        case ID_CYLINDRIAL_CAKE:
                            {
                                player.playerData.NumEatCylindricalStickyRice += 10 * count;
                                player.playerData.NumEatCylindricalStickyRiceCoin += 10 * count;
                                player.controller.subCountItem(item, count, GopetManager.NORMAL_INVENTORY);
                                player.okDialog(player.Language.EatCylindricalStickyRiceOK);
                            }
                            break;
                        case ID_SQUARE_CAKE:
                            {
                                player.playerData.NumEatSquareStickyRice += 10 * count;
                                player.playerData.NumEatSquareStickyRiceCoin += 10 * count;
                                player.controller.subCountItem(item, count, GopetManager.NORMAL_INVENTORY);
                                player.okDialog(player.Language.EatSquareStickyRiceOK);
                            }
                            break;
                        default:
                            player.redDialog(player.Language.ThisEventItemIsMaterial);
                            break;
                    }
                }
            }
        }

        void UseEventItem(int itemId, Player player)
        {
            Item item = player.controller.selectItemsbytemp(itemId, GopetManager.NORMAL_INVENTORY);
            if (item != null)
            {
                if (GameController.checkCount(item, 1))
                {
                    switch (item.itemTemplateId)
                    {
                        case ID_SQUARE_CAKE:

                            break;
                        case ID_CYLINDRIAL_CAKE:

                            break;
                        case ID_RANDOM_EVENT_BOX:
                            player.playerData.NumUseGiftBox2025++;
                            player.controller.subCountItem(item, 1, GopetManager.NORMAL_INVENTORY);
                            JArrayList<Popup> popups = player.controller.onReiceiveGift(GIFT_OPEN_EVENT_BOX_DATA);
                            JArrayList<String> textInfo = new();
                            foreach (Popup popup in popups)
                            {
                                textInfo.add(popup.getText());
                            }
                            player.okDialog(string.Format(player.Language.GetGiftCodeOK, String.Join(",", textInfo)));
                            break;
                        default:
                            throw new UnsupportedOperationException();
                    }
                }
            }
        }

        public override void Update()
        {

        }

        void MakeCake(sbyte Type, Player player)
        {
            if (Type >= 0 && Type < Data.Length)
            {
                Tuple<long, int> data = Data[Type];
                if (MenuController.checkMoney(Type, data.Item2, player))
                {
                    Item itemGlutinous = player.controller.selectItemsbytemp(ID_GLUTINOUS, GopetManager.NORMAL_INVENTORY);
                    Item itemPhrynium = player.controller.selectItemsbytemp(ID_PHRYNIUM, GopetManager.NORMAL_INVENTORY);
                    if (itemGlutinous != null && itemPhrynium != null)
                    {
                        if (GameController.checkCount(itemGlutinous, data.Item2) && GameController.checkCount(itemPhrynium, data.Item2))
                        {
                            MenuController.addMoney(Type, -data.Item1, player);
                            player.controller.subCountItem(itemGlutinous, data.Item2, GopetManager.NORMAL_INVENTORY);
                            player.controller.subCountItem(itemPhrynium, data.Item2, GopetManager.NORMAL_INVENTORY);
                            Item item = new Item(Type == 0 ? ID_SQUARE_CAKE : ID_CYLINDRIAL_CAKE, 1);
                            player.addItemToInventory(item);
                            player.okDialog(player.Language.MakeCakeOK);
                        }
                        else player.redDialog(player.Language.NotEnoughMaterial);
                    }
                    else player.redDialog(player.Language.MakeCakeErorr);
                }
                else MenuController.NotEngouhMoney(Type, data.Item2, player);
            }
        }
        public override void NpcOption(Player player, int optionId)
        {
            if (this.CheckEventStatus(player))
            {
                switch (optionId)
                {
                    case MenuController.OP_TOP_USE_GIFT_BOX_2025:
                        MenuController.showTop(TopUseGiftLootBox2025.Instance, player);
                        return;
                    case MenuController.OP_SHOW_SHOP_BIRTHDAY:
                        MenuController.showShop(MenuController.SHOP_BIRTHDAY_EVENT, player);
                        return;
                    case MenuController.OP_TOP_USE_CYLINDRICAL_STICKY_RICE_CAKE:
                        MenuController.showTop(TopUseCylindricalStickyRiceCake.Instance, player);
                        return;
                    case MenuController.OP_TOP_USE_SQUARE_STICKY_RICE_CAKE:
                        MenuController.showTop(TopUseSquareStickyRiceCake.Instance, player);
                        return;
                    case MenuController.OP_RECIVE_GIFT_MILISTONE_BIRTHDAY_EVNT:
                        ReceiveMilistoneGift(player);
                        return;
                    case MenuController.OP_MAKE_SQUARE_STICKY_RICE_CAKE:
                        MakeCake(0, player);
                        return;
                    case MenuController.OP_MAKE_CYLINDRICAL_STICKY_RICE_CAKE:
                        MakeCake(1, player);
                        return;
                    case MenuController.OP_GUIDE_EVENT_GAME_BIRTHDAY:
                        player.okDialog(player.Language.GuideEventGameBirthday);
                        return;
                    default:
                        player.redDialog(Name + " không có option này");
                        break;
                }
            }
        }
        /// <summary>
        /// Top ăn bánh tét
        /// </summary>
        protected class TopUseCylindricalStickyRiceCake : Top
        {
            public static readonly TopUseCylindricalStickyRiceCake Instance = new TopUseCylindricalStickyRiceCake();
            protected TopUseCylindricalStickyRiceCake() : base("use.cylindricalStickyRice")
            {
                this.name = "Top ăn bánh tét";
                this.desc = "Để chỉ số lần ăn bánh tét";
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
                        var topDataDynamic = conn.Query("SELECT user_id, name,avatarPath,NumEatCylindricalStickyRice  FROM `player` WHERE  isAdmin = 0 ORDER BY `player`.`NumEatCylindricalStickyRice` DESC LIMIT 50");
                        int index = 1;
                        foreach
                            (dynamic data in topDataDynamic)
                        {
                            TopData topData = new TopData();
                            topData.id = data.user_id;
                            topData.name = data.name;
                            topData.imgPath = data.avatarPath;
                            topData.title = topData.name;
                            topData.desc = $"Hạng {index}. Bạn đang có {Utilities.FormatNumber(data.NumEatCylindricalStickyRice)} điểm bánh tét.";
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
        /// <summary>
        /// Top ăn bánh chưng
        /// </summary>
        protected class TopUseSquareStickyRiceCake : Top
        {
            public static readonly TopUseSquareStickyRiceCake Instance = new TopUseSquareStickyRiceCake();
            public TopUseSquareStickyRiceCake() : base("use.squareStickyRice")
            {
                this.name = "Top ăn bánh chưng";
                this.desc = "Để chỉ số lần ăn bánh chưng";
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
                        var topDataDynamic = conn.Query("SELECT user_id, name,avatarPath,NumEatSquareStickyRice  FROM `player` WHERE  isAdmin = 0 ORDER BY `player`.`NumEatSquareStickyRice` DESC LIMIT 50");
                        int index = 1;
                        foreach
                            (dynamic data in topDataDynamic)
                        {
                            TopData topData = new TopData();
                            topData.id = data.user_id;
                            topData.name = data.name;
                            topData.imgPath = data.avatarPath;
                            topData.title = topData.name;
                            topData.desc = $"Hạng {index}. Bạn đang có {Utilities.FormatNumber(data.NumEatSquareStickyRice)} điểm bánh chưng.";
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

        public class TopUseGiftLootBox2025 : Top
        {
            public static readonly TopUseGiftLootBox2025 Instance = new TopUseGiftLootBox2025();
            protected TopUseGiftLootBox2025() : base("top.use.gift.loot.box.2025")
            {
                this.name = "Top sử dụng hộp quà Tết 2025";
                this.desc = "Để chỉ số lần sử dụng hộp quà Tết 2025";
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
                        var topDataDynamic = conn.Query("SELECT user_id, name,avatarPath, NumUseGiftBox2025  FROM `player` WHERE  isAdmin = 0 ORDER BY `player`.`NumUseGiftBox2025` DESC LIMIT 50");
                        int index = 1;
                        foreach
                            (dynamic data in topDataDynamic)
                        {
                            TopData topData = new TopData();
                            topData.id = data.user_id;
                            topData.name = data.name;
                            topData.imgPath = data.avatarPath;
                            topData.title = topData.name;
                            topData.desc = $"Hạng {index}. Bạn đã sử dụng {Utilities.FormatNumber(data.NumUseGiftBox2025)} hộp quà.";
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

        /// <summary>
        /// Lịch triệu hồi boss
        /// </summary>
        public class SummonBossSchedule : ScheduleManager.ScheduleItem
        {
            public SummonBossSchedule()
            {
            }

            public SummonBossSchedule(int? minute) : base(minute)
            {
            }

            public SummonBossSchedule(int? hour, int? minute) : base(hour, minute)
            {
            }

            public SummonBossSchedule(int? hour, int? minute, bool isNeedRemove) : base(hour, minute, isNeedRemove)
            {
            }

            public override void Execute()
            {
                DateTime dateTime = DateTime.Now.AddSeconds(3);
                while (dateTime > DateTime.Now)
                {
                    var gopetMaps = MapManager.mapArr.Where(x => x.mapID != MapTemplate.ẢI && x.places.Any(m => ((GopetPlace)m).mobs.Any())).ToArray();
                    var map = Utilities.RandomArray(gopetMaps);
                    var place = (GopetPlace)Utilities.RandomArray(map.places);
                    var mobs = place.mobs.Where(x => !x.HasBattle);
                    if (!mobs.Any())
                    {
                        continue;
                    }
                    Mob.Mob mob = Utilities.RandomArray(mobs);
                    place.mobs.remove(mob);
                    Boss boss = new Boss(ID_BOSS_BIRTHDAY_CAKE, mob.getMobLocation());
                    boss.isTimeOut = true;
                    boss.TimeOut = DateTime.Now.AddMilliseconds(GopetManager.TIME_BOSS_DISPOINTED);
                    place.addNewMob(boss);
                    PlayerManager.showBannerZ(string.Format("Boss {0} của sự kiện sinh nhật đã xuất hiện tại {1} nhanh tay nhé", boss.Template.name, place.map.mapTemplate.name));
#if DEBUG_LOG
                    GopetManager.ServerMonitor.LogWarning(string.Format("Boss {0} của sự kiện sinh nhật đã xuất hiện tại {1} khu {2} nhanh tay nhé", boss.Template.name, place.map.mapTemplate.name, place.zoneID));
#endif
                    return;
                }
            }
        }
    }
}
