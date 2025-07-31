package defpackage;

import vn.me.core.BaseCanvas;
import vn.me.ui.Label;
import vn.me.ui.Widget;
import vn.me.ui.WidgetGroup;

/* renamed from: Class105  reason: default package */
/* loaded from: gopet_repackage.jar:Class105.class */
public final class Class105 extends WidgetGroup {
    private int Field690;
    public byte Field691;
    public byte Field692;
    private byte Field693;
    private Label[] Field694;
    private int Field695;

    public Class105(int i, int i2, int i3, int i4, int i5) {
        super(15, i2, i3, i4);
        this.Field690 = 30;
        this.Field695 = i5;
        setViewMode(2);
        this.isScrollableY = true;
        this.Field693 = (byte) ((i3 + 2) / (i5 + 2));
        int i6 = (i4 + 2) / (i5 + 2);
        this.Field690 = this.Field693 * (i6 <= 0 ? 1 : i6);
    }

    public final void Method125(Label[] class173Arr) {
        this.Field694 = class173Arr;
        this.Field691 = (byte) (class173Arr.length / this.Field690);
        if (class173Arr.length % this.Field690 != 0) {
            this.Field691 = (byte) (this.Field691 + 1);
        }
    }

    public final void Method79(int i) {
        if (i >= this.Field691 || i < 0) {
            return;
        }
        this.Field692 = (byte) i;
        removeAll();
        int i2 = i * this.Field690;
        int i3 = (i2 + this.Field690) - 1;
        int i4 = i3;
        if (i3 >= this.Field694.length) {
            i4 = this.Field694.length - 1;
        }
        int i5 = 0;
        int i6 = 0;
        int i7 = 0;
        for (int i8 = 0; i8 < this.Field694.length; i8++) {
            if (i8 < i2 || i8 > i4) {
                this.Field694[i8].image = null;
            } else {
                addWidget(this.Field694[i8], false);
                this.Field694[i8].setPosition(i5, i6);
                this.Field694[i8].isFocused = false;
                i7++;
                if (i7 >= this.Field693) {
                    i7 = 0;
                    i5 = 0;
                    i6 += this.Field695 + 2;
                } else {
                    i5 += this.Field695 + 2;
                }
            }
        }
        BaseCanvas.getCurrentScreen().requestFocus(getWidgetAt(0));
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final boolean checkKeys(int i, int i2) {
        Widget Method315 = getFocusedWidget(false);
        if (Method315 == this || Method315 == null || !Method315.checkKeys(i, i2)) {
            int Method352 = getFocusedIndex();
            if (i == 0 && i2 == -3) {
                if (this.Field692 > 0 && Method352 % this.Field693 == 0) {
                    Method79(this.Field692 - 1);
                    return true;
                }
                int i3 = Method352 - 1;
                int i4 = i3;
                if (i3 < 0) {
                    i4 = 0;
                }
                BaseCanvas.getCurrentScreen().requestFocus(getWidgetAt(i4));
                return true;
            } else if (i == 0 && i2 == -4) {
                if (this.Field692 < this.Field691 - 1 && Method352 % this.Field693 == this.Field693 - 1) {
                    Method79(this.Field692 + 1);
                    return true;
                }
                Widget Method350 = getWidgetAt(Method352 + 1);
                if (Method350 != null) {
                    BaseCanvas.getCurrentScreen().requestFocus(Method350);
                    return true;
                }
                return true;
            } else if (i == 0 && i2 == -2) {
                int i5 = Method352 + this.Field693;
                int i6 = i5;
                if (i5 >= this.children.length) {
                    i6 = this.children.length - 1;
                }
                BaseCanvas.getCurrentScreen().requestFocus(getWidgetAt(i6));
                return true;
            } else if (i == 0 && i2 == -1) {
                int i7 = Method352 - this.Field693;
                int i8 = i7;
                if (i7 < 0) {
                    i8 = 0;
                }
                BaseCanvas.getCurrentScreen().requestFocus(getWidgetAt(i8));
                return true;
            } else {
                return true;
            }
        }
        return true;
    }
}
