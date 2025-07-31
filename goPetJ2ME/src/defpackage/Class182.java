package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.Button;
import vn.me.ui.common.ResourceManager;

/* renamed from: Class182  reason: default package */
/* loaded from: gopet_repackage.jar:Class182.class */
public final class Class182 extends Button {
    public Class182(String str) {
        super(str);
        this.normalfont = ResourceManager.defaultFont;
        this.align = 17;
        this.scrollVy = 16448250;
        this.scrollDy = 1664969;
    }

    ///////@Override // defpackage.Class165, defpackage.Class173, defpackage.Class184
    public final void paintBackground() {
        LAF.Method418(this);
    }
}
