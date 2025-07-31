package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.Label;
import vn.me.ui.common.T;

/* renamed from: Class123  reason: default package */
/* loaded from: gopet_repackage.jar:Class123.class */
public final class Class123 extends Dialog {
    private boolean Field783;
    private Label Field784;
    private String[] Field785;

    public Class123(boolean z, int i) {
        int i2 = BaseCanvas.w - (LAF.LOT_PADDING << 1);
        int i3 = 90 + (LAF.LOT_PADDING << 2) + 6;
        setMetrics(LAF.LOT_PADDING, ((BaseCanvas.h - LAF.Field1293) - i3) - this.padding, i2, i3);
        this.cmdRight = GameController.Field464;
        this.Field783 = z;
    }

    public final void Method169(Label class173, String str) {
        this.Field784 = class173;
        this.Field785 = ResourceManager.boldFont.wrap(str, ((BaseCanvas.w - (LAF.LOT_PADDING << 1)) - 50) - 5);
        addWidget(this.Field784, false);
        this.Field784.setPosition(10, 5 + ResourceManager.boldFont.getHeight());
    }

    ///////@Override // defpackage.Dialog, defpackage.Class184
    public final void paintBackground() {
        super.paintBackground();
        if (this.Field783) {
            ResourceManager.boldFont.drawString(BaseCanvas.g, T.gL(T.SUCCESS_STR), this.w >> 1, 5, 17);
        } else {
            ResourceManager.boldFont.drawString(BaseCanvas.g, T.gL(T.FAIL_STR), this.w >> 1, 5, 17);
        }
        int Method332 = 5 + ResourceManager.boldFont.getHeight() + 5;
        for (int i = 0; i < this.Field785.length; i++) {
            ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field785[i], 50, Method332 + ((ResourceManager.boldFont.getHeight() + 2) * i), 0);
        }
    }
}
