package defpackage;

import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;
import vn.me.ui.Widget;

/* renamed from: Class14  reason: default package */
/* loaded from: gopet_repackage.jar:Class14.class */
public final class Class14 extends Widget {
    public Class14() {
        this.preferredSize.height = 4;
        this.preferredSize.width = 30;
        this.h = this.preferredSize.height;
        this.w = this.preferredSize.width;
        this.isFocusable = false;
    }

    ///////@Override // defpackage.Class184
    public final void paint() {
        Effects.show1(BaseCanvas.g, 4945818, -1, 2, 0, (this.w - 4) >> 1, 1, true);
        Effects.show1(BaseCanvas.g, -1, 4945818, 2 + ((this.w - 4) >> 1), 0, (this.w - 4) >> 1, 1, true);
    }
}
