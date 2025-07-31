package vn.me.ui;

import defpackage.Command;
import vn.me.ui.common.T;
import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.core.BaseCanvas;
import javax.microedition.lcdui.Image;


public final class EmotionDialog extends Dialog implements IActionListener {

    private EditField Field1102;

    public EmotionDialog(EditField class168) {
        this.Field1102 = class168;
        this.cmdRight = new Command(-1, T.gL(0), this);
        this.cmdCenter = new Command(-2, T.gL(7), this);
        for (int i = 0; i < Font.emotions.length; i++) {
            Button btn = new Button(0);
            btn.padding = 1;
            this.border = 1;
            btn.setMetrics(0, 0, Font.emoSize + 4, Font.emoSize + 4);
            btn.setImage(Image.createImage(Font.emotionsImage, i * Font.emoSize, 0, Font.emoSize, Font.emoSize, 0));
            btn.text = Font.emotions[i];
            btn.isRenderImage = true;
            btn.cmdCenter = this.cmdCenter;
            addWidget(btn, false);
        }
        this.isAutoFit = true;
        this.columns = 5;
        setViewMode(2);
        this.x = 1;
        this.destX = 1;
        this.destY = (BaseCanvas.getCurrentScreen().container.h - this.h) - LAF.Field1293;
        this.y = BaseCanvas.getCurrentScreen().container.h - LAF.Field1293;
        this.isScrollableY = true;
        this.isLoop = true;
        BaseCanvas.getCurrentScreen().requestFocus(getWidgetAt(0));
    }

    ///////@Override // defpackage.Dialog, defpackage.Class184
    public final void paintBackground() {
        BaseCanvas.g.setColor(LAF.CLR_MENU_BAR_DARKER);
        BaseCanvas.g.fillRect(2, 2, this.w - 4, this.h - 4);
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
        switch (((Command) ((Object[]) obj)[0]).cmdId) {
            case -2:
                Method274();
                if (this.defaultFocusWidget == null) {
                    return;
                }
                if (this.Field1102 == null) {
                    if (BaseCanvas.getCurrentScreen().chatEditField != null) {
                        BaseCanvas.getCurrentScreen().chatEditField.isVisible = true;
                    }
                    this.Field1102 = BaseCanvas.getCurrentScreen().chatEditField;
                }
                this.Field1102.setText(new StringBuffer().append(this.Field1102.getText()).append(((Button) this.defaultFocusWidget).text).toString());
                BaseCanvas.getCurrentScreen().requestFocus(this.Field1102);
                if (this.Field1102.isVisible) {
                    return;
                }
                this.Field1102.isVisible = true;
                BaseCanvas.getCurrentScreen().addWidget(this.Field1102);
                return;
            case -1:
                Method274();
                return;
            default:
                return;
        }
    }
}
