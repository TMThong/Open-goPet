package vn.me.ui;

import vn.me.ui.common.LAF;
import vn.me.ui.interfaces.IActionListener;
import vn.me.ui.interfaces.IListModel;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.Label;
import vn.me.ui.WidgetGroup;
import vn.me.ui.Font;
import javax.microedition.lcdui.Image;

/* renamed from: Class174  reason: default package */
/* loaded from: gopet_repackage.jar:Class174.class */
public class ListItem extends WidgetGroup implements IActionListener {
    protected int iconSize;
    public IListModel model;
    public Font normalFont;
    public Font focusFont;
    public Font descriptionFont;
    public Font focusDescFont;
    public Font subDescriptionFont;
    protected String[] infos;
    private byte listMode;
    private boolean isSelected;

    public ListItem(IListModel object, int i, int i2, int i3, int i4) {
        super(i, i2, i3, i4);
        this.iconSize = 43;
        this.normalFont = ResourceManager.boldFont;
        this.focusFont = ResourceManager.boldFont;
        this.descriptionFont = ResourceManager.defaultFont;
        this.focusDescFont = ResourceManager.defaultFont;
        this.subDescriptionFont = ResourceManager.boldFont;
        this.listMode = (byte) 0;
        this.isSelected = false;
        this.model = object;
        this.border = 1;
        setMetrics(i, i2, i3, i4);
    }

    ///////@Override // defpackage.Class184
    public final void paintBackground() {
        LAF.paintListItemBG(this);
    }

    ///////@Override // defpackage.Class185, defpackage.Class184
    public void paint() {
        int yInfo;
        IListModel listModel = this.model;
        Image icon = listModel.getIcon();
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

    /* JADX INFO: Access modifiers changed from: protected */
    ///////@Override // defpackage.Class184
    public final void paintBorder() {
        if (this.border > 0) {
            LAF.paintListItemBorder(this);
        }
    }

    ///////@Override // defpackage.Class184
    public final void onFocused() {
        super.onFocused();
        if (this.model.getDescription() == null) {
            return;
        }
        Label class173 = new Label(this.model.getDescription(), this.focusDescFont);
        class173.padding = 0;
        IListModel class201 = this.model;
        int i = 0;
        if (LAF.Field1298 == 1) {
            i = LAF.LOT_PADDING << 1;
        }
        if (this.infos == null) {
            class173.setMetrics(LAF.LOT_PADDING + (this.model.getIcon() != null ? 40 : 0), ((this.h - this.focusDescFont.getHeight()) - this.padding) - 10, (this.w - (LAF.LOT_PADDING << 1)) - 40, this.focusDescFont.getHeight() + i);
        } else {
            class173.setMetrics(LAF.LOT_PADDING + (this.model.getIcon() != null ? 40 : 0), ((this.h - this.focusDescFont.getHeight()) - this.padding) - 10, (this.w - (LAF.LOT_PADDING << 1)) - 40, this.focusDescFont.getHeight() + i);
        }
        class173.isFocusable = false;
        removeAll();
        addWidget(class173, false);
        class173.startTicker(1000L);
    }

    ///////@Override // defpackage.Class184
    public final void onLostFocused() {
        super.onLostFocused();
        removeAll();
    }

    ///////@Override // defpackage.Class200
    public final void actionPerformed(Object obj) {
    }
}
