using Dapper;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.top
{
    public class TopAccumulatedPoint : Top
    {

        public static readonly TopAccumulatedPoint Instance = new TopAccumulatedPoint();

        public TopAccumulatedPoint() : base("Top.Accumulated.Point")
        {
            base.name = "Top điểm tích lũy";
            base.desc = "";
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
            topData.desc = $"Hạng chưa có : Bạn đang có {Utilities.FormatNumber(player.playerData.AccumulatedPoint)} (diem)";
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
                    var topDataDynamic = conn.Query("SELECT user_id, name,avatarPath,AccumulatedPoint  FROM `player` WHERE  isAdmin = 0 ORDER BY `player`.`AccumulatedPoint` DESC LIMIT 50");
                    int index = 1;
                    foreach
                        (dynamic data in topDataDynamic)
                    {
                        TopData topData = new TopData();
                        topData.id = data.user_id;
                        topData.name = data.name;
                        topData.imgPath = data.avatarPath;
                        topData.title = topData.name;
                        topData.desc = Utilities.Format("Hạng %s : đang có %s (diem)", index, Utilities.FormatNumber(data.AccumulatedPoint));
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
