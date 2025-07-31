package vn.me.ui.geom;

/* renamed from: Class197  reason: default package */
/* loaded from: gopet_repackage.jar:Class197.class */
public final class Dimension {
    public int width;
    public int height;

    public Dimension() {
    }

    public Dimension(int i, int i2) {
        this.width = i;
        this.height = i2;
    }

    ///////@Override
    public final String toString() {
        return new StringBuffer("Dimension{width=").append(this.width).append(", height=").append(this.height).append('}').toString();
    }
}
