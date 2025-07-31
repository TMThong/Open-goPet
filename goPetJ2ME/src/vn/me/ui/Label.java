package vn.me.ui;

import vn.me.core.BaseCanvas;
import vn.me.ui.common.FrameImage;
import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.ui.Widget;
import vn.me.ui.Font;
import vn.me.ui.geom.Dimension;
import vn.me.screen.Screen;
import javax.microedition.lcdui.Image;

/* renamed from: Class173  reason: default package */
 /* loaded from: gopet_repackage.jar:Class173.class */
public class Label extends Widget {

    public String text;
    public FrameImage image;
    public int frameIndex;
    public byte animationType;
    public boolean isAnimatable;
    public int align;
    private int textPosition;
    private int gap;
    private int shiftText;
    private int shiftTextLimit;
    private boolean Field1132;
    private boolean Field1133;
    public Font normalfont;
    public Font selectedfont;
    public boolean Field1136;
    public byte scrollType;
    private long Field1138;
    private long Field1139;
    private long Field1140;
    private long Field1141;
    public boolean isRenderImage = false;

    public Label() {
        this.frameIndex = 0;
        this.animationType = (byte) 0;
        this.isAnimatable = false;
        this.align = 20;
        this.textPosition = 8;
        this.gap = 2;
        this.shiftText = 0;
        this.shiftTextLimit = 0;
        this.Field1132 = false;
        this.Field1133 = true;
        this.normalfont = ResourceManager.boldFont;
        this.selectedfont = ResourceManager.boldFont;
        this.Field1136 = false;
        this.scrollType = (byte) 0;
        this.Field1139 = -1L;
        this.Field1141 = -1L;
    }

    public Label(String str) {
        this(str, LAF.Field1298 == 0 ? ResourceManager.defaultFont : ResourceManager.boldFont);
    }

    public Label(String str, Font class171) {
        this(str, class171, class171);
    }

    private Label(String str, Font class171, Font class1712) {
        this.frameIndex = 0;
        this.animationType = (byte) 0;
        this.isAnimatable = false;
        this.align = 20;
        this.textPosition = 8;
        this.gap = 2;
        this.shiftText = 0;
        this.shiftTextLimit = 0;
        this.Field1132 = false;
        this.Field1133 = true;
        this.normalfont = ResourceManager.boldFont;
        this.selectedfont = ResourceManager.boldFont;
        this.Field1136 = false;
        this.scrollType = (byte) 0;
        this.Field1139 = -1L;
        this.Field1141 = -1L;
        this.normalfont = class171;
        this.selectedfont = class1712 == null ? class171 : class1712;
        this.padding = LAF.LOT_PADDING;
        Method158(str);
        this.isFocusable = false;
    }

    private void Method6() {
        this.preferredSize.height = Math.max(this.normalfont.getHeight(), this.selectedfont.getHeight());
        if (this.text != null) {
            this.preferredSize.width = Math.max(this.normalfont.getWidth(this.text), this.selectedfont.getWidth(this.text));
        }
        if (this.image != null) {
            this.preferredSize.width += this.image.frameWidth + this.gap;
            this.preferredSize.height = Math.max(this.image.frameHeight, this.preferredSize.height);
        }
        this.h = this.preferredSize.height + (2 * (this.padding + this.border));
        this.w = this.preferredSize.width + (2 * (this.padding + this.border));
    }

    public final void Method158(String str) {
        this.text = str;
        Method6();
    }

    public final void Method324(Font class171, Font class1712) {
        this.normalfont = class171;
        this.selectedfont = class1712 == null ? class171 : class1712;
        Method6();
    }

    public void setImage(Image image) {
        if (image == null) {
            return;
        }
        set(image, new Dimension(image.getWidth(), image.getHeight()), false);
    }

    public final void setImage(Image image, Dimension class197) {
        if (image == null) {
            return;
        }
        set(image, class197, false);
    }

    private void set(Image image, Dimension class197, boolean z) {
        if (image == null) {
            return;
        }
        this.image = new FrameImage(image, class197.width, class197.height, false);
        this.frameIndex = 0;
        this.preferredSize = new Dimension(class197.width, class197.height);
    }

