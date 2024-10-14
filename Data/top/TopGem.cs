
using Dapper;
using Gopet.Data.User;
using Gopet.Util;

public class TopGem : Top
{

    public static readonly TopGem Instance = new TopGem();

    public TopGem() : base("top_gem")
    {

        base.name = "TOP Phú Hộ";
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
        topData.desc = $"Hạng chưa có : Bạn đang có {Utilities.FormatNumber(player.playerData.coin)} (ngoc)";
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
                    var topDataDynamic = conn.Query("SELECT user_id,name,avatarPath,coin FROM `player` WHERE coin > 0 && isAdmin = 0 ORDER BY `player`.`coin` DESC LIMIT 10");
                    int index = 1;
                    foreach
                        (dynamic data in topDataDynamic)
                    {
                        TopData topData = new TopData();
                        topData.id = data.user_id;
                        topData.name = data.name;
                        topData.imgPath = data.avatarPath;
                        topData.title = topData.name;
                        topData.desc = Utilities.Format("Hạng %s : đang có %s (ngoc)", index, Utilities.FormatNumber(data.coin));
                        datas.Add(topData);
                        index++;
                    }
                }
            }
            catch(Exception e)
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
