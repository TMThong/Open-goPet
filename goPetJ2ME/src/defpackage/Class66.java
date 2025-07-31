package defpackage;

import vn.me.ui.common.LAF;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;
import vn.me.screen.BoardListScreen;
import vn.me.ui.WidgetGroup;
import java.util.Vector;

/* JADX INFO: Access modifiers changed from: package-private */
/* renamed from: Class66  reason: default package */
/* loaded from: gopet_repackage.jar:Class66.class */
public final class Class66 extends WidgetGroup {
    private int Field424;
    private final Vector Field425;
    private final BoardListScreen Field426;

    /* JADX INFO: Access modifiers changed from: package-private */
    public Class66(BoardListScreen class64, int i, Vector vector) {
        super(2);
        this.Field426 = class64;
        this.Field425 = vector;
        this.Field424 = this.Field425.size();
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final void paint() {
        BaseCanvas.g.translate(-this.scrollX, -this.scrollY);
        int i = (this.scrollY / this.Field426.Field420) * this.columns;
        int i2 = i < 0 ? 0 : i;
        int i3 = i2;
        int Method20 = i2 + BoardListScreen.Method20(this.Field426);
        int i4 = Method20;
        if (Method20 > this.Field424) {
            i4 = this.Field424;
        }
        for (int i5 = i3; i5 < i4; i5++) {
            int i6 = i5 % this.columns;
            int i7 = i5 / this.columns;
            int i8 = (i6 * this.Field426.Field420) + ((i6 + 1) * this.spacing);
            int i9 = i7 * this.Field426.Field420;
            if (this.isFocused && i5 == this.Field426.Field418) {
                Effects.show1(BaseCanvas.g, this.isPressed ? LAF.Field1290 : LAF.CLR_MENU_BAR_LIGHTER, this.isPressed ? LAF.Field1289 : LAF.CLR_MENU_BAR_DARKER, i8 + 1, i9 + 1, this.Field426.Field420 - 2, this.Field426.Field420 - 2, false);
                BaseCanvas.g.setColor(LAF.Field1283);
                BaseCanvas.g.drawRoundRect(i8, i9, this.Field426.Field420 - 1, this.Field426.Field420 - 1, LAF.Field1297, LAF.Field1297);
            }
            Class141 class141 = (Class141) this.Field425.elementAt(i5);
            BaseCanvas.g.drawImage(GameResourceManager.Field600, i8 + (this.Field426.Field420 >> 1), i9 + (this.Field426.Field420 >> 1), 3);
            GameResourceManager.Method117().drawString(BaseCanvas.g, new StringBuffer().append(class141.Field973).toString(), (i8 + (this.Field426.Field420 >> 1)) - 10, i9 + 15, 17);
            GameResourceManager.italicFont.drawString(BaseCanvas.g, class141.Method218(), i8 + (this.Field426.Field420 >> 1) + 10, i9 + 11, 17);
        }
        BaseCanvas.g.translate(this.scrollX, this.scrollY);
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final boolean checkKeys(int i, int i2) {
        if (i != 0) {
            return false;
        }
        boolean z = false;
        if (i2 == -2) {
            this.Field426.Field418 += this.columns;
            if (this.Field426.Field418 >= this.Field425.size()) {
                this.Field426.Field418 = 0;
            }
            z = true;
        } else if (i2 == -1) {
            this.Field426.Field418 -= this.columns;
            if (this.Field426.Field418 < 0) {
                this.Field426.Field418 = this.Field425.size() - 1;
            }
            z = true;
        } else if (i2 == -4) {
            this.Field426.Field418++;
            if (this.Field426.Field418 >= this.Field425.size()) {
                this.Field426.Field418 = 0;
            }
            z = true;
        } else if (i2 == -3) {
            this.Field426.Field418--;
            if (this.Field426.Field418 < 0) {
                this.Field426.Field418 = this.Field425.size() - 1;
            }
            z = true;
        }
        int i3 = this.Field426.Field418 % this.columns;
        scrollTo((i3 * this.Field426.Field420) + ((i3 + 1) * this.spacing), (this.Field426.Field418 / this.columns) * this.Field426.Field420, this.Field426.Field420, this.Field426.Field420);
        return z;
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final boolean pointerPressed(int i, int i2) {
        super.pointerPressed(i, i2);
        for (int i3 = 0; i3 < this.Field424; i3++) {
            int i4 = i3 % this.columns;
            int i5 = i3 / this.columns;
            int i6 = (i4 * this.Field426.Field420) + ((i4 + 1) * this.spacing);
            int i7 = i5 * this.Field426.Field420;
            int i8 = ((i - this.x) - this.padding) + this.scrollX;
            int i9 = ((i2 - this.y) - this.padding) + this.scrollY;
            if (i8 > i6 && i8 < i6 + this.Field426.Field420 && i9 > i7 && i9 < i7 + this.Field426.Field420) {
                this.Field426.Field418 = i3;
                return true;
            }
        }
        return false;
    }
}
