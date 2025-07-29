using Gopet.Data.Collections;
using Gopet.Data.User;
using Newtonsoft.Json;


public class GiftCodeData
{

    public int id { get; private set; }
    public string code { get; private set; }

    public int currentUser { get; set; }
    public int maxUser { get; private set; }

    public int[][] gift_data { get; private set; }

    public DateTime expire { get; private set; }

    public JArrayList<int> usersOfUseThis { get; private set; } = new();

    public bool isClanCode { get; private set; } = false;

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