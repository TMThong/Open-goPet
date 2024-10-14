
using Dapper;
using Gopet.Data.User;
using Gopet.Util;

public class TopSpendGold : Top
{
    public static readonly TopSpendGold Instance = new TopSpendGold();
    public TopSpendGold() : base("top_spendgold")
    {
        base.name = "Top Đại gia xuống núi";
        base.desc = "Chỉ người nạp số tiền cao nhất";
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
        topData.desc = $"Hạng chưa có : Bạn đã tiêu {Utilities.FormatNumber(player.playerData.spendGold)} (vang)";
        return topData;
    }

    public TopData find(int user_id)
    {
        return this.datas.Where(data => data.id == user_id).FirstOrDefault();
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
                    var topDataDynamic = conn.Query("SELECT user_id,name,avatarPath,spendGold FROM `player`  WHERE isAdmin = 0 ORDER BY  `spendGold` DESC LIMIT 300;");
                    int index = 1;
                    foreach (dynamic data in topDataDynamic)
                    {
                        TopData topData = new TopData();
                        topData.id = data.user_id;
                        topData.name = data.name;
                        topData.imgPath = data.avatarPath;
                        topData.title = topData.name;
                        topData.desc = Utilities.Format("Hạng %s: Đã tiêu %s (vang)", index, Utilities.FormatNumber(data.spendGold));
                        datas.Add(topData);
                        index++;
                    }
                    topDataDynamic = null;
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
