using Dapper;
using Gopet.Data.Event.Year2024;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event.Year2025
{
    public class GameBirthdayEvent : EventBase
    {
        public static readonly GameBirthdayEvent Instance = new GameBirthdayEvent();

        public const int NPC_BIRTHDAY_CAKE = -41;

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

        protected GameBirthdayEvent()
        {
            this.Name = "Sự kiện sinh nhật";
        }

        public override bool Condition => true;


        public override void Init()
        {
            if (this.Condition)
            {
                BXHManager.listTop.Add(TopUseCylindricalStickyRiceCake.Instance);
                BXHManager.listTop.Add(TopUseSquareStickyRiceCake.Instance);
                MapTemplate mapTemplate = GopetManager.mapTemplate[MapTemplate.THÀNH_PHỐ_LINH_THÚ];
                if (!mapTemplate.npc.Contains(NPC_BIRTHDAY_CAKE))
                {
                    mapTemplate.npc = mapTemplate.npc.Concat(new int[] { NPC_BIRTHDAY_CAKE }).ToArray();
                }
                foreach (var item1 in GopetManager.shopTemplate[MenuController.SHOP_GIAN_THUONG].getShopTemplateItems())
                {
                    ShopTemplateItem shopTemplateItem = item1.Clone();
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
                TopUseCylindricalStickyRiceCake.Instance.Update();
                TopUseSquareStickyRiceCake.Instance.Update();
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

            }
        }

        public override void Update()
        {

        }

        public override void NpcOption(Player player, int optionId)
        {
            if (this.CheckEventStatus(player))
            {
                switch (optionId)
                {
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
                    default:
                        player.redDialog(Name + " không có option này");
                        break;
                }
            }
        }

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
    }
}
