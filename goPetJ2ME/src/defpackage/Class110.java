package defpackage;

import vn.me.ui.common.LAF;
import vn.me.screen.PetInfoScreen;
import vn.me.core.BaseCanvas;
import vn.me.ui.Label;
import javax.microedition.lcdui.Image;

/* JADX INFO: Access modifiers changed from: package-private */
/* renamed from: Class110  reason: default package */
/* loaded from: gopet_repackage.jar:Class110.class */
public final class Class110 extends Label {
    public PetItem Field719;

    private Class110(PetInfoScreen class109) {
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(2, 2, this.w - 4, this.h - 4);
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public final void update() {
        super.update();
        if (this.Field719 != null) {
            this.Field719.update();
        }
    }

    /* JADX INFO: Access modifiers changed from: protected */
    ///////@Override // defpackage.Class184
    public final void paintBorder() {
        if (this.isFocused) {
            BaseCanvas.g.setColor(LAF.Field1283);
            BaseCanvas.g.drawRect(0, 0, this.w - 1, this.h - 1);
            BaseCanvas.g.drawRect(1, 1, this.w - 3, this.h - 3);
        }
    }

    ///////@Override // defpackage.Class173, defpackage.Class184
    public final void paint() {
        Image Method455;
        if (this.Field719 != null) {
            if (this.Field719.imgPathId != null && (Method455 = PetGameModel.Field284.requestImg(this.Field719.imgPathId)) != null) {
                BaseCanvas.g.drawImage(Method455, this.w >> 1, this.h >> 1, 3);
            }
            if (this.Field719.Field684 == 0 || this.Field719.Field685 == null) {
                return;
            }
            GameResourceManager.Method117().drawString(BaseCanvas.g, this.Field719.Field685, 0, 0, 0);
        }
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public Class110(PetInfoScreen class109, byte b) {
        this(class109);
    }
}
