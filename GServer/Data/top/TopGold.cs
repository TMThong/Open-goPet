using Gopet.Util;
using Gopet.Data.User;
using Dapper;
using System;
using System.Reflection;

public class TopGold : Top
{

    private String[] topNameStrings = new String[] { "Phú hộ", "Bá hộ", "Chủ nông" };
    public static TopGold Instance = new TopGold();

    public TopGold() : base("top_gold")
    {

        base.name = "TOP Đại gia";
        base.desc = "Chỉ những người chơi giàu có";
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
        topData.desc = $"Hạng chưa có : Bạn đang có {Utilities.FormatNumber(player.playerData.gold)} (vang)";
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
                    var topDataDynamic = conn.Query("SELECT user_id ,name, avatarPath , gold FROM `player` WHERE gold > 0 && isAdmin = 0 ORDER BY `player`.`gold` DESC LIMIT 10");
                    int index = 1;
                    foreach
                        (dynamic data in topDataDynamic)
                    {
                        TopData topData = new TopData();
                        topData.id = data.user_id;
                        topData.name = data.name;
                        topData.imgPath = data.avatarPath;
                        topData.title = topData.name;
                        topData.desc = Utilities.Format("Hạng %s : đang có %s (vang)", index, Utilities.FormatNumber(data.gold));
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
