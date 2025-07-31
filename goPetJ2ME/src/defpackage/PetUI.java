package defpackage;

import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.Widget;
import javax.microedition.lcdui.Image;

/* renamed from: Class114  reason: default package */
/* loaded from: gopet_repackage.jar:Class114.class */
public final class PetUI extends Widget {
    private Image petImg;
    private PetInfo petInfo;
    private String Field742;
    public boolean Field740 = true;
    private final int Field739 = ResourceManager.boldFont.getHeight();

    public PetUI(PetGameModel class43) {
        this.h = 77;
        int Method330 = ResourceManager.boldFont.getWidth("Level 10000");
        this.w = 43;
        if (Method330 > this.w) {
            this.w = Method330;
        }
        this.isFocusable = false;
    }

    public final void setPetInfo(PetInfo petInfo) {
        this.petInfo = petInfo;
    }

    public final void paintComponent() {
        BaseCanvas.g.translate(this.x, this.y);
        int i = this.Field739;
        if (this.petImg == null) {
            this.petImg = PetGameModel.Field284.requestImg(this.petInfo.petImgPath);
        } else {
            int width = (this.w - (this.petImg.getWidth() / this.petInfo.frameNum)) >> 1;
            PetGameModel.Field283.drawPetz(this.petImg, width, i, 0, this.petInfo.frameNum);
            String str = null;
            switch (this.petInfo.element) {
                case 1:
                    str = "(fire)";
                    break;
                case 2:
                    str = "(tree)";
                    break;
                case 3:
                    str = "(rock)";
                    break;
                case 4:
                    str = "(thunder)";
                    break;
                case 5:
                    str = "(water)";
                    break;
                case 6:
                    str = "(dark)";
                    break;
                case 7:
                    str = "(light)";
                    break;
            }
            if (str != null) {
                ResourceManager.boldFont.drawString(BaseCanvas.g, str, width + 25, i, 0);
            }
        }
        if (this.Field740) {
            int i2 = i + 37;
            ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("Cấp ").append(this.petInfo.petGymLvl).toString(), this.w >> 1, i2, 17);
            int i3 = i2 + this.Field739;
            int i4 = 50;
            if (50 > this.w) {
                i4 = this.w;
            }
            int i5 = (this.w - i4) >> 1;
            BaseCanvas.g.setColor(3872520);
            BaseCanvas.g.fillRect(i5, i3, i4, 1);
            BaseCanvas.g.fillRect(i5, i3 + 4, i4, 1);
            BaseCanvas.g.fillRect(i5 - 1, i3 + 1, 1, 3);
            BaseCanvas.g.fillRect(i5 + i4, i3 + 1, 1, 3);
            BaseCanvas.g.setColor(16039947);
            BaseCanvas.g.fillRect(i5, i3 + 1, i4, 3);
            int i6 = (int) (((this.petInfo.currentExp - this.petInfo.subExp) * i4) / (this.petInfo.maxExp - this.petInfo.subExp));
            BaseCanvas.g.setColor(1740031);
            BaseCanvas.g.fillRect(i5, i3 + 1, i6, 3);
            int i7 = i3 + 5;
            if (this.Field742 == null) {
                this.Field742 = new StringBuffer().append(Ulti.Method381(this.petInfo.currentExp - this.petInfo.subExp)).append("/").append(Ulti.Method381(this.petInfo.maxExp - this.petInfo.subExp)).toString();
            }
            GameResourceManager.Method117().drawString(BaseCanvas.g, this.Field742, i5, i7, 0);
        }
        BaseCanvas.g.translate(-this.x, -this.y);
    }

    public final boolean checkKeys(int i, int i2) {
        switch (i2) {
            case -4:
                return true;
            case -3:
                return true;
            default:
                return super.checkKeys(i, i2);
        }
    }
}
