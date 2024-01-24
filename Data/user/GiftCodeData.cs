
using Gopet.Data.Collections;
using Gopet.Data.user;
using Newtonsoft.Json;
 

public class GiftCodeData
{

    private int id;
    private string code;

    private int curUser;
    private int maxUser;

    private int[][] gift_data;

    private DateTime expire;

    private ArrayList<int> usersOfUseThis = new();

    public GiftCodeData(ResultSet resultSet)
    {
        this.id = resultSet.getInt("id");
        this.code = resultSet.getString("code");
        this.curUser = resultSet.getInt("currentUser");
        this.maxUser = resultSet.getInt("maxUser");
        this.gift_data = JsonConvert.DeserializeObject<int[][]>(resultSet.getString("gift_data"));
        this.expire = resultSet.getDateTime("expire");

        this.usersOfUseThis = JsonConvert.DeserializeObject<ArrayList<int>>(resultSet.getString("usersOfUseThis"));
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void setCode(string code)
    {
        this.code = code;
    }

    public void setCurUser(int curUser)
    {
        this.curUser = curUser;
    }

    public void setMaxUser(int maxUser)
    {
        this.maxUser = maxUser;
    }

    public void setGift_data(int[][] gift_data)
    {
        this.gift_data = gift_data;
    }

    public void setExpire(DateTime expire)
    {
        this.expire = expire;
    }

    public void setUsersOfUseThis(ArrayList<int> usersOfUseThis)
    {
        this.usersOfUseThis = usersOfUseThis;
    }

    public int getId()
    {
        return this.id;
    }

    public string getCode()
    {
        return this.code;
    }

    public int getCurUser()
    {
        return this.curUser;
    }

    public int getMaxUser()
    {
        return this.maxUser;
    }

    public int[][] getGift_data()
    {
        return this.gift_data;
    }

    public DateTime getExpire()
    {
        return this.expire;
    }

    public ArrayList<int> getUsersOfUseThis()
    {
        return this.usersOfUseThis;
    }

}
