 
public class TopSpendGold : Top {

    public static readonly TopSpendGold instance = new TopSpendGold();

    public TopSpendGold()  : base("top_spendgold"){
       
        base.name = "Top Đại gia xuống núi";
        base.desc = "Chỉ người nạp số tiền cao nhất";
    }

    public TopData find(int user_id) {
        foreach (TopData data in datas) {
            if (data.id == user_id) {
                return data;
            }
        }
        return null;
    }

     
    public override void update() {
        try {
            lastDatas.Clear();
            lastDatas.AddRange(datas);
            datas.Clear();
            try (ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `player`  WHERE isAdmin = 0 ORDER BY  `spendGold` DESC LIMIT 300;")) {
                int index = 1;
                while (resultSet.next()) {
                    TopData topData = new TopData();
                    topData.id = resultSet.getInt("user_id");
                    topData.name = resultSet.getString("name");
                    topData.imgPath = resultSet.getString("avatarPath");
                    topData.title = topData.name;
                    topData.desc = Utilities.Format("Hạng %s: Đã tiêu %s (vang)", index, Utilities.formatNumber(resultSet.getBigDecimal("spendGold").longValue()));
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
