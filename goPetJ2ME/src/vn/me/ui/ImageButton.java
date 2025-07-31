package vn.me.ui;

import defpackage.Command;
import vn.me.ui.Button;
import vn.me.core.BaseCanvas;
import vn.me.screen.GameScene;
import javax.microedition.lcdui.Image;

public final class ImageButton extends Button {

    private boolean Field919 = false;
    private long Field920 = 0;
    private Image Field921;
    private final GameScene Field922;

    public ImageButton(GameScene class134, Image image, Command Command) {
        this.Field922 = class134;
        this.border = 0;
        this.padding = 0;
        setMetrics(0, 0, 28, 27);
        this.Field921 = image;
        this.cmdCenter = Command;
        if (image == null) {
            throw new NullPointerException();
        }
    }

    ///////@Override // defpackage.Class173
    public final void setImage(Image image) {
        this.Field921 = image;
    }

    /* JADX INFO: Access modifiers changed from: protected */
    ///////@Override // defpackage.Class165, defpackage.Class184
    public final void paintBorder() {
    }

    ///////@Override // defpackage.Class165, defpackage.Class173, defpackage.Class184
    public final void paintBackground() {
    }

    ///////@Override // defpackage.Class165, defpackage.Class173, defpackage.Class184
    public final void paint() {
        Image image = this.isFocused ? this.Field922.Field905.Field970[1] : this.Field922.Field905.Field970[0];
        BaseCanvas.g.drawImage(image, (this.w - image.getWidth()) >> 1, (this.h - image.getHeight()) >> 1, 0);
        if (!this.isFocused) {
            this.Field919 = false;
        }
        if (this.Field919) {
            BaseCanvas.g.drawImage(this.Field921, (this.w - this.Field921.getWidth()) >> 1, ((this.h - this.Field921.getHeight()) >> 1) - 2, 0);
        } else {
            BaseCanvas.g.drawImage(this.Field921, (this.w - this.Field921.getWidth()) >> 1, (this.h - this.Field921.getHeight()) >> 1, 0);
        }
        long currentTimeMillis = System.currentTimeMillis();
        if (!this.isFocused || currentTimeMillis - this.Field920 < 300) {
            return;
        }
        this.Field920 = currentTimeMillis;
        this.Field919 = !this.Field919;
    }
}
