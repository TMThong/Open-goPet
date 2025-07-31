package defpackage;

import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import java.io.IOException;
import javax.microedition.lcdui.Image;

/* JADX INFO: Access modifiers changed from: package-private */
/* renamed from: Class91  reason: default package */
/* loaded from: gopet_repackage.jar:Class91.class */
public final class Class91 extends Class108 {
    private final Class89 Field562;

    public Class91(Class89 class89, int i) {
        super(null);
        this.Field562 = class89;
        this.destScrollX = (byte) i;
    }

    ///////@Override // defpackage.Class165, defpackage.Class173, defpackage.Class184
    public final void paint() {
        BaseCanvas.g.translate(this.x, this.y);
        BaseCanvas.g.setColor(LAF.CLR_MENU_BAR_LIGHTER);
        BaseCanvas.g.fillRoundRect(0, 0, this.w, this.h, 10, 10);
        switch (Class89.Method80(this.Field562)[this.destScrollX]) {
            case -1:
                Image Method81 = Class89.Method81(this.Field562);
                if (Method81 == null) {
                    try {
                        Method81 = Class89.Method82(this.Field562, Image.createImage("/lock.png"));
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
                BaseCanvas.g.drawImage(Class89.Method81(this.Field562), -1, -4, 0);
                break;
            case 1:
                Image Method455 = PetGameModel.Field284.requestImg(Class89.Method83(this.Field562)[this.destScrollX]);
                if (Method455 != null) {
                    BaseCanvas.g.drawImage(Method455, -1, this.h >> 1, 6);
                    if (Class89.Method84(this.Field562)[this.destScrollX] != null) {
                        ResourceManager.boldFont.drawString(BaseCanvas.g, Class89.Method84(this.Field562)[this.destScrollX], Method455.getWidth() + 2, 3, 20);
                        break;
                    }
                }
                break;
        }
        BaseCanvas.g.translate(-this.x, -this.y);
    }

    ///////@Override // defpackage.Class165, defpackage.Class173, defpackage.Class184
    public final void onFocused() {
        super.onFocused();
        Class89.Method85(this.Field562, this.destScrollX);
        if (Class89.Method80(this.Field562)[this.destScrollX] == 1) {
            Class89.Method86(this.Field562, ResourceManager.boldFont.wrap(Class89.Method87(this.Field562)[this.destScrollX], BaseCanvas.w - 25));
        } else {
            Class89.Method86(this.Field562, null);
        }
    }
}
