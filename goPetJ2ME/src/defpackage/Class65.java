package defpackage;

import vn.me.ui.common.LAF;
import vn.me.screen.AnimationMenu;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;
import vn.me.ui.Widget;

/* renamed from: Class65  reason: default package */
/* loaded from: gopet_repackage.jar:Class65.class */
public class Class65 extends Widget {
    public byte Field422;
    public byte Field423;

    public Class65(AnimationMenu class61) {
    }

    /* JADX INFO: Access modifiers changed from: protected */
    ///////@Override // defpackage.Class184
    public final void paintBorder() {
        if (this.isFocused) {
            BaseCanvas.g.setColor(5592405);
            BaseCanvas.g.drawLine(0, 0, this.w - 1, 0);
            BaseCanvas.g.setColor(LAF.Field1283);
            BaseCanvas.g.drawRoundRect(0, 0, this.w - 1, this.h - 1, LAF.Field1297, LAF.Field1297);
        }
    }

    ///////@Override // defpackage.Class184
    public final void paintBackground() {
        if (this.isPressed) {
            Effects.show1(BaseCanvas.g, LAF.Field1290, LAF.Field1289, 0, 0, this.w - 1, this.h - 1, false);
        } else if (this.isFocused) {
            Effects.show1(BaseCanvas.g, LAF.CLR_MENU_BAR_DARKER, LAF.CLR_MENU_BAR_LIGHTER, 0, 0, this.w - 1, this.h - 1, false);
        }
    }
}
