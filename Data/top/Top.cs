 
using Gopet.Data.Collections;
 


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

    public void update()
    {

    }

    public void updateSQLBXH()
    {
        if (!lastDatas.isEmpty() || !datas.isEmpty())
        {
            if (!lastDatas.isEmpty() && !datas.isEmpty())
            {
                TopData topDataLast = lastDatas.get(0);
                TopData topDataNew = datas.get(0);
                if (topDataLast.id != topDataNew.id)
                {
                    MYSQLManager.updateSql(String.format("DELETE FROM `top_data` WHERE user_id != %s && timeReceiveTop < %s && topID = '%s'", topDataNew.id, System.currentTimeMillis(), top_id));
                    MYSQLManager.updateSql(String.format("INSERT INTO `top_data`(`user_id`, `topID`, `timeBeginTop`, `timeReceiveTop`) VALUES (%s, '%s', %s, %s)", topDataNew.id, top_id, System.currentTimeMillis(), 0));
                }
                else
                {
                    return;
                }
            }
            else if (lastDatas.isEmpty() && !datas.isEmpty())
            {
                TopData topD = datas.get(0);
                ResultSet resultSet = MYSQLManager.jquery(String.format("SELECT * FROM `top_data` where user_id = %s && topID = '%s'", topD.id, top_id));
                if (!resultSet.next())
                {
                    MYSQLManager.updateSql(String.format("INSERT INTO `top_data`(`user_id`, `topID`, `timeBeginTop`, `timeReceiveTop`) VALUES (%s, '%s', %s, %s)", topD.id, top_id, System.currentTimeMillis(), 0));
                }
                resultSet.close();
                MYSQLManager.updateSql(String.format("DELETE FROM `top_data` WHERE user_id != %s && timeReceiveTop < %s && topID = '%s'", topD.id, System.currentTimeMillis(), top_id));
            }
        }
    }

    public CopyOnWriteArrayList<TopData> getTOPData()
    {
        return datas;
    }
}
