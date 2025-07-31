package defpackage;

import vn.me.ui.Button;
import vn.me.core.BaseCanvas;
import javax.microedition.lcdui.Image;

/* renamed from: Class119  reason: default package */
/* loaded from: gopet_repackage.jar:Class119.class */
public final class Class119 extends Button {
    private Image Field770;

    public Class119(Image image) {
        this.Field770 = image;
    }

    ///////@Override // defpackage.Class165, defpackage.Class173, defpackage.Class184
    public final void paint() {
        int translateX = BaseCanvas.g.getTranslateX();
        int translateY = BaseCanvas.g.getTranslateY();
        BaseCanvas.g.translate((-translateX) + this.x, (-translateY) + this.y);
        BaseCanvas.g.drawImage(this.Field770, (this.w - this.Field770.getWidth()) >> 1, (this.h - this.Field770.getHeight()) >> 1, 0);
        BaseCanvas.g.translate(-((-translateX) + this.x), -((-translateY) + this.y));
    }
}
