
using Dapper;
using Gopet.Data.User;
using Gopet.Util;

public class TopLVLClan : Top {

    public static readonly TopLVLClan Instance = new TopLVLClan();

    public TopLVLClan() : base("top_clan")
    {
       
        base.name = "TOP LVL Bang hội";
        base.desc = "Chỉ những bang hội có cấp độ cao";
    }

    
    public override void Update() {
        try {
            lastDatas.Clear();
            lastDatas.AddRange(datas);
            datas.Clear();
            try   {
                using (var conn = MYSQLManager.create())
                {
                    var topDataDynamic = conn.Query("SELECT * FROM `clan` ORDER BY `lvl` DESC LIMIT 10;");
                    int index = 1;
                    foreach
                        (dynamic data in topDataDynamic)
                    {
                        TopData topData = new TopData();
                        topData.id = data.clanId;
                        topData.name = "Bang " + data.name;
                        topData.imgPath = "npcs/gopet.png";
                        topData.title = topData.name;
                        topData.desc = Utilities.Format("Hạng %s : bang lvl %s", index, data.lvl);
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
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
