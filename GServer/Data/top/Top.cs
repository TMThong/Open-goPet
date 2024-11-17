
using Gopet.Data.Collections;
using Gopet.Data.User;
using Gopet.Util;

public class Top
{

    public String name;
    public String desc;
    public CopyOnWriteArrayList<TopData> datas = new();
    public CopyOnWriteArrayList<TopData> lastDatas = new();
    public String top_id { get; }

    public Top(String top_idString)
    {
        top_id = top_idString;
    }

    public virtual void Update()
    {

    }

    public virtual void updateSQLBXH()
    {

    }

    public virtual TopData getMyInfo(Player player)
    {
        return null;
    }


    public virtual CopyOnWriteArrayList<TopData> getTOPData()
    {
        return datas;
    }

    public virtual string HrImagePath { get; set; } = "npcs/lixi.png";
}
