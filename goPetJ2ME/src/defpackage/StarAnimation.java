package defpackage;

import vn.me.core.BaseCanvas;
import java.util.Random;

/* renamed from: Class26  reason: default package */
/* loaded from: gopet_repackage.jar:Class26.class */
public final class StarAnimation extends AnimationEffect {
    private int Field125;
    private int Field126;
    private int Field127;
    private int Field128;
    private int[] Field129;
    private int[] Field130;
    private Random Field131;
    private long Field132;
    private long Field133;
    private int Field134 = 0;

    ///////@Override // defpackage.Class193
    public final void start() {
        super.start();
        this.Field127 = 0;
        this.Field131 = new Random(System.currentTimeMillis());
        Method339();
    }

    private void Method339() {
        this.Field128 = 0;
        this.Field129 = new int[10];
        this.Field130 = new int[10];
        this.Field125 = (this.Field131.nextInt() % (BaseCanvas.Field157 - 20)) + BaseCanvas.Field157;
        this.Field126 = (this.Field131.nextInt() % (BaseCanvas.Field158 - 20)) + BaseCanvas.Field158;
        for (int i = 0; i < 10; i++) {
            this.Field129[i] = (this.Field131.nextInt() % 50) + this.Field125;
            this.Field130[i] = (this.Field131.nextInt() % 50) + this.Field126;
        }
    }

    ///////@Override // defpackage.Class193
    public final void paint() {
        for (int i = 0; i < 10; i++) {
            BaseCanvas.g.drawRegion(GameResourceManager.Field599, this.Field134 * 14, 0, 14, 14, 0, this.Field125 + (((this.Field129[i] - this.Field125) * this.Field128) / 10), this.Field126 + (((this.Field130[i] - this.Field126) * this.Field128) / 10), 0);
        }
    }

    ///////@Override // defpackage.Class193
    public final void update(long j) {
        if (j - this.Field133 > 100) {
            this.Field134 = (this.Field134 + 1) % 5;
            this.Field133 = j;
        }
        if (j - this.Field132 >= 0) {
            this.Field132 = j;
            this.Field128++;
            if (this.Field128 > 10) {
                Method339();
                this.Field127++;
                if (this.Field127 > 5) {
                    this.isInEffect = false;
                }
            }
        }
    }
}
