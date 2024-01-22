 
public class BuffExp : DataVersion {

     
    private long buffExpTime = -1;
     
    private int itemTemplateIdBuff = -1;
    
    private float _buffPercent = 0f;
    
    private long currentTime = System.currentTimeMillis();

    public BuffExp() {

    }

    public float getPercent() {
        if (buffExpTime > 0) {
            return _buffPercent;
        }
        return 0;
    }

    public void update() {
        if (buffExpTime >= 0 && System.currentTimeMillis() - currentTime > 2000L) {
            buffExpTime -= (System.currentTimeMillis() - currentTime);
            currentTime = System.currentTimeMillis();
        }
    }
    
    public void loadCurrentTime() {
        currentTime = System.currentTimeMillis();
    }

    public void addTime(long time) {
        currentTime = System.currentTimeMillis();
        if (buffExpTime + time > GopetManager.MAX_TIME_BUFF_EXP) {
            buffExpTime = GopetManager.MAX_TIME_BUFF_EXP;
        } else {
            if (buffExpTime <= 0) {
                buffExpTime = time;
            } else {
                buffExpTime += time;
            }
        }
    }
}
