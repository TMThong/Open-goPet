package vn.me.ui;

import defpackage.Class108;
import vn.me.core.BaseCanvas;
import javax.microedition.lcdui.Image;

public final class BattleButton extends Class108 {
    private Image Field698;

    public BattleButton(Image image) {
        super(image);
        this.Field698 = image;
    }

    public final void paint() {
        BaseCanvas.g.drawImage(this.Field698, this.x, this.y, 0);
    }
}
