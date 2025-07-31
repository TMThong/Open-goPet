package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.Dialog;
import javax.microedition.lcdui.Image;

/* renamed from: Class102  reason: default package */
/* loaded from: gopet_repackage.jar:Class102.class */
public final class Class102 extends Dialog implements IActionListener {
    private String[] Field620;
    private String Field622;
    public int Field623;
    private String Field624;
    private String[] Field628;
    private int Field629;
    private int Field621 = 0;
    private Command Field625 = new Command(1, ActorFactory.gL(664), this);
    private Command Field626 = new Command(2, ActorFactory.gL(364), this);
    private Command Field627 = new Command(3, "", this);

    public Class102(int i, String str, String[] strArr, int i2) {
        this.Field623 = i;
        this.Field622 = str;
        this.Field620 = strArr;
        this.Field624 = this.Field620[0];
        this.padding = LAF.LOT_PADDING;
        this.cmdCenter = GameController.Field463;
        this.cmdRight = this.Field625;
        this.Field1058 = true;
        this.border = 3;
        this.w = BaseCanvas.w - (LAF.LOT_PADDING << 1);
        this.preferredSize.width = this.w - (2 * this.padding);
        this.Field628 = ResourceManager.boldFont.wrap(this.Field624, this.w - ((LAF.LOT_PADDING + this.border) << 2));
        this.h = (ResourceManager.boldFont.getHeight() * this.Field628.length) + ((this.padding + this.border) << 1);
        if (this.h < 60) {
            this.h = 60;
        }
        setMetrics(LAF.LOT_PADDING, ((BaseCanvas.h - this.h) - this.padding) - LAF.Field1292, this.w, this.h);
        this.Field629 = (((this.h >> 1) - ((this.Field628.length * ResourceManager.boldFont.getHeight()) / 2)) - this.padding) - this.border;
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public final void paint() {
        super.paint();
        Image Method122 = GameResourceManager.loadResourceImg(this.Field622, (byte) 1);
        if (this.Field622 == null || Method122 == null) {
            int i = 0;
            int i2 = this.Field629;
            while (true) {
                int i3 = i2;
                if (i >= this.Field628.length) {
                    return;
                }
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field628[i], (this.padding + this.border) << 1, i3, 20);
                i++;
                i2 = i3 + ResourceManager.boldFont.getHeight();
            }
        } else {
            BaseCanvas.g.drawImage(Method122, Method122.getWidth() >> 1, ((this.h - this.padding) - this.border) >> 1, 3);
            int i4 = 0;
            int i5 = this.Field629;
            while (true) {
                int i6 = i5;
                if (i4 >= this.Field628.length) {
                    return;
                }
                ResourceManager.boldFont.drawString(BaseCanvas.g, this.Field628[i4], Method122.getWidth() + LAF.LOT_PADDING, i6, 20);
                i4++;
                i5 = i6 + ResourceManager.boldFont.getHeight();
            }
        }
    }

    private void Method47(String str) {
        this.Field624 = str;
        Image Method122 = GameResourceManager.loadResourceImg(this.Field622, (byte) 1);
        if (this.Field622 == null || Method122 == null) {
            return;
        }
        this.Field628 = ResourceManager.boldFont.wrap(this.Field624, (this.w - ((LAF.LOT_PADDING + this.border) << 2)) - (Method122 != null ? Method122.getWidth() : 0));
        this.h = (ResourceManager.boldFont.getHeight() * this.Field628.length) + ((this.padding + this.border) << 1);
        if (this.h < (Method122 != null ? Method122.getHeight() : 0) + (2 * (this.padding + this.border))) {
            this.h = (Method122 != null ? Method122.getHeight() : 0) + (2 * (this.padding + this.border));
        }
        if (this.h < 60) {
            this.h = 60;
        }
        setMetrics(LAF.LOT_PADDING, ((BaseCanvas.h - this.h) - this.padding) - LAF.Field1292, this.w, this.h);
        this.Field629 = (((this.h >> 1) - ((this.Field628.length * ResourceManager.boldFont.getHeight()) / 2)) - this.padding) - this.border;
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case 0:
                Method274();
                GameController.Field460 = 1;
                ActorFactory.saveInt("guide", GameController.Field460);
                return;
            case 1:
                if (this.Field621 < this.Field620.length - 1) {
                    this.Field621++;
                    this.cmdLeft = this.Field626;
                    if (this.Field621 == this.Field620.length - 1) {
                        this.cmdRight = this.Field627;
                    }
                    Method47(this.Field620[this.Field621]);
                    return;
                }
                return;
            case 2:
                if (this.Field621 > 0) {
                    this.Field621--;
                    this.cmdRight = this.Field625;
                    if (this.Field621 == 0) {
                        this.cmdLeft = this.Field627;
                    }
                    if (this.Field621 > 0 && this.Field621 < this.Field620.length) {
                        this.cmdRight = this.Field625;
                    }
                    Method47(this.Field620[this.Field621]);
                    return;
                }
                return;
            case 3:
            default:
                return;
        }
    }
}
