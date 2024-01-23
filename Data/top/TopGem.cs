 
public class TopGem : Top {

    public static readonly TopGem instance = new TopGem();

    public TopGem() : base("top_gem")
    {
        
        base.name = "TOP Phú Hộ";
        base.desc = "";
    }

    
    public override void update() {
        try {

            lastDatas.Clear();
            lastDatas.AddRange(datas);
            datas.Clear();
            try (ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `player` WHERE coin > 0 && isAdmin = 0 ORDER BY `player`.`coin` DESC LIMIT 10")) {
                int index = 1;
                while (resultSet.next()) {
                    TopData topData = new TopData();
                    topData.id = resultSet.getInt("user_id");
                    topData.name = resultSet.getString("name");
                    topData.imgPath = resultSet.getString("avatarPath");
                    topData.title = topData.name;
                    topData.desc = Utilities.Format("Hạng %s : đang có %s (ngoc)", index, Utilities.formatNumber(resultSet.getBigDecimal("coin").longValue()));
                    datas.add(topData);
                    index++;
                }
            }
            updateSQLBXH();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
