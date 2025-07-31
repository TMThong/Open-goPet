package defpackage;

import vn.me.ui.common.LAF;
import vn.me.core.BaseCanvas;
import vn.me.ui.Label;

/* JADX INFO: Access modifiers changed from: package-private */
/* renamed from: Class81  reason: default package */
/* loaded from: gopet_repackage.jar:Class81.class */
public final class Class81 extends Label {
    private Class81(PetUprgadeScreen class79) {
    }

    /* JADX INFO: Access modifiers changed from: protected */
    ///////@Override // defpackage.Class184
    public final void paintBorder() {
        if (this.isFocused) {
            BaseCanvas.g.setColor(LAF.Field1283);
            BaseCanvas.g.drawRect(0, 0, this.w - 1, this.h - 1);
            BaseCanvas.g.drawRect(1, 1, this.w - 3, this.h - 3);
        }
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public Class81(PetUprgadeScreen class79, byte b) {
        this(class79);
    }
}
