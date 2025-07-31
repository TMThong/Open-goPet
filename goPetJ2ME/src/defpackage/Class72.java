package defpackage;

import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.geom.Dimension;

/* renamed from: Class72  reason: default package */
/* loaded from: gopet_repackage.jar:Class72.class */
public final class Class72 extends Dialog {
    private boolean Field449;
    private int Field450;
    private int Field451;

    public final void Method29(int i, int i2) {
        this.Field450 = i;
        this.Field451 = i2;
        this.x = this.Field450 - (this.w >> 1);
        this.y = (this.Field451 - this.h) - 6;
        if (this.Field449 && this.y < 0) {
            this.y = 0;
        }
        setMetrics(this.x, this.y, this.w, this.h);
    }

    public Class72(int i, int i2, int i3, int i4, boolean z) {
        this.Field449 = false;
        this.Field449 = true;
        this.padding = 0;
        this.spacing = 0;
        this.isAutoFit = true;
        this.isLoop = true;
        this.Field450 = 0;
        this.Field451 = 0;
        this.w = i3;
        this.h = i4;
        this.x = this.Field450 - (this.w >> 1);
        this.y = (this.Field451 - this.h) - 5;
        if (this.Field449 && this.y < 0) {
            this.y = 0;
        }
        this.border = 3;
        setMetrics(this.x, this.y, this.w, this.h);
    }

    ///////@Override // defpackage.Dialog, defpackage.Class184
    public final void paintBorder() {
        Class18.Method319(BaseCanvas.g, 0, 0, this.w, this.h - 5);
    }

    ///////@Override // defpackage.Dialog, defpackage.Class184
    public final void paintBackground() {
        Class18.Method320(BaseCanvas.g, this.w, this.h - 5);
    }

    ///////@Override 
    public final void doListLayout() {
        super.doListLayout();
        Dimension class197 = this.preferredSize;
        int i = this.h + 5;
        class197.height = i;
        this.h = i;
    }
}
