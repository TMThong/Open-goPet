using Dapper;
using Gopet.Data.GopetItem;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Event.Year2024
{
    internal class Summer2024Event : EventBase
    {
        public static IEnumerable<EventData> EventDatas { get; set; }
        public static Summer2024Event Instance => new Summer2024Event();
        private DateTime EndOfEvent = new DateTime(2024, 6, 1);
        public const int ITEM_PAPER = 240000;
        public const int ITEM_KITE_FABRIC = 240001;
        public const int ITEM_KITE_PAPER = 240002;
        public const int ITEM_BAMBOO = 240003;
        public const int ITEM_ROPE = 240004;
        public const int ITEM_COLOR_ART_VIP = 240005;
        public const int ITEM_COLOR_ART_NORMAL = 240006;
        public const int ITEM_FABRIC = 240007;

        public const byte RECIPE_KITE_NORMAL = 0;
        public const byte RECIPE_KITE_VIP = 1;
        private DateTime timeUpdateSQL = DateTime.Now;
        public override int[] ItemsOfEvent => new int[] {
            ITEM_PAPER,
            ITEM_KITE_FABRIC,
            ITEM_KITE_PAPER,
            ITEM_BAMBOO,
            ITEM_ROPE,
            ITEM_COLOR_ART_VIP,
            ITEM_COLOR_ART_NORMAL,
            ITEM_FABRIC,
        };


        public static readonly Dictionary<byte, Tuple<int[], int[], int[], int[]>> RECIPES = new Dictionary<byte, Tuple<int[], int[], int[], int[]>>()
        {
            [RECIPE_KITE_NORMAL] = new Tuple<int[], int[], int[], int[]>(new int[] { ITEM_PAPER, 5 }, new int[] { ITEM_BAMBOO, 5 }, new int[] { ITEM_ROPE, 5 }, new int[] { ITEM_COLOR_ART_NORMAL, 1 }),
            [RECIPE_KITE_VIP] = new Tuple<int[], int[], int[], int[]>(new int[] { ITEM_FABRIC, 10 }, new int[] { ITEM_BAMBOO, 10 }, new int[] { ITEM_ROPE, 10 }, new int[] { ITEM_COLOR_ART_VIP, 1 })
        };


        public Dictionary<int, Mutex> MutexEventKite = new Dictionary<int, Mutex>()
        {
            [ITEM_KITE_PAPER] = new Mutex(),
            [ITEM_KITE_FABRIC] = new Mutex()
        };

        public override string Name => "Sự kiện mùa hè năm 2024";
        public override bool Condition
        {
            get
            {
                return DateTime.Now < EndOfEvent;
            }
        }
        public override bool NeedRemove
        {
            get
            {
                return !Condition;
            }
        }

        public byte GetType(int itemId) => (itemId == ITEM_KITE_FABRIC ? (byte)1 : (byte)0);

        public override void UseItem(int itemId, Player player)
        {
            Item itemEvent = player.controller.selectItemsbytemp(itemId, GopetManager.NORMAL_INVENTORY);
            if (!player.controller.checkCount(itemId, 1, GopetManager.NORMAL_INVENTORY))
            {
                player.fastAction();
                return;
            }
            if (MutexEventKite.ContainsKey(itemId))
            {
                MutexEventKite[itemId].WaitOne();
                try
                {
                    var findEventData = EventDatas.Where(p => p.Type == GetType(itemId) && p.Max > p.NumOfUse);
                    if (findEventData.Any())
                    {
                        while (findEventData.Any())
                        {
                            var randomData = Utilities.RandomArray(findEventData);
                            if (randomData.Percent >= Utilities.NextFloatPer())
                            {
                                player.controller.subCountItem(itemEvent, 1, GopetManager.NORMAL_INVENTORY);
                                randomData.NumOfUse++;
                                Item item = new Item(randomData.ItemId);
                                item.count = 1;
                                item.SourcesItem.Add(Data.item.ItemSource.TỪ_SỰ_KIỆN);
                                player.okDialog("Chúc mừng bạn nhận được " + item.Template.name);
                                player.addItemToInventory(item);
                                if (GetType(itemId) == RECIPE_KITE_NORMAL)
                                    player.playerData.NumOfUseKiteNormal++;
                                else
                                    player.playerData.NumOfUseKiteVip++;
                                break;
                            }
                            else continue;
                        }
                    }
                    else
                    {
                        player.redDialog("Rất tiếc hết quà rồi");
                    }
                }
                finally
                {
                    MutexEventKite[itemId].ReleaseMutex();
                }
            }
            else
            {
                player.redDialog("Không thể sử dụng vật phẩm này");
            }
        }


        public void MakeKite(byte type, Player player)
        {
            var data = RECIPES[type];
            int[][] material = new int[4][] { data.Item1, data.Item2, data.Item3, data.Item4 };
            for (int i = 0; i < material.Length; i++)
            {
                if (!player.controller.checkCount(material[i][0], material[i][1], GopetManager.NORMAL_INVENTORY))
                {
                    player.redDialog("Không thể làm vật phẩm sự kiện do không đủ nguyên liệu");
                    return;
                }
            }
            for (int i = 0; i < material.Length; i++)
            {
                player.controller.subCountItem(player.controller.selectItemsbytemp(material[i][0], GopetManager.NORMAL_INVENTORY), material[i][1], GopetManager.NORMAL_INVENTORY);
            }
            int itemID = type == RECIPE_KITE_NORMAL ? ITEM_KITE_PAPER : ITEM_KITE_FABRIC;
            Item item = new Item(itemID);
            item.SourcesItem.Add(Data.item.ItemSource.TỪ_SỰ_KIỆN);
            item.count = 1;
            player.addItemToNormalInventory(item);
            player.okDialog("Làm diều thành công");
        }


        public string RecipeText(byte type)
        {
            var data = RECIPES[type];

            string[] materialText = new string[4];
            int[][] material = new int[4][] { data.Item1, data.Item2, data.Item3, data.Item4 };
            for (int i = 0; i < material.Length; i++)
            {
                materialText[i] = GopetManager.itemTemplate[material[i][0]].name + " x" + material[i][1];
            }
            return $" {string.Join('+', materialText)}";
        }



        public override void Update()
        {
            if (timeUpdateSQL < DateTime.Now)
            {
                using (var conn = MYSQLManager.create())
                {
                    foreach (var eventData in EventDatas)
                    {
                        conn.Execute("UPDATE `summer_2024_event` SET  NumOfUse = @NumOfUse Where Id = @Id", eventData);
                    }
                }
                timeUpdateSQL.AddMinutes(5);
            }
        }

        static Summer2024Event()
        {
            BXHManager.listTop.Add(TopKiteNormal.Instance);
            BXHManager.listTop.Add(TopKiteVip.Instance);
        }


        public class TopKiteNormal : Top
        {
            public static readonly TopKiteNormal Instance = new TopKiteNormal();

            public TopKiteNormal() : base("top.kite.normal")
            {
                base.name = "Top diều giấy";
                base.desc = "Top những người sử dụng diều thường trong sự kiện";
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
                topData.desc = $"Hạng chưa có : Bạn đang có {Utilities.FormatNumber(player.playerData.NumOfUseKiteNormal)} lần sử dụng diều giấy";
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
                        var topDataDynamic = conn.Query("SELECT * FROM `player` WHERE  isAdmin = 0 ORDER BY `player`.`NumOfUseKiteNormal` DESC LIMIT 10");
                        int index = 1;
                        foreach
                            (dynamic data in topDataDynamic)
                        {
                            TopData topData = new TopData();
                            topData.id = data.user_id;
                            topData.name = data.name;
                            topData.imgPath = data.avatarPath;
                            topData.title = topData.name;
                            topData.desc = Utilities.Format("Hạng %s : đang có %s lần sử dụng diều giấy", index, Utilities.FormatNumber(data.NumOfUseKiteNormal));
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

        public class TopKiteVip : Top
        {
            public static readonly TopKiteVip Instance = new TopKiteVip();

            public TopKiteVip() : base("top.kite.vip")
            {
                base.name = "Top diều vải";
                base.desc = "Top những người sử dụng diều vải trong sự kiện";
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
                topData.desc = $"Hạng chưa có : Bạn đang có {Utilities.FormatNumber(player.playerData.NumOfUseKiteVip)} lần sử dụng diều vải";
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
                        var topDataDynamic = conn.Query("SELECT * FROM `player` WHERE  isAdmin = 0 ORDER BY `player`.`NumOfUseKiteVip` DESC LIMIT 10");
                        int index = 1;
                        foreach
                            (dynamic data in topDataDynamic)
                        {
                            TopData topData = new TopData();
                            topData.id = data.user_id;
                            topData.name = data.name;
                            topData.imgPath = data.avatarPath;
                            topData.title = topData.name;
                            topData.desc = Utilities.Format("Hạng %s : đang có %s lần sử dụng diều vải", index, Utilities.FormatNumber(data.NumOfUseKiteVip));
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

        public class EventData
        {
            public int Id { get; set; }
            public byte Type { get; set; }
            public int ItemId { get; set; }

            public int Max { get; set; }

            public int NumOfUse { get; set; }

            public float Percent { get; set; }
        }
    }
}
