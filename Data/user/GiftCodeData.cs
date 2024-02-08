
using Gopet.Data.Collections;
using Gopet.Data.User;
using Newtonsoft.Json;
 

public class GiftCodeData
{

    public int id {  get; private set; }
    public string code { get; private set; }

    public int currentUser { get; private set; }
    public int maxUser { get; private set; }
    
    public int[][] gift_data { get; private set; }
    
    public DateTime expire { get; private set; }

    public JArrayList<int> usersOfUseThis { get; private set; } = new();

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
        this.currentUser = curUser;
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

    public void setUsersOfUseThis(JArrayList<int> usersOfUseThis)
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
        return this.currentUser;
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

    public JArrayList<int> getUsersOfUseThis()
    {
        return this.usersOfUseThis;
    }

}
