package vn.me.ui.geom;

import vn.me.ui.geom.Dimension;

/* renamed from: Class199  reason: default package */
 /* loaded from: gopet_repackage.jar:Class199.class */
public final class Rectangle {

    public int x;
    public int y;
    public Dimension size;

    public Rectangle(int x, int y, Dimension size) {
        this.x = x;
        this.y = y;
        this.size = size;
    }

    public Rectangle(int x, int y, int w, int h) {
        this.x = x;
        this.y = y;
        this.size = new Dimension(w, h);
    }

    public int getWidth() {
        return this.size.width;
    }

    public int getHeight() {
        return this.size.height;
    }

    public boolean contains(int rX, int rY) {
        return this.x <= rX && this.y <= rY && this.x + this.size.width >= rX && this.y + this.size.height >= rY;
    }

    public boolean intersect(int x, int y, int w, int h) {
        boolean xOverlap = valueInRange(this.x, x, x + w) || valueInRange(x, this.x, this.x + this.size.width);
        boolean yOverlap = valueInRange(this.y, y, y + h) || valueInRange(y, this.y, this.y + this.size.height);
        return xOverlap && yOverlap;
    }

    public boolean intersect(Rectangle rect) {
        return intersect(rect.x, rect.y, rect.getWidth(), rect.getHeight());
    }

    public static boolean valueInRange(int x, int x1, int x2) {
        return x >= x1 && x <= x2;
    }

    public static boolean intersect(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2) {
        boolean xOverlap = valueInRange(x1, x2, x2 + w2) || valueInRange(x2, x1, x1 + w1);
        boolean yOverlap = valueInRange(y1, y2, y2 + h2) || valueInRange(y2, y1, y1 + h1);
        return xOverlap && yOverlap;
    }
}
