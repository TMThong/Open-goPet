package defpackage;

import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.screen.Screen;
import javax.microedition.lcdui.Image;

/* renamed from: Class17  reason: default package */
/* loaded from: gopet_repackage.jar:Class17.class */
public final class Class17 extends Screen {
    public Image[] Field60;
    private int Field61;
    private int Field62;
    private int Field63;
    private int Field64;
    private int Field65;

    public Class17() {
        super(true);
        this.Field60 = new Image[9];
        this.Field1184 = true;
    }

    public final void Method50() {
        this.Field61 = this.Field60[0].getWidth() + this.Field60[1].getWidth() + this.Field60[2].getWidth();
        this.Field62 = this.Field60[0].getHeight() + this.Field60[3].getHeight() + this.Field60[6].getHeight();
        this.Field63 = (BaseCanvas.w - this.Field61) >> 1;
        this.Field64 = (BaseCanvas.h - this.Field62) >> 1;
    }

    ///////@Override // defpackage.Screen
    public final void paintBackground() {
        int width;
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        BaseCanvas.g.translate(this.Field63, this.Field64);
        int i = 0;
        int i2 = 0;
        for (int i3 = 0; i3 < this.Field60.length; i3++) {
            BaseCanvas.g.drawImage(this.Field60[i3], i, i2, 0);
            if (i3 % 3 == 2) {
                i2 += this.Field60[i3].getHeight();
                width = 0;
            } else {
                width = i + this.Field60[i3].getWidth();
            }
            i = width;
        }
        BaseCanvas.g.translate(-this.Field63, -this.Field64);
    }

    ///////@Override // defpackage.Screen
    public final boolean checkKeys(int i, int i2) {
        if (currentDialog == null && i == 1) {
            Method6();
            return true;
        }
        return super.checkKeys(i, i2);
    }

    public final void Method271(int i, int i2, int i3, int i4) {
        this.Field65 = i2;
    }

    private void Method6() {
        if (this.Field65 < 0) {
            Method297(null);
            return;
        }
        GameController.waitDialog();
        Message message = new Message(81);
        message.putByte(65);
        GlobalService.session.sendMessage(message);
        message.cleanup();
    }

    ///////@Override // defpackage.Screen
    public final void pointerPressed(int i, int i2) {
        if (currentDialog == null) {
            Method6();
        } else {
            super.pointerPressed(i, i2);
        }
    }
}
