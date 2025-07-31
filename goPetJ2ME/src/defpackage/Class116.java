package defpackage;

import vn.me.ui.common.LAF;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import vn.me.ui.Label;
import vn.me.ui.EditField;
import javax.microedition.lcdui.Graphics;

/* renamed from: Class116  reason: default package */
/* loaded from: gopet_repackage.jar:Class116.class */
final class Class116 extends Dialog {
    private final ChatPlace Field761;

    public Class116(ChatPlace class115, int i, String str, String[] strArr, byte[] bArr, Command Command, Command Command2, Command Command3) {
        this.Field761 = class115;
        if (Command != null) {
            this.cmdLeft = Command;
        }
        if (Command3 != null) {
            this.cmdRight = Command3;
        }
        if (Command2 != null) {
            this.cmdCenter = Command2;
        }
        int i2 = BaseCanvas.w - (LAF.LOT_PADDING << 2);
        this.padding = 2;
        this.border = 2;
        this.spacing = 0;
        Label class173 = new Label(str, GameResourceManager.regularFont);
        class173.align = 17;
        addWidget(class173, false);
        Class14 class14 = new Class14();
        addWidget(class14, false);
        int i3 = 0;
        int i4 = (LAF.Field1293 << 1) + (LAF.LOT_PADDING << 2);
        if (strArr != null) {
            i4 = ((strArr.length << 1) + 2) * LAF.Field1293;
            setMetrics(BaseCanvas.Field157 - (i2 >> 1), BaseCanvas.Field158 - (i4 >> 1), i2, i4);
            for (int i5 = 0; i5 < strArr.length; i5++) {
                String[] Method331 = GameResourceManager.regularFont.wrap(strArr[i5], i2 - (LAF.LOT_PADDING << 1));
                Label class1732 = new Label(Method331[0], GameResourceManager.regularFont);
                class1732.align = 20;
                addWidget(class1732, false);
                for (int i6 = 1; i6 < Method331.length; i6++) {
                    Label class1733 = new Label(Method331[i6], GameResourceManager.regularFont);
                    class1733.align = 20;
                    addWidget(class1733, false);
                    i3++;
                }
                EditField class168 = new EditField();
                class168.setMetrics(LAF.LOT_PADDING << 2, 0, this.w - (LAF.LOT_PADDING << 2), LAF.Field1293);
                int i7 = 1;
                if (bArr[i5] == 0) {
                    i7 = 0;
                }
                class168.Method79(i7);
                addWidget(class168, false);
            }
        } else {
            EditField class1682 = new EditField();
            class1682.setMetrics(LAF.LOT_PADDING << 2, 0, i2 - (LAF.LOT_PADDING << 2), LAF.Field1293);
            class1682.Method79(bArr[0]);
            addWidget(class1682, false);
            if (this.cmdCenter != null) {
                this.cmdCenter.objPerfomed = class1682;
            }
        }
        setMetrics(BaseCanvas.Field157 - (i2 >> 1), BaseCanvas.Field158 - (i4 >> 1), i2, i4);
        setViewMode(1);
        class14.w = BaseCanvas.Field160;
        int i8 = BaseCanvas.Field159 >> 1;
        class14.x = i8;
        class14.destX = i8;
        this.h += i3 * LAF.Field1293;
        this.isScrollableY = true;
        this.isLoop = true;
    }

    ///////@Override // defpackage.Dialog, defpackage.Class184
    public final void paintBackground() {
        int i = this.w;
        int i2 = this.h;
        Graphics graphics = BaseCanvas.g;
        BaseCanvas.g.setColor(14473688);
        Ulti.Method377(2, 2, i - 4, i2 - 4, graphics);
        int width = ChatPlace.Method136(this.Field761).getWidth();
        int i3 = (i / width) + 1;
        for (int i4 = 0; i4 < i3; i4++) {
            BaseCanvas.g.drawImage(ChatPlace.Method136(this.Field761), 2 + (width * i4), 2, 0);
        }
    }
}
