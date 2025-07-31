package defpackage;

import vn.me.core.BaseCanvas;
import vn.me.screen.PetGameScreen;

/* renamed from: Class52  reason: default package */
/* loaded from: gopet_repackage.jar:Class52.class */
final class Class52 extends mObject {
    private byte Field352;
    private final PetGameScreen Field353;

    public Class52(PetGameScreen class49, int i) {
        this.Field353 = class49;
        this.Field352 = (byte) i;
    }

    ///////@Override // defpackage.Class133
    public final void paintInMap(int i, int i2) {
        switch (this.Field352) {
            case 0:
                this.Field353.Field328.draw(BaseCanvas.g, this.xChar - i, (this.yChar - i2) - 31, 0);
                return;
            case 1:
                this.Field353.Field329.draw(BaseCanvas.g, this.xChar - i, (this.yChar - i2) - 11, 0);
                return;
            case 2:
                this.Field353.Field330.draw(BaseCanvas.g, this.xChar - i, (this.yChar - i2) - 31, 0);
                return;
            default:
                return;
        }
    }
}
