using Gopet.Util;
using Gopet.Data.User;

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
                ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `player` WHERE gold > 0 && isAdmin = 0 ORDER BY `player`.`gold` DESC LIMIT 10");
                int index = 1;
                while (resultSet.next())
                {
                    TopData topData = new TopData();
                    topData.id = resultSet.getInt("user_id");
                    topData.name = resultSet.getString("name");
                    topData.imgPath = resultSet.getString("avatarPath");
                    topData.title = topData.name;
                    topData.desc = Utilities.Format("Hạng %s : đang có %s (vang)", index, Utilities.FormatNumber(resultSet.getBigDecimal("gold").longValue()));
                    datas.add(topData);
                    index++;
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
