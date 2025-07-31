package defpackage;

import vn.me.core.BaseCanvas;

/* renamed from: Class22  reason: default package */
/* loaded from: gopet_repackage.jar:Class22.class */
public final class BigTextEffect extends AnimationEffect {
    private int Field91;
    private byte Field92;
    private String Field93;
    private int Field94;
    private boolean Field95 = true;
    private int Field96;
    private int Field97;
    private int Field98;

    ///////@Override // defpackage.Class193
    public final void start() {
        this.isInEffect = true;
        this.Field92 = (byte) (System.currentTimeMillis() % 3);
    }

    public BigTextEffect(String str) {
        this.Field93 = str;
        this.overCommandBar = true;
        if (str == null || str.length() <= 0) {
            return;
        }
        this.Field94 = (GameResourceManager.getSpecialFont().getWidth(str) + 2) >> 1;
        this.Field97 = -this.Field94;
        this.Field98 = BaseCanvas.Field157;
    }

    ///////@Override // defpackage.Class193
    public final void paint() {
        BaseCanvas.g.setColor(0);
        if (this.Field95) {
            switch (this.Field92) {
                case 0:
                    BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, (BaseCanvas.h >> 1) - this.Field91);
                    int i = (BaseCanvas.h >> 1) + this.Field91;
                    BaseCanvas.g.fillRect(0, i, BaseCanvas.w, BaseCanvas.h - i);
                    break;
                case 1:
                    int i2 = 32 - (this.Field91 << 1);
                    for (int i3 = 0; i3 < (BaseCanvas.h >> 5) + 1; i3++) {
                        for (int i4 = 0; i4 < (BaseCanvas.w >> 5) + 1; i4++) {
                            BaseCanvas.g.setColor(0);
                            BaseCanvas.g.fillRect((i4 << 5) + this.Field91, (i3 << 5) + this.Field91, i2, i2);
                        }
                    }
                    break;
                case 2:
                    int i5 = 46 - (this.Field91 << 1);
                    for (int i6 = 0; i6 < (BaseCanvas.h >> 5) + 1; i6++) {
                        for (int i7 = 0; i7 < (BaseCanvas.w >> 5) + 1; i7++) {
                            BaseCanvas.g.setColor(0);
                            BaseCanvas.g.fillArc(((i7 << 5) + this.Field91) - 7, ((i6 << 5) + this.Field91) - 7, i5, i5, 0, 360);
                        }
                    }
                    break;
            }
        }
        if (this.Field96 > 1) {
            GameResourceManager.getSpecialFont().drawString(BaseCanvas.g, this.Field93, this.Field97, BaseCanvas.Field158, 17);
        }
    }

    ///////@Override // defpackage.Class193
    public final void update(long j) {
        if (!this.Field95) {
            this.Field96++;
            if (this.Field96 < 15 || this.Field96 > 17) {
                this.Field97 = (this.Field98 + this.Field97) >> 1;
            } else if (this.Field96 == 15) {
                this.Field98 = BaseCanvas.w + this.Field94;
            }
            if (this.Field96 > 32) {
                this.isInEffect = false;
                return;
            }
            return;
        }
        switch (this.Field92) {
            case 0:
                this.Field91 += 10;
                if (this.Field91 > (BaseCanvas.h >> 1)) {
                    this.Field95 = false;
                    return;
                }
                return;
            case 1:
                this.Field91++;
                if (this.Field91 > 16) {
                    this.Field95 = false;
                    return;
                }
                return;
            case 2:
                this.Field91++;
                if (this.Field91 > 23) {
                    this.Field95 = false;
                    return;
                }
                return;
            default:
                return;
        }
    }
}
