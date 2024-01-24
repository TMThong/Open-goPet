[Serializable]
public class ExchangeData
{
    public int id;
    public int amount;
    public int gold;
    public void setId(int id)
    {
        this.id = id;
    }

    public void setAmount(int amount)
    {
        this.amount = amount;
    }

    public void setGold(int gold)
    {
        this.gold = gold;
    }

    public int getId()
    {
        return this.id;
    }

    public int getAmount()
    {
        return this.amount;
    }

    public int getGold()
    {
        return this.gold;
    }

}
