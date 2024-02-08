
using Dapper;
using Gopet.Data.User;
using Gopet.Util;

public class TopGem : Top
{

    public static readonly TopGem instance = new TopGem();

    public TopGem() : base("top_gem")
    {

        base.name = "TOP Phú Hộ";
        base.desc = "";
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

                using (var conn = MYSQLManager.create())
                {
                    var topDataDynamic = conn.Query("SELECT * FROM `player` WHERE coin > 0 && isAdmin = 0 ORDER BY `player`.`coin` DESC LIMIT 10");
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
                        datas.add(topData);
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
