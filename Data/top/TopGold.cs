using Gopet.Util;
using Gopet.Data.User;
using Dapper;

public class TopGold : Top
{

    private String[] topNameStrings = new String[] { "Phú hộ", "Bá hộ", "Chủ nông" };
    public static TopGold instance = new TopGold();

    public TopGold() : base("top_gold")
    {

        base.name = "TOP Đại gia";
        base.desc = "Chỉ những người chơi giàu có";
    }


    public void update()
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
                    var topDataDynamic = conn.Query("SELECT * FROM `player` WHERE gold > 0 && isAdmin = 0 ORDER BY `player`.`gold` DESC LIMIT 10");
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
