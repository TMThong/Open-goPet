package defpackage;

import vn.me.ui.geom.Rectangle;
import vn.me.core.BaseCanvas;

/* renamed from: Class130  reason: default package */
/* loaded from: gopet_repackage.jar:Class130.class */
public final class Class130 extends mObject {
    public int Field850;
    public int Field851;
    private int Field852;
    private long Field853;
    public int Field854;
    public int Field855;
    private boolean Field856 = true;

    public Class130() {
        setCollisionRec(new Rectangle(-5, -4, 9, 8));
    }

    ///////@Override // defpackage.Class133
    public final void paintInMap(int i, int i2) {
        BaseCanvas.g.drawRegion(GameResourceManager.Field604, 9 * this.Field852, 0, 9, 8, this.Field856 ? 0 : 2, this.xChar - i, (this.yChar - i2) - 50, 0);
    }

    ///////@Override // defpackage.Class133
    public final void update(long j) {
        super.update(j);
        if (j - this.Field853 > 100) {
            this.Field853 = j;
            this.Field852 = (this.Field852 + 1) % 2;
        }
        if (Ulti.Method369(this.Field855 - this.yChar) <= 2 && Ulti.Method369(this.Field854 - this.xChar) <= 2) {
            this.xChar = this.Field854;
            this.yChar = this.Field855;
            this.Field854 += Ulti.Method370(40) - 20;
            this.Field855 += Ulti.Method370(40) - 20;
            if (this.Field854 < 0) {
                this.Field854 = 0;
            }
            if (this.Field854 > this.Field850) {
                this.Field854 = this.Field850;
            }
            if (this.Field855 - 40 < 0) {
                this.Field855 = 40;
            }
            if (this.Field855 > this.Field851) {
                this.Field855 = this.Field851;
            }
        } else if (Ulti.Method369(this.Field854 - this.xChar) <= Ulti.Method369(this.Field855 - this.yChar)) {
            if (this.Field855 > this.yChar) {
                this.yChar += 2;
            } else {
                this.yChar -= 2;
            }
            if (this.Field855 != this.yChar) {
                if (this.Field855 > this.yChar) {
                    this.xChar += (2 * (this.Field854 - this.xChar)) / (this.Field855 - this.yChar);
                } else {
                    this.xChar += (2 * (this.xChar - this.Field854)) / (this.Field855 - this.yChar);
                }
            }
        } else {
            if (this.Field854 > this.xChar) {
                this.xChar += 2;
                this.Field856 = true;
            } else {
                this.Field856 = false;
                this.xChar -= 2;
            }
            if (this.Field854 != this.xChar) {
                if (this.Field854 > this.xChar) {
                    this.yChar += (2 * (this.Field855 - this.yChar)) / (this.Field854 - this.xChar);
                } else {
                    this.yChar += (2 * (this.yChar - this.Field855)) / (this.Field854 - this.xChar);
                }
            }
        }
    }
}
