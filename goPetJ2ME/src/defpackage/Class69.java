package defpackage;

import vn.me.screen.AnimationMenu;
import vn.me.core.BaseCanvas;
import vn.me.ui.Font;

/* renamed from: Class69  reason: default package */
/* loaded from: gopet_repackage.jar:Class69.class */
public final class Class69 extends Class65 {
    public String Field442;
    public String[] Field443;
    public byte Field444;

    public Class69(AnimationMenu class61) {
        super(class61);
    }

    ///////@Override // defpackage.Class184
    public final void paint() {
        int i = 0;
        if (this.Field423 == 17) {
            i = this.w >> 1;
        } else if (this.Field423 == 24) {
            i = this.w;
        }
        Font Method124 = GameResourceManager.Method124(this.Field444);
        int i2 = 2;
        for (int i3 = 0; i3 < this.Field443.length; i3++) {
            Method124.drawString(BaseCanvas.g, this.Field443[i3], i, i2, this.Field423);
            i2 += Method124.getHeight();
        }
    }
}
