package vn.me.ui;

import vn.me.core.BaseCanvas;
import defpackage.Command;
import vn.me.ui.common.Effects;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.ui.common.T;
import vn.me.ui.Widget;
import vn.me.ui.WidgetGroup;
import vn.me.ui.Font;
import java.util.Vector;

/* renamed from: Class175  reason: default package */
/* loaded from: gopet_repackage.jar:Class175.class */
public final class Menu extends WidgetGroup implements IActionListener {
    public Widget focusWid;
    private Vector Field1156;
    private Font Field1153 = ResourceManager.boldFont;
    private Font Field1154 = ResourceManager.boldFont;
    private int Field1155 = Math.max(this.Field1153.getHeight(), this.Field1154.getHeight()) + (2 * LAF.LOT_PADDING);
    private int Field1157 = 0;
    public boolean isShowNextMenu = false;

    public Menu() {
        this.padding = 1;
        this.border = 3;
        this.cmdRight = new Command(0, T.gL(0), this);
        this.cmdCenter = new Command(2, T.gL(7), this);
        this.isLoop = true;
        this.isScrollableY = true;
    }

    public final void showNewMenu(Vector vector, int i) {
        this.Field1157 = 0;
        this.isPressed = false;
        if (this.Field1156 != null && this.Field1156 != vector) {
            this.isShowNextMenu = true;
        }
        this.Field1156 = vector;
        int size = vector.size();
        this.Field1155 = Math.max(this.Field1153.getHeight(), this.Field1154.getHeight()) + (2 * LAF.LOT_PADDING);
        this.preferredSize.height = size * this.Field1155;
        this.h = this.preferredSize.height + ((this.border + this.padding) << 1) > BaseCanvas.Field162 ? BaseCanvas.Field162 : this.preferredSize.height + ((this.border + this.padding) << 1);
        int i2 = size;
        while (true) {
            i2--;
            if (i2 < 0) {
                break;
            }
            int Method330 = this.Field1153.getWidth(((Command) vector.elementAt(i2)).Field1321);
            this.preferredSize.width = this.preferredSize.width > Method330 ? this.preferredSize.width : Method330;
        }
        if (this.preferredSize.width < BaseCanvas.Field157) {
            this.preferredSize.width = BaseCanvas.Field157;
        }
        this.w = this.preferredSize.width + (2 * (this.padding + this.border));
        this.destY = ((BaseCanvas.h - LAF.LOT_ITEM_HEIGHT) - this.h) - 1;
        this.y = BaseCanvas.h - LAF.LOT_ITEM_HEIGHT;
        if (i == 0) {
            this.x = 1;
            this.destX = 1;
        } else if (i == 1) {
            int i3 = BaseCanvas.w - this.w;
            this.x = i3;
            this.destX = i3;
        } else {
            int i4 = BaseCanvas.Field157 - (this.w >> 1);
            this.x = i4;
            this.destX = i4;
        }
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final void paint() {
        BaseCanvas.g.translate(-this.scrollX, -this.scrollY);
        if (this.Field1156 != null && !this.Field1156.isEmpty()) {
            int size = this.Field1156.size();
            for (int i = 0; i < size; i++) {
                if (this.Field1157 == i) {
                    Effects.show1(BaseCanvas.g, this.isPressed ? LAF.Field1290 : LAF.CLR_MENU_BAR_LIGHTER, this.isPressed ? LAF.Field1289 : LAF.CLR_MENU_BAR_DARKER, 0, i * this.Field1155, (this.w - ((this.padding + this.border) << 1)) - 1, this.Field1155 - 1, false);
                }
                this.Field1153.drawString(BaseCanvas.g, ((Command) this.Field1156.elementAt(i)).Field1321, LAF.LOT_PADDING, (i * this.Field1155) + LAF.LOT_PADDING, 20);
                if (this.Field1157 == i) {
                    BaseCanvas.g.setColor(LAF.Field1283);
                    BaseCanvas.g.drawRect(0, i * this.Field1155, (this.w - ((this.padding + this.border) << 1)) - 1, this.Field1155 - 1);
                }
            }
        }
        BaseCanvas.g.translate(this.scrollX, this.scrollY);
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final boolean pointerPressed(int i, int i2) {
        int Method322 = Method322(i, i2);
        this.Field1157 = Method322 >= 0 ? Method322 : this.Field1157;
        this.isPressed = Method322 >= 0;
        return super.pointerPressed(i, i2);
    }

    ///////@Override // defpackage.Class184
    public final boolean pointerDragged(int i, int i2) {
        if (super.pointerDragged(i, i2)) {
            this.isPressed = false;
            return true;
        }
        return false;
    }

    ///////@Override // defpackage.Class184
    public final boolean pointerReleased(int i, int i2) {
        int Method322 = Method322(i, i2);
        if (this.Field1157 == Method322) {
            this.Field1157 = Method322;
            if (this.isPressed && !this.isDragActivated) {
                Method7();
                return true;
            }
            this.isPressed = false;
        }
        return super.pointerReleased(i, i2);
    }

    private int Method322(int i, int i2) {
        if (this.Field1156 == null || this.Field1156.isEmpty()) {
            return -1;
        }
        int size = this.Field1156.size();
        for (int i3 = 0; i3 < size; i3++) {
            int i4 = ((i - this.x) - this.padding) + this.scrollX;
            int i5 = ((i2 - this.y) - this.padding) + this.scrollY;
            if (i4 > 0 && i4 < this.w && i5 > i3 * this.Field1155 && i5 < (i3 * this.Field1155) + this.Field1155) {
                return i3;
            }
        }
        return -1;
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final boolean checkKeys(int i, int i2) {
        if (i2 == -3 || i2 == -4) {
            return true;
        }
        if (i2 == -5) {
            if (i != 1) {
                this.isPressed = true;
                return true;
            }
            Method7();
            this.isPressed = false;
            return true;
        }
        boolean z = false;
        if (i2 == -2 && i == 0) {
            if (this.Field1156 != null && !this.Field1156.isEmpty()) {
                if (this.Field1157 < this.Field1156.size() - 1) {
                    this.Field1157++;
                } else {
                    this.Field1157 = 0;
                }
                z = true;
            }
        } else if (i2 == -1 && i == 0 && this.Field1156 != null && !this.Field1156.isEmpty()) {
            if (this.Field1157 > 0) {
                this.Field1157--;
            } else {
                this.Field1157 = this.Field1156.size() - 1;
            }
            z = true;
        }
        if (z) {
            scrollTo(0, this.Field1157 * this.Field1155, this.w - ((this.border + this.padding) << 1), this.Field1155);
        }
        return z;
    }

    /* JADX INFO: Access modifiers changed from: protected */
    ///////@Override // defpackage.Class184
    public final void paintBorder() {
        super.paintBorder();
        LAF.Method422(this);
    }

    ///////@Override // defpackage.Class184
    public final void paintBackground() {
        LAF.Method423(this);
    }

    public static void hide() {
        BaseCanvas.getCurrentScreen().hideMenu();
    }

    private void Method7() {
        if (this.Field1156 == null || this.Field1157 < 0 || this.Field1157 >= this.Field1156.size()) {
            return;
        }
        if (this.isShowNextMenu) {
            this.isShowNextMenu = false;
        } else {
            BaseCanvas.getCurrentScreen().hideMenu();
        }
        ((Command) this.Field1156.elementAt(this.Field1157)).actionPerformed(new Command[]{(Command) this.Field1156.elementAt(this.Field1157)});
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 0:
                BaseCanvas.getCurrentScreen().hideMenu();
                return;
            case 2:
                Method7();
                return;
            default:
                return;
        }
    }
}
