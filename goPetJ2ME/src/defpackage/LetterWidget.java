package defpackage;

import vn.me.ui.common.LAF;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;
import vn.me.ui.WidgetGroup;
import vn.me.screen.MGOMessageScreen;

/* renamed from: Class99  reason: default package */
/* loaded from: gopet_repackage.jar:Class99.class */
public final class LetterWidget extends WidgetGroup {
    public Letter letter;
    private final MGOMessageScreen Field611;

    public LetterWidget(MGOMessageScreen class95, Letter class97) {
        this.Field611 = class95;
        setSize(BaseCanvas.w, 15 + (2 * GameResourceManager.Method124((byte) 0).getHeight()));
        this.letter = class97;
    }

    ///////@Override // defpackage.Class184
    public final void paintBackground() {
        if (this.isPressed) {
            Effects.show1(BaseCanvas.g, LAF.Field1290, LAF.Field1289, 0, 0, this.w - 1, this.h - 1, false);
        } else if (this.isFocused) {
            Effects.show1(BaseCanvas.g, LAF.CLR_MENU_BAR_DARKER, LAF.CLR_MENU_BAR_LIGHTER, 0, 0, this.w - 1, this.h - 1, false);
        }
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final void paint() {
        int Method332 = GameResourceManager.Method124((byte) 0).getHeight();
        GameResourceManager.Method124((byte) 0).drawString(BaseCanvas.g, this.letter.Field578, 10, 5, 0);
        BaseCanvas.g.drawImage(this.letter.isMark ? MGOMessageScreen.getMarkImg(this.Field611) : MGOMessageScreen.getNonIsMarkImg(this.Field611), this.w - 2, 5, 24);
        GameResourceManager.Method124((byte) 9).drawString(BaseCanvas.g, this.letter.Field579, 10, 5 + Method332 + 5, 0);
    }

    ///////@Override // defpackage.Class184
    public final void paintBorder() {
        if (this.border > 0) {
            BaseCanvas.g.setColor(5592405);
            BaseCanvas.g.drawLine(0, 0, this.w - 1, 0);
            if (this.isFocused) {
                BaseCanvas.g.setColor(LAF.Field1283);
                BaseCanvas.g.drawRoundRect(0, 0, this.w - 1, this.h - 1, LAF.Field1297, LAF.Field1297);
            }
        }
    }
}
