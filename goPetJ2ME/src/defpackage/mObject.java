package defpackage;

import vn.me.ui.geom.Rectangle;
import vn.me.core.BaseCanvas;
import javax.microedition.lcdui.Image;

public class mObject {
    public String name;
    public Command centerObjectCMD;
    public int xChar;
    public int yChar;
    public int x;
    public int y;
    public Image img;
    public boolean Field878;
    public boolean Field864 = true;
    public Rectangle position = new Rectangle(0, 0, 0, 0);
    public boolean Field872 = false;
    public int Field873 = 1;
    public int Field874 = 1;
    public long Field875 = System.currentTimeMillis();
    public boolean Field876 = true;

    public final void setCollisionRec(Rectangle rect) {
        this.x = rect.x;
        this.y = rect.y;
        this.position.size.width = rect.size.width;
        this.position.size.height = rect.size.height;
    }

    public void paintInMap(int i, int i2) {
    }

    public void update(long j) {
        this.position.x = this.x + this.xChar;
        this.position.y = this.y + this.yChar;
        if (j - this.Field875 > 50) {
            this.Field875 = j;
            this.Field873 += this.Field874;
            if (this.Field873 < 0 || this.Field873 > 1) {
                this.Field874 = -this.Field874;
            }
        }
    }

    public void paintObj(int i, int i2) {
        BaseCanvas.g.drawImage(GameResourceManager.Field601, this.xChar - i, (((-i2) + this.yChar) - GameResourceManager.Field601.getHeight()) - this.Field873, 17);
    }

    public Rectangle getPosition() {
        return this.position;
    }
}
