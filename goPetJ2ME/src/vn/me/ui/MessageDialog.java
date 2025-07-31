package vn.me.ui;

import defpackage.Command;
import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;

public final class MessageDialog extends Dialog {
    public int Field1159;
    private String[] Field1160;
    private int Field1161;
    private int Field1162;
    private int Field1163;
    private byte Field1164 = 0;

    public MessageDialog(String str, Command Command, Command Command2, Command Command3, int i) {
        this.Field1159 = 0;
        this.padding = LAF.LOT_PADDING;
        this.cmdLeft = Command == null ? cmdNull : Command;
        this.cmdCenter = Command2 == null ? cmdNull : Command2;
        this.cmdRight = Command3 == null ? cmdNull : Command3;
        this.Field1159 = i;
        if (i == 2) {
            this.Field1162 = ResourceManager.Field1307.frameWidth >> 2;
            this.Field1163 = ResourceManager.Field1307.frameHeight;
        } else if (i == 1) {
            this.Field1058 = true;
        }
        this.w = BaseCanvas.w - (LAF.LOT_PADDING << 1);
        this.preferredSize.width = this.w - (2 * this.padding);
        this.Field1160 = ResourceManager.boldFont.wrap(str, this.w - (i == 2 ? (this.Field1162 + this.padding) + this.border : 2 * (this.padding + this.border)));
        this.h = (ResourceManager.boldFont.getHeight() * this.Field1160.length) + ((this.padding + this.border) << 1);
        if (this.h < 60) {
            this.h = 60;
        }
        setMetrics(LAF.LOT_PADDING, ((BaseCanvas.h - LAF.Field1293) - this.h) - this.padding, this.w, this.h);
        this.Field1161 = (((this.h >> 1) - ((this.Field1160.length * ResourceManager.boldFont.getHeight()) / 2)) - this.padding) - this.border;
    }

    ///////@Override 
    public final void paintBackground() {
        if (this.Field1159 != 1) {
            super.paintBackground();
            return;
        }
        BaseCanvas.g.setColor(LAF.Field1284);
        BaseCanvas.g.fillRect(2, 2, this.w - 4, this.h - 4);
    }

    ///////@Override 
    public final void paint() {
        super.paint();
        if (this.Field1159 == 2) {
            ResourceManager.Field1307.drawFrame(BaseCanvas.g, this.Field1164, this.padding, ((this.h >> 1) - (this.Field1163 >> 1)) - this.padding, 0, 0);
        }
        int i = 0;
        int i2 = this.Field1161;
        while (true) {
            int i3 = i2;
            if (i >= this.Field1160.length) {
                return;
            }
            ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field1160[i], ((this.w + (this.Field1159 == 2 ? this.Field1162 + this.padding : 0)) >> 1) - (this.padding << 1), i3, 17);
            i++;
            i2 = i3 + ResourceManager.boldFont.getHeight();
        }
    }

    ///////@Override 
    public final void update() {
        super.update();
        if (this.Field1159 == 2 && BaseCanvas.ticks % 2 == 0) {
            this.Field1164 = (byte) (this.Field1164 + 1);
            if (this.Field1164 == ResourceManager.Field1307.nFrame) {
                this.Field1164 = (byte) 0;
            }
        }
    }
}
