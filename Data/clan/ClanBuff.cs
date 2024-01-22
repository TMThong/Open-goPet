
public class ClanBuff : DataVersion
{

    public const int BUFF_GEM = 1;
    public const int BUFF_EXP = 2;
    public const int BUFF_SKIP_PET_DIE_WHEN_PK = 3;
    public const int BUFF_SKIP_TASK_CLAN = 42;
    public const int BUFF_AUTO_GO_BACK_WHEN_INVITE_PK = 5;
    public const int BUFF_DESCREASE_EXP_WHEN_PK = 6;
    private int buffId;
    private int value;
    private long timeEndBuff;

    public ClanBuff()
    {
    }

    public ClanBuff(int buffId, int value, long timeEndBuff)
    {
        this.buffId = buffId;
        this.value = value;
        this.timeEndBuff = timeEndBuff;
    }

    public float getPercent()
    {
        return value / 100f;
    }

    public String getDesc()
    {
        return getDesc(value, buffId);
    }

    public const String getDesc(int value, int buffId) {
        return String.format(GopetManager.CLANBUFF_HASH_MAP.get(buffId).getDesc(), value / 100f).replace('/', '%');
}

public  String getName() {
    return getName(value, buffId);
}

public const String getName(int value, int buffId) {
    return String.format(GopetManager.CLANBUFF_HASH_MAP.get(buffId).getName(), value / 100f).replace('/', '%');
}
}
