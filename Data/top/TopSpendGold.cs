
using Dapper;
using Gopet.Data.User;
using Gopet.Util;

public class TopSpendGold : Top
{

    public static readonly TopSpendGold instance = new TopSpendGold();

    public TopSpendGold() : base("top_spendgold")
    {

        base.name = "Top Đại gia xuống núi";
        base.desc = "Chỉ người nạp số tiền cao nhất";
    }

    public TopData find(int user_id)
    {
        foreach (TopData data in datas)
        {
            if (data.id == user_id)
            {
                return data;
            }
        }
        return null;
    }


    public override void update()
    {
        try
        {
            lastDatas.Clear();
            lastDatas.AddRange(datas);
            datas.Clear();
            try
            {
                
                using(var conn = MYSQLManager.create())
                {
                    var topDataDynamic = conn.Query("SELECT * FROM `player`  WHERE isAdmin = 0 ORDER BY  `spendGold` DESC LIMIT 300;");
                    int index = 1;
                    foreach
                        (dynamic data in topDataDynamic)
                    {
                        TopData topData = new TopData();
                        topData.id = data.user_id;
                        topData.name = data.name;
                        topData.imgPath = data.avatarPath;
                        topData.title = topData.name;
                        topData.desc = Utilities.Format("Hạng %s: Đã tiêu %s (vang)", index, Utilities.FormatNumber(data.spendGold));
                        datas.add(topData);
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
