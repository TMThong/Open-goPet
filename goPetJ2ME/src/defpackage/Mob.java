package defpackage;

import vn.me.ui.geom.Rectangle;
import vn.me.core.BaseCanvas;

public final class Mob extends mObject {

    public int mobId;
    private String Field261;
    private int petWidth;
    private int petHeight;
    private int ticks;
    private long Field265;
    private long Field266;
    private long Field267;
    private int Field268;
    private StringBuffer Field269;
    private byte frameNum;
    private int vY;
    public boolean isBoss = false;

    public Mob(int i, String str, String str2, int i2, int i3, int i4, byte b, byte fNum, short vY_, boolean isBoss_) {
        this.mobId = i;
        this.Field261 = str;
        this.name = str2;
        this.xChar = i3;
        this.yChar = i4;
        this.Field872 = true;
        setCollisionRec(new Rectangle(-10, -10, 20, 20));
        this.Field267 = (Ulti.Method370(5) * 1000) + 3000;
        this.Field269 = new StringBuffer("LV ").append(String.valueOf(i2));
        this.frameNum = fNum;
        this.vY = vY_;
        this.isBoss = isBoss_;
    }

    public final void paintInMap(int i, int i2) {
        BaseCanvas.g.drawImage(GameResourceManager.Field603, (this.xChar - i) - 11, (this.yChar - 6) - i2, 0);
        if (this.img == null) {
            this.img = PetGameModel.Field284.requestImg(this.Field261);
            if (this.img != null) {
                this.petWidth = this.img.getWidth() / frameNum;
                this.petHeight = this.img.getHeight();
                return;
            }
            return;
        }
        BaseCanvas.g.drawRegion(this.img, this.ticks * this.petWidth, 0, this.petWidth, this.petHeight, this.Field268, this.xChar - i, (this.yChar - this.img.getHeight()) - i2 + this.vY, 17);
        long currentTimeMillis = System.currentTimeMillis();
        if (currentTimeMillis - this.Field265 >= 200) {
            this.Field265 = currentTimeMillis;
            this.ticks = (this.ticks + 1) % frameNum;
        }
        if (currentTimeMillis - this.Field266 >= this.Field267) {
            this.Field268 = (this.Field268 + 2) % 4;
            this.Field267 = (Ulti.Method370(5) * 1000) + 3000;
            this.Field266 = currentTimeMillis;
        }
        GameResourceManager.getDefaultFont().drawString(BaseCanvas.g, this.Field269.toString(), this.xChar - i, ((this.yChar - this.petHeight) - 5) - i2, 17);
    }
}
