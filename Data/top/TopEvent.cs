using Dapper;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.top
{
    public class TopEvent : Top
    {
        public static TopEvent Instance = new TopEvent();

        private static string[] TOP_MASK = new string[]
        {
            "Chiến Sĩ Cao Thượng",
            "Anh Hùng Huyền Thoại",
            "Siêu Chiến Binh",
            "Thần Thú Vô Song",
            "Vị Vua Chiến Trường",
            "Người Chinh Phục",
            "Đại Đô Vật Chiến Trường",
            "Bậc Thầy Chiến Đấu",
            "Siêu Nhân Vô Địch",
            "Thiên Tài Chiến Thuật"
        };


        public TopEvent() : base("top.event")
        {
            this.name = "Bảng Anh Hùng";
            this.desc = string.Empty;

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
            topData.desc = $"Hạng chưa có : Bạn đang có {Utilities.FormatNumber(player.playerData.EventPoint)} điểm sự kiện";
            return topData;
        }

        public override void Update()
        {
            try
            {

                lastDatas.Clear();
                lastDatas.AddRange(datas);
                datas.Clear();
                try
                {

                    using (var conn = MYSQLManager.create())
                    {
                        var topDataDynamic = conn.Query("SELECT user_id,name,avatarPath,EventPoint FROM `player` WHERE coin > 0 && isAdmin = 0 ORDER BY `player`.`EventPoint` DESC LIMIT 20");
                        int index = 1;
                        foreach
                            (dynamic data in topDataDynamic)
                        {
                            TopData topData = new TopData();
                            topData.id = data.user_id;
                            topData.name = data.name;
                            topData.imgPath = data.avatarPath;
                            topData.title = topData.name + (index - 1 > TOP_MASK.Length - 1 ? string.Empty : $" (Biệt hiệu: {TOP_MASK[index - 1]})");
                            topData.desc = Utilities.Format("Hạng %s : Đang có %s điểm sự kiện", index, Utilities.FormatNumber(data.EventPoint));
                            datas.Add(topData);
                            index++;
                        }
                    }
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
                updateSQLBXH();
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
        }
    }
}
