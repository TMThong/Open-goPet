package vn.me.ui.common;

import javax.microedition.lcdui.Graphics;
import javax.microedition.lcdui.Image;

/* loaded from: goWin246.jar:vn/me/ui/model/FrameImage.class */
public class FrameImage {

    public int frameWidth;
    public int frameHeight;
    public int nFrame;
    private Image imgFrame;
    private int[] pos;
    private int total;
    private boolean isHorizontal;

    public FrameImage(Image img, int fwidth, int fheight) {
        this(img, fwidth, fheight, false);
    }

    public FrameImage(Image img, int fwidth, int fheight, boolean isH) {
        this.isHorizontal = false;
        this.imgFrame = img;
        this.frameWidth = fwidth;
        this.frameHeight = fheight;
        this.total = isH ? img.getWidth() : img.getHeight();
        this.nFrame = this.total / (isH ? fwidth : fheight);
        this.pos = new int[this.nFrame];
        for (int i = 0; i < this.nFrame; i++) {
            this.pos[i] = i * (isH ? fwidth : fheight);
        }
        this.isHorizontal = isH;
    }

    public FrameImage(Image img, int numberOfFrame) {
        this(img, img.getWidth(), img.getHeight() / numberOfFrame);
    }

    public FrameImage(Image img, int noFrame, boolean isHorizontital) {
        this(img, img.getWidth() / noFrame, img.getHeight(), isHorizontital);
    }

    public void drawFrame(int index, int x, int y, int trans, Graphics g) {
        drawFrame(g, index, x, y, trans, 20);
    }

    public void drawFrame(Graphics g, int index, int x, int y, int trans, int anchor) {
        if (index >= 0 && index < this.nFrame && !this.isHorizontal && this.imgFrame != null) {
            g.drawRegion(this.imgFrame, 0, this.pos[index], this.frameWidth, this.frameHeight, trans, x, y, anchor);
        } else if (index >= 0 && index < this.nFrame && this.imgFrame != null && this.isHorizontal) {
            g.drawRegion(this.imgFrame, this.pos[index], 0, this.frameWidth, this.frameHeight, trans, x, y, anchor);
        }
    }

    public void clear() {
        this.imgFrame = null;
    }

    public Image getImage(int id) {
        int xx = this.isHorizontal ? id * this.frameWidth : 0;
        int yy = this.isHorizontal ? 0 : id * this.frameHeight;
        return Image.createImage(this.imgFrame, xx, yy, this.frameWidth, this.frameHeight, 0);
    }

    public Image getImage() {
        return this.imgFrame;
    }
}
