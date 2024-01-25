
using Gopet.Data.User;
using Gopet.Util;

public class TopLVLClan : Top {

    public static readonly TopLVLClan instance = new TopLVLClan();

    public TopLVLClan() : base("top_clan")
    {
       
        base.name = "TOP LVL Bang hội";
        base.desc = "Chỉ những bang hội có cấp độ cao";
    }

    
    public override void update() {
        try {
            lastDatas.Clear();
            lastDatas.AddRange(datas);
            datas.Clear();
            try   {
                ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `clan` ORDER BY `lvl` DESC LIMIT 10;");
                int index = 1;
                while (resultSet.next()) {
                    TopData topData = new TopData();
                    topData.id = resultSet.getInt("clanId");
                    topData.name = "Bang " + resultSet.getString("name");
                    topData.imgPath = "npcs/gopet.png";
                    topData.title = topData.name;
                    topData.desc = Utilities.Format("Hạng %s : bang lvl %s", index, resultSet.getInt("lvl"));
                    datas.add(topData);
                    index++;
                }
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
            updateSQLBXH();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
