package vn.me.ui;

import defpackage.Command;
import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.common.Effects;


public final class InputDialog extends Dialog {

    private String[] info;
    public EditField txtInput;
    private EditField[] txtArrInput;
    private Label[] lblLabel;
    private Label lblTitle;
    private int nGroup;
    private boolean type;

    public InputDialog(String str, Command Command, Command Command2, int i) {
        super(8, ((BaseCanvas.h - LAF.Field1293) - 69) - LAF.LOT_PADDING, BaseCanvas.w - 16, 69);
        this.nGroup = 0;
        this.type = false;
        this.padding = LAF.LOT_PADDING;
        this.type = true;
        this.txtInput = new EditField();
        this.txtInput.setMetrics(0, (this.h - LAF.Field1293) - (2 * (this.padding + this.border)), this.w - (2 * (this.padding + this.border)), LAF.Field1293);
        addWidget(this.txtInput, false);
        this.cmdCenter = Command;
        this.cmdLeft = Command2;
        this.txtInput.Method79(i);
        this.txtInput.setText("");
        this.info = ResourceManager.boldFont.wrap(str, this.w - (2 * (this.padding + this.border)));
        this.preferredSize.height = (this.info.length * ResourceManager.boldFont.getHeight()) + this.txtInput.h + (2 * (this.padding + this.border));
        if (this.preferredSize.height >= 70) {
            this.h = this.preferredSize.height;
        }
        this.defaultFocusWidget = this.txtInput;
    }

    public final String Method327(int i) {
        return this.txtInput != null ? this.txtInput.getText() : this.txtArrInput[i].getText();
    }

    public InputDialog(String str, String[] strArr, int[] iArr, Command Command, Command Command2) {
        this(str, strArr, iArr, null, Command, Command2);
    }

    private InputDialog(String str, String[] strArr, int[] iArr, int[] iArr2, Command Command, Command Command2) {
        super(BaseCanvas.w / 10, 100, (4 * BaseCanvas.w) / 5, (2 * LAF.Field1293 * strArr.length) + LAF.LOT_PADDING + LAF.Field1292);
        this.nGroup = 0;
        this.type = false;
        this.padding = LAF.LOT_PADDING;
        this.nGroup = iArr.length;
        this.txtArrInput = new EditField[this.nGroup];
        this.lblLabel = new Label[this.nGroup];
        this.type = false;
        this.isLoop = true;
        this.spacing = 2;
        if (str != null) {
            this.lblTitle = new Label(str, ResourceManager.boldFont);
            this.lblTitle.padding = 0;
            this.lblTitle.setMetrics(0, 0, this.w, this.lblTitle.normalfont.getHeight() + 2);
            this.lblTitle.align = 17;
            addWidget(this.lblTitle, false);
        }
        for (int i = 0; i < this.nGroup; i++) {
            this.txtArrInput[i] = new EditField(0, LAF.Field1293 + this.padding, this.w - ((this.padding + this.border) << 1), LAF.Field1293);
            this.txtArrInput[i].Field1076 = iArr[i];
            this.lblLabel[i] = new Label(strArr[i]);
            this.lblLabel[i].padding = 0;
            this.lblLabel[i].setMetrics(0, this.padding << 1, this.w - (this.padding << 1), this.lblLabel[i].normalfont.getHeight() + 2);
            addWidget(this.lblLabel[i], false);
            addWidget(this.txtArrInput[i], false);
        }
        this.cmdCenter = Command;
        this.cmdLeft = Command2;
        setViewMode(1);
        this.defaultFocusWidget = this.txtArrInput[0];
        if (this.preferredSize.height < (BaseCanvas.h - LAF.Field1292) - LAF.LOT_ITEM_HEIGHT) {
            this.h = this.preferredSize.height + ((this.border + this.padding) << 1);
        } else {
            this.lblTitle.isFocusable = true;
            this.h = ((BaseCanvas.h - LAF.Field1292) - LAF.LOT_ITEM_HEIGHT) - 10;
        }
        this.destY = (BaseCanvas.h - this.h) / 2;
        if (this.preferredSize.height > this.h) {
            this.isScrollableY = true;
        }
    }

    public final void paint() {
        super.paint();
        if (!this.type) {
            if (LAF.Field1298 == 0) {
                Effects.show1(BaseCanvas.g, LAF.CLR_MENU_BAR_DARKER, -1, this.padding, ((-this.scrollY) + LAF.Field1293) - 6, (this.w >> 1) - this.padding, 1, true);
                Effects.show1(BaseCanvas.g, -1, LAF.CLR_MENU_BAR_DARKER, (this.w >> 1) - this.padding, ((-this.scrollY) + LAF.Field1293) - 6, (this.w >> 1) - this.padding, 1, true);
                return;
            }
            Effects.show1(BaseCanvas.g, 4945818, -1, this.padding, ((-this.scrollY) + LAF.Field1293) - 6, (this.w >> 1) - this.padding, 1, true);
            Effects.show1(BaseCanvas.g, -1, 4945818, (this.w >> 1) - this.padding, ((-this.scrollY) + LAF.Field1293) - 6, (this.w >> 1) - this.padding, 1, true);
            return;
        }
        int i = 0;
        int length = (this.txtInput.y >> 1) - ((this.info.length * ResourceManager.boldFont.getHeight()) >> 1);
        while (true) {
            int i2 = length;
            if (i >= this.info.length) {
                return;
            }
            ResourceManager.boldFont.drawString(BaseCanvas.g, this.info[i], ((this.w >> 1) - this.padding) - this.border, i2, 17);
            i++;
            length = i2 + ResourceManager.boldFont.getHeight();
        }
    }
}