    public void paint() {
        if (this.image == null && (this.text == null || this.text.length() == 0)) {
            (this.isFocused ? this.selectedfont : this.normalfont).drawString(BaseCanvas.g, "", this.align == 17 ? (this.w >> 1) - this.padding : this.shiftText, 0, this.align);
        } else {
            int cx = BaseCanvas.g.getClipX();
            int cy = BaseCanvas.g.getClipY();
            int cw = BaseCanvas.g.getClipWidth();
            int ch = BaseCanvas.g.getClipHeight();
            int fontHeight;
            int yy;
            if (this.image != null) {
                if (this.text == null || "".equals(this.text) || isRenderImage) {
                    fontHeight = (this.h >> 1) - this.padding - this.border + (this.isAnimatable && (this.animationType == 2 || this.animationType == 4) && BaseCanvas.ticks % 12 == 0 ? 2 : 0);
                    this.image.drawFrame(BaseCanvas.g, this.frameIndex, (this.w >> 1) - this.padding - this.border, fontHeight, 0, 3);
                    return;
                }

                if (this.textPosition == 32) {
                    fontHeight = Math.max(this.normalfont.getHeight(), this.selectedfont.getHeight());
                    yy = (this.h - (this.padding + this.border << 1) - fontHeight >> 1) + (this.isAnimatable && (this.animationType == 2 || this.animationType == 4) && BaseCanvas.ticks % 12 == 0 ? 2 : 0);
                    this.image.drawFrame(BaseCanvas.g, this.frameIndex, (this.w >> 1) - this.padding - this.border, yy, 0, 3);
                } else {
                    fontHeight = this.textPosition == 4 ? this.w - (this.padding + this.border << 1) : 1;
                    yy = this.isAnimatable && (this.animationType == 2 || this.animationType == 4) && BaseCanvas.ticks % 12 == 0 ? 2 : 0;
                    this.image.drawFrame(BaseCanvas.g, this.frameIndex, fontHeight, yy, 0, this.textPosition == 8 ? 20 : 24);
                    if (this.textPosition == 8) {
                        BaseCanvas.g.clipRect(this.image.frameWidth, 0, this.w - this.image.frameWidth - (this.padding << 1) - this.gap, this.h);
                    } else if (this.textPosition == 4) {
                        BaseCanvas.g.clipRect(0, 0, this.w - this.image.frameWidth - (this.padding << 1) - this.gap, this.h);
                    }
                }
            }
            if (this.align == 24) {
                if (this.image == null) {
                    (this.isFocused ? this.selectedfont : this.normalfont).drawString(BaseCanvas.g, this.text, this.w - (this.padding << 1), (this.h >> 1) - (this.isFocusable ? this.selectedfont.getHeight() >> 1 : this.normalfont.getHeight() >> 1) - LAF.LOT_PADDING, this.align);
                } else {
                    (this.isFocused ? this.selectedfont : this.normalfont).drawString(BaseCanvas.g, this.text, this.textPosition == 8 ? this.w - (this.padding << 1) : this.w - (this.padding << 1) - this.image.frameWidth + this.gap, (this.h >> 1) - (this.isFocusable ? this.selectedfont.getHeight() >> 1 : this.normalfont.getHeight() >> 1) - LAF.LOT_PADDING, this.align);
                }
            } else {
                fontHeight = this.align == 17 ? (this.w >> 1) - this.padding - this.border : (this.textPosition == 8 ? this.shiftText + (this.image == null ? 0 : this.image.frameWidth + this.gap) : (this.w >> 1) - (this.isFocused ? this.selectedfont : this.normalfont).getWidth(this.text) - LAF.LOT_PADDING);
                yy = Math.max(this.normalfont.getHeight(), this.selectedfont.getHeight());
                yy = this.textPosition == 32 ? this.h - (this.border + this.padding << 1) - yy : (this.h >> 1) - ((this.isFocused ? this.selectedfont : this.normalfont).getHeight() >> 1) - LAF.LOT_PADDING;
                (this.isFocused ? this.selectedfont : this.normalfont).drawString(BaseCanvas.g, this.text, fontHeight, yy, this.align);
            }
            BaseCanvas.g.setClip(cx, cy, cw, ch);
        }
    }

    ///////@Override // defpackage.Class184
    public void paintBackground() {
        if (this.Field1136 && BaseCanvas.ticks % 10 > 3) {
            BaseCanvas.g.setColor(15597568);
        } else if (this.scrollType != 1) {
            return;
        } else {
            if (LAF.Field1298 == 0) {
                BaseCanvas.g.setColor(LAF.Field1284);
            } else {
                BaseCanvas.g.setColor(802440);
            }
        }
        BaseCanvas.g.fillRect(0, 0, this.w, this.h);
    }

    public final void startTicker(long delay) {
        int i = 0;
        if (this.text != null) {
            i = this.selectedfont.getWidth(this.text);
        }
        if (this.scrollType == 0) {
            this.shiftTextLimit = i - ((this.w - (this.padding << 1)) - ((this.image == null || this.align == 1) ? 0 : this.gap + this.image.frameWidth));
        } else {
            this.shiftTextLimit = i;
        }
        if (this.shiftTextLimit > 0 && this.w > 0) {
            this.Field1138 = System.currentTimeMillis();
            this.Field1139 = 1000L;
        }
    }

    private void Method79(int i) {
        this.Field1132 = false;
        this.Field1140 = System.currentTimeMillis();
        this.Field1141 = i;
    }

    ///////@Override // defpackage.Class184
    public void update() {
        super.update();
        long currentTimeMillis = System.currentTimeMillis();
        if (this.Field1139 != -1 && currentTimeMillis - this.Field1138 >= this.Field1139) {
            this.Field1139 = -1L;
            this.Field1132 = true;
        }
        if (this.Field1141 != -1 && currentTimeMillis - this.Field1140 >= this.Field1141) {
            this.Field1141 = -1L;
            if (this.scrollType == 1) {
                this.destY = -this.h;
            } else {
                this.shiftText = 0;
            }
        }
        if (this.Field1132 && this.y == this.destY && this.x == this.destX) {
            this.shiftText -= 2;
            if (this.shiftText < (-this.shiftTextLimit)) {
                Method79(1000);
            }
        }
        if (this.y <= (-this.h)) {
            Screen.Field1189 = null;
        }
        if (BaseCanvas.ticks % 3 == 0 && this.image != null && this.isAnimatable) {
            this.frameIndex++;
            if (this.frameIndex >= this.image.nFrame) {
                this.frameIndex = 0;
            }
        }
    }

    ///////@Override // defpackage.Class184
    public void onFocused() {
        super.onFocused();
        if (this.Field1133) {
            startTicker(1000L);
        }
    }

    ///////@Override // defpackage.Class184
    public void onLostFocused() {
        super.onLostFocused();
        if (this.Field1133 && this.Field1132) {
            Method79(0);
        }
    }
}
