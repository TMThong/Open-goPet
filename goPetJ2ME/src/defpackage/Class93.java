package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.network.Message;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import javax.microedition.lcdui.Image;
import vn.me.ui.common.T;

/* renamed from: Class93  reason: default package */
/* loaded from: gopet_repackage.jar:Class93.class */
public final class Class93 extends Dialog implements IActionListener {
    private int Field563;
    private String Field564;
    private int Field565;
    private int Field566;
    private long Field567;
    private int Field568;
    private Image Field569;

    public Class93(int i, String str, int i2, int i3, int i4, int i5) {
        this.Field563 = i;
        this.Field564 = str;
        this.Field565 = i4;
        this.Field566 = i5;
        this.w = BaseCanvas.w - (LAF.LOT_PADDING << 1);
        if (this.h > 0) {
            this.h = i3 + ((this.padding + this.border) << 1) + 10;
        } else {
            this.h = 150;
        }
        setMetrics(LAF.LOT_PADDING, ((BaseCanvas.h - LAF.Field1293) - this.h) - this.padding, this.w, this.h);
        this.cmdCenter = new Command(0, T.gL(T.OK), this);
        this.cmdRight = new Command(1, T.gL(T.CLOSE_STR), this);
    }

    ///////@Override // defpackage.Dialog, defpackage.Class185, defpackage.Class184
    public final void update() {
        super.update();
        long currentTimeMillis = System.currentTimeMillis();
        if (currentTimeMillis - this.Field567 >= this.Field566) {
            this.Field568 = (this.Field568 + 1) % this.Field565;
            this.Field567 = currentTimeMillis;
        }
    }

    ///////@Override // defpackage.Dialog, defpackage.Class184
    public final void paintBackground() {
        super.paintBackground();
        if (this.Field569 == null) {
            this.Field569 = PetGameModel.Field284.requestImg(this.Field564);
            return;
        }
        BaseCanvas.g.translate(this.w >> 1, this.h >> 1);
        int width = this.Field569.getWidth() / this.Field565;
        BaseCanvas.g.drawRegion(this.Field569, width * this.Field568, 0, width, this.Field569.getHeight(), 0, 0, 0, 3);
        BaseCanvas.g.translate(-(this.w >> 1), -(this.h >> 1));
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 0:
                Message message = new Message(122);
                message.putByte(11);
                message.putInt(this.Field563);
                GlobalService.session.sendMessage(message);
                message.cleanup();
                return;
            case 1:
                PetGameModel.Field284.Method456(this.Field564);
                Method274();
                return;
            default:
                return;
        }
    }
}
