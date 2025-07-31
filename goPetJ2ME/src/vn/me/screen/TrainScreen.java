package vn.me.screen;

import vn.me.core.BaseCanvas;
import defpackage.Class108;
import vn.me.ui.common.ResourceManager;
import vn.me.ui.common.LAF;
import vn.me.ui.common.T;

public class TrainScreen extends Class108 {

    private final TrScreen magicScreen;

    public TrainScreen(TrScreen class111, int i) {
        super(null);
        this.magicScreen = class111;
        this.destScrollX = (byte) i;
        if (this.magicScreen.pet.skillName == null) {
            this.magicScreen.pet.skillName = new String[0];
        }
    }

    public final void paint() {
        BaseCanvas.g.translate(this.x, this.y);
        BaseCanvas.g.setColor(LAF.CLR_MENU_BAR_LIGHTER);
        BaseCanvas.g.fillRoundRect(0, 0, this.w, this.h, 10, 10);
        switch (this.magicScreen.Field730) {
            case 0:
                switch (this.destScrollX) {
                    case 0:
                        ResourceManager.boldFont.drawString(BaseCanvas.g, "(str) " + T.gL(T.TRAINING_STR), 2, 2, 20);
                        break;
                    case 1:
                        ResourceManager.boldFont.drawString(BaseCanvas.g, "(agi) " + T.gL(T.TRAINING_AGI), 2, 2, 20);
                        break;
                    case 2:
                        ResourceManager.boldFont.drawString(BaseCanvas.g, "(int) " + T.gL(T.TRAINING_INT), 2, 2, 20);
                        break;
                }
            case 1:
                if (this.destScrollX < this.magicScreen.pet.skillName.length) {
                    ResourceManager.boldFont.drawString(BaseCanvas.g, this.magicScreen.pet.skillName[this.destScrollX], 2, 2, 20);
                    break;
                }
                break;
        }
        BaseCanvas.g.translate(-this.x, -this.y);
    }

    public final void onFocused() {
        super.onFocused();
        this.magicScreen.Field724 = this.destScrollX;
        switch (this.magicScreen.Field730) {
            case 0:
                String str = "";
                switch (this.destScrollX) {
                    case 0:
                        str = new StringBuffer().append(str).append("+").append((int) this.magicScreen.Field725[this.destScrollX]).append(" (str) ").toString();
                        break;
                    case 1:
                        str = new StringBuffer().append(str).append("+").append((int) this.magicScreen.Field725[this.destScrollX]).append(" (agi) ").toString();
                        break;
                    case 2:
                        str = new StringBuffer().append(str).append("+").append((int) this.magicScreen.Field725[this.destScrollX]).append(" (int) ").toString();
                        break;
                }
                String stringBuffer = new StringBuffer().append(new StringBuffer().append(str).append("-").append(this.magicScreen.Field727[this.destScrollX]).append(" (ngoc) -").append(this.magicScreen.Field726[this.destScrollX]).append(" (vang)\n").toString()).append(TrScreen.Method141(this.magicScreen)).toString();
                this.magicScreen.Field729 = ResourceManager.boldFont.wrap(stringBuffer, BaseCanvas.w - 25);
                return;
            case 1:
                if (this.destScrollX < this.magicScreen.pet.skillName.length) {
                    this.magicScreen.Field729 = ResourceManager.boldFont.wrap(this.magicScreen.pet.skillDesc[this.destScrollX], BaseCanvas.w - 25);
                    return;
                }
                return;
            default:
                return;
        }
    }
}
