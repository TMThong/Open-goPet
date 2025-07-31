package defpackage;

import vn.me.ui.common.LAF;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;

/* renamed from: Class60  reason: default package */
/* loaded from: gopet_repackage.jar:Class60.class */
public final class Class60 extends Dialog {
    public Class60() {
        super((BaseCanvas.w - 171) >> 1, ((BaseCanvas.h - 64) - LAF.Field1293) - 10, 171, 64);
        this.cmdRight = GameController.Field464;
    }

    public final void Method13(int[] iArr, String[] strArr, String[] strArr2, int[] iArr2, Command[] CommandArr) {
        for (int i = 0; i < strArr.length; i++) {
            Class118 class118 = new Class118(iArr[i], strArr[i], strArr2[i], iArr2[i]);
            class118.setSize(this.w - 8, 20);
            class118.setPosition(17, i * 20);
            addWidget(class118, false);
            class118.cmdCenter = CommandArr[i];
        }
    }

    ///////@Override // defpackage.Dialog, defpackage.Class184
    public final void paintBackground() {
        int i = BaseCanvas.w;
        int i2 = BaseCanvas.h;
        BaseCanvas.g.setColor(16579281);
        BaseCanvas.g.fillRect(0, 0, this.w, this.h);
        BaseCanvas.g.setColor(3872520);
        BaseCanvas.g.drawRect(0, 0, this.w - 1, this.h - 1);
    }
}
