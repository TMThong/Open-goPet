
using Gopet.Data.GopetItem;
using Gopet.Util;

public class BuffExp
{


    public long buffExpTime = -1;

    public int itemTemplateIdBuff = -1;

    public float _buffPercent = 0f;

    public long currentTime = Utilities.CurrentTimeMillis;

    public int itemBuffId = 0;


    public BuffExp()
    {

    }


    public ItemTemplate Template
    {
        get
        {
            if (itemBuffId == 0)
            {
                switch ((int)getPercent())
                {
                    case 100:
                        return GopetManager.itemTemplate[198];
                    case 200:
                        return GopetManager.itemTemplate[199];
                    case 300:
                        return GopetManager.itemTemplate[200];
                }
                return null;
            }

            return GopetManager.itemTemplate[itemBuffId];
        }
    }

    public float getPercent()
    {
        if (buffExpTime > 0)
        {
            return _buffPercent;
        }
        return 0;
    }

    public void update()
    {
        if (buffExpTime >= 0 && Utilities.CurrentTimeMillis - currentTime > 2000L)
        {
            buffExpTime -= (Utilities.CurrentTimeMillis - currentTime);
            currentTime = Utilities.CurrentTimeMillis;
        }
    }

    public void loadCurrentTime()
    {
        currentTime = Utilities.CurrentTimeMillis;
    }

    public void addTime(long time)
    {
        currentTime = Utilities.CurrentTimeMillis;
        if (buffExpTime + time > GopetManager.MAX_TIME_BUFF_EXP)
        {
            buffExpTime = GopetManager.MAX_TIME_BUFF_EXP;
        }
        else
        {
            if (buffExpTime <= 0)
            {
                buffExpTime = time;
            }
            else
            {
                buffExpTime += time;
            }
        }
    }

    public void setBuffExpTime(long buffExpTime)
    {
        this.buffExpTime = buffExpTime;
    }

    public void setItemTemplateIdBuff(int itemTemplateIdBuff)
    {
        this.itemTemplateIdBuff = itemTemplateIdBuff;
    }

    public void set_buffPercent(float _buffPercent)
    {
        this._buffPercent = _buffPercent;
    }

    public void setCurrentTime(long currentTime)
    {
        this.currentTime = currentTime;
    }

    public long getBuffExpTime()
    {
        return this.buffExpTime;
    }

    public int getItemTemplateIdBuff()
    {
        return this.itemTemplateIdBuff;
    }

    public float get_buffPercent()
    {
        return this._buffPercent;
    }

    public long getCurrentTime()
    {
        return this.currentTime;
    }

}
