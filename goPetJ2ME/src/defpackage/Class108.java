package defpackage;

import vn.me.ui.Button;
import vn.me.core.BaseCanvas;
import javax.microedition.lcdui.Image;

/* renamed from: Class108  reason: default package */
/* loaded from: gopet_repackage.jar:Class108.class */
public class Class108 extends Button {
    private int Field699;
    private long Field700;

    public Class108(Image image) {
        super(image);
    }

    ///////@Override // defpackage.Class184
    public final void paintComponent() {
        paint();
        if (this.isFocused) {
            BaseCanvas.g.drawImage(PetGameModel.arrow2Img, (this.x - 6) + this.Field699, this.y + ((this.h - 14) / 2), 0);
            long currentTimeMillis = System.currentTimeMillis();
            if (currentTimeMillis - this.Field700 >= 200) {
                this.Field700 = currentTimeMillis;
                if (this.Field699 == -2) {
                    this.Field699 = 0;
                } else {
                    this.Field699 = -2;
                }
            }
        }
    }
}
