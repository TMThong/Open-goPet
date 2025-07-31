package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import javax.microedition.lcdui.Image;

/* renamed from: Class27  reason: default package */
/* loaded from: gopet_repackage.jar:Class27.class */
public final class Popup extends AnimationEffect {
    private byte Field135;
    private int Field136;
    private int Field137;
    private int Field138;
    private int Field139;
    private int Field140;
    private String[] Field141;
    private long Field142;
    private Image Field143;
    private String Field144;

    public Popup(String str) {
        this.Field141 = ResourceManager.defaultFont.wrap(str, BaseCanvas.w >> 1);
        if (this.Field141.length > 0) {
            this.Field139 = ResourceManager.defaultFont.getWidth(this.Field141[0]) + 4;
            for (int i = 1; i < this.Field141.length; i++) {
                int Method330 = ResourceManager.defaultFont.getWidth(this.Field141[i]) + 4;
                if (Method330 > this.Field139) {
                    this.Field139 = Method330;
                }
            }
            this.Field138 = BaseCanvas.w - this.Field139;
            this.Field140 = (ResourceManager.defaultFont.getHeight() * this.Field141.length) + 2;
        }
        this.Field143 = null;
        this.Field144 = "";
    }

    ///////@Override // defpackage.Class193
    public final void start() {
        this.isInEffect = true;
        this.Field135 = (byte) 0;
        this.Field136 = BaseCanvas.h - LAF.Field1293;
        this.Field137 = (BaseCanvas.h - LAF.Field1293) - this.Field140;
        if (BaseCanvas.getCurrentScreen() == null) {
        }
    }

    ///////@Override // defpackage.Class193
    public final void paint() {
        BaseCanvas.g.setColor(14328834);
        BaseCanvas.g.fillRect(this.Field138, this.Field136, this.Field139, this.Field140);
        for (int i = 0; i < this.Field141.length; i++) {
            ResourceManager.defaultFont.drawString(BaseCanvas.g, this.Field141[i], this.Field138 + 2, this.Field136 + 1 + (i * ResourceManager.defaultFont.getHeight()), 0);
        }
        if (this.Field143 != null) {
            BaseCanvas.g.drawImage(this.Field143, (BaseCanvas.w - this.Field143.getWidth()) >> 1, this.Field136 + (this.Field141.length * ResourceManager.defaultFont.getHeight()), 0);
        } else if (this.Field144.trim().length() > 0) {
            this.Field143 = GameResourceManager.loadResourceImg(this.Field144, (byte) 3);
        }
    }

    ///////@Override // defpackage.Class193
    public final void update(long j) {
        switch (this.Field135) {
            case 0:
                if (Ulti.Method369(this.Field136 - this.Field137) >= 3) {
                    this.Field136 -= 3;
                    return;
                }
                this.Field136 = this.Field137;
                this.Field135 = (byte) 1;
                this.Field142 = j;
                return;
            case 1:
                if (j - this.Field142 >= 3000) {
                    this.Field135 = (byte) 2;
                    return;
                }
                return;
            case 2:
                if (Ulti.Method369((BaseCanvas.h - LAF.Field1293) - this.Field136) >= 3) {
                    this.Field136 += 3;
                    return;
                } else {
                    this.isInEffect = false;
                    return;
                }
            case 3:
                if (Ulti.Method369(this.Field136 - this.Field137) >= 6) {
                    this.Field136 += 6;
                    return;
                }
                this.Field136 = this.Field137;
                this.Field135 = (byte) 4;
                this.Field142 = j;
                return;
            case 4:
                if (j - this.Field142 >= 2500) {
                    this.Field135 = (byte) 5;
                    return;
                }
                return;
            case 5:
                this.Field143 = null;
                this.isInEffect = false;
                return;
            default:
                return;
        }
    }
}
