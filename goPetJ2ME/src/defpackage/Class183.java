package defpackage;

import vn.me.ui.common.LAF;
import vn.me.core.BaseCanvas;
import vn.me.ui.Widget;
import vn.me.ui.Font;

/* renamed from: Class183  reason: default package */
/* loaded from: gopet_repackage.jar:Class183.class */
public final class Class183 extends Widget {
    private Font Field1200;
    private int Field1201;
    public int Field1202 = 20;
    public int Field1203 = 3;
    private String[] Field1199 = new String[0];

    public Class183() {
        this.padding = LAF.LOT_PADDING;
        this.isScrollableY = true;
    }

    public final void Method367(String str, Font class171) {
        if (str == null) {
            str = "";
        }
        this.Field1200 = class171;
        this.Field1199 = class171.wrap(str, this.w - (this.padding << 1));
        this.preferredSize.height = (this.Field1199.length * (class171.getHeight() + this.Field1203)) - this.Field1203;
        this.preferredSize.width = this.w - (this.padding << 1);
        this.Field1201 = this.preferredSize.height - this.h;
    }

    ///////@Override // defpackage.Class184
    public final void paint() {
        if (this.Field1199 != null) {
            int i = 0;
            for (int i2 = 0; i2 < this.Field1199.length; i2++) {
                int i3 = this.Field1202 == 17 ? this.w >> 1 : this.Field1202 == 24 ? this.w - this.padding : 0;
                if ((i - this.scrollY) + this.Field1200.getHeight() >= 0) {
                    this.Field1200.drawString(BaseCanvas.g, this.Field1199[i2], i3 - this.scrollX, i - this.scrollY, this.Field1202);
                }
                int Method332 = i + this.Field1200.getHeight() + this.Field1203;
                i = Method332;
                if (Method332 - this.scrollY > this.h) {
                    return;
                }
            }
        }
    }

    ///////@Override // defpackage.Class184
    public final boolean checkKeys(int i, int i2) {
        if (i2 == -2 && this.preferredSize.height > this.h - (2 * this.padding) && this.destScrollY < this.Field1201) {
            scrollTo(this.x + this.padding, (this.destScrollY + this.h) - (2 * this.padding), this.w - (2 * this.padding), this.Field1200.getHeight());
            return true;
        } else if (i2 != -1 || this.preferredSize.height <= this.h - (2 * this.padding) || this.destScrollY <= 0) {
            return false;
        } else {
            scrollTo(this.x + this.padding, this.destScrollY - this.Field1200.getHeight(), this.w - (2 * this.padding), this.Field1200.getHeight());
            return true;
        }
    }
}
