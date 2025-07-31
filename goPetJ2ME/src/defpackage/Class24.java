package defpackage;

import vn.me.core.BaseCanvas;

/* renamed from: Class24  reason: default package */
/* loaded from: gopet_repackage.jar:Class24.class */
public final class Class24 extends AnimationEffect {
    private long Field108;
    private int Field109 = 0;
    private long Field110;
    private long Field111;
    private int Field112;
    private int Field113;
    private int Field114;
    private String Field115;

    public final void Method338(int i, int i2, long j) {
        if (j == 0) {
            return;
        }
        super.start();
        this.Field113 = i;
        this.Field114 = i2;
        this.Field110 = System.currentTimeMillis();
        this.Field111 = j;
        this.Field115 = new StringBuffer().append(j > 0 ? "+" : "-").append(Ulti.formatNumber(this.Field111)).toString();
        this.Field112 = GameResourceManager.Method116().getWidth(this.Field115);
        this.Field112 += 14;
        this.isInEffect = true;
    }

    ///////@Override // defpackage.Class193
    public final void update(long j) {
        if (j - this.Field108 > 100) {
            this.Field109 = (this.Field109 + 1) % 5;
            this.Field108 = j;
        }
        if (j - this.Field110 > 2000) {
            this.isInEffect = false;
        } else {
            this.Field114 -= 2;
        }
    }

    ///////@Override // defpackage.Class193
    public final void paint() {
        int i = this.Field113 - (this.Field112 >> 1);
        GameResourceManager.Method116().drawString(BaseCanvas.g, this.Field115, i, this.Field114, 0);
        BaseCanvas.g.drawRegion(GameResourceManager.Field599, this.Field109 * 14, 0, 14, 14, 0, ((i + this.Field112) + 1) - 14, this.Field114, 0);
    }
}
