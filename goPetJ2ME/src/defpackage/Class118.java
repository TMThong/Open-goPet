package defpackage;

import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.Font;

/* renamed from: Class118  reason: default package */
/* loaded from: gopet_repackage.jar:Class118.class */
public final class Class118 extends Class108 {
    public int Field766;
    public String Field767;
    public int Field768;
    private Font Field769;

    public Class118(int i, String str, String str2, int i2) {
        super(null);
        this.Field769 = ResourceManager.defaultFont;
        this.Field768 = Ulti.Method369(i2);
        this.text = str;
        this.Field767 = str2;
        this.Field766 = i;
    }

    public Class118(Font class171, int i, String str, String str2, int i2) {
        this(i, str, str2, i2);
        this.Field769 = class171;
    }

    ///////@Override // defpackage.Class165, defpackage.Class173, defpackage.Class184
    public final void paint() {
        if (this.Field768 != 0) {
            this.Field769.drawString(BaseCanvas.g, new StringBuffer().append(this.text).append(" ").append(this.Field768).append(" (mp)").toString(), this.x + 2, this.y + 2, 20);
        } else {
            this.Field769.drawString(BaseCanvas.g, this.text, this.x + 2, this.y + 2, 20);
        }
    }
}
