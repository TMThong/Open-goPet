package defpackage;

import vn.me.ui.geom.Rectangle;
import vn.me.core.BaseCanvas;

/* renamed from: Class131  reason: default package */
 /* loaded from: gopet_repackage.jar:Class131.class */
public final class Class131 extends mObject {

    private byte Field857;
    public int Field858;
    public byte Field859;
    SkillEffect[] Field860;

    public Class131(int i, byte b) {
        this.Field857 = b;
        this.Field858 = i;
    }

    public final void draw(int i, int i2) {
        switch (this.Field857) {
            case 0:
                if (this.img != null) {
                    BaseCanvas.g.drawImage(this.img, i - (this.img.getWidth() >> 1), i2 - this.img.getHeight(), 0);
                    return;
                }
                return;
            case 1:
                if (this.Field860 != null) {
                    this.Field860[this.Field859].draw(BaseCanvas.g, i, i2, 0);
                    return;
                }
                return;
            default:
                return;
        }
    }

    ///////@Override // defpackage.Class133
    public final void paintInMap(int i, int i2) {
        draw(this.xChar - i, this.yChar - i2);
    }

    ///////@Override // defpackage.Class133
    public final Rectangle getPosition() {
        this.position.x = this.x + this.xChar;
        this.position.y = this.y + this.yChar;
        return this.position;
    }
}
