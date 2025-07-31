package defpackage;

import vn.me.core.BaseCanvas;

/* renamed from: Class23  reason: default package */
/* loaded from: gopet_repackage.jar:Class23.class */
public final class Class23 extends AnimationEffect {
    private long Field99;
    private long Field101;
    private int Field104;
    private int Field105;
    private int Field106;
    private PetGameModel Field107;
    private int Field100 = 0;
    private long[] Field102 = new long[2];
    private byte[] Field103 = new byte[2];

    public final void Method333(PetGameModel class43, int i, int i2, long j, long j2) {
        if (j == 0 && j2 == 0) {
            return;
        }
        super.start();
        this.Field107 = class43;
        this.Field105 = i;
        this.Field106 = i2;
        this.Field101 = System.currentTimeMillis();
        this.Field102[0] = j;
        this.Field102[1] = j2;
        this.Field104 = 0;
        for (int i3 = 0; i3 < this.Field103.length; i3++) {
            this.Field103[i3] = (byte) GameResourceManager.Method116().getWidth(new StringBuffer("+").append(Ulti.formatNumber(this.Field102[i3])).toString());
            if (this.Field103[i3] > this.Field104) {
                this.Field104 = this.Field103[i3];
            }
        }
        this.Field104 += 14;
        this.isInEffect = true;
    }

    ///////@Override // defpackage.Class193
    public final void update(long j) {
        if (j - this.Field99 > 100) {
            this.Field100 = (this.Field100 + 1) % 5;
            this.Field99 = j;
        }
        if (j - this.Field101 > 2000) {
            this.isInEffect = false;
        } else {
            this.Field106 -= 2;
        }
    }

    ///////@Override // defpackage.Class193
    public final void paint() {
        int i = this.Field105 - (this.Field104 >> 1);
        int i2 = 0;
        for (int i3 = 0; i3 < 2; i3++) {
            if (this.Field102[i3] != 0) {
                GameResourceManager.Method116().drawString(BaseCanvas.g, this.Field102[i3] > 0 ? new StringBuffer("+").append(Ulti.formatNumber(this.Field102[i3])).toString() : Ulti.formatNumber(this.Field102[i3]), i, this.Field106 + i2, 0);
                switch (i3) {
                    case 0:
                        BaseCanvas.g.drawRegion(GameResourceManager.Field597, this.Field100 * 14, 0, 14, 14, 0, i + this.Field103[i3] + 1, this.Field106 + i2, 0);
                        break;
                    case 1:
                        BaseCanvas.g.drawImage(this.Field107.gemImg, i + this.Field103[i3] + 1, this.Field106 + i2, 0);
                        break;
                }
                i2 += 20;
            }
        }
    }
}
