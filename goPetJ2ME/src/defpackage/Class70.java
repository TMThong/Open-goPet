package defpackage;

import vn.me.ui.ListItem;
import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IListModel;
import vn.me.ui.common.Resource;
import vn.me.core.BaseCanvas;
import javax.microedition.lcdui.Image;

/* renamed from: Class70  reason: default package */
 /* loaded from: gopet_repackage.jar:Class70.class */
public final class Class70 extends ListItem {

    private static final Image[] Field445;

    public Class70(IListModel class201, int i, int i2, int i3, int i4) {
        super(class201, 0, 0, i3, i4);
    }

    public void paint() {
        MenuItemInfo menuItemInfo;
        Image icon = (menuItemInfo = (MenuItemInfo) this.model).getIcon();
        int num = this.padding;
        if (icon != null) {
            switch (menuItemInfo.saleStatus) {
                case 1:
                    BaseCanvas.g.drawImage(Class70.Field445[0], LAF.LOT_PADDING + this.viewMode, 0, 24);
                    break;
                case 2:
                    BaseCanvas.g.drawImage(Class70.Field445[1], LAF.LOT_PADDING + this.viewMode, 0, 24);
                    break;
                case 3:
                    BaseCanvas.g.drawImage(Class70.Field445[2], 1, 36, 0);
                    break;
            }
        }
        int yInfo;
        IListModel listModel = this.model;
        int yInfo2 = this.padding;
        if (icon != null) {
            BaseCanvas.g.drawImage(icon, LAF.LOT_PADDING + (this.iconSize >> 1), this.h >> 1, 3);
        }
        if (this.isFocused) {
            int cx = BaseCanvas.g.getClipX();
            int cy = BaseCanvas.g.getClipY();
            int cw = BaseCanvas.g.getClipWidth();
            int ch = BaseCanvas.g.getClipHeight();
            BaseCanvas.g.clipRect(LAF.LOT_PADDING + (icon != null ? this.iconSize : 0), 0, this.w - this.iconSize, this.h);
            this.focusFont.drawString(BaseCanvas.g, listModel.getTitle(), LAF.LOT_PADDING + (icon != null ? this.iconSize : 0), yInfo2, 20);
            BaseCanvas.g.setClip(cx, cy, cw, ch);
            yInfo = yInfo2 + this.focusFont.getHeight();
        } else {
            this.normalFont.drawString(BaseCanvas.g, listModel.getTitle(), LAF.LOT_PADDING + (icon != null ? this.iconSize : 0), yInfo2, 20);
            yInfo = yInfo2 + this.normalFont.getHeight();
        }
        if (this.infos != null) {
            if (this.isFocused) {
                for (int i = 0; i < this.infos.length; i++) {
                    this.focusDescFont.drawString(BaseCanvas.g, this.infos[i], LAF.LOT_PADDING + (icon != null ? this.iconSize : 0), yInfo, 20);
                    yInfo += this.focusDescFont.getHeight() + 2;
                }
            } else {
                for (int i2 = 0; i2 < this.infos.length; i2++) {
                    this.descriptionFont.drawString(BaseCanvas.g, this.infos[i2], LAF.LOT_PADDING + (icon != null ? this.iconSize : 0), yInfo, 20);
                    yInfo += this.descriptionFont.getHeight() + 2;
                }
            }
        }
        String desc = listModel.getDescription();
        if (desc != null && !this.isFocused) {
            int cx2 = BaseCanvas.g.getClipX();
            int cy2 = BaseCanvas.g.getClipY();
            int cw2 = BaseCanvas.g.getClipWidth();
            int ch2 = BaseCanvas.g.getClipHeight();
            BaseCanvas.g.clipRect(LAF.LOT_PADDING + (icon != null ? this.iconSize : 0), ((this.h - this.focusDescFont.getHeight()) - this.padding) - 10, ((this.w - 20) - LAF.LOT_PADDING) - this.iconSize, this.h);
            if (this.isFocused) {
                this.focusDescFont.drawString(BaseCanvas.g, desc, LAF.LOT_PADDING + (icon != null ? this.iconSize : 0), ((this.h - this.focusDescFont.getHeight()) - this.padding) - 10, 20);
            } else if (!this.isFocused) {
                this.descriptionFont.drawString(BaseCanvas.g, desc, LAF.LOT_PADDING + (icon != null ? this.iconSize : 0), ((this.h - this.focusDescFont.getHeight()) - this.padding) - 10, 20);
            }
            BaseCanvas.g.setClip(cx2, cy2, cw2, ch2);
        }
        if (this.isFocused || this.isPressed) {
            super.paint();
        }
    }

    static {
        Image[] imageArr = new Image[3];
        Field445 = imageArr;
        imageArr[0] = Resource.createImage(11);
        Field445[1] = Resource.createImage(12);
        Field445[2] = Resource.createImage(13);
    }
}
