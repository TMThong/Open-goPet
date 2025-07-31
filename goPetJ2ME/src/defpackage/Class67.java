package defpackage;

import vn.me.screen.AnimationMenu;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;
import javax.microedition.lcdui.Image;

/* renamed from: Class67  reason: default package */
/* loaded from: gopet_repackage.jar:Class67.class */
public final class Class67 extends Class65 {
    public String Field427;
    private Image Field428;
    public boolean Field429;
    public int Field430;
    public int Field431;
    private int Field432;
    private long Field433;
    private final AnimationMenu Field434;

    public Class67(AnimationMenu class61) {
        super(class61);
        this.Field434 = class61;
        this.Field430 = 1;
    }

    ///////@Override // defpackage.Class184
    public final void paint() {
        if (this.Field428 == null) {
            Image Method455 = PetGameModel.Field284.requestImg(this.Field427);
            Image image = Method455;
            if (Method455 != null) {
                if (this.Field429) {
                    image = Effects.show0(image, image.getHeight() << 1);
                }
                this.Field428 = image;
                setSize(this.w, image.getHeight() + 4);
                AnimationMenu.Method12(this.Field434).doLayout();
                return;
            }
            return;
        }
        byte b = this.Field423;
        int width = this.Field428.getWidth() / this.Field430;
        int i = 0;
        int i2 = 0;
        switch (b) {
            case 17:
                i = 24;
                i2 = this.w;
                break;
            case 20:
                i = 0;
                i2 = 0;
                break;
            case 24:
                i = 17;
                i2 = this.w >> 1;
                break;
        }
        BaseCanvas.g.drawRegion(this.Field428, this.Field432 * width, 0, width, this.Field428.getHeight(), 0, i2, 0, i);
        long currentTimeMillis = System.currentTimeMillis();
        if (currentTimeMillis - this.Field433 >= 200) {
            this.Field433 = currentTimeMillis;
            this.Field432 = (this.Field432 + 1) % this.Field430;
        }
    }
}
